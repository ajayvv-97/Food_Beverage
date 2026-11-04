using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FoodandBeverage.Forms.UserControls;

namespace FoodandBeverage.Forms
{
    public partial class FrmInventoryRequestApproval : Form
    {
        private const int ApprovalPending = 0;
        private const int ApprovalApproved = 1;
        private const int ApprovalRejected = 2;
        private const int ApprovalSentBack = 3;

        private readonly BllInventoryRequest _inventoryBll = new BllInventoryRequest();
        private readonly ClsGeneral clsgen = new ClsGeneral();
        private BlurPopupOverlay _blurPopupOverlay;

        private Panel pnl_reqapproval;
        private DataGridView dgv_req_app;
        private TextBox txt_search_reqapp;
        private ComboBox cmb_reqapp_status;
        private DataTable _requestApprovalData;

        private int? _currentRequestId;
        private int _currentApprovalStatus = ApprovalPending;
        private List<DmnInventoryRequestDetail> _loadedRequestDetails =
            new List<DmnInventoryRequestDetail>();

        // Read-only request header fields; txt_reqno and txtremarks are in the Designer.
        private TextBox txt_reqdate;
        private TextBox txt_requireddate;
        private TextBox txt_reqby;
        private TextBox txt_department;
        private TextBox txt_reqtype;
        private Label lbl_requesttype;
        private Label lbl_department;
        private PictureBox pb_status;
        private Label lbl_status_text;
        private ToolTip _approvalToolTip;

        private sealed class InventoryApprovalHeader
        {
            public int RequestId { get; set; }
            public string RequestNo { get; set; }
            public DateTime RequestDate { get; set; }
            public DateTime RequiredDate { get; set; }
            public int? DepartmentId { get; set; }
            public int? EmployeeId { get; set; }
            public string DepartmentName { get; set; }
            public string EmployeeName { get; set; }
            public string RequestRemarks { get; set; }
            public int RequestType { get; set; }
            public int ApprovalStatus { get; set; }
            public int? ApprovedBy { get; set; }
            public DateTime? ApprovedOn { get; set; }
            public string ApprovalRemarks { get; set; }
        }

        public FrmInventoryRequestApproval()
        {
            InitializeComponent();

            ConfigureReadOnlyHeader();
            ConfigureReadOnlyGrids();
            ConfigureApprovalActions();
            BuildRequestApprovalPopup();

            _blurPopupOverlay = new BlurPopupOverlay(this, pnl_reqapproval);

            btn_openrequest.Click += btn_reqpage_Click;
            btn_saverequest.Click += btn_approve_Click;
            btnsave_draft.Click += btn_reject_Click;
            btn_deletesaved_draft.Click += btn_sendback_Click;
            btnclose.Click += btnclose_Click;
            btncolse.Click += btnclose_Click;
            btnmini.Click += btnmini_Click;
            btn_batch_tab.Click += btn_batch_tab_Click;
            btn_raw_tab.Click += btn_raw_tab_Click;
            dgv_batch.CellClick += dgv_batch_CellClick;

            btn_openrequest.Text = "APPROVAL REQUESTS";
            UpdateDecisionButtons(false);
            ShowRequestTab(1);
        }

        private TextBox CreateReadOnlyTextBox(string name)
        {
            return new TextBox
            {
                Name = name,
                ReadOnly = true,
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(206, 209, 234),
                ForeColor = Color.FromArgb(25, 25, 50),
                Font = new Font("Microsoft Sans Serif", 10.5F, FontStyle.Regular),
                Dock = DockStyle.Fill,
                Margin = new Padding(3),
                TabStop = false
            };
        }

        private TextBox ReplaceWithReadOnlyTextBox(Control oldControl, string name)
        {
            TableLayoutPanelCellPosition cell = tlp_req_from.GetCellPosition(oldControl);
            tlp_req_from.Controls.Remove(oldControl);
            oldControl.Visible = false;

            TextBox replacement = CreateReadOnlyTextBox(name);
            tlp_req_from.Controls.Add(replacement, cell.Column, cell.Row);
            return replacement;
        }

        private void ConfigureReadOnlyHeader()
        {
            // Replace the entry controls with display-only controls while preserving
            // the existing request form layout and field positions.
            txt_reqdate = ReplaceWithReadOnlyTextBox(dtp_reqdate, "txt_reqdate");
            txt_requireddate = ReplaceWithReadOnlyTextBox(dtp_required_date, "txt_requireddate");
            txt_reqby = ReplaceWithReadOnlyTextBox(cmb_req_employee, "txt_reqby");
            txt_department = ReplaceWithReadOnlyTextBox(cmb_req_department, "txt_department");

            txt_reqno.ReadOnly = true;
            txt_reqno.TabStop = false;
            txt_reqno.BackColor = Color.FromArgb(206, 209, 234);
            txt_reqno.BorderStyle = BorderStyle.None;
            txt_reqno.Font = new Font("Microsoft Sans Serif", 10.5F, FontStyle.Regular);

            label5.Text = "Approval Remarks";
            label8.Text = "Department";
            lbl_department = label8;

            tlp_req_from.RowStyles[0] = new RowStyle(SizeType.Absolute, 24F);
            tlp_req_from.RowCount = 8;
            tlp_req_from.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));

            lbl_requesttype = new Label
            {
                Name = "lbl_requesttype",
                Text = "Request Type",
                AutoSize = true,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Microsoft Sans Serif", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 65),
                Margin = new Padding(3)
            };
            txt_reqtype = CreateReadOnlyTextBox("txt_reqtype");
            tlp_req_from.Controls.Add(lbl_requesttype, 0, 7);
            tlp_req_from.Controls.Add(txt_reqtype, 1, 7);

            // A compact current-status indicator in the empty top row.
            Panel statusPanel = new Panel
            {
                Name = "pnl_status_indicator",
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            pb_status = new PictureBox
            {
                Name = "pb_status",
                Size = new Size(14, 14),
                Location = new Point(3, 5),
                BackColor = Color.DarkOrange,
                BorderStyle = BorderStyle.FixedSingle
            };
            lbl_status_text = new Label
            {
                Name = "lbl_status_text",
                Text = "Not selected",
                AutoSize = false,
                Location = new Point(23, 0),
                Size = new Size(140, 24),
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 65),
                TextAlign = ContentAlignment.MiddleLeft
            };
            statusPanel.Controls.Add(pb_status);
            statusPanel.Controls.Add(lbl_status_text);
            tlp_req_from.Controls.Add(statusPanel, 0, 0);
            tlp_req_from.SetColumnSpan(statusPanel, 2);

            txtremarks.Multiline = true;
            txtremarks.ScrollBars = ScrollBars.Vertical;
            txtremarks.MaxLength = 2000;
            txtremarks.Text = string.Empty;
            txtremarks.TabIndex = 0;
            lbl_hint_raw.Text =
                "Review the requested quantities, recipe ingredients and stock before submitting your decision.";

            _approvalToolTip = new ToolTip();
            _approvalToolTip.SetToolTip(pb_status, "Current approval status");
        }

        private void ConfigureReadOnlyGrids()
        {
            ConfigureApprovalGrid(dgv_batch);
            ConfigureApprovalGrid(dgv_batch_ingrediant);
            ConfigureApprovalGrid(dgv_raw);

            dgv_batch.Columns["dbaction"].Visible = false;
            dgv_batch.Columns["dbitemid"].Visible = false;

            dgv_batch_ingrediant.Columns["dbiproductid"].Visible = false;
            dgv_batch_ingrediant.Columns["biIng_DetailsId"].Visible = false;

            dgv_raw.Columns["rawaction"].Visible = false;
            dgv_raw.Columns["rwaitemid"].Visible = false;
            dgv_raw.Columns["rawproductype"].Visible = false;

            tlp_batch_serach_nav.Visible = false;
            tableLayoutPanel5.Visible = false;
            dgv_batch_ingrediant.ReadOnly = true;

            lbl_IngrediantHeader.Text = "Ingredients for selected batch product";
        }

        private static void ConfigureApprovalGrid(DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.RowHeadersVisible = false;
            grid.ScrollBars = ScrollBars.Both;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            grid.RowTemplate.Height = 36;
            grid.RowTemplate.DefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Regular);
        }

        private void ConfigureApprovalActions()
        {
            ConfigureActionButton(btn_openrequest, "REQUEST LIST",
                Color.FromArgb(76, 55, 220), Color.White);
            ConfigureActionButton(btn_saverequest, "APPROVE",
                Color.FromArgb(35, 150, 75), Color.White);
            ConfigureActionButton(btnsave_draft, "REJECT",
                Color.FromArgb(210, 55, 55), Color.White);
            ConfigureActionButton(btn_deletesaved_draft, "SEND BACK",
                Color.FromArgb(235, 135, 25), Color.White);

            btn_opendraft.Visible = false;
            btnrefresh.Visible = false;
            btnSearchrequest.Visible = false;
            btnclose.Text = "CLOSE";
            btnclose.BackgroundImage = null;
            btnclose.BackColor = Color.FromArgb(90, 70, 160);
            btnclose.ForeColor = Color.White;
            btnclose.FlatStyle = FlatStyle.Flat;
        }

        private static void ConfigureActionButton(Button button, string text,
            Color backColor, Color foreColor)
        {
            button.Text = text;
            button.BackgroundImage = null;
            button.BackColor = backColor;
            button.ForeColor = foreColor;
            button.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
        }

        private void BuildRequestApprovalPopup()
        {
            pnl_reqapproval = new Panel
            {
                Name = "pnl_reqapproval",
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(1120, 590),
                Anchor = AnchorStyles.None,
                Visible = false,
                Padding = new Padding(10)
            };

            Panel header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 42,
                BackColor = Color.FromArgb(99, 76, 184)
            };
            Label title = new Label
            {
                Text = "Inventory Approval Requests",
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Button close = new Button
            {
                Text = "X",
                Dock = DockStyle.Right,
                Width = 42,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            close.FlatAppearance.BorderSize = 0;
            close.Click += delegate { pnl_reqapproval.Visible = false; };
            header.Controls.Add(title);
            header.Controls.Add(close);

            FlowLayoutPanel filters = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(2, 6, 2, 4),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            Label statusCaption = new Label
            {
                Text = "Status",
                Width = 55,
                Height = 28,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            cmb_reqapp_status = new ComboBox
            {
                Name = "cmb_reqapp_status",
                Width = 150,
                Height = 28,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9F)
            };
            cmb_reqapp_status.Items.AddRange(new object[]
            {
                "All", "Pending", "Sent Back", "Approved", "Rejected"
            });
            cmb_reqapp_status.SelectedIndex = 1;

            txt_search_reqapp = new TextBox
            {
                Name = "txt_search_reqapp",
                Width = 280,
                Height = 28,
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(20, 2, 3, 2)
            };
            filters.Controls.Add(statusCaption);
            filters.Controls.Add(cmb_reqapp_status);
            filters.Controls.Add(txt_search_reqapp);

            dgv_req_app = new DataGridView
            {
                Name = "dgv_req_app",
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.None,
                BackgroundColor = Color.FromArgb(245, 241, 252),
                EnableHeadersVisualStyles = false,
                ColumnHeadersHeight = 42,
                RowTemplate = { Height = 36 },
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ScrollBars = ScrollBars.Both
            };
            dgv_req_app.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 76, 184);
            dgv_req_app.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_req_app.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv_req_app.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            dgv_req_app.DefaultCellStyle.ForeColor = Color.FromArgb(35, 35, 65);
            dgv_req_app.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 216, 250);
            dgv_req_app.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 35, 65);
            dgv_req_app.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RequestId", DataPropertyName = "RequestId", Visible = false
            });
            AddApprovalListColumn("RequestNo", "Request No", 15);
            AddApprovalListColumn("RequestDate", "Request Date", 10);
            AddApprovalListColumn("RequiredDate", "Required Date", 10);
            AddApprovalListColumn("DepartmentName", "Department", 12);
            AddApprovalListColumn("EmployeeName", "Requested By", 12);
            AddApprovalListColumn("RequestTypeName", "Request Type", 14);
            AddApprovalListColumn("StatusText", "Status", 10);
            AddApprovalListColumn("ApprovalRemarks", "Remarks", 17);
            dgv_req_app.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "ViewRequest",
                HeaderText = "",
                Text = "View",
                UseColumnTextForButtonValue = true,
                Width = 70,
                MinimumWidth = 70,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                FlatStyle = FlatStyle.Flat
            });

            pnl_reqapproval.Controls.Add(dgv_req_app);
            pnl_reqapproval.Controls.Add(filters);
            pnl_reqapproval.Controls.Add(header);
            Controls.Add(pnl_reqapproval);
            pnl_reqapproval.BringToFront();

            txt_search_reqapp.TextChanged += delegate { ApplyApprovalRequestFilter(); };
            cmb_reqapp_status.SelectedIndexChanged += delegate { ApplyApprovalRequestFilter(); };
            dgv_req_app.CellClick += dgv_req_app_CellClick;
        }

        private void AddApprovalListColumn(string name, string header, float fillWeight)
        {
            dgv_req_app.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = name,
                FillWeight = fillWeight,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
        }

        private void btn_reqpage_Click(object sender, EventArgs e)
        {
            try
            {
                CenterRequestApprovalPopup();
                LoadApprovalRequests();
                txt_search_reqapp.Clear();
                cmb_reqapp_status.SelectedIndex = 1;
                ApplyApprovalRequestFilter();
                pnl_reqapproval.Visible = true;
                pnl_reqapproval.BringToFront();
            }
            catch (Exception ex)
            {
                LogApprovalException("btn_reqpage_Click", ex);
                ShowApprovalError("Unable to open approval requests. " + ex.Message);
            }
        }

        private void CenterRequestApprovalPopup()
        {
            int width = Math.Min(1160, Math.Max(760, ClientSize.Width - 40));
            int height = Math.Min(620, Math.Max(420, ClientSize.Height - 40));
            pnl_reqapproval.Size = new Size(width, height);
            pnl_reqapproval.Location = new Point(
                Math.Max(0, (ClientSize.Width - width) / 2),
                Math.Max(0, (ClientSize.Height - height) / 2));
        }

        private void LoadApprovalRequests()
        {
            DmnSearch search = new DmnSearch
            {
                Select =
                    "m.InventoryRequestId AS Id, m.RequestNo AS Field1, " +
                    "CONVERT(NVARCHAR(30),m.RequestDate,106) AS Field2, " +
                    "CONVERT(NVARCHAR(30),m.RequiredDate,106) AS Field3, " +
                    "ISNULL(d.DepartmentName,'-') AS Field4, " +
                    "ISNULL(e.EmployeeName,'-') AS Field5, " +
                    "CASE m.RequestType WHEN 1 THEN 'Batch Preparation' " +
                    "WHEN 2 THEN 'Raw Material' ELSE 'Mixed' END AS Field6, " +
                    "CASE ISNULL(m.ApprovalStatus,0) WHEN 0 THEN 'Pending' " +
                    "WHEN 1 THEN 'Approved' WHEN 2 THEN 'Rejected' " +
                    "WHEN 3 THEN 'Sent Back' ELSE 'Unknown' END AS Field7, " +
                    "ISNULL(m.ApprovalRemarks,'-') AS Field8",
                Table = "tblInventoryRequestMaster m " +
                        "LEFT JOIN tblDepartment d ON d.DepartmentId = m.DepartmentId " +
                        "LEFT JOIN tblEmployee e ON e.EmployeeId = m.EmployeeId",
                WhereNdGroupBy = "WHERE ISNULL(m.Status,1)=1 AND m.BranchId=" +
                                 Convert.ToString(FrmDashboard.BranchId),
                OrderBy = "ORDER BY m.InventoryRequestId DESC"
            };

            BllSearch<DmnGridFields> searchBll = new BllSearch<DmnGridFields>();
            List<DmnGridFields> rows = searchBll.GetSearchData("prdSearch", search) ??
                new List<DmnGridFields>();

            _requestApprovalData = new DataTable();
            _requestApprovalData.Columns.Add("RequestId", typeof(long));
            _requestApprovalData.Columns.Add("RequestNo", typeof(string));
            _requestApprovalData.Columns.Add("RequestDate", typeof(string));
            _requestApprovalData.Columns.Add("RequiredDate", typeof(string));
            _requestApprovalData.Columns.Add("DepartmentName", typeof(string));
            _requestApprovalData.Columns.Add("EmployeeName", typeof(string));
            _requestApprovalData.Columns.Add("RequestTypeName", typeof(string));
            _requestApprovalData.Columns.Add("StatusText", typeof(string));
            _requestApprovalData.Columns.Add("ApprovalRemarks", typeof(string));

            foreach (DmnGridFields row in rows)
            {
                _requestApprovalData.Rows.Add(
                    row.Id,
                    row.Field1 ?? string.Empty,
                    row.Field2 ?? string.Empty,
                    row.Field3 ?? string.Empty,
                    row.Field4 ?? "-",
                    row.Field5 ?? "-",
                    row.Field6 ?? "Mixed",
                    row.Field7 ?? "Pending",
                    row.Field8 ?? "-");
            }
        }

        private void ApplyApprovalRequestFilter()
        {
            if (_requestApprovalData == null)
                return;

            string status = Convert.ToString(cmb_reqapp_status.SelectedItem ?? "Pending");
            string search = (txt_search_reqapp.Text ?? string.Empty).Trim();

            DataTable filtered = _requestApprovalData.Clone();
            foreach (DataRow row in _requestApprovalData.Rows)
            {
                string rowStatus = Convert.ToString(row["StatusText"]);
                if (status != "All" &&
                    !string.Equals(rowStatus, status, StringComparison.OrdinalIgnoreCase))
                    continue;

                bool matches = string.IsNullOrWhiteSpace(search) ||
                    Convert.ToString(row["RequestNo"]).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    Convert.ToString(row["DepartmentName"]).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    Convert.ToString(row["EmployeeName"]).IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0;

                if (matches)
                    filtered.ImportRow(row);
            }

            dgv_req_app.DataSource = filtered;
            if (dgv_req_app.Columns.Contains("RequestId"))
                dgv_req_app.Columns["RequestId"].Visible = false;
            if (dgv_req_app.Columns.Contains("ViewRequest"))
                dgv_req_app.Columns["ViewRequest"].DisplayIndex = dgv_req_app.Columns.Count - 1;
        }

        private void dgv_req_app_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dgv_req_app.Columns[e.ColumnIndex].Name != "ViewRequest")
                return;

            object id = dgv_req_app.Rows[e.RowIndex].Cells["RequestId"].Value;
            if (id == null || id == DBNull.Value)
                return;

            try
            {
                LoadApprovalRequest(Convert.ToInt32(id));
                pnl_reqapproval.Visible = false;
            }
            catch (Exception ex)
            {
                LogApprovalException("dgv_req_app_CellClick", ex);
                ShowApprovalError("Unable to load the selected request. " + ex.Message);
            }
        }

        private void LoadApprovalRequest(int requestId)
        {
            DmnSearch masterSearch = new DmnSearch
            {
                Select =
                    "m.InventoryRequestId AS Id, m.RequestNo AS Field1, " +
                    "CONVERT(NVARCHAR(30),m.RequestDate,121) AS Field2, " +
                    "CONVERT(NVARCHAR(30),m.RequiredDate,121) AS Field3, " +
                    "m.RequestType AS Field4, ISNULL(m.ApprovalStatus,0) AS Field5, " +
                    "ISNULL(m.ApprovalRemarks,'') AS Field6, ISNULL(m.Remarks,'') AS Field7, " +
                    "ISNULL(d.DepartmentName,'-') AS Field8, ISNULL(e.EmployeeName,'-') AS Field9",
                Table = "tblInventoryRequestMaster m " +
                        "LEFT JOIN tblDepartment d ON d.DepartmentId=m.DepartmentId " +
                        "LEFT JOIN tblEmployee e ON e.EmployeeId=m.EmployeeId",
                WhereNdGroupBy = "WHERE m.InventoryRequestId=" + requestId +
                    " AND ISNULL(m.Status,1)=1 AND m.BranchId=" +
                    Convert.ToString(FrmDashboard.BranchId),
                OrderBy = ""
            };

            BllSearch<DmnGridFields> masterBll = new BllSearch<DmnGridFields>();
            DmnGridFields master = masterBll.GetSearchData("prdSearch", masterSearch)
                .FirstOrDefault();
            if (master == null)
                throw new InvalidOperationException("The selected request is not available.");

            DmnSearch detailSearch = new DmnSearch
            {
                Select =
                    "d.InventoryRequestDetailId, d.InventoryRequestId, d.RequestType, d.SlNo, " +
                    "d.ProductId, d.PlannedQty, d.RecipeItemId, d.PerPortionQty, " +
                    "d.TotalRequiredQty, d.ItemId, d.ProductType, d.CurrentStock, " +
                    "d.IssuedQty, d.ProjectedBalance, d.Unit, d.PurposeRemarks, " +
                    "i.Itemname AS ItemName, u.UnitName AS UnitName",
                Table = "tblInventoryRequestDetails d " +
                        "LEFT JOIN tblItem i ON i.ItemId=d.ItemId " +
                        "LEFT JOIN tblUnit u ON u.UId=d.Unit",
                WhereNdGroupBy = "WHERE d.InventoryRequestId=" + requestId +
                                 " AND ISNULL(d.Status,1)=1",
                OrderBy = "ORDER BY d.RequestType,d.SlNo,d.InventoryRequestDetailId"
            };

            BllSearch<DmnInventoryRequestDetail> detailsBll =
                new BllSearch<DmnInventoryRequestDetail>();
            _loadedRequestDetails = detailsBll.GetSearchData("prdSearch", detailSearch) ??
                new List<DmnInventoryRequestDetail>();

            LoadBatchProductNames(_loadedRequestDetails);

            _currentRequestId = requestId;
            _currentApprovalStatus = ToInt(master.Field5, ApprovalPending);

            txt_reqno.Text = master.Field1 ?? string.Empty;
            txt_reqdate.Text = FormatRequestDate(master.Field2);
            txt_requireddate.Text = FormatRequestDate(master.Field3);
            txt_department.Text = master.Field8 ?? "-";
            txt_reqby.Text = master.Field9 ?? "-";
            txt_reqtype.Text = GetRequestTypeName(ToInt(master.Field4, 0));
            txt_requ_by.Text = txt_reqby.Text;
            // Show the last manager feedback for completed / sent-back requests.
            // A Pending request gets a clean field for the next decision.
            txtremarks.Text = _currentApprovalStatus == ApprovalPending
                ? string.Empty
                : (master.Field6 ?? string.Empty);

            // Show the request type and department in the summary captions as well.
            lbl_requesttype.Text = "Request Type";
            lbl_department.Text = "Department";

            PopulateApprovalGrids(_loadedRequestDetails);
            UpdateApprovalSummary();
            UpdateStatusIndicator(_currentApprovalStatus);
            UpdateDecisionButtons(_currentApprovalStatus == ApprovalPending);
        }

        private static string FormatRequestDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date)
                ? date.ToString("dd/MM/yyyy")
                : string.Empty;
        }

        private static int ToInt(string value, int fallback)
        {
            int result;
            return int.TryParse(value, out result) ? result : fallback;
        }

        private static string GetRequestTypeName(int requestType)
        {
            switch (requestType)
            {
                case 1: return "Batch Preparation";
                case 2: return "Raw Material";
                case 3: return "Mixed";
                default: return "Unknown";
            }
        }

        private void LoadBatchProductNames(List<DmnInventoryRequestDetail> details)
        {
            foreach (int productId in details
                .Where(x => x.RequestType == 1 && x.ProductId.HasValue)
                .Select(x => x.ProductId.Value)
                .Distinct())
            {
                DmnSearch search = new DmnSearch
                {
                    Select = "Product",
                    Table = "tblProduct",
                    WhereNdGroupBy = "WHERE ProductId=" + productId,
                    OrderBy = ""
                };
                BllSearch<DmnProducts> productSearch = new BllSearch<DmnProducts>();
                DmnProducts product = productSearch.GetSearchData("prdSearch", search)
                    .FirstOrDefault();
                if (product == null)
                    continue;

                foreach (DmnInventoryRequestDetail row in details.Where(
                    x => x.RequestType == 1 && x.ProductId == productId))
                {
                    row.ProductName = product.Product;
                }
            }
        }

        private void PopulateApprovalGrids(List<DmnInventoryRequestDetail> details)
        {
            dgv_batch.Rows.Clear();
            dgv_batch_ingrediant.Rows.Clear();
            dgv_raw.Rows.Clear();

            List<IGrouping<int, DmnInventoryRequestDetail>> products = details
                .Where(x => x.RequestType == 1 && x.ProductId.HasValue)
                .GroupBy(x => x.ProductId.Value)
                .OrderBy(x => x.Min(y => y.SlNo ?? int.MaxValue))
                .ToList();

            int sl = 1;
            foreach (IGrouping<int, DmnInventoryRequestDetail> product in products)
            {
                DmnInventoryRequestDetail first = product.First();
                int index = dgv_batch.Rows.Add();
                DataGridViewRow row = dgv_batch.Rows[index];
                row.Cells["slno"].Value = sl++;
                row.Cells["dbmenuite"].Value = string.IsNullOrWhiteSpace(first.ProductName)
                    ? "Product " + product.Key : first.ProductName;
                row.Cells["dbplannedqty"].Value = first.PlannedQty ?? 0m;
                row.Cells["dbunit"].Value = first.UnitName ?? string.Empty;
                row.Cells["brecipengrediant"].Value = "View ingredients";
                row.Cells["dbitemid"].Value = product.Key;
            }

            sl = 1;
            foreach (DmnInventoryRequestDetail detail in details
                .Where(x => x.RequestType == 2 && x.ItemId.HasValue)
                .OrderBy(x => x.SlNo))
            {
                int index = dgv_raw.Rows.Add();
                DataGridViewRow row = dgv_raw.Rows[index];
                row.Cells[0].Value = sl++;
                row.Cells[1].Value = detail.ItemName ?? ("Raw Material " + detail.ItemId.Value);
                row.Cells[2].Value = detail.CurrentStock ?? 0m;
                row.Cells[3].Value = detail.IssuedQty ?? 0m;
                row.Cells[4].Value = detail.UnitName ?? string.Empty;
                row.Cells[5].Value = detail.ProjectedBalance ?? 0m;
                row.Cells[6].Value = detail.PurposeRemarks ?? string.Empty;
                row.Cells[8].Value = detail.ItemId.Value;
                row.Cells[9].Value = detail.ProductType ?? 2;
            }

            if (products.Count > 0)
            {
                int firstProductId = products[0].Key;
                if (dgv_batch.Rows.Count > 0)
                    dgv_batch.CurrentCell = dgv_batch.Rows[0].Cells["dbmenuite"];
                LoadBatchIngredients(firstProductId);
            }
            else
            {
                lbl_IngrediantHeader.Text = "Ingredients for selected batch product";
            }
        }

        private void dgv_batch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_batch.Rows.Count)
                return;

            object value = dgv_batch.Rows[e.RowIndex].Cells["dbitemid"].Value;
            if (value == null || value == DBNull.Value)
                return;

            LoadBatchIngredients(Convert.ToInt32(value));
        }

        private void LoadBatchIngredients(int productId)
        {
            dgv_batch_ingrediant.Rows.Clear();
            List<DmnInventoryRequestDetail> rows = _loadedRequestDetails
                .Where(x => x.RequestType == 1 && x.ProductId == productId)
                .OrderBy(x => x.SlNo)
                .ToList();

            string productName = rows.FirstOrDefault()?.ProductName ?? ("Product " + productId);
            lbl_IngrediantHeader.Text = "Ingredients for: " + productName;

            int sl = 1;
            foreach (DmnInventoryRequestDetail detail in rows)
            {
                int index = dgv_batch_ingrediant.Rows.Add();
                DataGridViewRow row = dgv_batch_ingrediant.Rows[index];
                row.Cells[0].Value = sl++;
                row.Cells[1].Value = detail.ItemName ?? string.Empty;
                row.Cells[2].Value = detail.PerPortionQty ?? 0m;
                row.Cells[3].Value = detail.TotalRequiredQty ?? 0m;
                row.Cells[4].Value = detail.UnitName ?? string.Empty;
                row.Cells[5].Value = detail.CurrentStock ?? 0m;
                row.Cells[6].Value = detail.ProjectedBalance ?? 0m;
                row.Cells[7].Value = detail.ProductId;
                row.Cells[8].Value = detail.InventoryRequestDetailId;
            }
        }

        private void UpdateApprovalSummary()
        {
            decimal foodQty = dgv_batch.Rows.Cast<DataGridViewRow>()
                .Where(x => !x.IsNewRow)
                .Sum(x => ToDecimal(x.Cells["dbplannedqty"].Value));

            decimal rawQty = dgv_raw.Rows.Cast<DataGridViewRow>()
                .Where(x => !x.IsNewRow)
                .Sum(x => ToDecimal(x.Cells[3].Value));

            txt_batchfoodqty.Text = foodQty.ToString("0.####");
            txt_totalrawmaterialqty.Text = rawQty.ToString("0.####");
            total_food_raw_qty.Text = (foodQty + rawQty).ToString("0.####");
        }

        private void UpdateStatusIndicator(int status)
        {
            string text;
            Color color;
            switch (status)
            {
                case ApprovalApproved:
                    text = "Approved";
                    color = Color.SeaGreen;
                    break;
                case ApprovalRejected:
                    text = "Rejected";
                    color = Color.Firebrick;
                    break;
                case ApprovalSentBack:
                    text = "Sent Back";
                    color = Color.DarkOrange;
                    break;
                default:
                    text = "Pending";
                    color = Color.FromArgb(230, 145, 35);
                    break;
            }

            pb_status.BackColor = color;
            lbl_status_text.Text = text;
            lbl_status_text.ForeColor = color;
            _approvalToolTip.SetToolTip(pb_status, text);
        }

        private void UpdateDecisionButtons(bool enabled)
        {
            btn_saverequest.Enabled = enabled;
            btnsave_draft.Enabled = enabled;
            btn_deletesaved_draft.Enabled = enabled;

            // Keep historical approval remarks visible when the request is no longer
            // Pending; they remain selectable/copyable but cannot be changed.
            txtremarks.Enabled = true;
            txtremarks.ReadOnly = !enabled;
        }

        private void btn_approve_Click(object sender, EventArgs e)
        {
            SaveApprovalDecision(ApprovalApproved);
        }

        private void btn_reject_Click(object sender, EventArgs e)
        {
            SaveApprovalDecision(ApprovalRejected);
        }

        private void btn_sendback_Click(object sender, EventArgs e)
        {
            SaveApprovalDecision(ApprovalSentBack);
        }

        private void SaveApprovalDecision(int decision)
        {
            try
            {
                if (!_currentRequestId.HasValue || _currentApprovalStatus != ApprovalPending)
                {
                    ShowApprovalError("Select a Pending request before submitting an approval decision.");
                    return;
                }

                string remarks = (txtremarks.Text ?? string.Empty).Trim();
                if ((decision == ApprovalRejected || decision == ApprovalSentBack) &&
                    string.IsNullOrWhiteSpace(remarks))
                {
                    ShowApprovalError(decision == ApprovalRejected
                        ? "Enter a reason before rejecting this request."
                        : "Enter the correction required before sending this request back.");
                    txtremarks.Focus();
                    return;
                }

                string action = decision == ApprovalApproved ? "approve" :
                    decision == ApprovalRejected ? "reject" : "send this request back";
                DialogResult confirm = CMessageBox.Show(
                    "Are you sure you want to " + action + "?",
                    "Confirm Approval Decision",
                    MessageBoxButtons.YesNo);
                if (confirm != DialogResult.Yes)
                    return;

                int employeeId = FrmLogin.EmployeeId > 0
                    ? FrmLogin.EmployeeId
                    : FrmLogin.UserId;

                _inventoryBll.UpdateInventoryRequestApproval(
                    "dbo.prdUpdate_InventoryRequestApproval",
                    _currentRequestId.Value,
                    decision,
                    employeeId,
                    remarks,
                    FrmDashboard.BranchId);

                CMessageBox.Show(
                    decision == ApprovalApproved ? "Inventory request approved successfully." :
                    decision == ApprovalRejected ? "Inventory request rejected." :
                    "Inventory request sent back to the requester.");

                LoadApprovalRequest(_currentRequestId.Value);
                LoadApprovalRequests();
                ApplyApprovalRequestFilter();
            }
            catch (Exception ex)
            {
                LogApprovalException("SaveApprovalDecision", ex);
                ShowApprovalError("Unable to save the approval decision. " + ex.Message);
            }
        }

        private void btn_batch_tab_Click(object sender, EventArgs e)
        {
            ShowRequestTab(1);
        }

        private void btn_raw_tab_Click(object sender, EventArgs e)
        {
            ShowRequestTab(2);
        }

        private void ShowRequestTab(int requestType)
        {
            bool showBatch = requestType == 1;
            tlp_Batchpreparation.Visible = showBatch;
            tlp_raw.Visible = !showBatch;
            btn_batch_tab.BackColor = showBatch
                ? Color.FromArgb(76, 55, 220) : Color.FromArgb(238, 233, 255);
            btn_batch_tab.ForeColor = showBatch ? Color.White : Color.FromArgb(35, 35, 65);
            btn_raw_tab.BackColor = !showBatch
                ? Color.FromArgb(76, 55, 220) : Color.FromArgb(238, 233, 255);
            btn_raw_tab.ForeColor = !showBatch ? Color.White : Color.FromArgb(35, 35, 65);
        }

        private static decimal ToDecimal(object value)
        {
            decimal result;
            return decimal.TryParse(Convert.ToString(value), out result) ? result : 0m;
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowApprovalError(string message)
        {
            CMessageBox.Show(message, "Inventory Approval",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LogApprovalException(string method, Exception ex)
        {
            try
            {
                clsgen.LogException(
                    "FrmInventoryRequestApproval " + method,
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
            catch
            {
                // Keep logging failures from interrupting the approval workflow.
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_blurPopupOverlay != null)
            {
                _blurPopupOverlay.Dispose();
                _blurPopupOverlay = null;
            }
            base.OnFormClosed(e);
        }
    }
}

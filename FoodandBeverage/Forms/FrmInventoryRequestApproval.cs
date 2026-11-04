using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmInventoryRequestApproval : Form
    {
        private const int ApprovalPending = 0;
        private const int ApprovalApproved = 1;
        private const int ApprovalRejected = 2;
        private const int ApprovalSentBack = 3;
        private const int ApprovalPageSize = 8;
        private const string ApprovalRemarksPlaceholder = "Enter remarks";

        private bool _remarksPlaceholderVisible;

        private readonly BllInventoryRequest _inventoryBll = new BllInventoryRequest();
        private readonly ClsGeneral clsgen = new ClsGeneral();
        private BlurPopupOverlay _blurPopupOverlay;

        private DataTable _requestApprovalData;
        private DataTable _requestApprovalView;
        private int _requestApprovalPage = 1;
        private string _requestApprovalStatus = "Pending";
        private bool _requestApprovalEventsWired;

        private int? _currentRequestId;
        private int _currentApprovalStatus = ApprovalPending;
        private List<DmnInventoryRequestDetail> _loadedRequestDetails =
            new List<DmnInventoryRequestDetail>();

        public FrmInventoryRequestApproval()
        {
            InitializeComponent();

            ConfigureReadOnlyHeader();
            ConfigureApprovalGrids();
            InitializeRequestApprovalPopup();

            _blurPopupOverlay = new BlurPopupOverlay(this, pnl_reqapproval);

            UpdateDecisionButtons(false);
            ClearSelectedRequest();
        }
       
        private void ConfigureReadOnlyHeader()
        {
            Text = "Inventory Request Approval";
            label2.Text = "Inventory Request Approval";
            label20.Text = "Approval Remarks";

            ConfigureHeaderTextBox(txt_reqno);
            ConfigureHeaderTextBox(txt_reqdate);
            ConfigureHeaderTextBox(txt_requireddate);
            ConfigureHeaderTextBox(txt_department);
            ConfigureHeaderTextBox(txt_reqby);
            ConfigureHeaderTextBox(txt_reqtype);

            txtremarks.Multiline = true;
            txtremarks.ScrollBars = ScrollBars.Vertical;
            txtremarks.MaxLength = 2000;
            txtremarks.ReadOnly = true;
            txtremarks.Enter += txtremarks_Enter;
            txtremarks.Leave += txtremarks_Leave;
            ShowRemarksPlaceholder();

            lbl_requesttype.Text = "-";
            lbl_deoartment.Text = "-";
            txt_requ_by.Text = "-";
            txt_batchfoodqty.Text = "0";
            txt_totalrawmaterialqty.Text = "0";
            lbl_information.Text = "Select an inventory request";
            pb_status.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private static void ConfigureHeaderTextBox(TextBox textBox)
        {
            textBox.ReadOnly = true;
            textBox.TabStop = false;
            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = Color.FromArgb(206, 209, 234);
            textBox.ForeColor = Color.FromArgb(25, 25, 50);
            textBox.Font = new Font("Microsoft Sans Serif", 10.5F, FontStyle.Regular);
        }

        private void ShowRemarksPlaceholder()
        {
            _remarksPlaceholderVisible = true;
            txtremarks.Text = ApprovalRemarksPlaceholder;
            txtremarks.ForeColor = Color.Gray;
        }

        private void SetRemarksText(string remarks)
        {
            if (string.IsNullOrWhiteSpace(remarks))
            {
                ShowRemarksPlaceholder();
                return;
            }

            _remarksPlaceholderVisible = false;
            txtremarks.ForeColor = Color.FromArgb(45, 42, 60);
            txtremarks.Text = remarks;
        }

        private void txtremarks_Enter(object sender, EventArgs e)
        {
            if (!_remarksPlaceholderVisible)
                return;

            txtremarks.Clear();
            _remarksPlaceholderVisible = false;
            txtremarks.ForeColor = Color.FromArgb(45, 42, 60);
        }

        private void txtremarks_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtremarks.Text))
                ShowRemarksPlaceholder();
        }

        private string GetApprovalRemarksValue()
        {
            if (_remarksPlaceholderVisible)
                return string.Empty;

            return (txtremarks.Text ?? string.Empty).Trim();
        }

        private void ConfigureApprovalGrids()
        {
            ConfigureReadOnlyGrid(dgv_batch);
            ConfigureReadOnlyGrid(dgv_raw);

            if (dgv_batch.Columns.Contains("dbaction"))
                dgv_batch.Columns["dbaction"].Visible = false;
            if (dgv_batch.Columns.Contains("dbitemid"))
                dgv_batch.Columns["dbitemid"].Visible = false;

            if (dgv_raw.Columns.Contains("rawaction"))
                dgv_raw.Columns["rawaction"].Visible = false;
            if (dgv_raw.Columns.Contains("rwaitemid"))
                dgv_raw.Columns["rwaitemid"].Visible = false;
            if (dgv_raw.Columns.Contains("rawproductype"))
                dgv_raw.Columns["rawproductype"].Visible = false;

            // Match FrmInventoryrequest: visible columns fill the complete grid width,
            // with the product/material name receiving the largest share.
            ApplyApprovalGridFillLayout(dgv_batch);
            ApplyApprovalGridFillLayout(dgv_raw);

            SetApprovalGridFillWeight(dgv_batch, "slno", 8F);
            SetApprovalGridFillWeight(dgv_batch, "dbmenuite", 42F);
            SetApprovalGridFillWeight(dgv_batch, "dbplannedqty", 13F);
            SetApprovalGridFillWeight(dgv_batch, "dbunit", 9F);
            SetApprovalGridFillWeight(dgv_batch, "brecipengrediant", 21F);

            SetApprovalGridFillWeight(dgv_raw, "dataGridViewTextBoxColumn10", 7F);
            SetApprovalGridFillWeight(dgv_raw, "dataGridViewTextBoxColumn11", 34F);
            SetApprovalGridFillWeight(dgv_raw, "dataGridViewTextBoxColumn12", 11F);
            SetApprovalGridFillWeight(dgv_raw, "rawissuedqty", 11F);
            SetApprovalGridFillWeight(dgv_raw, "dataGridViewTextBoxColumn13", 9F);
            SetApprovalGridFillWeight(dgv_raw, "rawprojectedbalance", 12F);
            SetApprovalGridFillWeight(dgv_raw, "rawpurpoe", 15F);

            dgv_batch.Resize += ApprovalGrid_Resize;
            dgv_raw.Resize += ApprovalGrid_Resize;
        }

        private static void ApplyApprovalGridFillLayout(DataGridView grid)
        {
            if (grid == null)
                return;

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (column.Visible)
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private static void SetApprovalGridFillWeight(
            DataGridView grid, string columnName, float fillWeight)
        {
            if (grid == null || !grid.Columns.Contains(columnName))
                return;

            DataGridViewColumn column = grid.Columns[columnName];
            if (column.Visible)
                column.FillWeight = fillWeight;
        }

        private void ApprovalGrid_Resize(object sender, EventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid == null)
                return;

            ApplyApprovalGridFillLayout(grid);
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtremarks, txt_reqtype, txt_department, txt_reqby, txt_requireddate, txt_reqdate, txt_reqno })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    //textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }

            catch (Exception ex)
            {
                LogInventoryException("textb_shape", ex);
            }
        }
        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            try
            {
                // Create a new GraphicsPath object
                GraphicsPath path = new GraphicsPath();
                // Define the shape of the path using a rounded rectangle
                Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();
                // Set the Region property of the textbox to the new path
                textBox.Region = new Region(path);
            }
            catch (Exception ex)
            {
                LogInventoryException("ApplyRoundedTextBox", ex);
            }
        }

        #endregion
        private static void ConfigureReadOnlyGrid(DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.RowHeadersVisible = false;
            grid.ScrollBars = ScrollBars.Both;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            grid.RowTemplate.Height = 36;
        }

        private void InitializeRequestApprovalPopup()
        {
            pnl_reqapproval.Visible = false;
            pnl_reqapproval.BackColor = Color.White;
            pnl_reqapproval.BorderStyle = BorderStyle.FixedSingle;
            pnl_reqapproval.Anchor = AnchorStyles.None;

            dgv_req_app.AutoGenerateColumns = false;
            dgv_req_app.Columns.Clear();
            dgv_req_app.AllowUserToAddRows = false;
            dgv_req_app.AllowUserToDeleteRows = false;
            dgv_req_app.AllowUserToResizeRows = false;
            dgv_req_app.ReadOnly = true;
            dgv_req_app.MultiSelect = false;
            dgv_req_app.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_req_app.RowHeadersVisible = false;
            dgv_req_app.BorderStyle = BorderStyle.None;
            dgv_req_app.BackgroundColor = Color.FromArgb(245, 241, 252);
            dgv_req_app.EnableHeadersVisualStyles = false;
            dgv_req_app.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(99, 76, 184);
            dgv_req_app.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv_req_app.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);
            dgv_req_app.ColumnHeadersHeight = 40;
            dgv_req_app.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv_req_app.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            dgv_req_app.DefaultCellStyle.ForeColor = Color.FromArgb(35, 35, 65);
            dgv_req_app.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 216, 250);
            dgv_req_app.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 35, 65);
            dgv_req_app.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgv_req_app.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(239, 235, 250);
            dgv_req_app.RowTemplate.DefaultCellStyle.Font =
                new Font("Segoe UI", 9F, FontStyle.Regular);
            dgv_req_app.RowTemplate.Height = 36;
            dgv_req_app.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv_req_app.ScrollBars = ScrollBars.Both;

            dgv_req_app.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "RequestId",
                DataPropertyName = "RequestId",
                Visible = false
            });

            AddApprovalListColumn("SlNo", "Sl No", 55);
            AddApprovalListColumn("RequestNo", "Request No", 145);
            AddApprovalListColumn("RequestDate", "Request Date", 105);
            AddApprovalListColumn("RequiredDate", "Required Date", 105);
            AddApprovalListColumn("DepartmentName", "Department", 125);
            AddApprovalListColumn("EmployeeName", "Requested By", 125);
            AddApprovalListColumn("RequestTypeName", "Request Type", 145);
            AddApprovalListColumn("StatusText", "Status", 90);
            AddApprovalListColumn("ApprovalRemarks", "Remarks", 210);

            dgv_req_app.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "ViewRequest",
                HeaderText = "",
                Text = "Open",
                UseColumnTextForButtonValue = true,
                Width = 65,
                MinimumWidth = 65,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                FlatStyle = FlatStyle.Flat
            });

            if (!_requestApprovalEventsWired)
            {
                txt_search_reqapp.TextChanged += txt_search_reqapp_TextChanged;

                WireApprovalTab(tlp_all, lbl_all, pb_all, "All");
                WireApprovalTab(tlp_pending, lbl_pending, pb_pending, "Pending");
                WireApprovalTab(tlp_approved, lbl_approved, pb_approved, "Approved");
                WireApprovalTab(tlp_rejected, lbl_rejected, pb_rejected, "Rejected");

                _requestApprovalEventsWired = true;
            }

            UpdateApprovalTabAppearance();
        }

        private void AddApprovalListColumn(string name, string header, int width)
        {
            dgv_req_app.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                DataPropertyName = name,
                Width = width,
                MinimumWidth = width,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                    WrapMode = DataGridViewTriState.False
                }
            });
        }

        private void WireApprovalTab(TableLayoutPanel tab, Label caption,
            PictureBox icon, string status)
        {
            tab.Tag = status;
            caption.Tag = status;
            icon.Tag = status;

            tab.Cursor = Cursors.Hand;
            caption.Cursor = Cursors.Hand;
            icon.Cursor = Cursors.Hand;

            tab.Click += ApprovalTab_Click;
            caption.Click += ApprovalTab_Click;
            icon.Click += ApprovalTab_Click;

            tab.MouseEnter += ApprovalTab_MouseEnter;
            caption.MouseEnter += ApprovalTab_MouseEnter;
            icon.MouseEnter += ApprovalTab_MouseEnter;

            tab.MouseLeave += ApprovalTab_MouseLeave;
            caption.MouseLeave += ApprovalTab_MouseLeave;
            icon.MouseLeave += ApprovalTab_MouseLeave;
        }

        private void ApprovalTab_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null)
                return;

            SetApprovalStatusFilter(Convert.ToString(control.Tag));
        }

        private void ApprovalTab_MouseEnter(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null)
                return;

            string status = Convert.ToString(control.Tag);
            TableLayoutPanel tab = GetApprovalTab(status);
            if (tab != null && status != _requestApprovalStatus)
                tab.BackColor = GetApprovalTabHoverColor(status);
        }

        private void ApprovalTab_MouseLeave(object sender, EventArgs e)
        {
            UpdateApprovalTabAppearance();
        }

        private TableLayoutPanel GetApprovalTab(string status)
        {
            switch (status)
            {
                case "All": return tlp_all;
                case "Pending": return tlp_pending;
                case "Approved": return tlp_approved;
                case "Rejected": return tlp_rejected;
                default: return null;
            }
        }

        private Color GetApprovalTabBaseColor(string status)
        {
            switch (status)
            {
                case "All": return Color.FromArgb(238, 233, 255);
                case "Pending": return Color.FromArgb(255, 242, 218);
                case "Approved": return Color.FromArgb(226, 247, 226);
                case "Rejected": return Color.FromArgb(255, 230, 230);
                default: return Color.White;
            }
        }

        private Color GetApprovalTabSelectedColor(string status)
        {
            switch (status)
            {
                case "All": return Color.FromArgb(76, 55, 220);
                case "Pending": return Color.FromArgb(239, 145, 28);
                case "Approved": return Color.FromArgb(35, 166, 75);
                case "Rejected": return Color.FromArgb(218, 63, 63);
                default: return Color.FromArgb(99, 76, 184);
            }
        }

        private Color GetApprovalTabHoverColor(string status)
        {
            switch (status)
            {
                case "All": return Color.FromArgb(220, 211, 255);
                case "Pending": return Color.FromArgb(255, 228, 185);
                case "Approved": return Color.FromArgb(204, 239, 207);
                case "Rejected": return Color.FromArgb(255, 207, 207);
                default: return Color.LightGray;
            }
        }

        private void UpdateApprovalTabAppearance()
        {
            int allCount = _requestApprovalData == null ? 0 : _requestApprovalData.Rows.Count;
            SetApprovalTabAppearance(tlp_all, lbl_all, pb_all, "All", allCount);
            SetApprovalTabAppearance(tlp_pending, lbl_pending, pb_pending, "Pending",
                CountApprovalStatus("Pending"));
            SetApprovalTabAppearance(tlp_approved, lbl_approved, pb_approved, "Approved",
                CountApprovalStatus("Approved"));
            SetApprovalTabAppearance(tlp_rejected, lbl_rejected, pb_rejected, "Rejected",
                CountApprovalStatus("Rejected"));
        }

        private void SetApprovalTabAppearance(TableLayoutPanel tab, Label caption,
            PictureBox icon, string status, int count)
        {
            bool selected = status == _requestApprovalStatus;
            tab.BackColor = selected
                ? GetApprovalTabSelectedColor(status)
                : GetApprovalTabBaseColor(status);
            tab.BorderStyle = BorderStyle.FixedSingle;

            caption.Text = (status == "All" ? "ALL" : status) + " (" + count + ")";
            caption.ForeColor = selected ? Color.White : Color.FromArgb(35, 35, 65);
            caption.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            caption.BackColor = Color.Transparent;
            caption.Dock = DockStyle.Fill;
            caption.TextAlign = ContentAlignment.MiddleCenter;

            icon.BackColor = Color.Transparent;
            icon.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private int CountApprovalStatus(string status)
        {
            if (_requestApprovalData == null ||
                !_requestApprovalData.Columns.Contains("StatusText"))
                return 0;

            return _requestApprovalData.Rows.Cast<DataRow>().Count(row =>
                string.Equals(Convert.ToString(row["StatusText"]), status,
                    StringComparison.OrdinalIgnoreCase));
        }

        private void CenterRequestApprovalPopup()
        {
            // Keep the approval popup at 80% of the form's available screen area.
            // ClientSize is used so the panel remains inside the form's client area.
            int width = Math.Max(1, (int)Math.Round(ClientSize.Width * 0.80));
            int height = Math.Max(1, (int)Math.Round(ClientSize.Height * 0.80));

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
                    "ISNULL(NULLIF(m.ApprovalRemarks,''),'-') AS Field8",
                Table = "tblInventoryRequestMaster m " +
                        "LEFT JOIN tblDepartment d ON d.DepartmentId = m.DepartmentId " +
                        "LEFT JOIN tblEmployee e ON e.EmployeeId = m.EmployeeId",
                WhereNdGroupBy = "WHERE ISNULL(m.Status,1)=1 AND m.BranchId=" +
                                 Convert.ToString(FrmDashboard.BranchId),
                OrderBy = "ORDER BY m.InventoryRequestId DESC"
            };

            BllSearch<DmnGridFields> searchBll = new BllSearch<DmnGridFields>();
            List<DmnGridFields> rows =
                searchBll.GetSearchData("prdSearch", search) ?? new List<DmnGridFields>();

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
                    Convert.ToInt64(row.Id),
                    row.Field1 ?? string.Empty,
                    row.Field2 ?? string.Empty,
                    row.Field3 ?? string.Empty,
                    row.Field4 ?? "-",
                    row.Field5 ?? "-",
                    row.Field6 ?? "Mixed",
                    row.Field7 ?? "Pending",
                    row.Field8 ?? "-");
            }

            _requestApprovalPage = 1;
            ApplyApprovalRequestFilterAndPaging();
        }

        private void ApplyApprovalRequestFilterAndPaging()
        {
            DataTable filtered = _requestApprovalData == null
                ? new DataTable()
                : _requestApprovalData.Clone();

            string searchText = (txt_search_reqapp.Text ?? string.Empty).Trim();

            if (_requestApprovalData != null)
            {
                IEnumerable<DataRow> rows = _requestApprovalData.Rows.Cast<DataRow>();

                if (_requestApprovalStatus != "All")
                {
                    rows = rows.Where(row =>
                        string.Equals(Convert.ToString(row["StatusText"]),
                            _requestApprovalStatus, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    rows = rows.Where(row =>
                        Convert.ToString(row["RequestNo"]).IndexOf(searchText,
                            StringComparison.OrdinalIgnoreCase) >= 0 ||
                        Convert.ToString(row["DepartmentName"]).IndexOf(searchText,
                            StringComparison.OrdinalIgnoreCase) >= 0 ||
                        Convert.ToString(row["EmployeeName"]).IndexOf(searchText,
                            StringComparison.OrdinalIgnoreCase) >= 0 ||
                        Convert.ToString(row["RequestTypeName"]).IndexOf(searchText,
                            StringComparison.OrdinalIgnoreCase) >= 0 ||
                        Convert.ToString(row["ApprovalRemarks"]).IndexOf(searchText,
                            StringComparison.OrdinalIgnoreCase) >= 0);
                }

                foreach (DataRow row in rows.OrderByDescending(row =>
                    Convert.ToInt64(row["RequestId"])))
                    filtered.ImportRow(row);
            }

            _requestApprovalView = filtered;
            int totalPages = TotalApprovalPages;
            _requestApprovalPage = Math.Max(1, Math.Min(_requestApprovalPage, totalPages));

            BindApprovalPage();
            UpdateApprovalTabAppearance();
        }

        private int TotalApprovalPages
        {
            get
            {
                int count = _requestApprovalView == null
                    ? 0
                    : _requestApprovalView.Rows.Count;

                return Math.Max(1, (int)Math.Ceiling(count / (double)ApprovalPageSize));
            }
        }

        private void BindApprovalPage()
        {
            DataTable page = _requestApprovalView == null
                ? new DataTable()
                : _requestApprovalView.Clone();

            int total = _requestApprovalView == null
                ? 0
                : _requestApprovalView.Rows.Count;

            int start = (_requestApprovalPage - 1) * ApprovalPageSize;

            if (_requestApprovalView != null)
            {
                for (int i = start; i < Math.Min(start + ApprovalPageSize, total); i++)
                    page.ImportRow(_requestApprovalView.Rows[i]);
            }

            if (!page.Columns.Contains("SlNo"))
                page.Columns.Add("SlNo", typeof(int));

            for (int i = 0; i < page.Rows.Count; i++)
                page.Rows[i]["SlNo"] = start + i + 1;

            dgv_req_app.DataSource = page;

            if (dgv_req_app.Columns.Contains("RequestId"))
                dgv_req_app.Columns["RequestId"].Visible = false;

            if (dgv_req_app.Columns.Contains("ViewRequest"))
                dgv_req_app.Columns["ViewRequest"].DisplayIndex =
                    dgv_req_app.Columns.Count - 1;

            int first = total == 0 ? 0 : start + 1;
            int last = Math.Min(start + ApprovalPageSize, total);
            lblpagenodescrption.Text = "Showing " + first + " to " + last +
                                       " of " + total + " requests";

            Button[] pageButtons =
            {
                btn_page1, btn_page2, btn_page3, btn_page4, btn_page5
            };

            int firstPage = Math.Max(1,
                Math.Min(_requestApprovalPage - 2, Math.Max(1, TotalApprovalPages - 4)));

            for (int i = 0; i < pageButtons.Length; i++)
            {
                int pageNumber = firstPage + i;
                pageButtons[i].Text = pageNumber.ToString();
                pageButtons[i].Tag = pageNumber;
                pageButtons[i].Visible = pageNumber <= TotalApprovalPages;
                pageButtons[i].BackColor = pageNumber == _requestApprovalPage
                    ? Color.FromArgb(99, 76, 184)
                    : Color.FromArgb(239, 235, 250);
                pageButtons[i].ForeColor = pageNumber == _requestApprovalPage
                    ? Color.White
                    : Color.FromArgb(35, 35, 65);
            }

            btn_movefrist.Enabled = btn_previous.Enabled = _requestApprovalPage > 1;
            btn_next.Enabled = btn_movelast.Enabled =
                _requestApprovalPage < TotalApprovalPages;
        }

        private void SetApprovalStatusFilter(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                status = "Pending";

            _requestApprovalStatus = status;
            _requestApprovalPage = 1;
            ApplyApprovalRequestFilterAndPaging();
        }

        private void txt_search_reqapp_TextChanged(object sender, EventArgs e)
        {
            _requestApprovalPage = 1;
            ApplyApprovalRequestFilterAndPaging();
        }

        private void btn_reqpage_Click(object sender, EventArgs e)
        {
            try
            {
                CenterRequestApprovalPopup();
                txt_search_reqapp.Clear();
                LoadApprovalRequests();
                SetApprovalStatusFilter("Pending");

                pnl_reqapproval.Visible = true;
                pnl_reqapproval.BringToFront();
            }
            catch (Exception ex)
            {
                LogInventoryException("btn_reqpage_Click", ex);
                ShowApprovalError("Unable to open approval requests. " + ex.Message);
            }
        }

        private void dgv_req_app_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgv_req_app.Rows.Count)
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
                LogInventoryException("dgv_req_app_CellClick", ex);
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
                    "ISNULL(d.DepartmentName,'-') AS Field8, " +
                    "ISNULL(e.EmployeeName,'-') AS Field9",
                Table = "tblInventoryRequestMaster m " +
                        "LEFT JOIN tblDepartment d ON d.DepartmentId=m.DepartmentId " +
                        "LEFT JOIN tblEmployee e ON e.EmployeeId=m.EmployeeId",
                WhereNdGroupBy = "WHERE m.InventoryRequestId=" + requestId +
                    " AND ISNULL(m.Status,1)=1 AND m.BranchId=" +
                    Convert.ToString(FrmDashboard.BranchId),
                OrderBy = ""
            };

            BllSearch<DmnGridFields> masterBll = new BllSearch<DmnGridFields>();
            List<DmnGridFields> masterRows =
                masterBll.GetSearchData("prdSearch", masterSearch);
            DmnGridFields master = masterRows == null
                ? null
                : masterRows.FirstOrDefault();

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
            _loadedRequestDetails =
                detailsBll.GetSearchData("prdSearch", detailSearch) ??
                new List<DmnInventoryRequestDetail>();

            LoadBatchProductNames(_loadedRequestDetails);

            _currentRequestId = requestId;
            _currentApprovalStatus = ToInt(master.Field5, ApprovalPending);

            txt_reqno.Text = master.Field1 ?? string.Empty;
            txt_reqdate.Text = FormatRequestDate(master.Field2);
            txt_requireddate.Text = FormatRequestDate(master.Field3);
            txt_reqtype.Text = GetRequestTypeName(ToInt(master.Field4, 0));
            txt_department.Text = master.Field8 ?? "-";
            txt_reqby.Text = master.Field9 ?? "-";

            // A Pending request starts with a clean decision field. Completed decisions
            // retain the approval/rejection/send-back feedback for read-only viewing.
            SetRemarksText(_currentApprovalStatus == ApprovalPending
                ? string.Empty
                : (master.Field6 ?? string.Empty));

            lbl_requesttype.Text = txt_reqtype.Text;
            lbl_deoartment.Text = txt_department.Text;
            txt_requ_by.Text = txt_reqby.Text;

            PopulateApprovalGrids(_loadedRequestDetails);
            UpdateApprovalSummary();
            UpdateStatusIndicator(_currentApprovalStatus);
            UpdateDecisionButtons(_currentApprovalStatus == ApprovalPending);
        }

        private void LoadBatchProductNames(List<DmnInventoryRequestDetail> details)
        {
            if (details == null || details.Count == 0)
                return;

            List<int> productIds = details
                .Where(item => item.RequestType == 1 && item.ProductId.HasValue)
                .Select(item => item.ProductId.Value)
                .Distinct()
                .ToList();

            BllSearch<DmnProducts> productSearch = new BllSearch<DmnProducts>();

            foreach (int productId in productIds)
            {
                DmnSearch search = new DmnSearch
                {
                    Select = "Product",
                    Table = "tblProduct",
                    WhereNdGroupBy = "WHERE ProductId=" + productId,
                    OrderBy = ""
                };

                List<DmnProducts> productRows =
                    productSearch.GetSearchData("prdSearch", search);
                DmnProducts product = productRows == null
                    ? null
                    : productRows.FirstOrDefault();

                string productName = product == null
                    ? "Product " + productId
                    : product.Product;

                foreach (DmnInventoryRequestDetail detail in details.Where(
                    item => item.RequestType == 1 && item.ProductId == productId))
                    detail.ProductName = productName;
            }
        }

        private void PopulateApprovalGrids(List<DmnInventoryRequestDetail> details)
        {
            dgv_batch.Rows.Clear();
            dgv_raw.Rows.Clear();

            List<IGrouping<int, DmnInventoryRequestDetail>> products = details
                .Where(item => item.RequestType == 1 && item.ProductId.HasValue)
                .GroupBy(item => item.ProductId.Value)
                .OrderBy(group => group.Min(item => item.SlNo ?? int.MaxValue))
                .ToList();

            int slNo = 1;
            foreach (IGrouping<int, DmnInventoryRequestDetail> product in products)
            {
                DmnInventoryRequestDetail first = product.First();
                int rowIndex = dgv_batch.Rows.Add();
                DataGridViewRow row = dgv_batch.Rows[rowIndex];

                row.Cells["slno"].Value = slNo++;
                row.Cells["dbmenuite"].Value = string.IsNullOrWhiteSpace(first.ProductName)
                    ? "Product " + product.Key
                    : first.ProductName;
                row.Cells["dbplannedqty"].Value = first.PlannedQty ?? 0m;
                row.Cells["dbunit"].Value = first.UnitName ?? string.Empty;
                row.Cells["brecipengrediant"].Value =
                    product.Count() + (product.Count() == 1 ? " ingredient" : " ingredients");
                row.Cells["dbitemid"].Value = product.Key;
            }

            slNo = 1;
            foreach (DmnInventoryRequestDetail detail in details
                .Where(item => item.RequestType == 2 && item.ItemId.HasValue)
                .OrderBy(item => item.SlNo))
            {
                int rowIndex = dgv_raw.Rows.Add();
                DataGridViewRow row = dgv_raw.Rows[rowIndex];

                row.Cells["dataGridViewTextBoxColumn10"].Value = slNo++;
                row.Cells["dataGridViewTextBoxColumn11"].Value =
                    detail.ItemName ?? ("Raw Material " + detail.ItemId.Value);
                row.Cells["dataGridViewTextBoxColumn12"].Value = detail.CurrentStock ?? 0m;
                row.Cells["rawissuedqty"].Value = detail.IssuedQty ?? 0m;
                row.Cells["dataGridViewTextBoxColumn13"].Value = detail.UnitName ?? string.Empty;
                row.Cells["rawprojectedbalance"].Value = detail.ProjectedBalance ?? 0m;
                row.Cells["rawpurpoe"].Value = detail.PurposeRemarks ?? string.Empty;
                row.Cells["rwaitemid"].Value = detail.ItemId.Value;
                row.Cells["rawproductype"].Value = detail.ProductType ?? 2;
            }
        }

        private void UpdateApprovalSummary()
        {
            List<IGrouping<int, DmnInventoryRequestDetail>> products = _loadedRequestDetails
                .Where(item => item.RequestType == 1 && item.ProductId.HasValue)
                .GroupBy(item => item.ProductId.Value)
                .ToList();

            decimal batchFoodQty = products.Sum(group =>
                group.First().PlannedQty ?? 0m);

            decimal rawMaterialQty = _loadedRequestDetails
                .Where(item => item.RequestType == 2 && item.ItemId.HasValue)
                .Sum(item => item.IssuedQty ?? 0m);

            txt_batchfoodqty.Text = batchFoodQty.ToString("0.####");
            txt_totalrawmaterialqty.Text = rawMaterialQty.ToString("0.####");
        }

        private void UpdateStatusIndicator(int status)
        {
            string statusText;
            Image statusImage;

            switch (status)
            {
                case ApprovalApproved:
                    statusText = "Approved";
                    statusImage = Properties.Resources.Invapp_Status_Approved;
                    break;
                case ApprovalRejected:
                    statusText = "Rejected";
                    statusImage = Properties.Resources.Invapp_Status_Rejected;
                    break;
                case ApprovalSentBack:
                    statusText = "Sent Back";
                    statusImage = Properties.Resources.Invapp_Status_SendBack;
                    break;
                default:
                    statusText = "Pending";
                    statusImage = Properties.Resources.Inv_Hourglass;
                    break;
            }

            pb_status.Image = statusImage;
            pb_status.SizeMode = PictureBoxSizeMode.Zoom;
            lbl_information.Text = statusText;
        }

        private void UpdateDecisionButtons(bool enabled)
        {
            btn_approval.Enabled = enabled;
            btnreject.Enabled = enabled;
            btnsendback.Enabled = enabled;

            txtremarks.Enabled = true;
            txtremarks.ReadOnly = !enabled;
        }

        private void btn_approval_Click(object sender, EventArgs e)
        {
            SaveApprovalDecision(ApprovalApproved);
        }

        private void btnreject_Click(object sender, EventArgs e)
        {
            SaveApprovalDecision(ApprovalRejected);
        }

        private void btnsendback_Click(object sender, EventArgs e)
        {
            SaveApprovalDecision(ApprovalSentBack);
        }

        private void SaveApprovalDecision(int decision)
        {
            try
            {
                if (!_currentRequestId.HasValue ||
                    _currentApprovalStatus != ApprovalPending)
                {
                    ShowApprovalError("Select a Pending request before submitting a decision.");
                    return;
                }

                string remarks = GetApprovalRemarksValue();
                if ((decision == ApprovalRejected || decision == ApprovalSentBack) &&
                    string.IsNullOrWhiteSpace(remarks))
                {
                    ShowApprovalError(decision == ApprovalRejected
                        ? "Enter a reason before rejecting this request."
                        : "Enter the correction required before sending this request back.");
                    txtremarks.Focus();
                    return;
                }

                string action = decision == ApprovalApproved
                    ? "approve"
                    : decision == ApprovalRejected
                        ? "reject"
                        : "send this request back";

                DialogResult confirmation = CMessageBox.Show(
                    "Are you sure you want to " + action + "?",
                    "Confirm Approval Decision",
                    MessageBoxButtons.YesNo);

                if (confirmation != DialogResult.Yes)
                    return;

                int employeeId = FrmLogin.EmployeeId > 0
                    ? FrmLogin.EmployeeId
                    : FrmLogin.UserId;

                int requestId = _currentRequestId.Value;

                _inventoryBll.UpdateInventoryRequestApproval(
                    "dbo.prdUpdate_InventoryRequestApproval",
                    requestId,
                    decision,
                    employeeId,
                    remarks,
                    FrmDashboard.BranchId);

                CMessageBox.Show(
                    decision == ApprovalApproved
                        ? "Inventory request approved successfully."
                        : decision == ApprovalRejected
                            ? "Inventory request rejected."
                            : "Inventory request sent back to the requester.",
                    "Inventory Request Approval",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadApprovalRequest(requestId);
                LoadApprovalRequests();
            }
            catch (Exception ex)
            {
                LogInventoryException("SaveApprovalDecision", ex);
                ShowApprovalError("Unable to save the approval decision. " + ex.Message);
            }
        }

        private void ClearSelectedRequest()
        {
            _currentRequestId = null;
            _currentApprovalStatus = ApprovalPending;
            _loadedRequestDetails = new List<DmnInventoryRequestDetail>();

            txt_reqno.Clear();
            txt_reqdate.Clear();
            txt_requireddate.Clear();
            txt_reqtype.Clear();
            txt_department.Clear();
            txt_reqby.Clear();
            ShowRemarksPlaceholder();

            lbl_requesttype.Text = "-";
            lbl_deoartment.Text = "-";
            txt_requ_by.Text = "-";
            txt_batchfoodqty.Text = "0";
            txt_totalrawmaterialqty.Text = "0";

            dgv_batch.Rows.Clear();
            dgv_raw.Rows.Clear();

            UpdateStatusIndicator(ApprovalPending);
            UpdateDecisionButtons(false);
        }

        private static int ToInt(string value, int fallback)
        {
            int result;
            return int.TryParse(value, out result) ? result : fallback;
        }

        private static string FormatRequestDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date)
                ? date.ToString("dd/MM/yyyy")
                : string.Empty;
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

        private void btn_adv_search_reqapp_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();

                DmnSearch search = new DmnSearch
                {
                    ColumnHeaders = "ID,Request No,Request Date,Required Date,Department,Requested By",
                    Select = "m.InventoryRequestId AS Id,m.RequestNo AS Field1," +
                             "CONVERT(VARCHAR(30),m.RequestDate,106) AS Field2," +
                             "CONVERT(VARCHAR(30),m.RequiredDate,106) AS Field3," +
                             "ISNULL(d.DepartmentName,'-') AS Field4," +
                             "ISNULL(e.EmployeeName,'-') AS Field5",
                    Table = "tblInventoryRequestMaster m " +
                            "LEFT JOIN tblDepartment d ON d.DepartmentId = m.DepartmentId " +
                            "LEFT JOIN tblEmployee e ON e.EmployeeId = m.EmployeeId",
                    WhereNdGroupBy = "WHERE ISNULL(m.Status,1) = 1 AND m.BranchId = " +
                                     Convert.ToString(FrmDashboard.BranchId),
                    OrderBy = "ORDER BY m.InventoryRequestId DESC"
                };

                // Check for available records before opening the search popup.
                List<DmnGridFields> requestRows =
                    bllSearch.GetSearchData("prdSearch", search);

                if (requestRows == null || requestRows.Count == 0)
                {
                    CMessageBox.Show(
                        "No inventory requests found.",
                        "Empty",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DataTable alignment = new DataTable();
                alignment.Columns.Add();

                int selectedId = 0;

                using (FrmSearch<DmnGridFields> popup = new FrmSearch<DmnGridFields>(
                    search,
                    SearchMode.FirstCharacter,
                    Width - 150,
                    Height - 150,
                    Location.Y + 50,
                    Location.X + 20,
                    alignment))
                {
                    if (popup.ShowDialog(this) == DialogResult.OK)
                        int.TryParse(Convert.ToString(popup._retValue), out selectedId);
                }

                if (selectedId <= 0)
                    return;

                // Load the selected record into the approval form using the same
                // master/detail loading method as the approval request list.
                LoadApprovalRequest(selectedId);

                // Close the request-list popup after loading the selected request.
                pnl_reqapproval.Visible = false;
            }
            catch (Exception ex)
            {
                LogInventoryException("btn_adv_search_reqapp_Click", ex);
                ShowApprovalError("Unable to open advanced request search. " + ex.Message);
            }
        }

        private void btn_reqapp_cls_Click(object sender, EventArgs e)
        {
            pnl_reqapproval.Visible = false;
        }

        private void btnclose_reqapp_Click(object sender, EventArgs e)
        {
            pnl_reqapproval.Visible = false;
        }

        private void btn_movefrist_Click(object sender, EventArgs e)
        {
            GoToApprovalPage(1);
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            GoToApprovalPage(_requestApprovalPage - 1);
        }

        private void btn_page1_Click(object sender, EventArgs e)
        {
            GoToApprovalPage(Convert.ToInt32(btn_page1.Tag ?? btn_page1.Text));
        }

        private void btn_page2_Click(object sender, EventArgs e)
        {
            GoToApprovalPage(Convert.ToInt32(btn_page2.Tag ?? btn_page2.Text));
        }

        private void btn_page3_Click(object sender, EventArgs e)
        {
            GoToApprovalPage(Convert.ToInt32(btn_page3.Tag ?? btn_page3.Text));
        }

        private void btn_page4_Click(object sender, EventArgs e)
        {
            GoToApprovalPage(Convert.ToInt32(btn_page4.Tag ?? btn_page4.Text));
        }

        private void btn_page5_Click(object sender, EventArgs e)
        {
            GoToApprovalPage(Convert.ToInt32(btn_page5.Tag ?? btn_page5.Text));
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            GoToApprovalPage(_requestApprovalPage + 1);
        }

        private void btn_movelast_Click(object sender, EventArgs e)
        {
            GoToApprovalPage(TotalApprovalPages);
        }

        private void GoToApprovalPage(int page)
        {
            _requestApprovalPage = Math.Max(1, Math.Min(page, TotalApprovalPages));
            BindApprovalPage();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmInventoryRequestApproval_Load(object sender, EventArgs e)
        {
            textb_shape();

            UIHelper.RoundCorners(tlp_batch_raw, 25);
            UIHelper.RoundCorners(tlp_decision, 25);
            UIHelper.RoundCorners(tlp_info, 25);
            UIHelper.RoundCorners(tlp_header, 25);

            CenterRequestApprovalPopup();
            UpdateStatusIndicator(ApprovalPending);
        }

        private void FrmInventoryRequestApproval_Resize(object sender, EventArgs e)
        {
            textb_shape();
            if (pnl_reqapproval != null)
                CenterRequestApprovalPopup();
        }

        private void ShowApprovalError(string message)
        {
            //CMessageBox.Show(
            //    message,
            //    "Inventory Request Approval",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.Warning);
        }

        private void LogInventoryException(string eventName, Exception ex)
        {
            try
            {
                clsgen.LogException(
                    "InventoryManagement page " + eventName,
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.InventoryManagement));
            }
            catch
            {
                // Logging must not interrupt the inventory approval workflow.
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
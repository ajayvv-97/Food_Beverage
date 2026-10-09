using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmReason : Form
    {
        private readonly ClsGeneral clsgen = new ClsGeneral();

        public bool deleteclcik = false;

        private const int SlNoColumn = 0;
        private const int IdColumn = 1;
        private const int ReasonNameColumn = 2;
        //private const int DescriptionColumn = 3;
        private const int StatusColumn = 3;

        public FrmReason()
        {
            InitializeComponent();
        }
        private void ConfigureGrid()
        {
            dgv_reason.BorderStyle = BorderStyle.None;
            dgv_reason.BackgroundColor = Color.White;
            dgv_reason.EnableHeadersVisualStyles = false;

            dgv_reason.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(238, 239, 249);

            dgv_reason.CellBorderStyle =
                DataGridViewCellBorderStyle.SingleHorizontal;

            dgv_reason.DefaultCellStyle.SelectionBackColor =
                Color.DarkTurquoise;

            dgv_reason.DefaultCellStyle.SelectionForeColor =
                Color.WhiteSmoke;

            dgv_reason.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgv_reason.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(20, 25, 72);

            dgv_reason.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!MandatoryCheck())
                    return;

                if (!NameExist())
                    return;

                long reasonId = 0;

                if (!string.IsNullOrWhiteSpace(txtReasonId.Text))
                {
                    if (!long.TryParse(txtReasonId.Text, out reasonId))
                    {
                        CMessageBox.Show(
                            "Invalid Reason ID.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }

                DmnReason reason = new DmnReason
                {
                    ReasonId = reasonId,
                    ReasonName = txtReasonName.Text.Trim(),
                    Description = txtdescr.Text.Trim(),
                    BranchId = FrmDashboard.BranchId,
                    CreatedBy = FrmDashboard.UserId,
                    Status = chk_active.Checked ? 1 : 0
                };

                BllReason bllReason = new BllReason();

                DmnReason result = bllReason.AddeditData(
                    "prdInsert_upd_Reason",
                    reason);

                if (result != null && result.ReasonId > 0)
                {
                    CMessageBox.Show(
                        deleteclcik
                            ? "Reason deleted successfully."
                            : reasonId > 0
                                ? "Reason updated successfully."
                                : "Reason saved successfully.");

                    clearall();
                    FillReason();
                }
                else
                {
                    CMessageBox.Show(
                        "Save failed.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowError("btnsave_Click()", ex);
            }
        }

        private bool MandatoryCheck()
        {
            if (string.IsNullOrWhiteSpace(txtReasonName.Text))
            {
                CMessageBox.Show(
                    "Please enter Reason Name.",
                    "Mandatory Field",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtReasonName.Focus();
                return false;
            }

            if (txtReasonName.Text.Trim().Length > 200)
            {
                CMessageBox.Show(
                    "Reason Name cannot exceed 200 characters.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtReasonName.Focus();
                return false;
            }

            return true;
        }

        private bool NameExist()
        {
            try
            {
                if (deleteclcik)
                    return true;

                BllSearch<DmnReason> bllSearch =
                    new BllSearch<DmnReason>();

                DmnSearch search = new DmnSearch
                {
                    Select = "ReasonId, ReasonName",
                    Table = "tblReason",
                    WhereNdGroupBy =
                        "WHERE Status = 1 AND BranchId = " +
                        Convert.ToInt64(FrmDashboard.BranchId) +
                        " AND ReasonName = '" +
                        txtReasonName.Text.Trim().Replace("'", "''") +
                        "' AND ReasonId <> " +
                        (string.IsNullOrWhiteSpace(txtReasonId.Text)
                            ? "0"
                            : txtReasonId.Text.Trim())
                };

                var records = bllSearch.GetSearchData(
                    "prdSearch", search);

                if (records.Count > 0)
                {
                    CMessageBox.Show(
                        "Reason Name already exists.",
                        "Duplicate Reason",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtReasonName.Focus();
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ShowError("NameExist()", ex);
                return false;
            }
        }

        public void FillReason()
        {
            try
            {
                BllSearch<DmnReason> bllSearch =       new BllSearch<DmnReason>();
                DmnSearch search = new DmnSearch
                {
                    Select ="ReasonId, ReasonName, Description, Status",
                    Table = "tblReason",
                    WhereNdGroupBy = "WHERE BranchId = " +   Convert.ToInt64(FrmDashboard.BranchId) + " AND Status = 1",
                    OrderBy = "ORDER BY ReasonName"
                };

                var records = bllSearch.GetSearchData("prdSearch", search);

                dgv_reason.Rows.Clear();

                if (records == null || records.Count == 0)
                    return;

                foreach (var item in records)
                {
                    int index = dgv_reason.Rows.Add();

                    dgv_reason.Rows[index].Cells[SlNoColumn].Value =  index + 1;

                    dgv_reason.Rows[index].Cells[IdColumn].Value = item.ReasonId;

                    dgv_reason.Rows[index].Cells[ReasonNameColumn].Value =  item.ReasonName;

                    //dgv_reason.Rows[index].Cells[DescriptionColumn].Value = item.Description;

                    dgv_reason.Rows[index].Cells[StatusColumn].Value = item.Status;
                }
            }
            catch (Exception ex)
            {
                ShowError("FillReason()", ex);
            }
        }

        private void FillDetails(long reasonId)
        {
            try
            {
                BllSearch<DmnReason> bllSearch =
                    new BllSearch<DmnReason>();

                DmnSearch search = new DmnSearch
                {
                    Select = "*",
                    Table = "tblReason",
                    WhereNdGroupBy =
                        "WHERE BranchId = " +
                        Convert.ToInt64(FrmDashboard.BranchId) +
                        " AND ReasonId = " + reasonId,
                    OrderBy = ""
                };

                var records = bllSearch.GetSearchData(
                    "prdSearch", search);

                if (records == null || records.Count == 0)
                    return;

                DmnReason item = records[0];

                txtReasonId.Text = item.ReasonId.ToString();
                txtReasonName.Text = item.ReasonName;
                txtdescr.Text = item.Description;
                chk_active.Checked = item.Status == 1;

                btn_del_comp.Visible = item.Status == 1;
            }
            catch (Exception ex)
            {
                ShowError("FillDetails()", ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch =
                    new BllSearch<DmnGridFields>();

                DmnSearch search = new DmnSearch
                {
                    ColumnHeaders = "ID,Reason Name,Description",
                    Select =
                        "ReasonId AS Id, " +
                        "ReasonName AS Field1, " +
                        "Description AS Field2",
                    Table = "tblReason",
                    WhereNdGroupBy =
                        "WHERE BranchId = " +
                        Convert.ToInt64(FrmDashboard.BranchId) +
                        " AND Status = 1",
                    OrderBy = "ORDER BY ReasonName"
                };

                var records = bllSearch.GetSearchData(
                    "prdSearch", search);

                if (records == null || records.Count == 0)
                {
                    CMessageBox.Show(
                        "No Records Found.",
                        "Search",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();

                int selectedId = 0;

                using (FrmSearch<DmnGridFields> popup =
                    new FrmSearch<DmnGridFields>(
                        search,
                        SearchMode.FirstCharacter,
                        this.Width - 150,
                        this.Height - 150,
                        this.Location.Y + 50,
                        this.Location.X + 20,
                        dtColumnAlign))
                {
                    if (popup.ShowDialog() == DialogResult.OK)
                    {
                        int.TryParse(
                            Convert.ToString(popup._retValue),
                            out selectedId);
                    }
                }

                if (selectedId > 0)
                    FillDetails(selectedId);
            }
            catch (Exception ex)
            {
                ShowError("btnSearch_Click()", ex);
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
            FillReason();
        }

        public void clearall()
        {
            deleteclcik = false;

            txtReasonId.Clear();
            txtReasonName.Clear();
            txtdescr.Clear();

            chk_active.Checked = true;
            btn_del_comp.Visible = false;

            txtReasonName.Focus();
        }

        private void btn_del_unit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtReasonId.Text))
                    return;

                DialogResult answer = CMessageBox.Show(
                    "Do you want to delete this Reason?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (answer != DialogResult.Yes)
                    return;

                deleteclcik = true;
                chk_active.Checked = false;

                // Reuse the save procedure to perform a soft delete.
                btnsave.PerformClick();
            }
            catch (Exception ex)
            {
                ShowError("btn_del_unit_Click()", ex);
            }
        }

        private void dgv_reason_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 ||
                    dgv_reason.Rows[e.RowIndex].IsNewRow)
                    return;

                object value =
                    dgv_reason.Rows[e.RowIndex].Cells[IdColumn].Value;

                if (value == null)
                    return;

                long reasonId;

                if (long.TryParse(value.ToString(), out reasonId) &&
                    reasonId > 0)
                {
                    FillDetails(reasonId);
                }
            }
            catch (Exception ex)
            {
                ShowError("dgv_unit_CellClick()", ex);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowError(string method, Exception ex)
        {
            clsgen.LogException("Reason page " + method, ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Reason));

        }

      

        private void FrmReason_Load(object sender, EventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.RegisterForm(this, (int)LogoutFormList.operation);
                }

                if (Application.OpenForms["FrmDashboard"] is FrmDashboard dashboard)
                {
                    btnsave.Enabled = dashboard.ChkWritePermission(Convert.ToInt32(formsenum.Complementaryreason));
                }

                chk_active.Checked = true;
                btn_del_comp.Visible = false;

                ConfigureGrid();
                FillReason();

                txtReasonName.Focus();
            }
            catch (Exception ex)
            {
                ShowError("FrmReason_Load()", ex);
            }
        }

        private void FrmReason_FormClosing(object sender, FormClosingEventArgs e)
        {

                if (FrmDashboard.AutoLogoutWhenIdle)
                    AppSession.UnregisterForm(this);
            
        }
    }
}
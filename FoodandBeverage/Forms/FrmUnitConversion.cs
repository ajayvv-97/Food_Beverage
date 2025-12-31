using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;
using System.Collections.Generic;
using FoodandBeverage.Classes;

namespace FoodandBeverage.Forms
{
    public partial class FrmUnitConversion : Form
    {
        ClsGeneral clsgen = new ClsGeneral();
        //public bool deleteclcik = false;
        #region Declare Grid
        public int slno = 0;
        public int idno = 1;
        public int ifromunit = 2;
        public int itounit= 3;
        public int ifactor = 4;
        public int istatusno = 5;
        #endregion
        public FrmUnitConversion()
        {
            InitializeComponent();
        }


        private void FrmUnitConversion_Load(object sender, EventArgs e)
        {
            Fillunit();
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = " UId,UnitName";
            objSearch.Table = " tblUnit";
            objSearch.WhereNdGroupBy = "where status=1";
            objSearch.OrderBy = "";
            var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
            var gridSource2 = bllSearch.GetSearchData("prdComboFill", objSearch);
            cmb_fromunit.DataSource = gridSource1;
            cmb_fromunit.ValueMember = "Id";
            cmb_fromunit.DisplayMember = "Value";

            cmb_tounit.DataSource = gridSource2;
            cmb_tounit.ValueMember = "Id";
            cmb_tounit.DisplayMember = "Value";
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
            Fillunit();
        }
        public void clearall()
        {
            txtUid.Text = "";
            txtFactor.Text = "";
            cmb_fromunit.Text = "";
            cmb_tounit.Text = "";
            Fillunit();

        }
        public void Fillunit()
        {
            try
            {
                BllSearch<DmnUnitConversion> bllSearch = new BllSearch<DmnUnitConversion>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "id,U.UnitName as FUnit,UU.UnitName as TUnit,Factor as Fctr";
                objSearch.Table = "tblUnitConversion UC left join tblUnit U ON U.UId = UC.FromUnit left join tblUnit UU ON UU.UId = UC.ToUnit ";
                objSearch.WhereNdGroupBy = " ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_unit.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_unit.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_unit.Rows[i].Cells[slno].Value = i + 1;
                        dgv_unit.Rows[i].Cells[idno].Value = itemSource[i].id;
                        dgv_unit.Rows[i].Cells[ifromunit].Value = itemSource[i].FUnit;
                        dgv_unit.Rows[i].Cells[itounit].Value = itemSource[i].TUnit;
                        dgv_unit.Rows[i].Cells[ifactor].Value = itemSource[i].Fctr;
                        dgv_unit.Rows[i].Cells[istatusno].Value = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Unit page Fillunit()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Unit));
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheckuser())
                {
                        bool savestatus;
                        if (cmb_fromunit.SelectedValue !="" || cmb_tounit.SelectedValue !="")
                        {
                            DmnUnitConversion dmnunitconversion = new DmnUnitConversion();
                            BllUnit bllunit = new BllUnit();
                            if (string.IsNullOrWhiteSpace(txtUid.Text))
                                txtUid.Text = "0";
                            dmnunitconversion.id = Convert.ToInt32(txtUid.Text);
                            dmnunitconversion.FromUnit = Convert.ToInt32(cmb_fromunit.SelectedValue);
                            dmnunitconversion.ToUnit = Convert.ToInt32(cmb_tounit.SelectedValue);
                            dmnunitconversion.Factor = Convert.ToDecimal(txtFactor.Text);

                            var usertype = bllunit.AddeditConversion("prdInsert_upd_UnitConv", dmnunitconversion);
                            if (usertype.id > 0)
                            {
                                if (Convert.ToUInt32(txtUid.Text) > 0)
                                    CMessageBox.Show("Update Successfully saved");
                                else
                                    CMessageBox.Show("Successfully saved");
                                clearall();
                                Fillunit();
                            }
                            else
                                CMessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                        }
                       
                    
                }
            }
            catch (Exception ex)
            {
                // CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Unit page chk_seleall_pro_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Unit));
            }
        }
        public bool MandatoryCheckuser()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFactor.Text))
                {
                    CMessageBox.Show("Please Enter Factor", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Unit page MandatoryCheckuser()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Unit));
                return false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,From Unit,To Unit, Factor";
                _objSearch.Select = "id as Id,U.UnitName as Field1,UU.UnitName as Field2,Factor as Field3";
                _objSearch.Table = "tblUnitConversion UC left join tblUnit U ON U.UId = UC.FromUnit left join tblUnit UU ON UU.UId = UC.ToUnit ";
                _objSearch.WhereNdGroupBy = "";
                _objSearch.OrderBy = " ORDER BY id";
                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                    {
                        var dialogResult = search.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            returnValue = Convert.ToInt32(search._retValue);
                        }
                    }
                    if (returnValue > 0)
                    {
                        int unitid = Convert.ToInt32(returnValue);
                        BllSearch<DmnUnitConversion> bllSearch1 = new BllSearch<DmnUnitConversion>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch.Select = "*";
                        objSearch.Table = "tblUnitConversion";
                        objSearch.WhereNdGroupBy = "where id=" + unitid;
                        objSearch.OrderBy = "";
                        var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                        if (itemSource.Count > 0)
                        {
                            txtUid.Text = Convert.ToString(unitid);
                            cmb_fromunit.SelectedValue = Convert.ToInt64(itemSource[0].FromUnit);
                            cmb_tounit.SelectedValue = Convert.ToInt64(itemSource[0].ToUnit);
                            txtFactor.Text = Convert.ToString(itemSource[0].Factor);
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Unit page btnSearch_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Unit));
            }
        }
    }
}

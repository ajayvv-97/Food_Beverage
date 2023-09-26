using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmGeneralOptions : MetroForm
    {

        #region Declarations
        ClsGeneral objGeneral = new ClsGeneral();// for validation
        long UserId = Convert.ToInt32(FrmDashboard.UserId);
        long GeneralId = 0;
        int Status = 0;//to check modifyable or not


        #endregion

        #region Functions
        public FrmGeneralOptions()
        {
            InitializeComponent();
        }
        public void RefreshForm()
        {
            MetroTextBox[] txtlist = {mtxtCaption,mtxtText1,mtxtText2,mtxtText3,mtxtValue1,mtxtValue2,mtxtValue3 };
            objGeneral.ResetSelectedMetroTextBoxes(txtlist);
          //  objGeneral.ResetAllTextBoxes(this);
            MetroComboBox[] list = { mcmbGType };
         //   objGeneral.ResetSelectedMetroComboBoxes(list);
            hlTitle.Text = "General Options";
            GeneralId = 0;
            mcmbGType.Enabled = true;
            mbtnDelete.Enabled = false;
            mdtpDate1.Checked = false;
            mdtpDate2.Checked = false;
            mdtpDate3.Checked = false;
            FillGTypeComboBox();
            mcmbGType.Focus();

        }
        public bool IsCaptionValid()
        {
            DmnGeneralOptions objUnit = new DmnGeneralOptions();
            BllSearch<DmnUnit> bllSearch = new BllSearch<DmnUnit>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "GeneralId ";
            objSearch.Table = " tblGeneralOptions ";
            if (GeneralId == 0)
            {
                objSearch.WhereNdGroupBy = " Where Caption = '" + mtxtCaption.Text.Trim().ToUpper() + "' AND GTypeId='" + mcmbGType.SelectedValue + "'AND Status=0";
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where GeneralId<> " + GeneralId + " AND GTypeId='" + mcmbGType.SelectedValue + "' AND Status=0 AND Caption='" + mtxtCaption.Text.Trim() + "'";

            }
            var lstUnit = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstUnit.Count > 0)
            {
                MetroMessageBox.Show(this, "\nCaption  already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                mtxtCaption.Focus();
                return false;
            }
            return true;
        }

        public void FillGTypeComboBox()
        {
            try
            {

                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "GTypeId,Gtype";
                objSearch.Select = " GTypeId,GType ";
                objSearch.Table = " tblGType ";
                objSearch.WhereNdGroupBy = " Where  Status = 2 ";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                mcmbGType.DataSource = gridSource;
                mcmbGType.ValueMember = "Id";
                mcmbGType.DisplayMember = "Value";
                mcmbGType.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }


        public bool IsSavable()
        {
            Control[] controllist = { mcmbGType, mtxtCaption };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            if (!IsCaptionValid())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion

        #region Events

        private void FrmGeneralOptions_Load(object sender, EventArgs e)
        {
            try
            {
                //FillGTypeComboBox();
                //mdtpDate1.Checked = false;
                //mdtpDate2.Checked = false;
                //mdtpDate3.Checked = false;
                //mcmbGType.Focus();
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MetroTextBox4_Click(object sender, EventArgs e)
        {

        }

        private void MbtnBranchSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (IsSavable())
                {

                    DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                    BllGeneralOptions bllGen = new BllGeneralOptions();
                    dmnGen.GTypeId = Convert.ToInt32(mcmbGType.SelectedValue.ToString());
                    dmnGen.Caption = mtxtCaption.Text.Trim().ToUpper();
                    dmnGen.GTypeSubValue1 = decimal.Parse(mtxtValue1.Text == string.Empty ? "0" : mtxtValue1.Text);
                    dmnGen.GTypeSubValue2 = decimal.Parse(mtxtValue2.Text == string.Empty ? "0" : mtxtValue2.Text);
                    dmnGen.GTypeSubValue3 = decimal.Parse(mtxtValue3.Text == string.Empty ? "0" : mtxtValue3.Text);
                    if (mdtpDate1.Checked == true)
                    {
                        dmnGen.GTypeSubDate1 = mdtpDate1.Value.Date;
                    }
                    else
                    {
                        dmnGen.GTypeSubDate1 = null;
                    }
                    if (mdtpDate2.Checked == true)
                    {
                        dmnGen.GTypeSubDate2 = mdtpDate2.Value.Date;
                    }
                    else
                    {
                        dmnGen.GTypeSubDate2 = null;
                    }
                    if (mdtpDate3.Checked == true)
                    {
                        dmnGen.GTypeSubDate3 = mdtpDate3.Value.Date;
                    }
                    else
                    {
                        dmnGen.GTypeSubDate3 = null;
                    }
                    dmnGen.GTypeSubText1 = mtxtText1.Text.Trim();
                    dmnGen.GTypeSubText2 = mtxtText2.Text.Trim();
                    dmnGen.GTypeSubText3 = mtxtText3.Text.Trim();
                    if (GeneralId == 0)//insertion
                    {
                        dmnGen.CreatedBy = UserId;
                        bool insertStatus = bllGen.AddData("prdInsertGeneralOptions", dmnGen);
                        {
                            if (insertStatus)
                            {

                                MetroMessageBox.Show(this, "\nSuccessfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            }
                            else
                            {
                                MetroMessageBox.Show(this, "Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            }
                        }

                    }
                    else//updation
                    {
                       
                        if (Status == (int)StatusValue.Locked)
                        {
                            MetroMessageBox.Show(this, "\nCannot modify data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);

                        }
                        else
                        {
                            dmnGen.ModifiedBy = UserId;
                            dmnGen.GeneralId = GeneralId;
                            bool updateStatus = bllGen.UpdateData("prdUpdateGeneralOptions", dmnGen);
                            if (updateStatus)
                            {
                                MetroMessageBox.Show(this, "\nSuccessfully updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);

                            }
                            else
                            {
                                MetroMessageBox.Show(this, "update Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            }
                        }

                    }
                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MtxtValue1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MtxtValue2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MtxtValue3_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }

        }

        private void MbtnBranchRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MbtnListCompany_Click(object sender, EventArgs e)
        {
            try
            {
                #region pop up form 

                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Name,Type,Value 1,Value 2,Value 3,Text 1,Text 2,Text 3,Date 1,Date 2";
                _objSearch.Select = " geno.GeneralId as Id,geno.Caption as Field1,gt.GType as Field2,geno.GTypeSubValue1 as Field3," +
                    " geno.GTypeSubValue2 as Field4,geno.GTypeSubValue3 as Field5,geno.GTypeSubText1 as Field6,geno.GTypeSubText2 as Field7," +
                    "geno.GTypeSubText3 as Field8,geno.GTypeSubDate1 as Field9,geno.GTypeSubDate2 as Field10  ";                   
                _objSearch.Table = " tblGeneralOptions as geno inner join tblGType gt on gt.GTypeId = geno.GTypeId";
                _objSearch.WhereNdGroupBy = "where  geno.Status=0";
                _objSearch.OrderBy = "order by Caption ASC ";
                //using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 850, 550, 50, 240))
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width , (this.Height-50), this.Location.Y + 35, this.Location.X+50))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                #endregion

                if (returnValue != "")
                {
                    GeneralId = Convert.ToInt64(returnValue);
                    DmnGeneralOptions objCompany = new DmnGeneralOptions();
                    BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
                    DmnSearch objSearch = new DmnSearch();
                    // objSearch.ColumnHeaders = "DepartmentName";
                    objSearch.Select = "Caption,GTypeId,GTypeSubValue1,GTypeSubValue2,GTypeSubValue3,GtypeSubText1,GtypeSubText2,GtypeSubText3," +
                        "GTypeSubDate1,GTypeSubDate2,GTypeSubDate3,Status";
                    objSearch.Table = " tblGeneralOptions ";
                    objSearch.WhereNdGroupBy = " Where GeneralId =" + GeneralId; 

                    var lstResult = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id

                    mcmbGType.SelectedValue = lstResult[0].GTypeId;
                    mtxtCaption.Text = lstResult[0].Caption;

                    mtxtValue1.Text = Convert.ToString(lstResult[0].GTypeSubValue1);//Convert.ToString(decimal.Parse(lstResult[0].GTypeSubValue1.ToString() == string.Empty ? "0.00" : lstResult[0].GTypeSubValue1.ToString()));
                    mtxtValue2.Text = Convert.ToString(lstResult[0].GTypeSubValue2);
                    mtxtValue3.Text = Convert.ToString(lstResult[0].GTypeSubValue3);
                    mtxtText1.Text = lstResult[0].GTypeSubText1;
                    mtxtText2.Text = lstResult[0].GTypeSubText2;
                    mtxtText3.Text = lstResult[0].GTypeSubText3;
                    Status = lstResult[0].Status;
                    //check if date is null then display todays date
                    if (lstResult[0].GTypeSubDate1.ToString() == "")
                    {
                        mdtpDate1.Value = DateTime.Now.Date;
                        mdtpDate1.Checked = false;
                    }
                    else
                    {
                        mdtpDate1.Value = Convert.ToDateTime(lstResult[0].GTypeSubDate1);
                        mdtpDate1.Checked = true;
                    }
                    if (lstResult[0].GTypeSubDate2.ToString() == "")
                    {
                        mdtpDate2.Value = DateTime.Now.Date;
                        mdtpDate2.Checked = false;
                    }
                    else
                    {
                        mdtpDate2.Value = Convert.ToDateTime(lstResult[0].GTypeSubDate2);
                        mdtpDate2.Checked = true;
                    }
                    if (lstResult[0].GTypeSubDate3.ToString() == "")
                    {
                        mdtpDate3.Value = DateTime.Now.Date;
                        mdtpDate3.Checked = false;
                    }
                    else
                    {
                        mdtpDate3.Value = Convert.ToDateTime(lstResult[0].GTypeSubDate3);
                        mdtpDate3.Checked = true;
                    }

                    hlTitle.Text = "General Options [Edit]";
                    mcmbGType.Enabled = false;
                    mbtnDelete.Enabled = true;
                    mcmbGType.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void MbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Status == (int)StatusValue.Locked)
                {
                    MetroMessageBox.Show(this, "\nCannot delete data", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);

                }
                else
                {

                    DialogResult result = MetroMessageBox.Show(this, "\nDo you want to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                    if (result == DialogResult.Yes)
                    {
                        DmnGeneralOptions dmnGeneralOptions = new DmnGeneralOptions();
                        BllGeneralOptions bllCompany = new BllGeneralOptions();
                        dmnGeneralOptions.GeneralId = GeneralId;
                        bool deleteStatus = bllCompany.DeleteById("prdDeleteGeneralOptions", dmnGeneralOptions);

                        if (deleteStatus)
                        {
                            MetroMessageBox.Show(this, "\nSuccessfully deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information, 150);
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "\nError in deletion", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        RefreshForm();

                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void FrmGeneralOptions_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender,e);
                if (e.KeyCode == Keys.F2)
                    MbtnBranchSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnListCompany_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnBranchRefresh_Click(sender, e);
                if (e.KeyCode == Keys.F4)
                    MbtnDelete_Click(sender, e);
                    
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

       
    }
}

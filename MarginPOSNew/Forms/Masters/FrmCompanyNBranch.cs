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
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmCompanyNBranch : MetroForm
    {
       
        #region Declaration
        
        ClsGeneral objGeneral = new ClsGeneral();// for validation
        long CompanyId = 0;//used in edit
        long BrId = 0;//usedin edit
        long BranchId = Convert.ToInt64(FrmDashboard.BranchId);
        long _CompanyId = Convert.ToInt64(FrmDashboard.CompanyId);
        long UserId = Convert.ToInt64(FrmDashboard.UserId);
        string imageName = string.Empty;
        string brLogoName = string.Empty;
        ClsDocument clsDocument = new ClsDocument();
        bool imageExist,brLogoExist;
        Bitmap bm,bmbrLogo;
        public const int tabCompany = 0;
        #endregion

        #region Functions
        public void RefreshCompanyForm()
        {
            CompanyId = 0;
            hlTitle.Text = "Company And Branch";
            imageName = string.Empty;
            //_CompanyId = 7;//id from login.........................................................................need to remove...........................
           MetroTextBox[] mtxtlist = {mtxtCompanyName,mtxtCompanyCode,mtxtCompAddress,mtxtPostalCode,mtxtMobileNo,
            mtxtEmail,mtxtWebsite,mtxtPAN,mtxtContactName,mtxtContactNo,mtxtGSTNo,mtxtTINNo,mtxtSalesTaxNo,mtxtTelephoneNo};
            objGeneral.ResetSelectedMetroTextBoxes(mtxtlist);
            pbLogo.Image = null;
            //MetroComboBox[] cmbList = { mcmbCompanyBank };
            //objGeneral.ResetSelectedMetroComboBoxes(cmbList);
            FillCompanyComboBox();
            FillBankComboBox();
            mbtnDeleteCompany.Enabled = false;
            mtxtCompanyName.Focus();
            if(string.IsNullOrWhiteSpace(mtxtCompanyName.Text))
            {
                mbtnSave.Enabled = false;
            }
            if (bm != null)
            {
                bm.Dispose();
                bm = null;
            }
        }
        public void RefreshBranchForm()
        {
            BrId = 0;
            hlTitle.Text = "Company And Branch";

            MetroTextBox[] mtxtlist = { mtxtBranchName,mtxtBranchCode,mtxtBrAddress,mtxtBrPostalCode,mtxtBrTelephoneNo,mtxtBrMobileNo,
            mtxtBrFax,mtxtBrEmail,mtxtBrWebsite,mtxtBrPAN,mtxtBrContactName,mtxtBrContactNo,mtxtBrGSTNo,mtxtBrTIN,mtxtBrSalesPrefix,
            mtxtBrSalesSrlNo,mtxtBrPurchaseSrlNo,mtxtBrPurchaseRtnSrlNo,mtxtBrSalesRtnSrlNo,mtxtBrInwardPrefix,mtxtBrInwardSrlNo,
            mtxtBrOutwardPrefix,mtxtBrOutwardSrlNo};
            objGeneral.ResetSelectedMetroTextBoxes(mtxtlist);
            objGeneral.ResetAllTextBoxes(this);
            FillCompanyComboBox();
            FillBankComboBox();
            mbtnDeleteBranch.Enabled = false;
            mcmbCompanyName.Focus();
            if (string.IsNullOrWhiteSpace(mtxtBranchName.Text))
            {
                mbtnBranchSave.Enabled = false;
            }
            brLogoName = string.Empty;
            pbBranchLogo.Image = null;
            if (bmbrLogo != null)
            {
                bmbrLogo.Dispose();
                bmbrLogo = null;
            }

        }
        public bool IsSavable()//company
        {
            Control[] controllist = { mtxtCompanyName, mtxtCompanyCode, mtxtCompAddress, mtxtPostalCode, mtxtTelephoneNo, mtxtMobileNo, mtxtEmail, mtxtPAN, mtxtContactName, mcmbCompanyBank, mtxtContactNo, mtxtGSTNo, mtxtSalesTaxNo };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            if(!objGeneral.CheckEmailId(this,mtxtEmail.Text.Trim()) || !IsCompanyNameValid() || !IsCompanyCodeValid()|| !IsPANValid())
            {
                return false;
            }
            
            else
            {
                return true;
            }
        }


        public bool IsPANValid()
        {
            if (objGeneral.CheckPANNo(this, mtxtPAN.Text))
            {
                return true;
            }
            return false;
        }
        public bool IsBranchPANValid()
        {
            if (objGeneral.CheckPANNo(this, mtxtBrPAN.Text))
            {
                return true;
            }
            return false;
        }
        public bool IsCompanyCodeValid()
        {
            BllSearch<DmnCompany> bllSearch = new BllSearch<DmnCompany>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "CompanyId ";
            objSearch.Table = " tblCompany ";
            if (CompanyId == 0)
            {
                objSearch.WhereNdGroupBy = " Where CompanyCode = '" + mtxtCompanyCode.Text.Trim() + "' AND Status=0 ";// AND BranchId = " + BranchId;
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where CompanyId<> " + CompanyId + " AND Status=0 AND CompanyCode='" + mtxtCompanyCode.Text.Trim() + "'";// + "' AND BranchId = " + BranchId;

            }
            var lstCategory = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstCategory.Count > 0)
            {
                MetroMessageBox.Show(this, "\nCompany code already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                mtxtCompanyCode.Focus();
                return false;
            }
            return true;

        }
        public bool IsBranchSavable()
        {
            Control[] controllist = { mcmbCompanyName,mtxtBranchName, mtxtBranchCode, mtxtBrAddress, mtxtBrPostalCode, mtxtBrTelephoneNo, mtxtBrMobileNo,
                mtxtBrEmail, mcmbBranchBank, mtxtBrPAN, mtxtBrContactName,  mtxtBrContactNo, mtxtBrGSTNo,mtxtBrSalesPrefix,mtxtBrSalesSrlNo,mtxtBrPurchaseSrlNo,mtxtBrPurchaseRtnSrlNo,
            mtxtBrSalesRtnSrlNo,mtxtBrInwardPrefix,mtxtBrInwardSrlNo,mtxtBrOutwardPrefix,mtxtBrOutwardSrlNo};
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            if (!objGeneral.CheckEmailId(this,mtxtBrEmail.Text.Trim())||!IsCompanyCodeValid() ||!IsBranchCodeValid()|| !IsCompanyNameValid() || !IsSalesPrefixValid() || !IsInwardPrefixValid() || !IsOutwardPrefixValid()||!IsBranchPANValid())
            {
                return false;
            }

            else
            {
                return true;
            }
        }
        public bool IsSalesPrefixValid()
        {
            DmnBranch objBranch = new DmnBranch();
            BllSearch<DmnBranch> bllSearch = new BllSearch<DmnBranch>();
            DmnSearch objSearch = new DmnSearch();

            objSearch.Select = "BranchId ";
            objSearch.Table = " tblBranch ";
            if (BrId == 0)
            {
                objSearch.WhereNdGroupBy = " Where SalesPrefix ='" + mtxtBrSalesPrefix.Text.Trim() + "' And CompanyId='" + mcmbCompanyName.SelectedValue + "' AND BranchName='" + mtxtBranchName.Text.Trim() + "' And Status=0";
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where BranchId<> " + BrId + " And SalesPrefix ='" + mtxtBrSalesPrefix.Text.Trim() + "' And  CompanyId='" + mcmbCompanyName.SelectedValue + "'AND BranchName='" + mtxtBranchName.Text.Trim() + "' AND Status=0";

            }
            var lstUnit = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstUnit.Count > 0)
            {
                MetroMessageBox.Show(this, "\n Sales prefix  already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                mtxtBrSalesPrefix.Focus();
                return false;
            }
            return true;
        }
        public bool IsInwardPrefixValid()
        {
            DmnBranch objBranch = new DmnBranch();
            BllSearch<DmnUnit> bllSearch = new BllSearch<DmnUnit>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "BranchId ";
            objSearch.Table = " tblBranch ";
            if (BrId == 0)
            {
                objSearch.WhereNdGroupBy = " Where InwardPrefix ='" + mtxtBrInwardPrefix.Text.Trim() + "' And CompanyId='" + mcmbCompanyName.SelectedValue + "'  And BranchName='" + mtxtBranchName.Text.Trim() + "'And Status=0";
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where BranchId<> " + BrId + " And InwardPrefix ='" + mtxtBrInwardPrefix.Text.Trim() + "' And CompanyId='" + mcmbCompanyName.SelectedValue + "' AND BranchName='" + mtxtBranchName.Text.Trim() + "' And Status=0";

            }
            var lstUnit = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstUnit.Count > 0)
            {
                MetroMessageBox.Show(this, "\n Inward prefix  already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                mtxtBrInwardPrefix.Focus();
                return false;
            }
            return true;
        }
        public bool IsOutwardPrefixValid()
        {
            DmnBranch objBranch = new DmnBranch();
            BllSearch<DmnUnit> bllSearch = new BllSearch<DmnUnit>();
            DmnSearch objSearch = new DmnSearch();
            // objSearch.ColumnHeaders = "DepartmentName";
            objSearch.Select = "BranchId ";
            objSearch.Table = " tblBranch ";
            if (BrId == 0)
            {
                objSearch.WhereNdGroupBy = " Where OutwardPrefix ='" + mtxtBrOutwardPrefix.Text.Trim() + "' And CompanyId='" + mcmbCompanyName.SelectedValue + "'AND BranchName='" + mtxtBranchName.Text.Trim() + "' And Status=0";
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where BranchId<> " + BrId + " And OutwardPrefix ='" + mtxtBrOutwardPrefix.Text.Trim() + "' And CompanyId='" + mcmbCompanyName.SelectedValue + "' AND BranchName='" + mtxtBranchName.Text.Trim() + "' AND Status=0";

            }
            var lstUnit = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstUnit.Count > 0)
            {
                MetroMessageBox.Show(this, "\n Outward prefix  already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                mtxtBrOutwardPrefix.Focus();
                return false;
            }
            return true;

        }
        public void FillBankComboBox()
        {
            try
            {

                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "  geno.GeneralId, geno.Caption";
                objSearch.Table = " tblGeneralOptions geno inner join tblGType on tblGType.GTypeId=geno.GTypeId ";
                objSearch.WhereNdGroupBy = " Where geno.Status = 0 AND tblGType.GType='BANK'";
                //  objSearch.OrderBy = " Order By CategoryName ASC";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                mcmbCompanyBank.DataSource = gridSource;
                mcmbCompanyBank.ValueMember = "Id";
                mcmbCompanyBank.DisplayMember = "Value";

                #region assigining branch bank 

                mcmbBranchBank.DataSource = gridSource;
                mcmbBranchBank.ValueMember = "Id";
                mcmbBranchBank.DisplayMember = "Value";

                #endregion
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        public void FillCompanyComboBox()
        {
            try
            {

                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                // objSearch.ColumnHeaders = "Id,Category,CatSrlNoStart,CatSrlNoCurrent";
                objSearch.Select = "   CompanyId,CompanyName";
                objSearch.Table = " tblCompany ";
                objSearch.WhereNdGroupBy = " Where Status = 0";//AND BranchId = " + BranchId; 
                //  objSearch.OrderBy = " Order By CategoryName ASC";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                mcmbCompanyName.DataSource = gridSource;
                mcmbCompanyName.ValueMember = "Id";
                mcmbCompanyName.DisplayMember = "Value";
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        public bool IsCompanyNameValid()
        {
            BllSearch<DmnCompany> bllSearch = new BllSearch<DmnCompany>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "CompanyId ";
            objSearch.Table = " tblCompany ";
            if (CompanyId == 0)
            {
                objSearch.WhereNdGroupBy = " Where CompanyName = '" + mtxtCompanyName.Text.Trim() + "' AND Status=0 ";// AND BranchId = " + BranchId;
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where CompanyId<> " + CompanyId + " AND Status=0 AND CompanyName='" + mtxtCompanyName.Text.Trim() + "'";// + "' AND BranchId = " + BranchId;

            }
            var lstCategory = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstCategory.Count > 0)
            {
                MetroMessageBox.Show(this, "\nCompany name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                mtxtCompanyName.Focus();
                return false;
            }
            return true;
        }
        public bool IsBranchCodeValid()
        {
            BllSearch<DmnCompany> bllSearch = new BllSearch<DmnCompany>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "BranchId ";
            objSearch.Table = " tblBranch ";
            if (BrId == 0)//it was companyid
            {
                objSearch.WhereNdGroupBy = " Where BranchCode = '" + mtxtBranchCode.Text.Trim() + "' AND Status=0 ";// AND BranchId = " + BranchId;
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where  BranchCode = '" + mtxtBranchCode.Text.Trim() + "' AND BranchId<> " + BrId + " AND Status=0 ";
            }
            var lstCategory = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstCategory.Count > 0)
            {
                MetroMessageBox.Show(this, "\nBranch code already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                mtxtBranchCode.Focus();
                return false;
            }
            return true;
        }

        //public bool IsEmailValid()
        //{
        //    if (!string.IsNullOrWhiteSpace(mtxtEmail.Text))
        //    {
        //        bool emailValid = objGeneral.CheckEmailId(mtxtEmail.Text);
        //        if (!emailValid)
        //        {
        //            MetroMessageBox.Show(this, "Enter valid email", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
        //            return false;

        //        }
        //        return true;
        //    }
        //    if (!string.IsNullOrWhiteSpace(mtxtBrEmail.Text))
        //    {
        //        bool emailValid = objGeneral.CheckEmailId(mtxtBrEmail.Text);
        //        if (!emailValid)
        //        {
        //            MetroMessageBox.Show(this, "Enter valid email", "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
        //            return false;

        //        }
        //        return true;
        //    }
        //    return true;
        //}

        #endregion

        #region Events
        public FrmCompanyNBranch()
        {
            InitializeComponent();
        }
        private void FrmCompany_Load(object sender, EventArgs e)
        {
            try
            {
                FillBankComboBox();
                FillCompanyComboBox();
                metroTabControl1.SelectedTab = mtbCompany;
               // _CompanyId = 27;//id from login static value set
                               //enable tab page after checking the user id
                mtxtCompanyName.Focus();
                #region display company only for super admin
                //if (FrmDashboard.UserStatus != -1)
                //{
                //    metroTabControl1.TabPages.Remove(mtbCompany);
                //}
                //else
                //{
                //    metroTabControl1.TabPages.Add(mtbCompany);
                //}
                #endregion

                #region tooltip

                ToolTip toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 1000;
                toolTip1.ReshowDelay = 500;
                toolTip1.ShowAlways = true;
                toolTip1.SetToolTip(this.pbAddImage, "Add logo");
                toolTip1.SetToolTip(this.pbRemoveImage, "Remove logo");
                #endregion
                metroTabControl1.SelectedIndex = tabCompany;
                //metroTabControl1.Focus();
                mtxtCompanyName.Focus();
                ActiveControl = mtxtCompanyName;
                RefreshCompanyForm();
                RefreshBranchForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnSave_Click(object sender, EventArgs e)
        {
          
            try
            {
               
                if (IsSavable())
                {
                    DmnCompany dmnCompany = new DmnCompany();
                    BllCompany objBll = new BllCompany();

                    dmnCompany.CompanyName = mtxtCompanyName.Text.Trim();
                    dmnCompany.CompanyCode = mtxtCompanyCode.Text.Trim();
                    dmnCompany.CompanyAddress = mtxtCompAddress.Text.Trim();
                    dmnCompany.PostalCode = mtxtPostalCode.Text.Trim();
                    dmnCompany.Telephone = mtxtTelephoneNo.Text.Trim();
                    dmnCompany.Mobile = mtxtMobileNo.Text.Trim();
                    dmnCompany.Fax = mtxtFax.Text.Trim();
                    dmnCompany.Email = mtxtEmail.Text.Trim();
                    dmnCompany.Website = mtxtWebsite.Text.Trim();
                    imageName = mtxtCompanyCode.Text.Trim() + ".jpeg";//neeeded if only image is added in edit
                    dmnCompany.Logo = imageName;
                    dmnCompany.CompanyBankId = Convert.ToInt64(mcmbCompanyBank.SelectedValue);
                    dmnCompany.PAN = mtxtPAN.Text.Trim().ToUpper();
                    dmnCompany.ContactName = mtxtContactName.Text.Trim();
                    dmnCompany.ContactNo = mtxtContactNo.Text.Trim();
                    dmnCompany.GSTNo = mtxtGSTNo.Text.Trim();
                    dmnCompany.TINNo = mtxtTINNo.Text.Trim();
                    dmnCompany.STaxNo = mtxtSalesTaxNo.Text.Trim();
                    if (pbLogo.Image == null)
                    {
                        imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.CompanyLogo);
                        if (imageExist)
                            clsDocument.DeleteImage(imageName, DocumentType.CompanyLogo);
                        dmnCompany.Logo = "";
                        imageName = "";

                    }
                    else
                    {
                        bm = new Bitmap(pbLogo.Image);
                        dmnCompany.Logo = imageName; 
                    }
                    if (CompanyId == 0)//insertion
                    {
                        dmnCompany.CreatedBy = UserId;
                        bool insertStatus = objBll.AddData("prdInsertCompany", dmnCompany);
                        if (insertStatus == true)
                        {
                            if (pbLogo.Image != null)
                                clsDocument.UploadImage(bm, imageName, DocumentType.CompanyLogo);
                            MetroMessageBox.Show(this, "Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                       
                    }
                    else
                    {
                        dmnCompany.ModifiedBy = UserId;
                        dmnCompany.ModifiedDateTime = DateTime.Now;
                        dmnCompany.CompanyId = CompanyId;
                        bool updateResult = objBll.UpdateData("prdUpdateCompany", dmnCompany);

                        if (updateResult == true)
                        {
                            if (pbLogo.Image != null)
                                clsDocument.UploadImage(bm, imageName, DocumentType.CompanyLogo);
                            MetroMessageBox.Show(this, "Updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "Update failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                       
                    }                   
                    mbtnDeleteCompany.Enabled = false;
                    RefreshCompanyForm();

                   
                }
                mtxtCompanyName.Focus();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
           
        }
        private void MbtnBranchSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsBranchSavable())
                {
                    DmnBranch dmnBranch = new DmnBranch();
                    BllBranch objBll = new BllBranch();
                    dmnBranch.CompanyId = Convert.ToInt64(mcmbCompanyName.SelectedValue);
                    dmnBranch.BranchName = mtxtBranchName.Text.Trim();
                    dmnBranch.BranchCode = mtxtBranchCode.Text.Trim();
                    dmnBranch.BranchAddress = mtxtBrAddress.Text.Trim();
                    dmnBranch.PostalCode = mtxtBrPostalCode.Text.Trim();
                    dmnBranch.Telephone = mtxtBrTelephoneNo.Text.Trim();
                    dmnBranch.Mobile = mtxtBrMobileNo.Text.Trim();
                    dmnBranch.Fax = mtxtBrFax.Text.Trim();
                    dmnBranch.Email = mtxtBrEmail.Text.Trim();
                    dmnBranch.Website = mtxtBrWebsite.Text.Trim();
                    dmnBranch.CompanyBankId = Convert.ToInt64(mcmbBranchBank.SelectedValue);
                    dmnBranch.PAN = mtxtBrPAN.Text.Trim().ToUpper();
                    dmnBranch.ContactName = mtxtBrContactName.Text.Trim();
                    dmnBranch.ContactNo = mtxtBrContactNo.Text.Trim();
                    dmnBranch.GSTNo = mtxtBrGSTNo.Text.Trim();
                    dmnBranch.TINNo = mtxtBrTIN.Text.Trim();
                    dmnBranch.SalesPrefix = mtxtBrSalesPrefix.Text.Trim();
                    dmnBranch.SalesSrlNo = Convert.ToInt64(mtxtBrSalesSrlNo.Text.Trim());
                    dmnBranch.PurchaseSrlNo = Convert.ToInt64(mtxtBrPurchaseSrlNo.Text.Trim());
                    dmnBranch.PurchaseReturnSrlNo = Convert.ToInt64(mtxtBrPurchaseRtnSrlNo.Text.Trim());
                    dmnBranch.SalesReturnSrlNo = Convert.ToInt64(mtxtBrSalesRtnSrlNo.Text.Trim());
                    dmnBranch.InwardPrefix = mtxtBrInwardPrefix.Text.Trim();
                    dmnBranch.OutwardPrefix = mtxtBrOutwardPrefix.Text.Trim();
                    dmnBranch.OutwardSrlNo = Convert.ToInt64(mtxtBrOutwardSrlNo.Text.Trim());
                    dmnBranch.InwardSrlNo = Convert.ToInt64(mtxtBrInwardSrlNo.Text.Trim());
                    brLogoName = mtxtBranchCode.Text.Trim() + ".jpeg";
                    dmnBranch.Logo = brLogoName;
                    if (pbBranchLogo.Image == null)
                    {
                        brLogoExist = clsDocument.FileExistOrNot(brLogoName, DocumentType.BranchLogo);
                        if (brLogoExist)
                            clsDocument.DeleteImage(brLogoName, DocumentType.BranchLogo);
                        dmnBranch.Logo = "";
                        brLogoName = "";

                    }
                    else
                    {
                        bmbrLogo = new Bitmap(pbBranchLogo.Image);
                        dmnBranch.Logo = brLogoName;
                    }
                    if (BrId == 0)//insertion
                    {
                        dmnBranch.CreatedBy = UserId;
                        bool insertStatus = objBll.AddData("prdInsertBranch", dmnBranch);
                        if (insertStatus == true)
                        {
                            if (pbBranchLogo.Image != null)
                                clsDocument.UploadImage(bmbrLogo, brLogoName, DocumentType.BranchLogo);
                            MetroMessageBox.Show(this, "Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                    else
                    {
                        dmnBranch.ModifiedBy = UserId;
                        dmnBranch.ModifiedDateTime = DateTime.Now;
                        dmnBranch.BranchId = BrId;
                        bool updateResult = objBll.UpdateData("prdUpdateBranch", dmnBranch);

                        if (updateResult == true)
                        {
                            if (pbBranchLogo.Image != null)
                                clsDocument.UploadImage(bmbrLogo, brLogoName, DocumentType.BranchLogo);
                            MetroMessageBox.Show(this, "Updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "Update failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        mbtnDeleteBranch.Enabled = false;

                    }
                   
                    RefreshBranchForm();

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(mtxtCompanyName.Text))
                {
                    mbtnSave.Enabled = false;
                }
                RefreshCompanyForm();
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
                if(!string.IsNullOrWhiteSpace(mtxtBranchName.Text))
                {
                    mbtnBranchSave.Enabled = false;
                }
                RefreshBranchForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void MbtnListBranch_Click(object sender, EventArgs e)
        {
            try
            {
                #region pop up form 

                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Branch,Code,Address,Contact Name,Contact No,Email";
                _objSearch.Select = " BranchId as Id,BranchName as Field1,BranchCode as Field2,BranchAddress as Field3," +
                    "ContactName as Field4,ContactNo as Field5,Email as Field6 ";
                _objSearch.Table = " tblBranch ";
                if (FrmDashboard.UserStatus == -1)//super admin
                {
                    _objSearch.WhereNdGroupBy = " where Status=0 ";
                }
                else
                {
                    _objSearch.WhereNdGroupBy = " where CompanyId=" + _CompanyId + " And Status=0 ";
                }
                _objSearch.OrderBy = "order by BranchName ASC ";        
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width-45, this.Height / 2, this.Location.Y+60, this.Location.X+100))
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
                    BrId = Convert.ToInt64(returnValue);
                    DmnBranch objBranch = new DmnBranch();
                    BllSearch<DmnBranch> bllSearch = new BllSearch<DmnBranch>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "CompanyId,BranchCode,BranchName,BranchAddress,PostalCode,Telephone,Mobile,Fax,Email," +
                        "Website,Logo,CompanyBankId,PAN,ContactName,ContactNo,GSTNo,TINNo,SalesPrefix,SalesSrlNo,PurchaseSrlNo,PurchaseReturnSrlNo," +
                        "SalesReturnSrlNo,InwardPrefix,InwardSrlNo,OutwardPrefix,OutwardSrlNo ";
                    objSearch.Table = " tblBranch ";
                    objSearch.WhereNdGroupBy = " Where BranchId =" + BrId;

                    var lstResult = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
                    mcmbCompanyName.SelectedValue = lstResult[0].CompanyId;
                    mtxtBranchName.Text = lstResult[0].BranchName;
                    mcmbBranchBank.SelectedValue = lstResult[0].CompanyBankId;
                    mtxtBranchCode.Text = lstResult[0].BranchCode;
                    mtxtBrAddress.Text = lstResult[0].BranchAddress;
                    mtxtBrPostalCode.Text = lstResult[0].PostalCode;
                    mtxtBrTelephoneNo.Text = lstResult[0].Telephone;
                    mtxtBrMobileNo.Text = lstResult[0].Mobile;
                    mtxtBrFax.Text = lstResult[0].Fax;
                    mtxtBrEmail.Text = lstResult[0].Email;
                    mtxtBrWebsite.Text = lstResult[0].Website;
                    mcmbBranchBank.SelectedValue = lstResult[0].CompanyBankId;
                    mtxtBrPAN.Text = lstResult[0].PAN;
                    mtxtBrContactName.Text = lstResult[0].ContactName;
                    mtxtBrContactNo.Text = lstResult[0].ContactNo;
                    mtxtBrGSTNo.Text = lstResult[0].GSTNo;
                    mtxtBrTIN.Text = lstResult[0].TINNo;
                    mtxtBrSalesPrefix.Text = lstResult[0].SalesPrefix;
                    mtxtBrSalesSrlNo.Text = Convert.ToString(lstResult[0].SalesSrlNo);
                    mtxtBrPurchaseSrlNo.Text = Convert.ToString(lstResult[0].SalesSrlNo);
                    mtxtBrPurchaseRtnSrlNo.Text = Convert.ToString(lstResult[0].PurchaseReturnSrlNo);
                    mtxtBrSalesRtnSrlNo.Text = Convert.ToString(lstResult[0].SalesReturnSrlNo);
                    mtxtBrInwardPrefix.Text = lstResult[0].InwardPrefix;
                    mtxtBrInwardSrlNo.Text = Convert.ToString(lstResult[0].InwardSrlNo);
                    mtxtBrOutwardPrefix.Text = lstResult[0].OutwardPrefix;
                    mtxtBrOutwardSrlNo.Text = Convert.ToString(lstResult[0].OutwardSrlNo);
                    brLogoName = lstResult[0].Logo;
                    if (brLogoName != "")
                    {
                        brLogoExist = clsDocument.FileExistOrNot(brLogoName, DocumentType.BranchLogo);
                        if (brLogoExist)
                            pbBranchLogo.Image = clsDocument.DownloadImage(brLogoName, DocumentType.BranchLogo);
                    }
                    hlTitle.Text = "Company And Branch [Edit]";
                    mbtnDeleteBranch.Enabled = true;
                    mcmbCompanyName.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void MtxtPostalCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }
        private void MtxtTelephoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.TelephoneTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtMobileNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.TelephoneTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        //private void MtxtBrEmail_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        objGeneral.CheckEmailId(mtxtBrEmail.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
        //    }
        //}
        private void MtxtBrPostalCode_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }
        private void MtxtBrMobileNo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.TelephoneTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtBrTelephoneNo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.TelephoneTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnListCompany_Click(object sender, EventArgs e)
        {
            #region for grid double click
            try
            {
                #region pop up form 
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Company,Code,Address,Contact Name,Contact No,Email";
                _objSearch.Select = " CompanyId as Id,CompanyName as Field1,CompanyCode as Field2,CompanyAddress as Field3," +
                    "ContactName as Field4,ContactNo as Field5,Email as Field6 ";
                _objSearch.Table = " tblCompany ";
                _objSearch.WhereNdGroupBy = "where  Status=0 ";
                _objSearch.OrderBy = "order by CompanyName ASC ";
                // using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 800, 400, 150, 100))//850, 550, 50, 240
                // using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, Screen.PrimaryScreen.WorkingArea.X, Screen.PrimaryScreen.WorkingArea.Y))//850, 550, 50, 240
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter,  this.Width - 45, this.Height / 2, this.Location.Y + 60, this.Location.X+100))//850, 550, 50, 240
                //var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 45, this.Height / 2, this.Location.Y + 60, this.Location.X))
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
                    CompanyId = Convert.ToInt64(returnValue);
                    DmnCompany objCompany = new DmnCompany();
                    BllSearch<DmnCompany> bllSearch = new BllSearch<DmnCompany>();
                    DmnSearch objSearch = new DmnSearch();
                    // objSearch.ColumnHeaders = "DepartmentName";
                    objSearch.Select = "CompanyCode,CompanyName,CompanyAddress,PostalCode,Telephone,Mobile,Fax,Email," +
                        "Website,CompanyBankId,PAN,ContactName,ContactNo,Logo,GSTNo,TINNo,STaxNo";
                    objSearch.Table = " tblCompany ";
                    objSearch.WhereNdGroupBy = " Where CompanyId =" + CompanyId;

                    var lstResult = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id

                    mtxtCompanyName.Text = lstResult[0].CompanyName;
                    mtxtCompanyCode.Text = lstResult[0].CompanyCode;
                    mtxtCompAddress.Text = lstResult[0].CompanyAddress;
                    mtxtPostalCode.Text = lstResult[0].PostalCode;
                    mtxtTelephoneNo.Text = lstResult[0].Telephone;
                    mtxtMobileNo.Text = lstResult[0].Mobile;
                    mcmbCompanyBank.SelectedValue = lstResult[0].CompanyBankId;
                    mtxtFax.Text = lstResult[0].Fax;
                    mtxtEmail.Text = lstResult[0].Email;
                    mtxtWebsite.Text = lstResult[0].Website;
                    mtxtPAN.Text = lstResult[0].PAN;
                    mtxtContactName.Text = lstResult[0].ContactName;
                    mtxtContactNo.Text = lstResult[0].ContactNo;
                    mtxtGSTNo.Text = lstResult[0].GSTNo;
                    mtxtTINNo.Text = lstResult[0].TINNo;
                    mtxtSalesTaxNo.Text = lstResult[0].STaxNo;
                    imageName = lstResult[0].Logo;
                    if (imageName!= "")
                    {
                        imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.CompanyLogo);
                        if (imageExist)
                            pbLogo.Image = clsDocument.DownloadImage(imageName, DocumentType.CompanyLogo);
                    }
                    hlTitle.Text = "Company And Branch [Edit]";
                    mbtnDeleteCompany.Enabled = true;
                    mtxtCompanyName.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

        }
        private void MbtnDeleteBranch_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult result = MetroMessageBox.Show(this, "\nDo you want to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                if (result == DialogResult.Yes)
                {
                    DmnBranch dmnBranch = new DmnBranch();
                    BllBranch bllBranch = new BllBranch();
                    dmnBranch.BranchId = BrId;
                    dmnBranch.ModifiedBy = UserId;
                    bool deleteStatus = bllBranch.DeleteById("prdDeleteBranch", dmnBranch);

                    if (deleteStatus)
                    {
                        brLogoExist = clsDocument.FileExistOrNot(brLogoName, DocumentType.BranchLogo);
                        if (pbBranchLogo.Image != null)
                            clsDocument.DeleteImage(brLogoName, DocumentType.BranchLogo);
                        MetroMessageBox.Show(this, "\nSuccessfully deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information, 150);
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "\nError in deletion", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }

                }

                RefreshBranchForm(); //need to add refresh form code here

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnDeleteCompany_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MetroMessageBox.Show(this, "\nDo you want to delete?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                if (result == DialogResult.Yes)
                {
                    DmnCompany dmnCompany = new DmnCompany();
                    BllCompany bllCompany = new BllCompany();
                    dmnCompany.CompanyId = CompanyId;
                    bool deleteStatus = bllCompany.DeleteById("prdDeleteCompany", dmnCompany);
                    if (deleteStatus)
                    {
                        imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.CompanyLogo);
                        if (imageExist)
                            clsDocument.DeleteImage(imageName, DocumentType.CompanyLogo);
                        MetroMessageBox.Show(this, "\nSuccessfully deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information, 150);
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "\nError in deletion", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }

                }
                RefreshCompanyForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtContactNo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.TelephoneTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtBrContactNo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.TelephoneTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmCompanyNBranch_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
               
                if(e.KeyCode==Keys.Escape)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void PbAddImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFd = new OpenFileDialog();
                openFd.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dr = openFd.ShowDialog();
                if (openFd.FileName != "")
                {
                    pbLogo.Image = Image.FromFile(openFd.FileName);
                    //deletedStatus = false;//setting this to false to save to db 
                }
                mbtnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void PbRemoveImage_Click(object sender, EventArgs e)
        {
            try
            {
                pbLogo.Image = null;
                //deletedStatus = true;//this is to remove existing file from folder 
                mbtnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
      

        private void MtxtBrSalesSrlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MtxtBrPurchaseSrlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MtxtBrPurchaseRtnSrlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MtxtBrSalesRtnSrlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MtxtBrInwardSrlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }
        private void MtxtBrOutwardSrlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }
        private void MetroTabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (metroTabControl1.SelectedIndex == 0)
            {
                 if(!string.IsNullOrWhiteSpace(mtxtCompanyName.Text))
                {
                    if (e.KeyCode != Keys.F2 && e.KeyCode != Keys.F5 && e.KeyCode!=Keys.F3 && e.KeyCode!=Keys.F7)
                    {
                        mbtnSave.Enabled = true;
                    }
                }
                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled==true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnListCompany_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.F7)
                    MbtnDeleteCompany_Click(sender, e);
                if (e.KeyCode == Keys.F10)
                    PbAddImage_Click(sender, e);
                if (e.KeyCode == Keys.F11)
                    PbRemoveImage_Click(sender, e);
            }
            if (metroTabControl1.SelectedIndex == 1)
            {
                if(!string.IsNullOrWhiteSpace(mtxtBranchName.Text))
                {
                    if(e.KeyCode!=Keys.F2 && e.KeyCode!=Keys.F5 && e.KeyCode!=Keys.F3 && e.KeyCode!=Keys.F7)
                    {
                        mbtnBranchSave.Enabled = true;
                    }
                }
                if (e.KeyCode == Keys.F2 && mbtnBranchSave.Enabled==true)
                    MbtnBranchSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnListBranch_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnBranchRefresh_Click(sender, e);
                if (e.KeyCode == Keys.F7)
                    MbtnDeleteBranch_Click(sender, e);
                if(e.KeyCode == Keys.F10)
                    pbAddBranchImage_Click(sender, e);
                if (e.KeyCode == Keys.F11)
                    pbRemoveBranchImage_Click(sender, e);
            }
        }

        private void mtxtTINNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtxtBrTIN_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtxtCompanyName_Leave(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtCompanyName.Text = textInfo.ToTitleCase(mtxtCompanyName.Text);
                mtxtCompanyName.Select(mtxtCompanyName.Text.Length, 1);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtxtContactName_Leave(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtContactName.Text = textInfo.ToTitleCase(mtxtContactName.Text);
                mtxtContactName.Select(mtxtContactName.Text.Length, 1);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pbAddBranchImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFd = new OpenFileDialog();
                openFd.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dr = openFd.ShowDialog();
                if (openFd.FileName != "")
                {
                    pbBranchLogo.Image = Image.FromFile(openFd.FileName);
                    //deletedStatus = false;//setting this to false to save to db 
                }
                mbtnBranchSave.Enabled = true;
                
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void pbRemoveBranchImage_Click(object sender, EventArgs e)
        {
            try
            {
                pbBranchLogo.Image = null;
                //deletedStatus = true;//this is to remove existing file from folder 
                mbtnBranchSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtBranchName_Leave(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtBranchName.Text = textInfo.ToTitleCase(mtxtBranchName.Text);
                mtxtBranchName.Select(mtxtBranchName.Text.Length, 1);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtxtBrContactName_Leave(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtBrContactName.Text = textInfo.ToTitleCase(mtxtBrContactName.Text);
                mtxtBrContactName.Select(mtxtBrContactName.Text.Length, 1);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


    }
}

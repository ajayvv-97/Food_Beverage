using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
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
    public partial class FrmCustomerPersonal : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = FrmDashboard.BranchId;
        long CustomerId = 0;
        Bitmap bitmap;
        ClsDocument clsDocument = new ClsDocument();
        bool imageExist;
        #endregion

        #region Functions
        public FrmCustomerPersonal()
        {
            InitializeComponent();
        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtFirstName, mtxtMobile,mcmbMaritalStatus };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            else if (IsCustomerExist() == false)
            {
                MetroMessageBox.Show(this, "Customer already exist", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }
            else if (objGeneral.CheckEmailId(this, mtxtEmail.Text) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsCustomerExist()
        {
            DmnCustomerPersonal dmnCustomerPersonal = new DmnCustomerPersonal();
            BllSearch<DmnCustomerPersonal> bllSearch = new BllSearch<DmnCustomerPersonal>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " FirstName";
            dmnSearch.Table = " tblCustomerPersonal";
            if (CustomerId == 0)
                dmnSearch.WhereNdGroupBy = "Where FirstName='" + mtxtFirstName.Text + "' and Status=0";
            else
                dmnSearch.WhereNdGroupBy = "Where FirstName='" + mtxtFirstName.Text + "' and Status=0 and CustomerId!='" + CustomerId + "'";

            var customer = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (customer.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void FillCombo()
        {
            //Marital Status
            BllSearch<DmnComboBox> bllSearchMarital = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchMarital = new DmnSearch();
            dmnSearchMarital.Select = "GeneralId,Caption";
            dmnSearchMarital.Table = "tblGeneralOptions g " +
                "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchMarital.WhereNdGroupBy = "Where gt.GType='MARITAL STATUS' AND g.Status = 0 ";
            var maritalSource = bllSearchMarital.GetSearchData("prdComboFill", dmnSearchMarital);
            mcmbMaritalStatus.DataSource = maritalSource;
            mcmbMaritalStatus.DisplayMember = "Value";
            mcmbMaritalStatus.ValueMember = "Id";
        }
        public void RefreshForm()
        {
            hlTitle.Text = "Customer ";
            FillCombo();
            mtxtFirstName.Focus();
            ActiveControl = mtxtFirstName;
            CustomerId = 0;
            objGeneral.ResetAllTextBoxes(this);
            pbCustomer.Image = null;
            if (bitmap != null)
                bitmap.Dispose();
            GenerateCode();
            mbtnDelete.Enabled = false;
        }
        public void GenerateCode()
        {
            BllSearch<DmnBranch> bllSearch = new BllSearch<DmnBranch>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "BranchId,BranchCode";
            objSearch.Table = "tblBranch";
            objSearch.WhereNdGroupBy = "Where BranchId = " + BranchId;
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

            BllSearch<DmnCustomerPersonal> bllSearchCustId = new BllSearch<DmnCustomerPersonal>();
            DmnSearch dmnSearchCustId = new DmnSearch();
            dmnSearchCustId.Select = " top(1) CustomerId ";
            dmnSearchCustId.Table = "tblCustomerPersonal";
            dmnSearchCustId.OrderBy = " Order By CustomerId DESC";
            var gridSourCustID = bllSearchCustId.GetSearchData("prdSearch", dmnSearchCustId);
            mtxtCustomerCode.Text = gridSource[0].BranchCode + (gridSourCustID[0].CustomerId+1);
            
        }
        #endregion

        #region Events
        private void FrmCustomerPersonal_Load(object sender, EventArgs e)
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
        private void FrmCustomerPersonal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtMobile_KeyPress(object sender, KeyPressEventArgs e)
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
        private void MpbAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    //empImgExt = Path.GetExtension(openFileDialog.FileName);
                    pbCustomer.Image = Image.FromFile(openFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MpbRemove_Click(object sender, EventArgs e)
        {
            try
            {
                pbCustomer.Image = null;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(IsSavable())
                {
                    BllCustomerPersonal bllCustomerPersonal = new BllCustomerPersonal();
                    DmnCustomerPersonal dmnCustomerPersonal = new DmnCustomerPersonal();
                    dmnCustomerPersonal.FirstName = mtxtFirstName.Text;
                    dmnCustomerPersonal.LastName = mtxtLastName.Text;
                    dmnCustomerPersonal.ICardNo = mtxtICardNo.Text;
                    dmnCustomerPersonal.DOB = Convert.ToDateTime(mdtDob.Text);
                    if (mrbtnMale.Checked == true)
                        dmnCustomerPersonal.Gender = "Male";
                    else
                        dmnCustomerPersonal.Gender = "Female";
                    dmnCustomerPersonal.MaritalStatus = mcmbMaritalStatus.Text;
                    dmnCustomerPersonal.CustomerAddress = mtxtAddress.Text;
                    dmnCustomerPersonal.Telephone = mtxtTelephone.Text;
                    dmnCustomerPersonal.Mobile = mtxtMobile.Text;
                    dmnCustomerPersonal.EmailId = mtxtEmail.Text;
                    dmnCustomerPersonal.BranchId = BranchId;
                    dmnCustomerPersonal.CustomerCode = mtxtCustomerCode.Text;
                    if (pbCustomer.Image != null)
                    {
                        dmnCustomerPersonal.Photo = mtxtCustomerCode.Text + ".jpeg";
                        bitmap = new Bitmap(pbCustomer.Image);
                    }
                    else
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCustomerCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                        if (imageExist)
                            clsDocument.DeleteImage(mtxtCustomerCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                        dmnCustomerPersonal.Photo = "";
                    }
                    if (CustomerId == 0)
                    {
                        dmnCustomerPersonal.CreatedBy = FrmDashboard.UserId;
                        var saveStatus = bllCustomerPersonal.AddData("prdInsertCustomer", dmnCustomerPersonal);
                        if (!string.IsNullOrWhiteSpace(saveStatus.CustomerCode))
                        {
                            if (pbCustomer.Image != null)
                                clsDocument.UploadImage(bitmap, saveStatus.CustomerCode + ".jpeg", DocumentType.CustomerPhoto);
                            MetroMessageBox.Show(this, "Saved successfully " + mtxtFirstName.Text, "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    else
                    {
                        dmnCustomerPersonal.CustomerId = CustomerId;
                        dmnCustomerPersonal.ModifiedBy = FrmDashboard.UserId;
                        bool saveStatus = bllCustomerPersonal.UpdateData("prdUpdateCustomer", dmnCustomerPersonal);
                        if (saveStatus)
                        {
                            if (pbCustomer.Image != null)
                                clsDocument.UploadImage(bitmap, mtxtCustomerCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                            MetroMessageBox.Show(this, "Updated successfully " + mtxtFirstName.Text, "Upadted  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    RefreshForm();
                }

            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Customer Code,Customer Name,Mobile";
                _objSearch.Select = "CustomerId as Id,CustomerCode as Field1, FirstName as Field2,Mobile as Field3";
                _objSearch.Table = "tblCustomerPersonal ";
                _objSearch.WhereNdGroupBy = "Where Status=0";
                _objSearch.OrderBy = " Order By FirstName ASC ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter,700, 300, 200, 290))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != "")
                {
                    CustomerId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnCustomerPersonal> bllSearch = new BllSearch<DmnCustomerPersonal>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "CustomerId,CustomerCode,FirstName,LastName,ICardNo,DOB,Gender,MaritalStatus,CustomerAddress,Telephone," +
                        "Mobile,EmailId,Photo";
                    objSearch.Table = " tblCustomerPersonal ";
                    objSearch.WhereNdGroupBy = "Where CustomerId = " + CustomerId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mtxtCustomerCode.Text = gridSource[0].CustomerCode;
                    mtxtFirstName.Text = gridSource[0].FirstName;
                    mtxtLastName.Text = gridSource[0].LastName;
                    mtxtICardNo.Text = gridSource[0].ICardNo;
                    //a adminmdtDob.Text = Convert.ToString(gridSource[0].DOB);
                    if (gridSource[0].Gender == "Female")
                        mrbtnFemale.Checked = true;
                    else
                        mrbtnMale.Checked = true;
                    mcmbMaritalStatus.Text = gridSource[0].MaritalStatus;
                    mtxtAddress.Text = gridSource[0].CustomerAddress;
                    mtxtTelephone.Text = gridSource[0].Telephone;
                    mtxtMobile.Text = gridSource[0].Mobile;
                    mtxtEmail.Text = gridSource[0].EmailId;
                    if (gridSource[0].Photo != "")
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCustomerCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                        if (imageExist)
                            pbCustomer.Image = clsDocument.DownloadImage(mtxtCustomerCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                    }  
                        
                    hlTitle.Text = "Customer [Edit]";
                    mbtnDelete.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnRefresh_Click(object sender, EventArgs e)
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
        private void MbtnDelete_Click(object sender, EventArgs e)
        {
            bool deleteStatus;
            try
            {
                DialogResult dialogResult = MetroMessageBox.Show(this, "Are you sure you want to delete " + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    BllCustomerPersonal bllCustomerPersonal = new BllCustomerPersonal();
                    DmnCustomerPersonal dmnCustomerPersonal = new DmnCustomerPersonal();
                    dmnCustomerPersonal.CustomerId = CustomerId;
                    dmnCustomerPersonal.ModifiedBy = FrmDashboard.UserId;
                    deleteStatus = bllCustomerPersonal.DeleteById("prdDeleteCustomer", dmnCustomerPersonal);
                    if (deleteStatus)
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCustomerCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                        if (imageExist)
                            clsDocument.DeleteImage(mtxtCustomerCode.Text+".jpeg",DocumentType.CustomerPhoto);
                        MetroMessageBox.Show(this, "Deleted successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    RefreshForm();
                }
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        #endregion 
    }
}

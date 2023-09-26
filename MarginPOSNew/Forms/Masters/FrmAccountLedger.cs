using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Forms;
using ReaderSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using AForge.Video;
using AForge.Video.DirectShow;

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmAccountLedger : MetroForm
    {
        #region Declaration
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;

        public string _retValue { get; set; }
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt32(Forms.UserAccounts.FrmLogin.BranchId);
        long UserId = Convert.ToInt32(Forms.UserAccounts.FrmLogin.UserId);
        long LedgerId = 0;
        ClsDocument clsDocument = new ClsDocument();
        Bitmap bitmap;
        bool imageExist;
        long AccountGroupId = 0;
        string LedgerCode = string.Empty;
        int srlnocrnt = 0;
        public long frm_OpnePara1 = 0;
        #region RFID

        //private ACR122 reader;//for reader
        //int rcount = 0;
        //string first_16;
        //string second_16;
        //string third_16;
        //string read_encodedno;// to hold the encoded number completely
        //string completeCardData;//complete data in the card

        #endregion
        #endregion
        #region Functions
        public bool IsICardValid(string CardNo)
        {
            tmrRFID.Enabled = false;

            if (CardNo.Length == 28)//RESTRICTING NO TO 28 
            {
                string RcvdCardSeries = CardNo.Substring(0, 12);
                string CardType = CardNo.Substring(8, 4);

                if (CardType.StartsWith("C"))
                {

                    FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.ColumnHeaders = "Id";
                    objSearch.Select = "  count(*) AS Id ";
                    objSearch.Table = "  tblGeneralOptions G Left join tblGType GT on G.GTypeId=GT.GTypeId  ";
                    objSearch.WhereNdGroupBy = " where GT.GType='ICARDSERIES' and Caption like 'CUSTOMER%' AND  G.GTypeSubText1 in('" + RcvdCardSeries + "') AND G.Status=0";
                    var CardCount = frmGridSearch.GetSearchGrid(objSearch);

                    if (Convert.ToInt64(CardCount["Id", 0].Value) > 0)
                    {
                        tmrRFID.Enabled = true;
                        return true;
                    }
                    MetroMessageBox.Show(this, " This card is not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    tmrRFID.Enabled = true;
                    return false;

                }
                else
                {
                    MetroMessageBox.Show(this, "Not a customer card", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    tmrRFID.Enabled = true;
                    return false;
                }
            }

            MetroMessageBox.Show(this, "Invalid card series", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            tmrRFID.Enabled = true;
            return false;


        }
     
        public FrmAccountLedger()
        {
            InitializeComponent();
        }
        public FrmAccountLedger(long userId, long branchId,long frm_OpnePara1)
        {
            frm_OpnePara1 = frm_OpnePara1;
            InitializeComponent();
            UserId = userId;
            BranchId = BranchId;
            
        }
        public void FillAccountGroupComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "AccountGroupId,AccountGroupName";
            objSearch.Table = " tblAccountGroup ";
            objSearch.WhereNdGroupBy = "Where (Status = 0 or Status = 2)  " ;
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbAccountGroup.DataSource = gridSource;
            mcmbAccountGroup.ValueMember = "Id";
            mcmbAccountGroup.DisplayMember = "Value";
        }
        public void FillBankNameComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "g.GeneralId,g.Caption";
            objSearch.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearch.WhereNdGroupBy = " Where gt.GType='BANK' AND g.Status = 0";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbBankName.DataSource = gridSource;
            mcmbBankName.ValueMember = "Id";
            mcmbBankName.DisplayMember = "Value";
        }
        public void FillStateComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "g.GeneralId,g.Caption";
            objSearch.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearch.WhereNdGroupBy = " Where gt.GType='STATE' AND g.Status = 0";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbState.DataSource = gridSource;
            mcmbState.ValueMember = "Id";
            mcmbState.DisplayMember = "Value";


        }
        public void FillGender()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "g.GeneralId,g.Caption";
            objSearch.Table = " tblGeneralOptions g";
            objSearch.WhereNdGroupBy = " Where g.GTypeid=25 AND g.Status = 0";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbGender.DataSource = gridSource;
            mcmbGender.ValueMember = "Id";
            mcmbGender.DisplayMember = "Value";


        }
        public void FillList()
        {
            lbTransportation.Items.Clear();
            BllGeneralOptions bllGeneralOptions = new BllGeneralOptions();
            BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " GTypeSubText2 ";
            dmnSearch.Table = " tblGeneralOptions ";
            dmnSearch.WhereNdGroupBy = " Where GTypeSubText1 ='" + LedgerCode + "'";
            var lstdata = bllSearch.GetSearchData("prdSearch", dmnSearch);
            foreach (var item in lstdata)
            {
                lbTransportation.Items.Add(item.GTypeSubText2);
            }
        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtName, mcmbAccountGroup, mtxtMobile };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(mtxtEmailId.Text) && !objGeneral.CheckEmailId(this, mtxtEmailId.Text))
            {
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(mtxtPanNo.Text) && !objGeneral.CheckPANNo(this, mtxtPanNo.Text))
            {
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(mtxtGstNo.Text) && !objGeneral.CheckGSTNo(this, mtxtGstNo.Text))
            {
                return false;
            }
            else if (IsLedgerExist() == false)
            {
                MetroMessageBox.Show(this, "Ledger already exist", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool IsLedgerExist()
        {
            BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
            DmnAccountLedger dmnAccountLedger = new DmnAccountLedger();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " LedgerName";
            dmnSearch.Table = " tblAccountLedger";
            if (LedgerId == 0)
                dmnSearch.WhereNdGroupBy = "Where LedgerName='" + mtxtName.Text + "' and isnull(Status,0)=0 and BranchId="+BranchId ;
            else
                dmnSearch.WhereNdGroupBy = "Where LedgerName='" + mtxtName.Text + "' and isnull(Status,0)=0 and LedgerId!='" + LedgerId + "' and BranchId=" + BranchId;

            var ledger = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (ledger.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void RefreshForm()
        {
            LedgerId = 0;
            LedgerCode = string.Empty;
            AccountGroupId = 0;
            mtxtAge.Text = "";
            mtxtDOB.Text = "";

            FillAccountGroupComboBox();
            FillBankNameComboBox();
            FillStateComboBox();
            FillGender();
            mtxtTranspotation.Clear();
            mtxtName.Focus();
            mcmbState.Text = "KERALA";

            mbtnDelete.Enabled = false;
            mtxtCreditDays.Text = Convert.ToString("30");
            mcmbState.Text = "KERALA";
            mcmbGender.SelectedIndex = 0;
            
            mdtpDOB.Format = DateTimePickerFormat.Custom;
            mdtpDOB.CustomFormat = " "; //a string with one whitespace
            // mdtpValidTo
            if (UserAccounts.FrmLogin.UserAccountTypeId == 5)
            {
                mcmbCardType.SelectedIndex = 1;
                mcmbCardType.Enabled = false;
                groupBox1.Visible = false;
                pnlCardValid.Visible = true;
                mcmbAccountGroup.SelectedIndex = 20;
            }
            else
            {
                mcmbCardType.Enabled =true;
                groupBox1.Visible =true;
                pnlCardValid.Visible = false;
            }

            pbCustomer.Image = null;
            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }
            lbTransportation.Items.Clear();
            if (string.IsNullOrWhiteSpace(mtxtName.Text))
            {
                mbtnSave.Enabled = false;
            }
            hlblHeader.Text = "Ledger ";

            MetroTextBox[] listTbx = { mtxtName,mtxtMobile,mtxtAccountNo,mtxtAddress,mtxtBranchName,mtxtCode,mtxtCreditLimit,mtxtCreditDays,mtxtDiscountPer,mtxtEmailId,mtxtGstNo,
            mtxtIFSC,mtxtInterestPerDay,mtxtMobile,mtxtName,mtxtPanNo,mtxtRegNo,mtxtRemarks,mtxtTelephoneNo,mtxtTinNo,mtxtICardNo};
            objGeneral.ResetSelectedMetroTextBoxes(listTbx);

        }
        public Boolean NameExist()
        {
            if (Convert.ToString(mtxtName.Text) != "")
            {
                BllAccountGroup bllAccountGroup = new BllAccountGroup();
                BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "Id,LedgerName";
                objSearch.Select = " COUNT(LedgerId) As Count";
                objSearch.Table = " tblAccountLedger ";

                if (LedgerId == 0)
                {
                    objSearch.WhereNdGroupBy = " Where LedgerId= '" + Convert.ToInt32(mtxtName.Text) + "' AND LedgerName = '" + Convert.ToString(mtxtName.Text) + "' AND isnull(Status,0)=0 AND BranchId = " + BranchId;
                }
                else
                {
                    objSearch.WhereNdGroupBy = " Where LedgerId= '" + Convert.ToInt32(mtxtName.Text) + "' AND LedgerId!= " + LedgerId + " AND isnull(Status,0)=0 AND LedgerName = '" + Convert.ToString(mtxtName.Text) + "' AND BranchId = " + BranchId;
                }
                //objSearch.OrderBy = "Order By DesignationName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource[0].Count >= 1)
                {
                    MetroMessageBox.Show(this, "", "Ledger Name " + Convert.ToString(mtxtName.Text) + " Already Exist !!!", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    mtxtName.Text = "";
                    mtxtName.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        public void SelectCode()
        {
            int sel = Convert.ToInt32(mcmbAccountGroup.SelectedIndex);
            if (sel > 0)
            {
                BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "AccountGroupCode,SrlNoCurrent,SrlNoStart";
                 objSearch.Table = " tblAccountGroup ";
                objSearch.WhereNdGroupBy = "Where AccountGroupId = " + mcmbAccountGroup.SelectedValue;
                objSearch.OrderBy = "Order By AccountGroupName ";

                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                srlnocrnt = Convert.ToInt32(gridSource[0].SrlNoCurrent);

                if (srlnocrnt == 0)
                {
                    mtxtCode.Text = Convert.ToString(gridSource[0].AccountGroupCode) + Convert.ToString(gridSource[0].SrlNoStart);
                }
                else
                {
                    mtxtCode.Text = Convert.ToString(gridSource[0].AccountGroupCode) + Convert.ToString(gridSource[0].SrlNoCurrent + 1);

                }


            }
                
        }
        public void SelectCodeloyalty()
        {
            int sel = Convert.ToInt32(mcmbCardType.SelectedIndex);
            if (sel > 0)
            {
                BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "MAX(STUFF(LedgerCode, 1, PATINDEX('%[0-9]%', LedgerCode) - 1, '')) AS LedgerCode";
                objSearch.Table = " tblAccountLedger ";
                objSearch.WhereNdGroupBy = "Where cardtype = 143";
                //objSearch.OrderBy = "";

                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                // mtxtCode.Text = Convert.ToString(gridSource[0].AccountGroupCode) + Convert.ToString(gridSource[0].SrlNoCurrent + 1);
                long lc = Convert.ToInt64(gridSource[0].LedgerCode)+1;
                
                mtxtCode.Text = Convert.ToString(lc);
                
            }
            else
            {
               
            }

        }

        #endregion
        #region Events
        private void FrmLedger_Load(object sender, EventArgs e)
        {
            try
            {
                FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo filterInfo in FilterInfoCollection)
                    cmbImage.Items.Add(filterInfo.Name);
                cmbImage.SelectedIndex = 0;
                VideoCaptureDevice = new VideoCaptureDevice();
            }
            catch (Exception ex)
            {

            }

            try
            {
                mbtnUpdate2.Visible = false;
                if (UserAccounts.FrmLogin.UserAccountTypeId == 1)
                    mbtnUpdate2.Visible = true;


                 label6.Visible = false;
                label5.Visible = false;
                comboboxfill();
                RefreshForm();
                mcmbAccountGroup.Enabled = true;
                //if (frm_OpnePara1>0)
                //{
                //    mcmbAccountGroup.SelectedValue = frm_OpnePara1;
                //    mcmbAccountGroup.Enabled = false;
                //}

               
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

                if (IsSavable())
                {
                    DmnAccountLedger dmnLedger = new DmnAccountLedger();
                    DmnGeneralOptions dmnGeneralOptions = new DmnGeneralOptions();
                    BllGeneralOptions bllGeneralOptions = new BllGeneralOptions();
                    BllAccountLedger bllLedger = new BllAccountLedger();

                    dmnLedger.BankAccountNo = Convert.ToString(mtxtAccountNo.Text.Trim());
                    dmnLedger.LedgerAddress = Convert.ToString(mtxtAddress.Text.Trim());
                    dmnLedger.BankBranch = Convert.ToString(mtxtBranchName.Text.Trim());
                    dmnLedger.CreditLimit = decimal.Parse(mtxtCreditLimit.Text.Trim() == string.Empty ? "0.00" : mtxtCreditLimit.Text.Trim());
                    dmnLedger.DiscountPer = decimal.Parse(mtxtDiscountPer.Text.Trim() == string.Empty ? "0.00" : mtxtDiscountPer.Text.Trim());
                    dmnLedger.CreditDays = int.Parse(mtxtCreditDays.Text.Trim() == string.Empty ? "0" : mtxtCreditDays.Text.Trim());
                    dmnLedger.EmailId = Convert.ToString(mtxtEmailId.Text.Trim());
                    dmnLedger.GSTNo = Convert.ToString(mtxtGstNo.Text.Trim());
                    dmnLedger.BankIFSC = Convert.ToString(mtxtIFSC.Text.Trim());
                    dmnLedger.InterestPerDay = decimal.Parse(mtxtInterestPerDay.Text.Trim() == string.Empty ? "0.00" : mtxtInterestPerDay.Text.Trim());
                    dmnLedger.Mobile = Convert.ToString(mtxtMobile.Text.Trim());
                    dmnLedger.LedgerName = Convert.ToString(mtxtName.Text.Trim());
                    dmnLedger.PAN = Convert.ToString(mtxtPanNo.Text.Trim());
                    dmnLedger.CompanyRegNo = Convert.ToString(mtxtRegNo.Text.Trim());
                    dmnLedger.Remarks = Convert.ToString(mtxtRemarks.Text.Trim());
                    dmnLedger.Telephone = Convert.ToString(mtxtTelephoneNo.Text.Trim());
                    dmnLedger.TINNo = Convert.ToString(mtxtTinNo.Text.Trim());
                    dmnLedger.State = Convert.ToInt64(mcmbState.SelectedValue);
                    dmnLedger.AccountGroupId = Convert.ToInt64(mcmbAccountGroup.SelectedValue);
                    dmnLedger.BankId = Convert.ToInt64(mcmbBankName.SelectedValue);
                    dmnLedger.BranchId = Convert.ToInt64(BranchId);

                    dmnLedger.cardtype = Convert.ToInt32(mcmbCardType.SelectedValue);
                    dmnLedger.vfrom = Convert.ToDateTime(DateTime.Now);
                    dmnLedger.vto = Convert.ToDateTime(DateTime.Now.AddMonths(11));
                    dmnLedger.cardstatus = 1;
                    dmnLedger.ICardNo = mtxtICardNo.Text;
                    dmnLedger.age = int.Parse(mtxtAge.Text.Trim() == string.Empty ? "0" : mtxtAge.Text.Trim());
                    if (mtxtDOB.Text!="")
                    {
                        dmnLedger.dob = Convert.ToDateTime(mtxtDOB.Text);
                    }
                    else
                    {
                        dmnLedger.dob = Convert.ToDateTime(new DateTime(1900, 01, 01));
                        
                    }
                    
                    dmnLedger.gender = Convert.ToInt32(mcmbGender.SelectedValue);

                    if (pbCustomer.Image != null)
                    {
                        dmnLedger.Photo = mtxtCode.Text + ".jpeg";
                        bitmap = new Bitmap(pbCustomer.Image);
                    }
                    else
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                        if (imageExist)
                            clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                        dmnLedger.Photo = "";
                    }
                    imageExist = false;
                    clsDocument.UploadImage(bitmap, mtxtCode.Text + ".jpeg", DocumentType.CustomerPhoto);

                    dmnLedger.LedgerCode = Convert.ToString(mtxtCode.Text.Trim());
                    if (LedgerId == 0)
                    {
                        if (string.IsNullOrWhiteSpace(Convert.ToString(mtxtCode.Text.Trim())))
                        {
                            if (Convert.ToInt32(mcmbCardType.SelectedValue) == 143)
                            {
                                SelectCodeloyalty();
                            }
                            else
                            {
                                SelectCode();
                            }

                        }


                        dmnLedger.CreatedBy = UserId;
                        dmnLedger.CreatedDateTime = Convert.ToDateTime(DateTime.Now);
                        var lstData = bllLedger.AddData("prdInsertAccountLedger", dmnLedger);
                        this._retValue = Convert.ToString(lstData.LedgerId);

                        BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch = new DmnSearch();
                        objSearch.Select = "*";
                        objSearch.Table = " tblAccountLedger l";
                        objSearch.WhereNdGroupBy = "Where LedgerId = "  +_retValue;
                        //objSearch.OrderBy = "Order By LedgerName ";
                        var gridSource1 = bllSearch.GetSearchData("prdSearch", objSearch);

                        string lcode = Convert.ToString(gridSource1[0].LedgerCode);
                        if (!string.IsNullOrWhiteSpace(lcode))
                        {
                            #region InsertTransportation
                            dmnGeneralOptions.GTypeSubText1 = lstData.LedgerCode;
                            dmnGeneralOptions.CreatedBy = UserId;
                            foreach (var item in lbTransportation.Items)
                            {

                                dmnGeneralOptions.GTypeSubText2 = item.ToString();
                                bllGeneralOptions.AddTransportation("prdInsertTransportation", dmnGeneralOptions);
                            }
                            #endregion
                            if (pbCustomer.Image != null)
                               clsDocument.UploadImage(bitmap, lstData.LedgerCode + ".jpeg", DocumentType.CustomerPhoto);
                               MetroMessageBox.Show(this, "", mtxtName.Text + " Successfully Saved ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                                MetroMessageBox.Show(this, "", mtxtName.Text + " Save Failed ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }
                    else
                    {
                        
                        #region Delete Transportation
                        dmnGeneralOptions.GTypeSubText1 = LedgerCode;
                        bllGeneralOptions.DeleteTransportation("prdDeleteTransportation", dmnGeneralOptions);
                        #endregion
                        #region InsertTransportation
                        dmnGeneralOptions.GTypeSubText1 = LedgerCode;
                        dmnGeneralOptions.CreatedBy = UserId;
                        foreach (var item in lbTransportation.Items)
                        {

                            dmnGeneralOptions.GTypeSubText2 = item.ToString();
                            bllGeneralOptions.AddTransportation("prdInsertTransportation", dmnGeneralOptions);
                        }
                        #endregion

                        dmnLedger.ModifiedBy = UserId;
                        dmnLedger.ModifiedDateTime = Convert.ToDateTime(DateTime.Now);
                        dmnLedger.LedgerId = LedgerId;
                        if (AccountGroupId != Convert.ToInt32(mcmbAccountGroup.SelectedValue))
                        {
                            //SelectCode();
                            if (Convert.ToInt32(mcmbCardType.SelectedValue) == 143)
                            {
                                SelectCodeloyalty();
                            }
                            else
                            {
                                SelectCode();
                                DmnAccountGroup dmnAccountGroup = new DmnAccountGroup();
                                BllAccountGroup bllAccountGroup = new BllAccountGroup();
                                dmnAccountGroup.AccountGroupId = Convert.ToInt64(mcmbAccountGroup.SelectedValue);
                                var lstDataSrlno = bllAccountGroup.UpdateSrl("prdUpdateAccountGroupSrl", dmnAccountGroup);
                                dmnLedger.LedgerCode = mtxtCode.Text;
                            }
                        }
                            

                        #region Update LedgerCode
                        var lstData = bllLedger.UpdateData("prdUpdateAccountLedger", dmnLedger);
                        bool savestatus = lstData;
                        if (savestatus)
                        {   
                            if (pbCustomer.Image != null)
                            {
                                clsDocument.UploadImage(bitmap, mtxtCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                                 if (mtxtCode.Text != LedgerCode)
                                {
                                    imageExist = clsDocument.FileExistOrNot(LedgerCode + ".jpeg", DocumentType.CustomerPhoto);
                                    if (imageExist)
                                    clsDocument.DeleteImage(LedgerCode + ".jpeg", DocumentType.CustomerPhoto);
                                }
                                imageExist = false;

                            }
                       }
                            #endregion
                            MetroMessageBox.Show(this, "", mtxtName.Text + " Updated successfully", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        
                        //else
                        //    MetroMessageBox.Show(this, mtxtName.Text + " Save Failed " , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }
                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
            
        }
        private void MbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable dtColumnAlign = new DataTable();
                //dtColumnAlign.Columns.Add();
                //dtColumnAlign.Columns.Add();
                //dtColumnAlign.Rows.Add("8", "R");
                //dgvLstPurchaseMaster.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //dgvLstPurchaseMaster.Columns[1].Width = 80;

                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Name,Code,Group,Telephone,Mobile,Address,Loy.Pnts";
                _objSearch.Select = "L.LedgerId as Id,L.LedgerName as Field1,L.LedgerCode as Field2,Ac.AccountGroupName as Field3,L.Telephone as Field4,L.Mobile as Field5, L.LedgerAddress as Field6, L.loypoints as Field7";
                _objSearch.Table = " tblAccountLedger L LEFT JOIN tblAccountGroup Ac  ON L.AccountGroupId = Ac.AccountGroupId ";
                if (UserAccounts.FrmLogin.UserAccountTypeId==5 )  //Card
                {
                    _objSearch.WhereNdGroupBy = " WHERE  L.cardtype>0  and L.Status = 0";  // And L.BranchId = " + BranchId;
                    _objSearch.OrderBy = "Order By L.LedgerId desc";

                }
                else
                {
                    _objSearch.WhereNdGroupBy = " WHERE L.Status = 0 And L.BranchId = " + BranchId;
                    _objSearch.OrderBy = "Order By L.LedgerName";

                }


                //using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 1200, 400, 100, 100))
                //this.Location.Y + 50, this.Location.X + 40
                //using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.AllCharacters, this.Width - 90, (this.Height - 120), this.Location.Y + 50, this.Location.X + 100))
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.AllCharacters, this.Width - 90, (this.Height - 120), this.Location.Y + 50, this.Location.X + 40))
                {       
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue.ToString() != "")
                {
                    LedgerId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblAccountLedger l left join tblGeneralOptions g on l.cardtype = g.GeneralId ";
                    objSearch.WhereNdGroupBy = "Where LedgerId = " + LedgerId  ;
                    objSearch.OrderBy = "Order By LedgerName ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mcmbState.SelectedValue = gridSource[0].State;
                    mcmbAccountGroup.SelectedValue = gridSource[0].AccountGroupId;
                    AccountGroupId = gridSource[0].AccountGroupId;
                    mcmbBankName.SelectedValue = gridSource[0].BankId;
                    mtxtAccountNo.Text = Convert.ToString(gridSource[0].BankAccountNo);
                    mtxtAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                    mtxtBranchName.Text = Convert.ToString(gridSource[0].BankBranch);
                    mtxtCode.Text = Convert.ToString(gridSource[0].LedgerCode);
                    LedgerCode = Convert.ToString(gridSource[0].LedgerCode);
                    mtxtCreditLimit.Text = Convert.ToString(gridSource[0].CreditLimit);
                    mtxtCreditDays.Text = Convert.ToString(gridSource[0].CreditDays);
                    mtxtDiscountPer.Text = Convert.ToString(gridSource[0].DiscountPer);
                    mtxtEmailId.Text = Convert.ToString(gridSource[0].EmailId);
                    mtxtName.Text = Convert.ToString(gridSource[0].LedgerName);
                    mtxtGstNo.Text = Convert.ToString(gridSource[0].GSTNo);
                    mtxtIFSC.Text = Convert.ToString(gridSource[0].BankIFSC);
                    mtxtInterestPerDay.Text = Convert.ToString(gridSource[0].InterestPerDay);
                    mtxtMobile.Text = Convert.ToString(gridSource[0].Mobile);
                    mtxtPanNo.Text = Convert.ToString(gridSource[0].PAN);
                    mtxtRegNo.Text = Convert.ToString(gridSource[0].CompanyRegNo);
                    mcmbCardType.Text = Convert.ToString(gridSource[0].Caption);
                    mtxtRemarks.Text = gridSource[0].Remarks;
                    mtxtTelephoneNo.Text = gridSource[0].Telephone;
                    mtxtTinNo.Text = gridSource[0].TINNo;
                    mtxtAge.Text = Convert.ToString(gridSource[0].age);
                    mcmbGender.SelectedValue = gridSource[0].gender;
                    mdtpDOB.Text = Convert.ToString(gridSource[0].dob);
                    mdtpValidTo.Text = Convert.ToString(gridSource[0].vto);
                    mtxtDOB.Text = Convert.ToString(gridSource[0].dob);
                    mtxtDOB.Text = mtxtDOB.Text.Substring(0, 10);
                    if (gridSource[0].Photo != null)
                    {
                        imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                        if (imageExist)
                            pbCustomer.Image = clsDocument.DownloadImage(mtxtCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                        imageExist = false;
                    }
                    
                    mcmbAccountGroup.Focus();
                    mbtnDelete.Enabled = true;
                    hlblHeader.Text = "Ledger [Edit]";
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
                if(!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    mbtnSave.Enabled = false;

                }
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //bool deletestatus;
                //if (LedgerId != 0)
                //{
                //    DialogResult result = MetroMessageBox.Show(this, "Confirm to delete " + mtxtName.Text.ToString() + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //    if (result == DialogResult.Yes)
                //    {
                //        DmnAccountLedger dmnAccountLedger = new DmnAccountLedger();
                //        BllAccountLedger bllAccountLedger = new BllAccountLedger();
                //        dmnAccountLedger.LedgerId = LedgerId;
                //        dmnAccountLedger.ModifiedBy = UserId;
                //        var lstData = bllAccountLedger.DeleteById("prdDeleteAccountLedger", dmnAccountLedger);
                //        deletestatus = lstData;
                //        if (deletestatus)
                //        {
                //            imageExist = clsDocument.FileExistOrNot(mtxtCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                //            if (imageExist)
                //                clsDocument.DeleteImage(mtxtCode.Text + ".jpeg", DocumentType.CustomerPhoto);
                //            MetroMessageBox.Show(this, "Successfully Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                //        }
                //        else
                //            MetroMessageBox.Show(this, "Delete Failed " + mtxtName.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                //    }
                //}
                RefreshForm();
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
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtDiscountPer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox(sender, e);
                objGeneral.PercentageTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtInterestPerDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.DecimalTextBox(sender, e);
                objGeneral.PercentageTextBox(sender, e);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
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
        private void McmbAccountGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //SelectCode();
                //if (LedgerId != 0)
                //{
                //    if (AccountGroupId != Convert.ToInt32(mcmbDepartmentId.SelectedValue))
                //    {
                //        GenerateCode();
                //    }
                //    else
                //        mtxtCode.Text = EmployeeCode;
                //}
                //else
                //{
                //    GenerateCode();
                //}
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
                    pbCustomer.Image = Image.FromFile(openFileDialog.FileName);
                }
                mbtnSave.Enabled = true;
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
                mbtnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void McmbAccountGroup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (mtxtCode.Text == "")
                {
                    if (LedgerId != 0)
                    {
                        if (AccountGroupId != Convert.ToInt32(mcmbCardType.SelectedValue))
                        {
                            SelectCode();
                        }
                        else
                            mtxtCode.Text = LedgerCode;
                    }
                    else
                    {
                        SelectCode();
                    }
                }
                else
                {
                    if (UserAccounts.FrmLogin.UserAccountTypeId != 5)
                    {
                        DialogResult result = MessageBox.Show(this, "Want to Save the Changes ?", "Confirmation", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (result.Equals(DialogResult.Yes))
                        {

                            if (LedgerId != 0)
                            {
                                if (AccountGroupId != Convert.ToInt32(mcmbAccountGroup.SelectedValue))
                                {
                                    SelectCode();
                                }
                                else
                                    mtxtCode.Text = LedgerCode;
                            }
                            else
                            {
                                SelectCode();
                            }
                        }

                    }
                }

                }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmAccountLedger_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (_retValue != "")
                    this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MpbAddList_Click(object sender, EventArgs e)
        {
            try
            {
                mtxtTranspotation.Focus();

                if (!string.IsNullOrWhiteSpace(mtxtTranspotation.Text))
                {
                    lbTransportation.Items.Add(mtxtTranspotation.Text);
                    mtxtTranspotation.Clear();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmAccountLedger_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    if(e.KeyCode!=Keys.F6 && e.KeyCode!=Keys.F3 && e.KeyCode!=Keys.F5 && e.KeyCode!=Keys.F7 )
                    {
                        mbtnSave.Enabled = true;
                    }
                }
                if (e.KeyCode == Keys.F6 && mbtnSave.Enabled == true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnList_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.F7)
                    //MbtnDelete_Click(sender, e);
                if (e.KeyCode == Keys.F9)
                {
                    tmrRFID.Enabled = true;
                    pnlRFID.Visible = true;
                    mbtnRFIDRead_Click(sender, e);
                }
                if (e.KeyCode == Keys.F10)
                    MpbAdd_Click(sender, e);
                if (e.KeyCode == Keys.F11)
                    MpbRemove_Click(sender, e);
                if (e.KeyCode == Keys.Delete)
                    lbTransportation.Items.Remove(lbTransportation.SelectedItem);
                if (e.KeyCode == Keys.Escape)
                {
                    if(pnlRFID.Visible==true)
                    {
                        tmrRFID.Enabled = false;
                        pnlRFID.Visible = false;
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mbtnRFIDRead_Click(object sender, EventArgs e)
        {
          
        }
        private void tmrRFID_Tick(object sender, EventArgs e)
        {
         
        }
        private void mbtnCancelRFID_Click(object sender, EventArgs e)
        {
            try
            {
                tmrRFID.Enabled = false;
                pnlRFID.Visible = false;
                mtxtICardNo.Focus();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mcmbAccountGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F12)
                {
                    FrmAccountGroup frmAccountGroup = new FrmAccountGroup();
                    frmAccountGroup.ShowDialog();
                    FillAccountGroupComboBox();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtCreditLimit_KeyPress(object sender, KeyPressEventArgs e)
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
        private void mtxtTranspotation_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DialogResult result = MetroMessageBox.Show(this, "Add " + mtxtTranspotation.Text + " to transportation list ", "  ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                    if (result == DialogResult.Yes)
                        MpbAddList_Click(sender, e);
                    else
                        mtxtRemarks.Focus();
                        //objGeneral.NextConrolOnEnterPress(sender, e);

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void mtxtTranspotation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DialogResult result = MetroMessageBox.Show(this, "Add " + mtxtTranspotation.Text + " to transportation list ", "  ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                    if (result == DialogResult.Yes)
                        MpbAddList_Click(sender, e);
                    else
                        mtxtRemarks.Focus();
                        //objGeneral.NextConrolOnEnterPress(sender, e);
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }


        private void mtxtTinNo_KeyPress(object sender, KeyPressEventArgs e)
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
        private void mtxtName_Leave(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtName.Text = textInfo.ToTitleCase(mtxtName.Text);
                mtxtName.Select(mtxtName.Text.Length, 1);
                if (UserAccounts.FrmLogin.UserAccountTypeId == 5)
                {
                    mcmbAccountGroup.SelectedValue = 20;
                }
                    

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
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }


        #endregion


    public void comboboxfill()//hari new
    {
        BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
        DmnSearch objSearch = new DmnSearch();
        objSearch = new DmnSearch();
        objSearch.Select = "GeneralId,Caption";
        objSearch.Table = " tblGeneralOptions";
        objSearch.WhereNdGroupBy = "Where GTypeId=17 and GTypeSubId=4";
        var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
         mcmbCardType.DataSource = gridSource;
        mcmbCardType.ValueMember = "Id";
        mcmbCardType.DisplayMember = "Value";

       

    }

        private void mcmbCardType_SelectionChangeCommitted(object sender, EventArgs e)//hari new
        {
            try
            {
                if (mtxtCode.Text == "")
                {
                    if (LedgerId != 0)
                    {
                        if (AccountGroupId != Convert.ToInt32(mcmbCardType.SelectedValue))
                        {
                            SelectCodeloyalty();
                        }
                        else
                            mtxtCode.Text = LedgerCode;
                    }
                    else
                    {
                        SelectCodeloyalty();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(this, "Are you Want to Change ?", "Confirmation", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result.Equals(DialogResult.Yes))
                    {

                        if (LedgerId != 0)
                        {
                            if (AccountGroupId != Convert.ToInt32(mcmbCardType.SelectedValue))
                            {
                                SelectCodeloyalty();
                            }
                            else
                                mtxtCode.Text = LedgerCode;
                        }
                        else
                        {
                            SelectCodeloyalty();
                        }

                    }
                }
                } 
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtName_TextChanged(object sender, EventArgs e)
        {
            
            
            if (LedgerId == 0)
            {
                if (mtxtCode.Text == "")
                {
                        if (AccountGroupId != Convert.ToInt32(mcmbCardType.SelectedValue))
                        {
                            if (UserAccounts.FrmLogin.UserAccountTypeId == 5)
                            {
                                SelectCodeloyalty();
                            }
                            else
                            {
                            //    SelectCode();
                            }       
                           
                        }
                }

            }
            
        }

        private void mdtpDOB_MouseHover(object sender, EventArgs e)
        {

            mdtpDOB.Format = DateTimePickerFormat.Short;
        }

        private void mtxtAge_TextChanged(object sender, EventArgs e)
        {
        }

        private void mdtpDOB_ValueChanged(object sender, EventArgs e)
        {
            //int y1 = mdtpDOB.Value.Year;
            //int y2 = DateTime.Now.Year;
            //mtxtAge.Text = Convert.ToString( y2 - y1);
        }

        private void myDt(string xDt)
        {
            String expdt, d1, m1, y1;
            expdt = xDt;
            if (expdt == "")
            {
                return;
            }
            int expl = expdt.Length;
            switch (expl)
            {
                case 5:
                    d1 = expdt.Substring(0, 2);
                    // somestring.Substring(somestring.Length - nCount, nCount)
                    m1 = expdt.Substring(3, 2);
                    y1 = Convert.ToString(DateTime.Now.Year);
                    expdt = d1 + "/" + m1 + "/" + y1;

                    break;
                case 4:
                    d1 = expdt.Substring(0, 2);
                    if (d1.Contains("/"))
                    {
                        d1 = "0" + expdt.Substring(0, 1);
                    }
                    // somestring.Substring(somestring.Length - nCount, nCount)
                    m1 = "0" + expdt.Substring(3, 1);
                    y1 = Convert.ToString(DateTime.Now.Year);
                    expdt = d1 + "/" + m1 + "/" + y1;
                    break;
                case 3:
                    d1 = "0" + expdt.Substring(0, 1);

                    // somestring.Substring(somestring.Length - nCount, nCount)
                    m1 = "0" + expdt.Substring(2, 1);
                    y1 = Convert.ToString(DateTime.Now.Year);
                    expdt = d1 + "/" + m1 + "/" + y1;

                    break;

                default:
                    int pos1 = 0, pos2 = 0;

                    pos1 = expdt.IndexOf("/", 0);
                    d1 = expdt.Substring(0, pos1);
                    if (d1.Length == 1)
                    {
                        d1 = "0" + d1;
                    }
                    pos2 = expdt.IndexOf("/", pos1);
                    m1 = expdt.Substring(pos1 + 1, pos2);
                    if (m1.Length == 1)
                    {
                        m1 = "0" + m1;

                    }
                    else if (m1.Contains("/"))
                    {
                        m1 = m1.Substring(0, 1);
                        m1 = "0" + m1;
                    }

                    y1 = expdt.Substring(expdt.Length - 2, 2);
                    if (y1.Length == 2)
                    {
                        
                        if (Convert.ToInt16(y1)>20)
                        {
                            y1 = "19" + y1;
                        }
                        else
                        {
                            y1 = "20" + y1;
                        }
                        
                    }

                    expdt = d1 + "/" + m1 + "/" + y1;


                    break;
            }
                    mtxtDOB.Text = expdt;

        }

        private void mtxtDOB_Leave(object sender, EventArgs e)
        {

            if (mtxtDOB.Text.Length>=4)
            {
                myDt(mtxtDOB.Text);
                mdtpDOB.Value = Convert.ToDateTime(mtxtDOB.Text);
                int y1 = mdtpDOB.Value.Year;
                int y2 = DateTime.Now.Year;
                mtxtAge.Text = Convert.ToString( y2 - y1);

            }


        }

        private void mtxtAge_Leave(object sender, EventArgs e)
        {
            if (Convert.ToInt32(mtxtAge.Text) > 0)
            {
                if (mtxtDOB.Text.Length <= 0)
                {
                    int ag1 = 0;
                    ag1 = Convert.ToInt32(mtxtAge.Text);
                    mdtpDOB.Value = DateTime.Now.AddYears(-ag1);
                    mtxtDOB.Text = Convert.ToString(mdtpDOB.Value);
                    mtxtDOB.Text = mtxtDOB.Text.Substring(0, 10);
                }
            }

        }

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbCustomer_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        rMnuImage.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }
        }

        private void rMnuImageMnu1_Click(object sender, EventArgs e)
        {
            MpbAdd_Click(null, null);
        }

        private void rMnuImageMnu2_Click(object sender, EventArgs e)
        {
            MpbRemove_Click(null, null);
        }

        private void mtxtPanNo_Enter(object sender, EventArgs e)
        {
            label5.Visible = true;
        }

        private void mtxtPanNo_Leave(object sender, EventArgs e)
        {
            label5.Visible = false;
        }

        private void mtxtGstNo_Enter(object sender, EventArgs e)
        {
            label6.Visible = true;
        }

        private void mtxtGstNo_Leave(object sender, EventArgs e)
        {
            label6.Visible = false;
        }

        private void mbtnUpdate2_Click(object sender, EventArgs e)
        {
            if (LedgerId>0)
            {
                DmnAccountLedger dmnLedger = new DmnAccountLedger();
                BllAccountLedger bllLedger = new BllAccountLedger();
                dmnLedger.LedgerId = LedgerId;
                dmnLedger.ModifiedBy = UserId;
                var lstData = bllLedger.UpdateCustomerLoyalty("prdUpdateCustomer_loyalty", dmnLedger);
                bool savestatus = lstData;
                if (savestatus)
                    MetroMessageBox.Show(this, "", mtxtName.Text + " Updated successfully ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                else
                    MetroMessageBox.Show(this, "", mtxtName.Text + " Can't Update ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                //return false;
            }
            else
                MetroMessageBox.Show(this, "", "Select Customer", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            RefreshForm();
        }

        private void pbCustomer_DoubleClick(object sender, EventArgs e)
        {
            VideoCaptureDevice = new VideoCaptureDevice(FilterInfoCollection[cmbImage.SelectedIndex].MonikerString);
            VideoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            VideoCaptureDevice.Start();
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            VideoCaptureDevice.Stop();  //new
            VideoCaptureDevice = new VideoCaptureDevice(FilterInfoCollection[cmbImage.SelectedIndex].MonikerString);
            VideoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;

        }
        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pbCustomer.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
            pbCustomer.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pbCustomer_Click(object sender, EventArgs e)
        {

        }

        private void cmbImage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

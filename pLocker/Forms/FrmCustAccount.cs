using BLL;
using Domain;
using pLocker.Classes;
using pLocker.Forms;
using pLocker.Forms.UserControls;
using pLocker.Interfaces;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AForge.Video;
using AForge.Video.DirectShow;
using MetroFramework;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using pLocker.Classes;
using ClsGeneral = pLocker.Classes.ClsGeneral;

namespace pLocker.Forms
{
    public partial class FrmCustAccount : Form
    {
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsGeneral = new ClsGeneral();
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;


      
        long BranchId = Forms.frmLogin.BranchId;//16
        long UserId = Forms.frmLogin.UserId;//1
        long FinancialYearId = Convert.ToInt64(FrmLockerDashBoard.FinancialYearId);//10
        public static string empImgExt = "";
        string _retValue;
        long AccID;
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        int LedgerId;
        public FrmCustAccount(int ledgerid)
        {
            InitializeComponent();
            LedgerId = ledgerid;
        }
        private void FrmCustAccount_Load(object sender, EventArgs e)

        {
            ActiveControl = txtAccName;
            txtAccName.Focus();

            lblToolTipAccName.Visible = false;
            
            FillCombo();
            Refreshform();
            if (LedgerId != 0)
            {
                FillDetails();
            }

            try
            {
                FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                foreach (FilterInfo filterInfo in FilterInfoCollection)
                cmbImage.Items.Add(filterInfo.Name);
                cmbImage.SelectedIndex = 0;
                VideoCaptureDevice = new VideoCaptureDevice();
            }
            catch(Exception ex)
            {

            }
            if (!Directory.Exists(Compressedimg))
            {
                Directory.CreateDirectory(Compressedimg);
                DirectoryInfo dInfo = new DirectoryInfo(Compressedimg);
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);
            }
            else
            {
                foreach (string file in Directory.GetFiles(Compressedimg))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(file);
                }
            }


        }
        public void FillCombo()
        {
           // BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            // ============ Country
            BllSearch<DmnComboBox> bllSearch_Country = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=42";
            objSearch.OrderBy = "order by value";
            var gridSource = bllSearch_Country.GetSearchData("prdComboFill", objSearch);
            cboCountry.DataSource = gridSource;
            cboCountry.ValueMember = "Id";
            cboCountry.DisplayMember = "Value";
            cboCountry.SelectedIndex = 0;

            // ============state

            BllSearch<DmnComboBox> bllSearch_State = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();
            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=43";
            objSearch.OrderBy = "order by value";
            gridSource = bllSearch_State.GetSearchData("prdComboFill", objSearch);
            cboState.DataSource = gridSource;
            cboState.ValueMember = "Id";
            cboState.DisplayMember = "Value";
            cboState.SelectedIndex = 0;

            //============= City

            BllSearch<DmnComboBox> bllSearch_City = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();
            objSearch.Select = "city as Value";
            objSearch.Table = " tblAccountLedger";
            objSearch.WhereNdGroupBy = "where city is NOT NULL and ISNUMERIC(city)=0 and city !=''";
            objSearch.OrderBy = "group By city";
            gridSource = bllSearch_City.GetSearchData("prdSearch", objSearch);
            cboCity.DataSource = gridSource;
           // cboCity.ValueMember = "Id";
            cboCity.DisplayMember = "Value";
            cboCity.SelectedIndex = 0;

            // ============ Gender

            BllSearch<DmnComboBox> bllSearch_HireFor = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();
            objSearch.Select = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=25";
            objSearch.OrderBy = "order by value";
            gridSource = bllSearch_HireFor.GetSearchData("prdComboFill", objSearch);
            cboGender.DataSource = gridSource;
            cboGender.ValueMember = "Id";
            cboGender.DisplayMember = "Value";
            cboGender.SelectedIndex = 0;


            // ============ Proof
            BllSearch<DmnComboBox> bllSearch_Proof = new BllSearch<DmnComboBox>();
            objSearch = new DmnSearch();

            objSearch.Select  = "GTypeSubId,Caption";
            objSearch.Table = " tblGeneralOptions";
            objSearch.WhereNdGroupBy = "where isnull(status,0)=0 and GTypeId=6";
            objSearch.OrderBy = "order by value";
            gridSource = bllSearch_Proof.GetSearchData("prdComboFill", objSearch);
            cboProof.DataSource = gridSource;
            cboProof.ValueMember = "Id";
            cboProof.DisplayMember = "Value";
            cboProof.SelectedIndex = 0;
            // ============ Proof

        }
        private void Refreshform()
        {
            lblToolTipAccName.Visible = false;
            cboGender.SelectedIndex = 1;
            cboProof.SelectedIndex = 1;

            txtLedgerID.Text = "";
            txtAccNO.Text = "";
            txtAccName.Text = "";
            txtAccAddress.Text = "";
            cboGender.Text = "select";
            dtpBirthDate.Value = DateTime.Now;
            txtAge.Text = "";
            txtInstituteName.Text = "";
            txtOccupation.Text = "";
            txtZipcode.Text = "";
            txtAccPhone.Text = "";
            txtContEmgcy.Text = "";
            cboCountry.Text = "select";
            cboState.Text = "select";
            cboCity.Text = "select";
            txtAccEmail.Text = "";
            cboProof.Text = "select";
            txtProofNo.Text = "";
            txtRemarks.Text = "";
            txtICardNo.Text = "";
            chBAllowEmail.Checked = false;
            chBAllowSms.Checked = false;
            pbCapture.BackgroundImage = null;
            pbCapture.Image = null;
           

            FillCombo();

        }
        private bool Isvalid()
        {
           

            if (string.IsNullOrWhiteSpace(txtAccName.Text.Trim()))
            {

               MetroMessageBox.Show(this, "", "Account Name must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                txtAccName.Focus();
               return false;
            }
            else if (string.IsNullOrWhiteSpace(txtAccPhone.Text.Trim()))
            {
               MetroMessageBox.Show(this, "", "Phone No.must be specified", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
               txtAccPhone.Focus();
               return false;
            }
            else if (!string.IsNullOrWhiteSpace(txtAccEmail.Text) && clsGeneral.CheckEmailId(this, txtAccEmail.Text) == false)
            {
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(txtZipcode.Text) && clsGeneral.CheckPANNo(this, txtZipcode.Text) == false)
            {
                return false;
            }


            else
            {
                return true;
            }
        }


        public void Insert_Combo1(string caption,int GtypeId)
        {
            try
            {
                DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                BllGeneralOptions bllGen = new BllGeneralOptions();

                dmnGen.Caption = caption;
                dmnGen.GTypeId = GtypeId;
               
                bool insertStatus = bllGen.AddComboData("prdComboFillCountry", dmnGen);
             
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        public void Insert_Combo2(string caption, int GtypeId)
        {
            try
            {
                DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                BllGeneralOptions bllGen = new BllGeneralOptions();

                dmnGen.Caption = caption;
                dmnGen.GTypeId = GtypeId;

                bool insertStatus = bllGen.AddComboData("prdComboFillCountry", dmnGen);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 


        public void Insert_Combo3(string caption, int GtypeId)
        {
            try
            {
                DmnGeneralOptions dmnGen = new DmnGeneralOptions();
                BllGeneralOptions bllGen = new BllGeneralOptions();

                dmnGen.Caption = caption;
                dmnGen.GTypeId = GtypeId;

                bool insertStatus = bllGen.AddComboData("prdComboFillCountry", dmnGen);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                    );
            }
        }





        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Isvalid() == false)
                    return;

                DmnCustAccount dmnCustAccount = new DmnCustAccount();
                BllCustAccount bllCustAccount = new BllCustAccount();

                if (string.IsNullOrWhiteSpace(txtLedgerID.Text))
                    txtLedgerID.Text = "0";

                dmnCustAccount.AccID = Convert.ToInt32(txtLedgerID.Text);
                dmnCustAccount.DOB = Convert.ToDateTime(dtpBirthDate.Text);
                dmnCustAccount.Gender = Convert.ToInt32(cboGender.SelectedValue);
                dmnCustAccount.Proof = Convert.ToInt32(cboProof.SelectedValue);
                dmnCustAccount.ProofNo = Convert.ToString(txtProofNo.Text);
                dmnCustAccount.AccNo = Convert.ToString(txtAccNO.Text);
                dmnCustAccount.AccName = Convert.ToString(txtAccName.Text);
                dmnCustAccount.AccPhone = Convert.ToString(txtAccPhone.Text);
                dmnCustAccount.AccEmail = Convert.ToString(txtAccEmail.Text);
                dmnCustAccount.AccAddress = Convert.ToString(txtAccAddress.Text);
                dmnCustAccount.Remark = Convert.ToString(txtRemarks.Text);


                if (string.IsNullOrWhiteSpace(txtAge.Text))
                                       txtAge.Text = "0";

                if (string.IsNullOrWhiteSpace(txtZipcode.Text))
                    txtZipcode.Text = "0";



                dmnCustAccount.Age = Convert.ToInt32(txtAge.Text);
                dmnCustAccount.EnrollDate = Convert.ToDateTime(dtpEntrollDate.Text);
                dmnCustAccount.CInstitute = Convert.ToString(txtInstituteName.Text);
                dmnCustAccount.Occupation = Convert.ToString(txtOccupation.Text);
                dmnCustAccount.ZipCode = Convert.ToInt32(txtZipcode.Text);
                dmnCustAccount.cEmergency = Convert.ToString(txtContEmgcy.Text);
                dmnCustAccount.cCountry = Convert.ToString(cboCountry.SelectedValue);//change
                dmnCustAccount.State = Convert.ToString(cboState.SelectedValue);//change
                dmnCustAccount.City = Convert.ToString(cboCity.Text);


                if (chBAllowEmail.Checked == true)
                    dmnCustAccount.AllowEmail = 1;
                else
                    dmnCustAccount.AllowEmail = 0;

                if (chBAllowSms.Checked == true)
                    dmnCustAccount.AllowSMS = 1;

                else
                    dmnCustAccount.AllowSMS = 0;


                //dmnCustAccount.Proofimg1 = Convert.ToString(.Text);           
                dmnCustAccount.CreatedBy = UserId;
                //dmnCustAccount.ModifiedBy = UserId;
                dmnCustAccount.BranchId = BranchId;
                dmnCustAccount.Status = 1;
                dmnCustAccount.ICardNo = Convert.ToString(txtICardNo.Text);

                var InsertStatus = bllCustAccount.AddEditData("prdInsert_Upd_CustAccount", dmnCustAccount);
               
                //if (pbCapture.BackgroundImage != null)
                //{
                //    Bitmap bm = new Bitmap(pbCapture.BackgroundImage);
                //    clsDocument.UploadImage(bm, txtLedgerID.Text, DocumentType.Locker);
                //}
                if (InsertStatus.LedgerId!=0)
                {
                    if (dmnCustAccount.AccID > 0)
                    {
                        if (empImgExt != "")
                        {

                            ClsImgComprossor.CompressImage(empImgExt, Compressedimg, 30);

                        }
                        if (pbCapture.BackgroundImage != null)
                        {

                            bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", dmnCustAccount.AccID + ".jpeg", DocumentType.CustomerPhoto);

                        }
                        else
                        {
                          bool  imageExist = clsDocument.FileExistOrNot(dmnCustAccount.AccID + ".jpeg", DocumentType.CustomerPhoto);
                            if (imageExist)
                                clsDocument.DeleteImage(dmnCustAccount.AccID + ".jpeg", DocumentType.CustomerPhoto);
                            dmnCustAccount.Photo = "";
                            dmnCustAccount.Photo = "";
                        }
                        MetroMessageBox.Show(this, txtAccName.Text + "  ", "Detail Successfully Updated", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                        this.Close();
                    }
                    else
                        MetroMessageBox.Show(this, "  ", "Successfully Saved", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    Refreshform();
                }
                else
                {
                    MetroMessageBox.Show(this, " ", " Save Failed  ", MessageBoxButtons.OK);
                }
            }
             catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        public void LedgerPopup()
        {
           
            string returnValue = string.Empty;
            DataTable dtColumnAlign = new DataTable();
            //dtColumnAlign.Columns.Add();
            //dtColumnAlign.Rows.Add("6", "R");
            //dtColumnAlign.Rows.Add("7", "R");
            //dtColumnAlign.Rows.Add("8", "R");
            //dtColumnAlign.Rows.Add("9", "R");
            //dtColumnAlign.Rows.Add("10", "R");
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "Id,Name,Account no,Mobile,Email,Address,Valid To";
            _objSearch.Select = "L.LedgerId as Id,L.LedgerName as Field1,LedgerCode as Field2,L.Mobile as Field3,L.EmailId as Field4, L.LedgerAddress as Field5, L.vto as Field6";
            _objSearch.Table = " tblAccountLedger L LEFT JOIN tblAccountGroup Ac  ON L.AccountGroupId = Ac.AccountGroupId";
            _objSearch.WhereNdGroupBy = " WHERE isnull(L.Status,0) = 1  and  isnull(L.cardstatus,0)=1 and L.BranchId = " + frmLogin.BranchId;
            _objSearch.OrderBy = " ORDER BY L.LedgerName ASC";
            using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 100, (this.Height - 10), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))

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
                txtLedgerID.Text= Convert.ToString(LedgerId);
                BllSearch<DmnCustAccount> bllSearch = new BllSearch<DmnCustAccount>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = " tblAccountLedger ";
                objSearch.WhereNdGroupBy = "Where LedgerId = " + LedgerId;
                objSearch.OrderBy = "Order By LedgerName ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dtpEntrollDate.Value = gridSource[0].EnrollDate;
                txtLedgerID.Text = gridSource[0].LedgerId.ToString();
                txtAccName.Text = Convert.ToString(gridSource[0].LedgerName);
                txtAccNO.Text = Convert.ToString(gridSource[0].LedgerCode);
                txtAccAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                cboGender.SelectedValue = Convert.ToInt64(gridSource[0].gender);
                dtpBirthDate.Value = Convert.ToDateTime(gridSource[0].dob.ToString());
                txtAge.Text =gridSource[0].age.ToString();
                txtInstituteName.Text = gridSource[0].CInstitute;
                txtOccupation.Text = gridSource[0].Occupation;
                txtZipcode.Text = gridSource[0].ZipCode.ToString();
                txtAccPhone.Text = gridSource[0].Mobile;
                int i = gridSource[0].AllowSMS;
                if (i == 1)
                {
                    chBAllowSms.Checked = true;
                }
                else
                {
                    chBAllowSms.Checked = false;
                }
                i = gridSource[0].AllowEmail;
                if (i == 1)
                {
                    chBAllowEmail.Checked = true;
                }
                else
                {
                    chBAllowEmail.Checked = false;
                }
                txtContEmgcy.Text = gridSource[0].cEmergency;
                cboCountry.SelectedValue = Convert.ToInt64(gridSource[0].cCountry);//change
                cboState.SelectedValue = Convert.ToInt64(gridSource[0].State);//change
                cboCity.Text = gridSource[0].City;
                txtAccEmail.Text = gridSource[0].EmailId;
                cboProof.SelectedValue = Convert.ToInt64(gridSource[0].Proof);
                txtProofNo.Text = gridSource[0].ProofNo;
                txtRemarks.Text = gridSource[0].Remark;
                txtICardNo.Text = Convert.ToString(gridSource[0].ICardNo);

                var imageExist = clsDocument.FileExistOrNot(LedgerId.ToString()+".jpeg", DocumentType.CustomerPhoto);
                if (imageExist)
                    pbCapture.BackgroundImage = clsDocument.DownloadImage(LedgerId.ToString() + ".jpeg", DocumentType.CustomerPhoto);
                pbCapture.BackgroundImageLayout = ImageLayout.Stretch;

            }
        }
        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }

        }
        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void cboRateType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtHireFor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        private void txtExp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }
        
        private void txtAccName_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }

            if (e.KeyCode == Keys.F8)
                LedgerPopup();           
        }
        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtAccName_Enter(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = true;

        }
        private void getNewAccNo()
        {
            int sel = 143;
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
                long lc = Convert.ToInt64(gridSource[0].LedgerCode) + 1;

               txtAccNO.Text= Convert.ToString(lc);

            }
            else
            {

            }
        }
        private void txtAccName_Leave(object sender, EventArgs e)
        {
            lblToolTipAccName.Visible = false;

        }
        private void txtAccNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
                LedgerPopup();
            e.SuppressKeyPress = true;
            SendKeys.Send("{TAB}");


           
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {

            VideoCaptureDevice.Stop();
            empImgExt = "";
            Bitmap bm = new Bitmap(pbCapture.BackgroundImage);
            bm.Save(Compressedimg + "\\Image.jpeg"); //new
           // MessageBox.Show(Compressedimg);
        }

        private void cboCountry_Leave(object sender, EventArgs e)
        {
            if(cboCountry.Text!=null|| cboCountry.Text!="" )

            Insert_Combo1(cboCountry.Text.Trim().ToUpper(), 42);
        }

        private void cboState_Leave(object sender, EventArgs e)
        {
            if (cboState.Text!= null || cboState.Text!= "")

                Insert_Combo2(cboState.Text.Trim().ToUpper(), 43);
        }

        private void cboCity_Leave(object sender, EventArgs e)
        {
            if (cboCity.Text!= null || cboCity.Text!= "")

                Insert_Combo3(cboCity.Text.Trim().ToUpper(), 44);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LedgerPopup();
        }

        private void txtAccName_TextChanged(object sender, EventArgs e)
        {
            
            if (string.IsNullOrWhiteSpace(txtAccName.Text) == false && txtLedgerID.Text == "0" || string.IsNullOrWhiteSpace(txtLedgerID.Text))
            {
                getNewAccNo();
            }
        }

        private void btnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboGender_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void dtpBirthDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refreshform();
        }

        private void txtRemarks_TextChanged(object sender, EventArgs e)
        {

        }

        private void pbCapture_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if(cmbImage.Items.Count > 0)
                {
                    pbCapture.BackgroundImage = null;
                    empImgExt = "";
                    VideoCaptureDevice = new VideoCaptureDevice(FilterInfoCollection[cmbImage.SelectedIndex].MonikerString);
                    VideoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
                    VideoCaptureDevice.Start();
                }
                
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Work for Capture Device", MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
           
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pbCapture.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
            pbCapture.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void pbCapture_Click(object sender, EventArgs e)
        {

        }
        public void FillDetails()
        {

            txtLedgerID.Text = Convert.ToString(LedgerId);
            BllSearch<DmnCustAccount> bllSearch = new BllSearch<DmnCustAccount>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "LedgerId as AccID,EnrollDate,LedgerCode as AccNO,LedgerName as AccName,LedgerAddress as AccAddress,TGO.Caption as Gen,dob,age,cInstitute,Occupation,zipcode,Mobile as AccPhone,AllowSMS,cEmergency,TGO1.Caption as ccountry,TGO2.Caption as State,city,EmailId as AccEmail,AllowEmail,TGO3.Caption as ProofName,ProofNo,Remarks,IcardNo";
            objSearch.Table = " tblAccountLedger TAL left join tblGeneralOptions TGO ON TGO.GTypeSubId = TAL.gender and TGO.GTypeId = 25  " +
                "left join tblGeneralOptions TGO1 on TGO1.GTypeSubId = TAL.ccountry  and TGO1.GTypeId = 42  " +
                "left join tblGeneralOptions TGO2 on TGO2.GTypeSubId = TAL.State  and TGO2.GTypeId = 43  " +
                "left join tblGeneralOptions TGO3 on TGO3.GTypeSubId = TAL.Proof and TGO3.GTypeId = 6  ";
            objSearch.WhereNdGroupBy = "Where LedgerId = " + LedgerId;
            objSearch.OrderBy = "Order By LedgerName ";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

            dtpEntrollDate.Value = gridSource[0].EnrollDate;
            txtLedgerID.Text = gridSource[0].AccID.ToString();
            txtAccName.Text = Convert.ToString(gridSource[0].AccName);
            txtAccNO.Text = Convert.ToString(gridSource[0].AccNo);
            txtAccAddress.Text = Convert.ToString(gridSource[0].AccAddress);
            cboGender.Text =  gridSource[0].Gen;
            dtpBirthDate.Value = Convert.ToDateTime(gridSource[0].dob.ToString());
            txtAge.Text = gridSource[0].age.ToString();
            txtInstituteName.Text = gridSource[0].CInstitute;
            txtOccupation.Text = gridSource[0].Occupation;
            txtZipcode.Text = gridSource[0].ZipCode.ToString();
            txtAccPhone.Text = gridSource[0].AccPhone;
            int i = gridSource[0].AllowSMS;
            if (i == 1)
            {
                chBAllowSms.Checked = true;
            }
            else
            {
                chBAllowSms.Checked = false;
            }
            i = gridSource[0].AllowEmail;
            if (i == 1)
            {
                chBAllowEmail.Checked = true;
            }
            else
            {
                chBAllowEmail.Checked = false;
            }
            txtContEmgcy.Text = gridSource[0].cEmergency;
            cboCountry.Text =Convert.ToString(gridSource[0].cCountry);//change
            cboState.Text = Convert.ToString(gridSource[0].State);//change
            cboCity.Text = gridSource[0].City;
            txtAccEmail.Text = gridSource[0].AccEmail;
            cboProof.Text = Convert.ToString(gridSource[0].ProofName);
            txtProofNo.Text = gridSource[0].ProofNo;
            txtRemarks.Text = gridSource[0].Remarks;
            txtICardNo.Text = Convert.ToString(gridSource[0].ICardNo);

            bool imageExist = clsDocument.FileExistOrNot(LedgerId + ".jpeg", DocumentType.CustomerPhoto);
            if (imageExist)
                pbCapture.BackgroundImage = clsDocument.DownloadImage(LedgerId + ".jpeg", DocumentType.CustomerPhoto);
                pbCapture.BackgroundImageLayout = ImageLayout.Stretch;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtpBirthDate_ValueChanged(object sender, EventArgs e)
        {
            var today = DateTime.Now;
            var dob = dtpBirthDate.Value;
            txtAge.Text = Convert.ToString(today.Year - dob.Year);
        }

       

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtAccAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtInstituteName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtOccupation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtZipcode_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void chBAllowSms_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtContEmgcy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtAccEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void chBAllowEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboProof_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtProofNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Refreshform();
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtAccName_DoubleClick(object sender, EventArgs e)
        {
            LedgerPopup();
        }

        private void txtAccEmail_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cboProof_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtProofNo_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtRemarks_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbImage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnCapture_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnRefresh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }
        public string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        private void btnRFID_Click(object sender, EventArgs e)
        {
            string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul();
            if (str1 != "Card not in contact")
            {
                timer1.Stop();
                if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication")
                    str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi();
                str1 = RemoveSpecialCharacters(str1.Trim());
                if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact")
                    MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    BllSearch<DmnCustAccount> bllSearch = new BllSearch<DmnCustAccount>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = " tblAccountLedger ";
                    objSearch.WhereNdGroupBy = "Where ICardNo= '" + str1 + "'";
                    objSearch.OrderBy = "Order By LedgerName ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        dtpEntrollDate.Value = gridSource[0].EnrollDate;
                        txtLedgerID.Text = gridSource[0].LedgerId.ToString();
                        txtAccName.Text = Convert.ToString(gridSource[0].LedgerName);
                        txtAccNO.Text = Convert.ToString(gridSource[0].LedgerCode);
                        txtAccAddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                        cboGender.SelectedValue = Convert.ToInt64(gridSource[0].gender);
                        dtpBirthDate.Value = Convert.ToDateTime(gridSource[0].dob.ToString());
                        txtAge.Text = gridSource[0].age.ToString();
                        txtInstituteName.Text = gridSource[0].CInstitute;
                        txtOccupation.Text = gridSource[0].Occupation;
                        txtZipcode.Text = gridSource[0].ZipCode.ToString();
                        txtAccPhone.Text = gridSource[0].Mobile;
                        int i = gridSource[0].AllowSMS;
                        if (i == 1)
                        {
                            chBAllowSms.Checked = true;
                        }
                        else
                        {
                            chBAllowSms.Checked = false;
                        }
                        i = gridSource[0].AllowEmail;
                        if (i == 1)
                        {
                            chBAllowEmail.Checked = true;
                        }
                        else
                        {
                            chBAllowEmail.Checked = false;
                        }
                        txtContEmgcy.Text = gridSource[0].cEmergency;
                        cboCountry.SelectedValue = Convert.ToInt64(gridSource[0].cCountry);//change
                        cboState.SelectedValue = Convert.ToInt64(gridSource[0].State);//change
                        cboCity.Text = gridSource[0].City;
                        txtAccEmail.Text = gridSource[0].EmailId;
                        cboProof.SelectedValue = Convert.ToInt64(gridSource[0].Proof);
                        txtProofNo.Text = gridSource[0].ProofNo;
                        txtRemarks.Text = gridSource[0].Remark;
                        txtICardNo.Text = Convert.ToString(gridSource[0].ICardNo);

                        bool imageExist = clsDocument.FileExistOrNot(LedgerId.ToString(), DocumentType.Locker);
                        if (imageExist)
                            pbCapture.BackgroundImage = clsDocument.DownloadImage(LedgerId.ToString(), DocumentType.Locker);
                        pbCapture.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else
                    {
                        txtICardNo.Text = str1;
                    }
                }

            } 
        }

        private void FrmCustAccount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                    this.Close();
                else if (e.KeyData == Keys.F5)
                    Refreshform();

                
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAccName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAccName.Text.Length <= 0) return;
            string s = txtAccName.Text.Substring(0, 1);
            if (s != s.ToUpper())
            {
                int curSelStart = txtAccName.SelectionStart;
                int curSelLength = txtAccName.SelectionLength;
                txtAccName.SelectionStart = 0;
                txtAccName.SelectionLength = 1;
                txtAccName.SelectedText = s.ToUpper();
                txtAccName.SelectionStart = curSelStart;
                txtAccName.SelectionLength = curSelLength;
            }
        }

        private void txtICardNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void pbAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                DialogResult dialogResult = openFileDialog.ShowDialog();
                if (openFileDialog.FileName != "")
                {
                    // empImgExt = Path.GetExtension(openFileDialog.FileName);
                    empImgExt = openFileDialog.FileName;
                    pbCapture.BackgroundImage = Image.FromFile(openFileDialog.FileName);


                }
                btnSave.Enabled = true;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void pbRemove_Click(object sender, EventArgs e)
        {
            try
            {
                bool imageExist = clsDocument.FileExistOrNot(LedgerId.ToString(), DocumentType.Locker);
                if (imageExist)
                    clsDocument.DeleteImage(LedgerId.ToString(), DocumentType.Locker);
                pbCapture.Image = null;
                pbCapture.BackgroundImage = null;
                btnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        //new
        private void btnNew_MouseHover(object sender, EventArgs e)
        {
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.new_3));
        }

        private void btnNew_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.new_2));
        }

        private void btnNew_MouseLeave(object sender, EventArgs e)
        {
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.New_1));
        }

        private void btnNew_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.New_1));
        }

        //save
        private void btnSave_MouseHover(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_3));
        }

        private void btnSave_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_2));
        }

        private void btnSave_MouseLeave(object sender, EventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }
       
        private void btnSave_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }

        //refresh
        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_3));
        }

        private void btnRefresh_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_2));
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        private void btnRefresh_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        //close
        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_3));
        }

        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_2));
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        private void btnClose_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        //capture
        private void btnCapture_MouseHover(object sender, EventArgs e)
        {
            this.btnCapture.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.camera_3));
        }

        private void btnCapture_MouseDown(object sender, MouseEventArgs e)
        {
            this.btnCapture.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.camera_2));
        }

        private void btnCapture_MouseLeave(object sender, EventArgs e)
        {
            this.btnCapture.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.camera_1));
        }

        private void btnCapture_MouseUp(object sender, MouseEventArgs e)
        {
            this.btnCapture.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.camera_1));
        }

        //add
        private void pbAdd_MouseHover(object sender, EventArgs e)
        {
            this.pbAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Add_3));
        }

        private void pbAdd_MouseDown(object sender, MouseEventArgs e)
        {
            this.pbAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Add_2));
        }

        private void pbAdd_MouseLeave(object sender, EventArgs e)
        {
            this.pbAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Add_1));
        }

        private void pbAdd_MouseUp(object sender, MouseEventArgs e)
        {
            this.pbAdd.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Add_1));
        }

        //remove
        private void pbRemove_MouseHover(object sender, EventArgs e)
        {
            this.pbRemove.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_3_button));
        }

        private void pbRemove_MouseDown(object sender, MouseEventArgs e)
        {
            this.pbRemove.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_2_button));
        }

        private void pbRemove_MouseLeave(object sender, EventArgs e)
        {
            this.pbRemove.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1_button));
        }

        private void pbRemove_MouseUp(object sender, MouseEventArgs e)
        {
            this.pbRemove.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1_button));
        }

        private void txtZipcode_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtAccPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void txtContEmgcy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnRFID_Click(sender, e);
        }

        private void txtICardNo_Leave(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void txtICardNo_Enter(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
    
}

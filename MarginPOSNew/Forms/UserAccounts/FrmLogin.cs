using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
using ReaderSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AYJCardLib;



namespace MarginPOSNew.Forms.UserAccounts
    
{
    public partial class FrmLogin : MetroFramework.Forms.MetroForm
    {
        #region Declarations



        ClsGeneral objGeneral = new ClsGeneral();// for validation
        BllEncryption bllEncryption = new BllEncryption();
        BllUserAccount bllUserAccount = new BllUserAccount();
        byte[] passwordHash;
        byte[] passwordSalt;
        public static long UserId;
        public static long BranchId;
        int UserStatus;
        long EmployeeId;
        
        long CompanyId;
        long FinYrId;
        public static long UserAccountTypeId;

        string photo;
        bool imageExist;
        ClsDocument clsDocument = new ClsDocument();
        #endregion

        #region Functions
               
        #region shape
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        #endregion


        public bool IsICardValid(string CardNo)
        {

            if (CardNo.Length == 28)
            {
                string RcvdCardSeries = CardNo.Substring(0, 12);
                string CardType = CardNo.Substring(8, 4);

                if (CardType.StartsWith("E"))
                {


                    FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.ColumnHeaders = "Id";
                    objSearch.Select = "  count(*) AS Id ";
                    objSearch.Table = " tblGeneralOptions G inner join tblGType GT on G.GTypeId=GT.GTypeId  ";
                    objSearch.WhereNdGroupBy = " where GT.GType='ICARDSERIES' and Caption like 'EMPLOYEE%' AND  G.GTypeSubText1 in('" + RcvdCardSeries + "') AND G.Status=0";
                    var EmployeeCount = frmGridSearch.GetSearchGrid(objSearch);

                    if (Convert.ToInt64(EmployeeCount["Id", 0].Value) > 0)
                    {
                        
                        return true;
                    }
                    MetroMessageBox.Show(this, " This card not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    
                    return false;

                }
                else
                {
                    MetroMessageBox.Show(this, "Not an employee card", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    
                    return false;
                }
            }

            MetroMessageBox.Show(this, "Invalid card series", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            
            return false;


        }
      

        public bool IsValid()
        {
            Control[] controllist = { mtxtUsername, mtxtPassword };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsUsernameValid()
        {
            BllSearch<DmnUserAccount> bllSearch = new BllSearch<DmnUserAccount>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "PasswordHash,PasswordSalt ";
            objSearch.Table = " tblUserAccount ";
            objSearch.WhereNdGroupBy = " Where Username = '" + mtxtUsername.Text.Trim().ToUpper() + "'   AND Status=0 ";
            var pwdDetails = bllSearch.GetSearchData("prdSearch", objSearch);
            if (pwdDetails.Count > 0)
            {
                passwordSalt = pwdDetails[0].PasswordSalt;
                passwordHash = pwdDetails[0].PasswordHash;
                return true;
            }
            else
            {
               // MetroMessageBox.Show(this, "\nInvalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                mtxtUsername.Focus();
                return false;
            }
        }
        public bool IsPasswordvalid()
        {
            if (passwordSalt == null || passwordHash == null)
            {
                return false;
            }
            bool result = bllEncryption.verifyPassword(mtxtPassword.Text.Trim(), passwordSalt, passwordHash);
            if (result == true)
            {
                return true;
            }
            return false;
        }
        public FrmLogin()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                mlblAssemblyVersion.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();
                mtxtUsername.Clear();
                mtxtPassword.Clear();
                ActiveControl = mtxtUsername;
                mtxtUsername.Focus();


                #region check Ftp Status

                //DmnSearch dmnSearch = new DmnSearch();
                //ClsDocument clsDocument = new ClsDocument();
                //dmnSearch.Select = " gp.GTypeSubText1,gp.GTypeSubText2,gp.GTypeSubText3 ";
                //dmnSearch.Table = " tblGeneralOptions as gp inner join tblGType as g on gp.GtypeId=g.GTypeId";
                //dmnSearch.WhereNdGroupBy = " where g.GType='FTP' and gp.[Status]=2 ";
                //BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
                //var ftpCredentials = bllSearch.GetSearchData("prdSearch", dmnSearch);
                //NetworkCredential credential = new NetworkCredential(ftpCredentials[0].GTypeSubText2, ftpCredentials[0].GTypeSubText3);
                //clsDocument.CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "CustomerPhoto", credential);
                //clsDocument.CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "EmployeePhoto", credential);
                //clsDocument.CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "EmployeeProof", credential);
                //clsDocument.CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "ItemPhoto", credential);
                //clsDocument.CheckAndCreateDirectory(ftpCredentials[0].GTypeSubText1.ToString(), "CompanyLogo", credential);

                #endregion

                BllSearch<DmnComboBox> bllSearchBranch = new BllSearch<DmnComboBox>();
                DmnSearch dmnSearchBranch = new DmnSearch();
                dmnSearchBranch.Select = "BranchId,BranchName";
                dmnSearchBranch.Table = " tblBranch ";
                dmnSearchBranch.WhereNdGroupBy = " Where Status=0 ";
                var branch = bllSearchBranch.GetSearchData("prdComboFill", dmnSearchBranch);
                mcmbBranch.DataSource = branch;
                mcmbBranch.DisplayMember = "Value";
                mcmbBranch.ValueMember = "Id";
                FrmSplash frmsplash = new FrmSplash();
                frmsplash.Close();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid())
                {
                    if (mcmbBranch.SelectedIndex != 0)
                    {
                        if (IsUsernameValid())
                        {
                            if (IsPasswordvalid())
                            {
                                #region retrieve data after login

                                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                                DmnSearch objSearch = new DmnSearch();
                                objSearch.ColumnHeaders = "UserAccountId,Status,EmployeeId,BranchId,CompanyId,UserAccountTypeId";
                                objSearch.Select = " ua.UserAccountId as Id,ua.[Status] as Field1,emp.EmployeeId as Field2,ua.BranchId as Field3,br.CompanyId as Field4 ,ua.UserAccountTypeId as Field5";
                                objSearch.Table = " tblUserAccount ua inner join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                                    "inner join tblBranch br on br.BranchId = ua.BranchId ";
                                objSearch.WhereNdGroupBy = " Where Username = '" + mtxtUsername.Text.Trim().ToUpper() + "'";
                                var userDetails = frmGridSearch.GetSearchGrid(objSearch);

                                BranchId = Convert.ToInt64(mcmbBranch.SelectedValue);
                                UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);
                                if (UserAccountTypeId >= 2 && Convert.ToString(BranchId) != Convert.ToString(userDetails["Field3", 0].Value))
                                {
                                    MetroMessageBox.Show(this, "\nInvalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                                    mtxtUsername.Focus();
                                }
                                else
                                {
                                    UserId = Convert.ToInt64(userDetails["Id", 0].Value);
                                    UserStatus = Convert.ToInt16(userDetails["Field1", 0].Value);
                                    EmployeeId = Convert.ToInt64(userDetails["Field2", 0].Value);

                                    CompanyId = Convert.ToInt64(userDetails["Field4", 0].Value);
                                    UserAccountTypeId = Convert.ToInt64(userDetails["Field5", 0].Value);


                                    objSearch.ColumnHeaders = "FinancialYearId";
                                    objSearch.Select = " geno.GTypeSubText1 as Field1 ";
                                    objSearch.Table = " tblGeneralOptions geno inner join tblGType gt on geno.GTypeId=gt.GTypeId ";
                                    objSearch.WhereNdGroupBy = "where GType='FINANCIAL YEAR'  COLLATE SQL_Latin1_General_CP1_CS_AS ";
                                    var finYr = frmGridSearch.GetSearchGrid(objSearch);
                                    FinYrId = Convert.ToInt64(userDetails["Field1", 0].Value);
                                    // File obj = new File;

                                    #endregion
                                    if (UserAccountTypeId == 2)
                                    {
                                        Transactions.FrmSalesPOS layoutForm = new Transactions.FrmSalesPOS(UserId, UserStatus, EmployeeId, BranchId, CompanyId, FinYrId);
                                        this.Hide();
                                        layoutForm.ShowDialog();
                                    }
                                    else if (UserAccountTypeId == 4)
                                    {
                                        Masters.FrmAccountLedger layoutForm = new Masters.FrmAccountLedger();
                                        this.Hide();
                                        layoutForm.ShowDialog();
                                    }
                                    else
                                    {
                                        FrmDashboard layoutForm = new FrmDashboard(UserId, UserStatus, EmployeeId, BranchId, CompanyId, FinYrId);
                                        this.Hide();
                                        layoutForm.ShowDialog();
                                    }
                                }
                            }
                            //else
                            //{
                            //    MetroMessageBox.Show(this, "\nInvalid credentials", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            //    mtxtUsername.Focus();

                            //}
                        }
                    }
                    else
                    {

                        MetroMessageBox.Show(this, "\n Please select branch", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);
                        mcmbBranch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void FrmLogin_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
              
                objGeneral.NextConrolOnEnterPress(sender, e);
                
                if(e.KeyCode==Keys.Escape)
                {
                    mtxtUsername.Focus();
                }
                if (e.KeyCode == Keys.F9)
                {
                    MbtnRFIDRead_Click(sender,e);
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void FrmLogin_Activated(object sender, EventArgs e)
        {
            try
            {
               
                if (mcmbBranch.SelectedIndex != 0)
                {
                    mtxtUsername.Clear();
                    mtxtPassword.Clear();
                    mcmbBranch.Refresh();
                    mtxtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }

        private void MbtnRFIDRead_Click(object sender, EventArgs e)
        {
            try
            {
                

                //mtxtUsername.Text = BLL.myRFID.ChkgetId();
                //mtxtUsername.Text = App1.Form1.RFNo;
                

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        

        private void mbtnCancelRFID_Click(object sender, EventArgs e)
        {
            try
            {
                mtxtUsername.Focus();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtUsername_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtUsername.Text))
                {
                    FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.ColumnHeaders = "Id,Photo";
                    objSearch.Select = " ua.UserAccountId as Id,emp.Photo as Field1";
                    objSearch.Table = " tblUserAccount ua inner join tblEmployee emp on emp.EmployeeId = ua.EmployeeId ";
                    objSearch.WhereNdGroupBy = " Where Username = '" + mtxtUsername.Text.Trim().ToUpper() + "'";
                    var userPhoto = frmGridSearch.GetSearchGrid(objSearch);
                    int count = userPhoto.Rows.Count;
                    //if (count>0 && Convert.ToString(userPhoto["Field1", 0].Value) != string.Empty)
                    //{
                    //    photo = Convert.ToString(userPhoto["Field1", 0].Value);
                    //    imageExist = clsDocument.FileExistOrNot(photo, DocumentType.EmployeePhoto);
                    //    if (imageExist)
                    //        pbUser.Image = clsDocument.DownloadImage(photo, DocumentType.EmployeePhoto);
                    //}

                    objSearch.ColumnHeaders = "UserAccountId,Employeename,Branchname";
                    objSearch.Select = " ua.UserAccountId as Id,emp.Employeename as Field1,br.Branchname as Field2";
                    objSearch.Table = " tblUserAccount ua inner join  tblEmployee emp on emp.EmployeeId = ua.EmployeeId " +
                        "inner join tblBranch br on br.BranchId = ua.BranchId ";
                    objSearch.WhereNdGroupBy = " Where Username = '" + mtxtUsername.Text.Trim().ToUpper() + "'";
                    var userdetails = frmGridSearch.GetSearchGrid(objSearch);

                    //if (userdetails.Rows.Count>0 && Convert.ToInt32(userdetails["Id",0].Value)>2)
                    
                    if (userdetails.Rows.Count > 0 )
                    {
                        if (Convert.ToInt32(userdetails["Id", 0].Value)==1)
                        {
                            mcmbBranch.Text = Convert.ToString(userdetails["Field2", 0].Value);
                            mcmbBranch.Enabled = true;

                        }
                        else
                        {
                            mcmbBranch.Text = Convert.ToString(userdetails["Field2", 0].Value);
                            mcmbBranch.Enabled = false;

                        }
                    }
                    else if (userdetails.Rows.Count<=0)
                    {
                        MetroMessageBox.Show(this, "Invalid UserName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        mtxtUsername.Text = "";
                        mtxtUsername.Focus();
                    }

                }
                BranchId = Convert.ToInt64(mcmbBranch.SelectedValue);
                if (string.IsNullOrWhiteSpace(mtxtUsername.Text))
                {
                    pbUser.Image = null;
                    pbUser.Image = Properties.Resources.userProfile;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtPassword_Leave(object sender, EventArgs e)
        {
            try
            {
                if (IsUsernameValid())
                {
                    if (!IsPasswordvalid() && !string.IsNullOrWhiteSpace(mtxtPassword.Text.Trim()) && !string.IsNullOrWhiteSpace(mtxtUsername.Text.Trim()))
                    {


                        MetroMessageBox.Show(this, "Invalid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        mtxtPassword.Text = "";
                        mtxtPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(mtxtUsername.Text))
                {
                    MetroMessageBox.Show(this, "User is Empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    mtxtPassword.Text = "";
                    mtxtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }


        #endregion


        private void mtxtUsername_Click(object sender, EventArgs e)
        {
            
        }
    }
}

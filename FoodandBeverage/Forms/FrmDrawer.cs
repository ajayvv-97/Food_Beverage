using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmDrawer : Form
    {
        #region Decalartion
        ClsGeneral clsgen = new ClsGeneral();
        bool flagDrawerF = false, flagDrawer = false;

        /*permission checking for manager and supervisor*/
        public static int DrawerID;

        public string Drawerassginstaffname;
        public string Drawerassginstaffdes;
        public int Drawerassginid;
        public int Drawerassginstaffid;

        public string Drawersystemname;
        #endregion

        public FrmDrawer()
        {
            InitializeComponent();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrmDrawer_Load(object sender, EventArgs e)
        {
            CheckPermission();
            AssignDrawerCheck();
            UIHelper.RoundCorners(tlp_setting, 25, null);
        }
        public void AssignDrawerCheck()
        {
            try
            {
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9,t.InitilAmount Field10";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where t.DName='" + System.Environment.MachineName + "'  and t.Status=1";
                dmnsearckdrawer.OrderBy = "";
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource.Count > 0)
                {
                    DrawerID = Convert.ToInt32(gridesource[0].Field5);
                    txtdrawerid.Text= Convert.ToString(gridesource[0].Field5);

                    txtinitialamt.Text = Convert.ToString(gridesource[0].Field10);
                    txtinitialamt.Enabled = false;

                    txtextraloan.Text = Convert.ToString(gridesource[0].Field6);
                    txtextradrop.Text = Convert.ToString(gridesource[0].Field7);

                    txtloanamt.Text = Convert.ToString(gridesource[0].Field6);
                    txtdropamt.Text = Convert.ToString(gridesource[0].Field7);

                    txtloanamt.Enabled = true;
                    txtdropamt.Enabled = true;

                    //if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                    //{
                    //    FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4;
                    //    FrmDashboard.Instance.lbl_drawer.Visible = true;
                    //    FrmDashboard.Instance.lbl_drawer.ForeColor = Color.White;
                    //}
                }
                else //drawer not assign
                {
                    txtloanamt.Enabled = false;
                    txtdropamt.Enabled = false;
                    FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                    FrmDashboard.Instance.lbl_drawer.Visible = true;
                    FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Drawer page  AssignDrawerCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Drawer));
            }

        }
        public bool MandatoryCheckDrawer()
        {
            try
            {
                if (Convert.ToDecimal(txtloanamt.Text) > Convert.ToDecimal(txtinitialamt.Text))
                {
                    MessageBox.Show("Loan amount should not be greater than initial amount.",
                                    "Information",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return false;
                }
                if (flagDrawerF && string.IsNullOrWhiteSpace(txtstaffname.Text))
                {
                    MessageBox.Show("Please select Employee", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtstaffname.Focus();
                    return false;
                }
                if (string.IsNullOrWhiteSpace(txtinitialamt.Text) || Convert.ToDecimal(txtinitialamt.Text) == 0)
                {
                    MessageBox.Show("Please Enter Initial Amount", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                //clsg.LogException("Admin page MandatoryCheckDrawer()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private void btndrawersave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtdrawerid.Text))
                    txtdrawerid.Text = "0";
                if (string.IsNullOrWhiteSpace(txtinitialamt.Text))
                    txtinitialamt.Text = "0";
                if (string.IsNullOrWhiteSpace(txtloanamt.Text))
                    txtloanamt.Text = "0";
                if (MandatoryCheckDrawer())
                {
                    Dmndrawer dmndrawer = new Dmndrawer();
                    Blldrawer blldrawer = new Blldrawer();
                    dmndrawer.Id = Convert.ToInt32(txtdrawerid.Text);
                    dmndrawer.DName = System.Environment.MachineName;
                    dmndrawer.InitilAmount = Convert.ToDecimal(txtinitialamt.Text);
                    dmndrawer.EmployeeId = FrmLogin.UserId;
                    if (txtloanamt.Text != txtextraloan.Text)
                        dmndrawer.LoanAmount = Convert.ToDecimal(txtloanamt.Text);
                    else
                        dmndrawer.LoanAmount = 0;

                    if (txtdropamt.Text != txtextradrop.Text)
                        dmndrawer.Drops = Convert.ToDecimal(txtdropamt.Text);
                    else
                        dmndrawer.Drops = 0;

                    dmndrawer.BranchId = FrmDashboard.BranchId;
                    dmndrawer.CreatedBy = FrmDashboard.UserId;
                    dmndrawer.Status = 1;
                    var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer);

                    if (InsertStatus != null)
                    {
                        if (txtdrawerid.Text == null || txtdrawerid.Text == "0")
                        {
                            DrawerID = InsertStatus.Id;
                            MessageBox.Show("Drawer Assign Completed", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtinitialamt.ReadOnly = true;
                            ShowDrawerEmployee();
                            this.Close();   
                        }
                        else
                        {
                            DrawerID = Convert.ToInt32(txtdrawerid.Text);
                            MessageBox.Show("Update Sucessfull", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
        
                    }
                    else
                        MessageBox.Show("Assign Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (DrawerID > 0)
                    {
                        txtdrawerid.Text = Convert.ToString(DrawerID);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Drawer Page btndrawersave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Drawer));
            }
        }
        public void CheckPermission()
        {
            try
            {
                if (FrmDashboard.AllowCashDrawer)
                {
                    lbl_staffname.Visible = true;
                    tlpemplist.Visible = false;
                    lbl_staffname.Text = FrmLogin.EmployeName;
                    lbl_desigtnation.Text = FrmLogin.Designation;
                    txtempoyeId.Text = Convert.ToString(FrmLogin.EmployeeId);
                    lbl_staffname_tip.Visible = false;

                }
                else if (FrmDashboard.AllowDrawerpermissionFullControl)
                {
                    lbl_staffname.Visible = false;
                    tlpemplist.Visible = true;
                    lbl_desigtnation.Text = FrmLogin.Designation;
                    lbl_staffname_tip.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void ShowDrawerEmployee()
        {
            try
            {
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                //lbl_msg2.Text = "welcome " + FrmLogin.EmployeName + " , " + FrmLogin.Designation;
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where t.DName='" + System.Environment.MachineName + "' and t.Status=1";
                dmnsearckdrawer.OrderBy = "";
                var gridesource1 = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource1.Count > 0)
                {
                    if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                    {
                        FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource1[0].Field3 + " , " + gridesource1[0].Field4;
                        FrmDashboard.Instance.lbl_drawer.Visible = true;
                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Green;
                    }

                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Drawer page  ShowDrawerEmployee()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Drawer));
            }

        }
        public bool DrawerCheckBranch(int empid)
        {
            try
            {
                Drawersystemname = "";
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9,t.DName as Field10";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where t.Status=1 and t.EmployeeId =" + empid;
                dmnsearckdrawer.OrderBy = "";
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource.Count > 0)
                {
                    Drawersystemname = gridesource[0].Field10;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Drawer page DrawerCheckBranch()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Drawer));
                return true;
            }
        }
        public void StaffPopup()
        {
            try
            {
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,EmployeeCode,EmployeeName,DesignationName,Mobile";
                _objSearch.Select = "emp.EmployeeId as Id,emp.EmployeeCode as Field1,emp.EmployeeName as Field2,d.DesignationName as Field3,emp.Mobile as Field4";
                _objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId ";
                _objSearch.WhereNdGroupBy = "WHERE isnull(emp.Status,0) = 1 ";
                _objSearch.OrderBy = " ORDER BY emp.EmployeeName ASC";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))

                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue.ToString() != "")
                {
                    int eid = Convert.ToInt32(returnValue);
                    if (DrawerCheckBranch(eid))
                    {
                        MessageBox.Show("Drawer Already Assigned on " + Drawersystemname + "System", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }

                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "COUNT(*) OVER() AS Field1,emp.EmployeeName as Field2,d.DesignationName as Field3";
                    objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId";
                    objSearch.WhereNdGroupBy = "Where emp.status=1 and emp.EmployeeId = " + eid;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        txtstaffname.Text = Convert.ToString(itemSource[0].Field2);
                        lbl_desigtnation.Text = Convert.ToString(itemSource[0].Field3);
                        txtempoyeId.Text = Convert.ToString(eid);
                        lbl_staffname_tip.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ///clsg.LogException("Admin page  StaffPopup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                ////MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void txtstaffname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                    StaffPopup();
            }
            catch (Exception ex)
            {
                //clsg.LogException("Admin page txtstaffname_KeyDown", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string RemoveSpecialCharacters(string str)
        {
            try
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
            catch (Exception ex)
            {
                //clsg.LogException("Admin page  RemoveSpecialCharacters()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }

        }
        public void FetchDetails(string cardno)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "emp.EmployeeId as Field1";
                objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId";
                objSearch.WhereNdGroupBy = "Where emp.status=1 and emp.ICardNo = '" + cardno + "'";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    if (DrawerCheckBranch(Convert.ToInt32(itemSource[0].Field1)))
                    {
                        MessageBox.Show("Drawer Already Assigned on " + Drawersystemname + "System", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                    }
                    else
                    {
                        if (Checkcardstatus(cardno) == false)
                        {
                            MessageBox.Show("Staff Details Not Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsg.LogException("Admin page FetchDetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosForm.Admin));
            }
        }
        public bool Checkcardstatus(string cardno)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "COUNT(*) OVER() AS Field1,emp.EmployeeName as Field2,d.DesignationName as Field3,emp.EmployeeId as Field4";
                objSearch.Table = "tblEmployee  emp Left join tblDesignation d on d.DesignationId = emp.DesignationId";
                objSearch.WhereNdGroupBy = "Where emp.status=1 and emp.ICardNo = '" + cardno + "'";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txtstaffname.Text = Convert.ToString(itemSource[0].Field2);
                    lbl_desigtnation.Text = Convert.ToString(itemSource[0].Field3);
                    txtempoyeId.Text = Convert.ToString(itemSource[0].Field4);
                    lbl_staffname_tip.Visible = false;
                    return true;
                }
                else
                {
                    MessageBox.Show(this, "Invalid card number. Please try again.", "Card Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }
            catch (Exception ex)
            {
                //clsg.LogException("Admin page  Checkcardstatus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(101);
                if (str1 != "Card not in contact")
                {
                    timer1.Stop();
                    if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Invalid Card")
                        str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(101);
                    str1 = RemoveSpecialCharacters(str1.Trim());
                    if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact" || str1 == "InvalidCard")
                    {
                        MessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        FetchDetails(str1);
                    }
                }

            }
            catch (Exception ex)
            {
                // clsg.LogException("Admin page  timer1_Tick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(PosFormtype.Admin));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtstaffname_Enter(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void txtstaffname_Leave(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void btnclose_drawer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;

namespace pLocker.Forms
{
    public partial class FrmRegistration : Form
    {
        public static string mac_id = "";
        public static string CName = "";
        public static string PhoneNo = "";
        public static string Email = "";
        public static string Pincode = "";
        public static string otp = "";
        public static int status_val = 0;
        public static string Expiry_Check = "";
        public static bool RegReq = false;
        public static string con = "server=148.72.216.130;database=apps_registration;uid=ayj_admin;pwd=X+vdPX$7c,6T ;";
        MySqlConnection cnn = new MySqlConnection(con);
        public FrmRegistration()
        {
            InitializeComponent();
        }
        #region Functions
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public string FetchMacId()
        {
            string macAddresses = "";

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += nic.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return macAddresses;
        }
        static private string sendData(string MNo, string Msg)
        {
            WebRequest request = WebRequest.Create("http://sms.ayjsoft.com/api/sendmsg.php");
            request.Method = "POST";
            string postData = "user=ANIKAYAJ&pass=123456&sender=AYJSFT&phone=" + MNo + "&text=" + Msg + "&priority=ndnd&stype=normal";//username=nisarga&password=12345&mobile=" + MNo + "&message=" + Msg + "&senderid=NISRGA";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            //Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            return responseFromServer;
        }
        private void setButtonVisibility()
        {
            if ((txt_CName.Text != String.Empty) && (txt_Email.Text != String.Empty) && (txt_PhoneNo.Text != String.Empty) && (txt_Pincode.Text != String.Empty))
            {
                btn_Register.Enabled = true;

            }
            if ((txt_CName.Text != String.Empty) || (txt_Email.Text != String.Empty) || (txt_PhoneNo.Text != String.Empty) || (txt_Pincode.Text != String.Empty))
            {
                btn_Reset.Enabled = true;

            }
        }
        private void setButtonVisibility_Pincode()
        {
            if ((txt_Pincode.Text != String.Empty) && (txt_Pincode.Text.Length == 6) && (txt_PhoneNo.Text != String.Empty) && (txt_PhoneNo.Text.Length == 10))
            {
                btn_Register.Enabled = true;

            }
            else
            {
                btn_Register.Enabled = false;

            }
        }

        #endregion
        private void FrmRegistration_Load(object sender, EventArgs e)
        {
            bool net_connect = CheckForInternetConnection();
            if (net_connect == false)
            {
                DialogResult expr = MetroMessageBox.Show(this, "", "Check your Internet Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (expr == DialogResult.OK)
                {
                    this.Close();
                }
            }

            txt_CName.Select();
            mac_id = FetchMacId();  //MessageBox.Show("macid: " + mac_id);
            try
            {
                string qry = "select cname,ph1,email,pincode,macid,actv,expdt,regdt from appreg where macid='" + mac_id + "' ";
                MySqlCommand cmd = new MySqlCommand(qry, cnn);
                MySqlDataReader dr;
                cnn.Open();
                dr = cmd.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string macid = dr.GetValue(4).ToString();
                        int status_value = dr.GetInt32(5);
                        string expdt = dr.GetValue(6).ToString();
                        string regdt = dr.GetValue(7).ToString();
                        string status = "";

                        if (macid == mac_id)
                        {
                            if (status_value == 1)
                            {
                                status_val = 1;
                                status = "Activated";

                                //-------- Date Difference -----//

                                DateTime strt_date = DateTime.Now;
                                DateTime end_date = Convert.ToDateTime(expdt);
                                //DateTime add_days = end_date.AddDays(1);
                                TimeSpan nod = (end_date - strt_date);
                                int date_diff_days = int.Parse(nod.Days.ToString());
                                int date_diff_hours = int.Parse(nod.Hours.ToString());
                                int date_diff_min = int.Parse(nod.Minutes.ToString());

                                //-----------------------------------//

                                if (date_diff_days >= 0 && date_diff_hours >= 0 && date_diff_min >= 0)
                                {
                                    var Login = new frmLogin();
                                    Login.ShowDialog();
                                    Login.Close();
                                    this.Dispose();                            
                                }
                                else
                                {
                                    DialogResult expr = MetroMessageBox.Show(this,"", "Trial version has expired! Want to get full version ?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                    if (expr == DialogResult.No)
                                    {
                                        this.Close();
                                    }

                                    txt_CName.Text = dr.GetValue(0).ToString();
                                    txt_PhoneNo.Text = dr.GetValue(1).ToString();
                                    txt_Email.Text = dr.GetValue(2).ToString();
                                    txt_Pincode.Text = dr.GetValue(3).ToString();

                                    RegReq = true;
                                }

                            }

                            else
                            {
                                DialogResult expr = MetroMessageBox.Show(this,"", "Account has been Deactivated!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (expr == DialogResult.OK)
                                {
                                    this.Close();
                                }
                            }
                        }
                    }
                }

                cnn.Close();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message);
            }
        }
        private void Btn_Register_Click(object sender, EventArgs e)
        {
             CName = txt_CName.Text;
            PhoneNo = txt_PhoneNo.Text;
            Email = txt_Email.Text;
            Pincode = txt_Pincode.Text;
            string Expdate = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd hh:mm:ss");
            string Expdate_re = DateTime.Now.AddMonths(6).ToString("yyyy-MM-dd hh:mm:ss");

            // generate random OTP numbers //

            Random generator = new Random();
            otp = generator.Next(0, 999999).ToString("D6");

            //----------------------------//

            string msg = "Dear " + CName + ", " + otp + " is the OTP for your miLocker Account Registration initiated on " + DateTime.Now.ToString() + " .";
            string status = sendData(PhoneNo, msg);
            //string status = "Successfully";

            if (status.Contains("S."))
            {
                //---- To INVOKE NEW FORM----- // 
                if (RegReq == true)
                {
                    try
                    {
                        string Query = "update apps_registration.appreg set regreq='" + 1 + "',gotp='" + otp + "',expdt='" + Expdate_re + "' where macid='" + mac_id + "';";
                        MySqlCommand MyCommand2 = new MySqlCommand(Query, cnn);
                        MySqlDataReader MyReader2;
                        cnn.Open();
                        MyReader2 = MyCommand2.ExecuteReader();  // MessageBox.Show("Data Updated");
                        while (MyReader2.Read())
                        {
                        }
                        cnn.Close();
                    }
                    catch (Exception ex)
                    {
                        MetroMessageBox.Show(this, ex.Message);
                    }
                    // MessageBox.Show("Reactivated !", "OTP Authentication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        string qry = "select macid,actv,expdt,regdt from appreg where macid='" + mac_id + "' ";
                        MySqlCommand cmd = new MySqlCommand(qry, cnn);
                        MySqlDataReader dr;
                        cnn.Open();
                        dr = cmd.ExecuteReader();     // Here our query will be executed and data saved into the database.    
                        string mac_status = "";
                        if (dr.HasRows)
                        {
                            mac_status = "available";
                        }
                        cnn.Close();
                        if (mac_status == "available")
                        {
                            try
                            {
                                string dt = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                                dt = dt.Replace('/', '-');
                                string Query = "update apps_registration.appreg set cname='" + CName + "',ph1='" + PhoneNo + "',email='" + Email + "',pincode='" + Pincode + "',regdt='" + dt + "',gotp='" + otp + "',actv='0',expdt='" + Expdate + "' where macid='" + mac_id + "';";
                                MySqlCommand MyCommand2 = new MySqlCommand(Query, cnn);
                                MySqlDataReader MyReader2;
                                cnn.Open();
                                MyReader2 = MyCommand2.ExecuteReader();
                                while (MyReader2.Read())
                                {
                                }
                                cnn.Close();
                            }
                            catch (Exception ex)
                            {
                                MetroMessageBox.Show(this, ex.Message);
                            }
                        }
                        else
                        {
                            string dt = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                            dt = dt.Replace('/', '-');
                            string icard = "miLocker";

                            string Query = "insert into apps_registration.appreg(macid,cname,ph1,email,pincode,gotp,actv,regdt,expdt,iappl) values('" + mac_id + "','" + CName + "','" + PhoneNo + "','" + Email + "','" + Pincode + "','" + otp + "',0,'" + dt + "','" + Expdate + "','" + icard + "');";
                            MySqlCommand MyCommand2 = new MySqlCommand(Query, cnn);
                            MySqlDataReader MyReader2;
                            cnn.Open();
                            MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.                                             
                            while (MyReader2.Read())
                            {
                            }
                            cnn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MetroMessageBox.Show(this, ex.Message);
                    }
                }
                this.Hide();
                var frmOTP = new frmOTP();
                frmOTP.ShowDialog();
                frmOTP.Close();
                this.Close();
            }
        }
        private void Btn_Reset_Click(object sender, EventArgs e)
        {
            btn_Register.Enabled = false;
            txt_CName.Text = "";
            txt_PhoneNo.Text = "";
            txt_Pincode.Text = "";
            txt_Email.Text = "";
        }

        #region Controls

        //---------Customer Name----------//
        private void txt_CName_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
        }

        private void txt_CName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_CName.Text))
            {
                e.Cancel = true;
                txt_CName.Focus();
                errorProvider_CName.SetError(txt_CName, "Name should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider_CName.SetError(txt_CName, "");
            }
        }

        private void txt_CName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        //--------------------------------//

        //--------Phone Number -----------//
        private void txt_PhoneNo_Leave(object sender, EventArgs e)
        {
            if (txt_PhoneNo.Text.Length < 10)
            {
                MetroMessageBox.Show(this, "", "PhoneNo must be 10 digit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_PhoneNo.Focus();

            }
        }

        private void txt_PhoneNo_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
            setButtonVisibility_Pincode();
        }

        private void txt_PhoneNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_PhoneNo.Text))
            {
                e.Cancel = true;
                txt_PhoneNo.Focus();
                errorProvider_PhoneNo.SetError(txt_PhoneNo, "PhoneNo should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider_PhoneNo.SetError(txt_PhoneNo, "");
            }

        }

        private void txt_PhoneNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        //--------------------------------//

        //--------------Email--------------//
        private void txt_Email_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Email.Text))
            {
                e.Cancel = true;
                txt_Email.Focus();
                errorProvider_Email.SetError(txt_Email, "Email should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider_Email.SetError(txt_Email, "");
            }
        }

        private void txt_Email_Leave(object sender, EventArgs e)
        {
            if (txt_Email.Text.Trim() != string.Empty)
            {
                Regex mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                if (!mRegxExpression.IsMatch(txt_Email.Text.Trim()))
                {
                    MetroMessageBox.Show(this,"", "E-mail address format is not correct.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_Email.Focus();
                }
            }
        }

        private void txt_Email_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
        }
        //--------------------------------//

        //-------------Pincode--------------//
        private void txt_Pincode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            setButtonVisibility();
        }

        private void txt_Pincode_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Pincode.Text))
            {
                e.Cancel = true;
                txt_Pincode.Focus();
                errorProvider_Pincode.SetError(txt_Pincode, "Pincode should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider_Pincode.SetError(txt_Pincode, "");
            }
        }

        private void txt_Pincode_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
            setButtonVisibility_Pincode();
        }

        private void txt_Pincode_Leave(object sender, EventArgs e)
        {
            if (txt_Pincode.Text.Length < 6)
            {
                MetroMessageBox.Show(this, "", "Pincode must be 6 digit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_Pincode.Focus();

            }
        }
        //--------------------------------//

        #endregion

        private void FrmRegistration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                    this.Close();
               
                //    btnSave_Click(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void Minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        

        private void btn_Register_MouseDown(object sender, MouseEventArgs e)
        {
            this.btn_Register.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.get_otp_2));
        }

        private void btn_Register_MouseLeave(object sender, EventArgs e)
        {
            this.btn_Register.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.get_otp_1));
        }

        private void btn_Register_MouseUp(object sender, MouseEventArgs e)
        {
            this.btn_Register.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.get_otp_1));
        }

        //reset
        private void btn_Reset_MouseDown(object sender, MouseEventArgs e)
        {
            this.btn_Reset.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.reset__3));
        }

        private void btn_Reset_MouseUp(object sender, MouseEventArgs e)
        {
            this.btn_Reset.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.reset_1));
        }
    }
}

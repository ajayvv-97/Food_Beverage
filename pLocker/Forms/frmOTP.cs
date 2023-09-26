using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;

namespace pLocker.Forms
{
    public partial class frmOTP : Form
    {
        public static string gotp = "";
        public static string macid = "";
        public static string Re_otp = "";
        public static string con = "server=148.72.216.130;database=apps_registration;uid=ayj_admin;pwd=X+vdPX$7c,6T ;";
        MySqlConnection cnn = new MySqlConnection(con);
        public frmOTP()
        {
            InitializeComponent();
        }
        #region Functions
        private void setButtonVisibility()
        {
            if ((txt_OTP.Text != String.Empty) && (txt_OTP.Text.Length == 6))
            {
                btn_ValidateOTP.Enabled = true;

            }
            else
            {
                btn_ValidateOTP.Enabled = false;

            }
        }
        static private string sendData(string MNo, string Msg)
        {
            WebRequest request = WebRequest.Create("http://www.sapteleservices.in/SMS_API/sendsms.php");
            request.Method = "POST";
            string postData = "username=nisarga&password=12345&mobile=" + MNo + "&sendername=NISRGA&message=" + Msg + "&routetype=1";//username=nisarga&password=12345&mobile=" + MNo + "&message=" + Msg + "&senderid=NISRGA";
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

        #endregion
        private void FrmOTP_Load(object sender, EventArgs e)
        {
            lbl_CName.Text = FrmRegistration.CName;
            lbl_PhoneNo.Text = FrmRegistration.PhoneNo;
            gotp = FrmRegistration.otp;
            macid = FrmRegistration.mac_id;
        }
        private void Btn_ValidateOTP_Click(object sender, EventArgs e)
        {
            if (gotp == txt_OTP.Text)
            {
                try
                {
                    string Query = "update apps_registration.appreg set actv='" + 1 + "' where ph1='" + this.lbl_PhoneNo.Text + "'and macid='" + macid + "';";
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
                    MessageBox.Show(ex.Message);
                }
                MetroMessageBox.Show(this, "", "Activated !", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                var Login = new frmLogin();
                Login.Closed += (s, args) => this.Close();
                Login.ShowDialog();
            }
            else
            {
                MetroMessageBox.Show(this, "", "Invalid OTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btn_ResendOTP_Click(object sender, EventArgs e)
        {
            txt_OTP.Text = "";

            // generate random OTP numbers //

            Random generator = new Random();
            Re_otp = generator.Next(0, 999999).ToString("D6");
            gotp = Re_otp;

            //----------------------------//

            string msg = "Dear " + lbl_CName.Text + ", " + Re_otp + " is the OTP for your miLocker Account Registration initiated on " + DateTime.Now.ToString() + " .";

            //string status = sendData(txt_PhoneNo.Text, msg);
            string status = "Successfully";

            if (status.Contains("Successfully"))
            {
                //---- To INVOKE NEW FORM----- //
                try
                {
                    string Query = "update apps_registration.appreg set gotp='" + Re_otp + "' where ph1='" + this.lbl_PhoneNo.Text + "'and macid='" + macid + "';";
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
        }
        private void Txt_OTP_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_OTP.Text))
            {
                e.Cancel = true;
                txt_OTP.Focus();
                errorProvider_otp.SetError(txt_OTP, "OTP should not be left blank!");
            }
            else
            {
                e.Cancel = false;
                errorProvider_otp.SetError(txt_OTP, "");
            }
        }
        private void Txt_OTP_TextChanged(object sender, EventArgs e)
        {
            setButtonVisibility();
        }
        private void Txt_OTP_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ------textbox accepts only digits ----//

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

            //-----------------------------------//
        }
        private void FrmOTP_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void btn_ResendOTP_MouseDown(object sender, MouseEventArgs e)
        {
            this.btn_ResendOTP.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.resend_2));
        }

        private void btn_ResendOTP_MouseUp(object sender, MouseEventArgs e)
        {
            this.btn_ResendOTP.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.resend_1));
        }

        //validate otp
        private void btn_ValidateOTP_MouseDown(object sender, MouseEventArgs e)
        {
            this.btn_ValidateOTP.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.validate_otp1));
        }

        private void btn_ValidateOTP_MouseUp(object sender, MouseEventArgs e)
        {
            this.btn_ValidateOTP.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.validate_otp));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
;       }
    }
}

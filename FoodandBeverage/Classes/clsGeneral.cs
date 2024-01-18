using BLL;
using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using CustomMessageBox;

namespace FoodandBeverage.Classes
{
    public class ClsGeneral
    {
        public void NextConrolOnEnterPress(object sender, KeyEventArgs e)
        {
            Control ctrl;
            ctrl = (Control)sender;
            if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Enter)
                if (ctrl.GetType().Name == "MetroTextBox")
                {
                    MetroTextBox mtb = (MetroTextBox)sender;
                    mtb.Name = ctrl.Name;

                    if (mtb.Multiline != true)
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }
        }

        public bool IsControlNullOrDefault(Form form, Control[] controllList)
        {
            foreach (var item in controllList)
            {
                if (item.GetType().Name == "MetroTextBox")
                {
                    MetroTextBox curTxt = (MetroTextBox)item;
                    if (string.IsNullOrWhiteSpace(curTxt.Text))
                    {
                        MessageBox.Show("\n" + curTxt.Tag.ToString() + " is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        curTxt.Focus();
                        return true;
                    }
                }
                else if (item.GetType().Name == "MetroComboBox")
                {
                    MetroComboBox curCmbBox = (MetroComboBox)item;
                    if (curCmbBox.SelectedIndex == 0)
                    {
                        MessageBox.Show("\n" + "Please select " + curCmbBox.Tag.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        curCmbBox.Focus();
                        return true;
                    }
                }
            }
            return false;
        }
        //public string FetchMacId()
        //{
        //    string macAddresses = "";

        //    foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        //    {
        //        if (nic.OperationalStatus == OperationalStatus.Up)
        //        {
        //            macAddresses += nic.GetPhysicalAddress().ToString();
        //            break;
        //        }
        //    }
        //    return macAddresses;
        //}
        public bool CheckEmailId(Form form, string EmailId)
        {
            Regex mRegxExpression;
            mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
            if (!mRegxExpression.IsMatch(EmailId.Trim()))
            {
                CMessageBox.Show("Invalid email format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool CheckGSTNo(Form form, string GSTNo)
        {
            Regex mRegxExpression;
            mRegxExpression = new Regex(@"[0-9]{2}[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[0-9]{1}[a-zA-Z]{1}[a-zA-Z1-9]{1}");
            if (!mRegxExpression.IsMatch(GSTNo.Trim()))
            {
                MessageBox.Show("Invalid GST No format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public bool CheckPANNo(Form form, string PANNo)
        {
            Regex mRegxExpression;
            mRegxExpression = new Regex(@"^([A-Z]){5}([0-9]){4}([A-Z]){1}?$");
            if (!mRegxExpression.IsMatch(PANNo.Trim()))
            {
                MessageBox.Show("Invalid PAN No format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        public void IntegerTextBox(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8))
            {
                e.Handled = true;
                return;
            }
        }
        public void PercentageTextBox(object sender, KeyPressEventArgs e)
        {
            MetroTextBox curTxt = (MetroTextBox)sender;
            Regex reg = new Regex(@"^(0|[0-9]\d{1,2})([,\.]\d{1,3})?$");
            if (char.IsControl(e.KeyChar)) return;
            //if (!reg.IsMatch(curTxt.Text.Insert(curTxt.SelectionStart, e.KeyChar.ToString()) + "1")) e.Handled = true;
        }
        public void DecimalTextBox(object sender, KeyPressEventArgs e)
        {
            MetroTextBox curTxt = (MetroTextBox)sender;
            if (Regex.IsMatch(curTxt.Text, @"\.\d\d"))
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar)
        && !char.IsDigit(e.KeyChar)
        && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as MetroTextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }
        public void DecimalTextBoxSign(object sender, KeyPressEventArgs e)
        {
            MetroTextBox curTxt = (MetroTextBox)sender;
            Regex reg = new Regex(@"^-?(0|[0-9]\d*)([,\.]\d{1,3})?$");
            if (char.IsControl(e.KeyChar)) return;


        }
        public void DecimalTextBoxSignNormalTexbox(object sender, KeyPressEventArgs e)
        {
            TextBox curTxt = (TextBox)sender;
            Regex reg = new Regex(@"^-?(0|[0-9]\d*)([,\.]\d{1,3})?$");
            if (char.IsControl(e.KeyChar)) return;
            if (!reg.IsMatch(curTxt.Text.Insert(curTxt.SelectionStart, e.KeyChar.ToString()) + "1")) e.Handled = true;

        }
        public void DecimalTextBox3digits(object sender, KeyPressEventArgs e)
        {
            MetroTextBox curTxt = (MetroTextBox)sender;
            Regex reg = new Regex(@"^(0|[0-9]\d*)([,\.]\d{1,4})?$");
            if (char.IsControl(e.KeyChar)) return;
            //if (!reg.IsMatch(curTxt.Text.Insert(curTxt.SelectionStart, e.KeyChar.ToString()) + "1")) e.Handled = true;

        }
        public void DecimalGridviewTextBox(object sender, KeyPressEventArgs e)
        {


            TextBox curTxt = (TextBox)sender;
            if (Regex.IsMatch(curTxt.Text, @"\.\d\d\d"))
            {
                e.Handled = true;
            }

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
            if (e.KeyChar == 8)
            {
                e.Handled = false;
            }
        }
        public void TelephoneTextBox(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 44 && e.KeyChar != 43)
            {
                e.Handled = true;
                return;
            }
        }

        public void ResetAllTextBoxes(Control formObject)
        {
            foreach (var c in formObject.Controls)
            {
                if (c is MetroTextBox)
                {
                    ((MetroTextBox)c).Text = String.Empty;
                }
            }
        }
        public void ResetSelectedMetroTextBoxes(MetroTextBox[] textBoxList)
        {
            foreach (var item in textBoxList)
            {
                ((MetroTextBox)item).Text = string.Empty;

            }
        }
        //public void ResetSelectedMetroDateTime(MetroDateTime[] dtpList)
        //{
        //    foreach (var item in dtpList)
        //    {
        //        ((MetroDateTime)item).Value = DateTime.Now;

        //    }
        //}
        public void ResetSelectedDateTime(DateTimePicker[] dtpList)
        {
            foreach (var item in dtpList)
            {
                ((DateTimePicker)item).Value = DateTime.Now;

            }
        }
        public void ResetAllCheckBoxes(Control formObject)
        {
            foreach (var c in formObject.Controls)
            {
                if (c is MetroCheckBox)
                {
                    ((MetroCheckBox)c).Checked = false;
                }
            }
        }
        public string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "Minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        public static Int64 Get_Gtype_ID(string lTable, string lWhr1)
        {
            Int64 GID = 0;
            if (lTable.Length > 0)
            {
                DmnSearch dmnSearch = new DmnSearch();
                BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
                dmnSearch.Select = " GTypeId,GType";
                dmnSearch.Table = " tblGType";
                dmnSearch.WhereNdGroupBy = "where GType='" + lWhr1 + "'";
                var lID = bllSearch.GetSearchData("prdSearch", dmnSearch);
                GID = Convert.ToInt64(lID[0].GTypeId);
                return GID;
            }
            return GID;
        }
        public string FetchMacId()
        {
            string macAddresses = "";

            NetworkInterface[] ifConfig = NetworkInterface.GetAllNetworkInterfaces();
            int maxHash = int.MinValue;
            foreach (NetworkInterface net in ifConfig)
            {

                if (net.Name == "Ethernet")
                {

                    maxHash = net.GetPhysicalAddress().ToString().GetHashCode();
                    macAddresses = net.GetPhysicalAddress().ToString();
                    break;
                    //MessageBox.Show(id.ToString() + "------------------" + net.Name.ToString());
                }
                else if (net.Name == "Local Area Connection")
                {
                    //  MessageBox.Show(net.Name);
                    maxHash = net.GetPhysicalAddress().ToString().GetHashCode();
                    macAddresses = net.GetPhysicalAddress().ToString();


                }
                else if (net.Name == "Ethernet 2")
                {
                    //MessageBox.Show(net.Name);
                    maxHash = net.GetPhysicalAddress().ToString().GetHashCode();
                    macAddresses = net.GetPhysicalAddress().ToString();
                }

            }
            return macAddresses;
        }
        public Boolean NameExist_UserType(string value)
        {
            if (value != "")
            {
                BllSearch<DmnUserAccountType> bllSearch = new BllSearch<DmnUserAccountType>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "UserAccountTypeId";
                objSearch.Table = " tblUserAccountType";
                objSearch.WhereNdGroupBy = " Where UserAccountType= '" + value + "'";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count >= 1)
                {
                    MessageBox.Show("User Type Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

    }
   }

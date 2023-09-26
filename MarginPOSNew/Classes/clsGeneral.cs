using BLL;
using MarginPOSNew.Interfaces;
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
namespace MarginPOSNew.Classes
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
                    MetroTextBox mtb=(MetroTextBox)sender ;
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
                        MetroMessageBox.Show(form, "\n"+curTxt.Tag.ToString() + " is empty","Error" ,MessageBoxButtons.OK, MessageBoxIcon.Error,150);
                        curTxt.Focus();
                        return true;
                    }
                }
                else if (item.GetType().Name == "MetroComboBox")
                {
                    MetroComboBox curCmbBox = (MetroComboBox)item;
                    if (curCmbBox.SelectedIndex==0)
                    {
                        MetroMessageBox.Show(form,"\n"+ "Please select " + curCmbBox.Tag.ToString(), "Error",MessageBoxButtons.OK,MessageBoxIcon.Error,150);
                        curCmbBox.Focus();
                        return true;
                    }
                }
            }
            return false;
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
        public bool CheckEmailId(Form form,string EmailId)
        {
            Regex mRegxExpression;
            mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
            if (!mRegxExpression.IsMatch(EmailId.Trim()))
            {
                MetroMessageBox.Show(form,"Invalid email format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
                MetroMessageBox.Show(form, "Invalid GST No format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
                MetroMessageBox.Show(form, "Invalid PAN No format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
            if (!reg.IsMatch(curTxt.Text.Insert(curTxt.SelectionStart, e.KeyChar.ToString()) + "1")) e.Handled = true;
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
            if (!reg.IsMatch(curTxt.Text.Insert(curTxt.SelectionStart, e.KeyChar.ToString()) + "1")) e.Handled = true;

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
            if (!reg.IsMatch(curTxt.Text.Insert(curTxt.SelectionStart, e.KeyChar.ToString()) + "1")) e.Handled = true;

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
        public string DateTimeConversion(string datetimeString, DateTimeReturnType returnType)
        {
            DateTime newDate = Convert.ToDateTime(datetimeString);

            string returnvalue = string.Empty;
            switch (returnType)
            {
                case DateTimeReturnType.Date:
                    return newDate.Date.ToShortDateString();
                case DateTimeReturnType.Time:
                    return newDate.TimeOfDay.ToString();
                case DateTimeReturnType.DateTime:
                    return newDate.ToString();
                default:
                    return "error";
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
        public void ResetSelectedMetroDateTime(MetroDateTime[] dtpList)
        {
            foreach (var item in dtpList)
            {
                ((MetroDateTime)item).Value = DateTime.Now;

            }
        }
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
        public static Int64 Get_Gtype_ID(string lTable,string lWhr1)
        {
            Int64 GID = 0;
            if (lTable.Length >0)
            {
                DmnSearch dmnSearch = new DmnSearch();
                BllSearch<DmnGeneralOptions> bllSearch = new BllSearch<DmnGeneralOptions>();
                dmnSearch.Select = " GTypeId,GType";
                dmnSearch.Table = " tblGType";
                dmnSearch.WhereNdGroupBy = "where GType='"+ lWhr1 +"'";
                var lID= bllSearch.GetSearchData("prdSearch", dmnSearch);

                GID = Convert.ToInt64(lID[0].GTypeId);
                return GID;
            }
            return GID;
        }


        public bool CheckPINNo(Form form, string PINNo)
        {
            Regex mRegxExpression;
            mRegxExpression = new Regex(@"^\d{4}(?:\d{2})?$");
            if (!mRegxExpression.IsMatch(PINNo.Trim()))
            {
                MetroMessageBox.Show(form, "Invalid PIN No format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }
            return true;
        }

    }

}

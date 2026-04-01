using BLL;
using MetroFramework.Controls;
using System;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Domain;
using CustomMessageBox;
using System.IO;
using OfficeOpenXml;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Collections.Generic;
using FoodandBeverage.Interfaces;

namespace FoodandBeverage.Classes
{
    public class ClsGeneral
    {
        //public static string BugReportFileExcel = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\BugReport\BugReport.xlsx";
        //public static string BugReportFile = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\BugReport\BugReport.txt";
        ////public static string BugReportFileExcel = @"C:\Users\ajay\OneDrive\Desktop\Food_Beverage\FoodandBeverage\BugReport\BugReport.xlsx";
        ////public static string BugReportFile = @"C:\Users\ajay\OneDrive\Desktop\Food_Beverage\FoodandBeverage\BugReport\BugReport.txt";
        public static string BugReportFileExcel = Application.StartupPath + @"\BugReport\BugReport.xlsx";
        public static string BugReportFile = Application.StartupPath + @"\BugReport\BugReport.txt";

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
        public static void ReleaseMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void DisposeTableLayoutPanel(TableLayoutPanel tlp)
        {
            if (tlp == null) return;

            foreach (Control ctrl in tlp.Controls)
            {
                if (ctrl is PictureBox pb)
                    pb.Image?.Dispose();

                ctrl.Dispose();
            }

            tlp.Controls.Clear();
            tlp.Dispose();
        }
        public int CheckButtonPermission(int UserType,int Form)
        {
            try
            {
                BllSearch<DmnForms> bllacctype = new BllSearch<DmnForms>();
                DmnSearch dmnforms = new DmnSearch();
                dmnforms.Select = "*";
                dmnforms.Table = " tblforms ";
                dmnforms.WhereNdGroupBy = " where UserType=" + UserType + " and Status=1 and Form=" + Form;
                dmnforms.OrderBy = "order by Form";
                var gridesource = bllacctype.GetSearchData("prdSearch", dmnforms);
                if (gridesource.Count > 0)
                {
                    if (gridesource[0].Write == 1)
                    {
                        return Convert.ToInt32(PermissionLevel.FullPermission); // Full permission
                    }
                    else if (gridesource[0].Write == 1)
                    {
                        return Convert.ToInt32(PermissionLevel.ReadOnly); // Read-only permission
                    }
                }
                return Convert.ToInt32(PermissionLevel.NoPermission);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // LogException("DashBoard page ChkWritePermission()", ex, FrmLogin.EmployeName, frmLogin.Designation, Convert.ToInt32(formsenum.DashBoard));
                return Convert.ToInt32(PermissionLevel.NoPermission);
            }

        }

        private void ClearList<T>(List<T> list)
        {
            if (list == null) return;
            foreach (var item in list)
                if (item is IDisposable disposable)
                    disposable.Dispose();
            list.Clear();
        }

        public void ApplyRoundedCorners(Control control, float bd)
        {
            try
            {
                float borderRadius = bd + 20;

                if (control == null) return;

                GraphicsPath path = new GraphicsPath();
                RectangleF rect = new RectangleF(control.ClientRectangle.X, control.ClientRectangle.Y, control.ClientRectangle.Width, control.ClientRectangle.Height);

                path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
                path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
                path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
                path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
                path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
                path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
                path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
                path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
                path.CloseFigure();

                control.Region = new Region(path);
            }
            catch (Exception ex)
            {

            }
        }
        public void Memoryrelease()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        //public void LogExceptionExcel(string page, Exception ex, string userName, string designation, int formtypeid)
        //{
        //    try
        //    {
        //        XLWorkbook workbook;
        //        IXLWorksheet worksheet;

        //        // Check if the file exists
        //        if (!File.Exists(BugReportFileExcel))
        //        {
        //            // Create a new workbook and add a worksheet
        //            workbook = new XLWorkbook();
        //            worksheet = workbook.Worksheets.Add("BugReport");

        //            // Add headers
        //            worksheet.Cell(1, 1).Value = "Date and Time";
        //            worksheet.Cell(1, 2).Value = "Page";
        //            worksheet.Cell(1, 3).Value = "Exception";
        //            worksheet.Cell(1, 4).Value = "User Name";
        //            worksheet.Cell(1, 5).Value = "Designation";
        //            worksheet.Cell(1, 6).Value = "Form Type";
        //        }
        //        else
        //        {
        //            // Load existing workbook
        //            workbook = new XLWorkbook(BugReportFileExcel);

        //            // Check if the worksheet exists, create it if it doesn't
        //            if (!workbook.Worksheets.TryGetWorksheet("BugReport", out worksheet))
        //            {
        //                worksheet = workbook.Worksheets.Add("BugReport");

        //                // Add headers
        //                worksheet.Cell(1, 1).Value = "Date and Time";
        //                worksheet.Cell(1, 2).Value = "Page";
        //                worksheet.Cell(1, 3).Value = "Exception";
        //                worksheet.Cell(1, 4).Value = "User Name";
        //                worksheet.Cell(1, 5).Value = "Designation";
        //                worksheet.Cell(1, 6).Value = "Form Type";
        //            }
        //        }

        //        // Determine the next empty row
        //        var newRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 2;

        //        // Insert new data
        //        worksheet.Cell(newRow, 1).Value = DateTime.Now.ToString();
        //        worksheet.Cell(newRow, 2).Value = page;
        //        worksheet.Cell(newRow, 3).Value = ex.ToString(); // Adjust if you need less verbose information
        //        worksheet.Cell(newRow, 4).Value = userName;
        //        worksheet.Cell(newRow, 5).Value = designation;
        //        worksheet.Cell(newRow, 6).Value = formtypeid;

        //        // Save the workbook
        //        workbook.SaveAs(BugReportFileExcel);
        //        MessageBox.Show("Exception logged successfully");
        //    }
        //    catch (IOException ioEx)
        //    {
        //        MessageBox.Show("File is currently in use or locked by another process: " + ioEx.Message);
        //    }
        //    catch (UnauthorizedAccessException uaEx)
        //    {
        //        MessageBox.Show("You do not have permission to write to this file: " + uaEx.Message);
        //    }
        //    catch (Exception logEx)
        //    {
        //        MessageBox.Show("An error occurred while logging the exception: " + logEx.Message);
        //    }
        //}
        public void LogExceptionExcel(string page, Exception ex, string userName, string designation, int formTypeId)
        {
            
        }

        public  void LogException(string page, Exception ex,string userName, string designation,int Formtypeid)
        {
            try
            {
                //#region Excel Write Log
                //try
                //{
                //    FileInfo fileInfo = new FileInfo(BugReportFileExcel);
                //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                //    using (ExcelPackage package = new ExcelPackage(fileInfo))
                //    {
                //        ExcelWorksheet worksheet = package.Workbook.Worksheets.Count > 0
                //            ? package.Workbook.Worksheets[0]
                //            : package.Workbook.Worksheets.Add("BugReports");

                //        int row = worksheet.Dimension?.Rows + 1 ?? 1;

                //        if (row == 1)
                //        {
                //            worksheet.Cells[row, 1].Value = "Date and Time";
                //            worksheet.Cells[row, 2].Value = "Page";
                //            worksheet.Cells[row, 3].Value = "Exception";
                //            worksheet.Cells[row, 4].Value = "User Name";
                //            worksheet.Cells[row, 5].Value = "Designation";
                //            worksheet.Cells[row, 6].Value = "FormTypeId";
                //            row++;
                //        }

                //        worksheet.Cells[row, 1].Value = DateTime.Now.ToString();
                //        worksheet.Cells[row, 2].Value = page;
                //        worksheet.Cells[row, 3].Value = ex.Message;
                //        worksheet.Cells[row, 4].Value = userName;
                //        worksheet.Cells[row, 5].Value = designation;
                //        worksheet.Cells[row, 6].Value = Formtypeid;

                //        package.Save();
                //    }
                //}
                //catch (Exception logEx)
                //{
                //    CMessageBox.Show("An error occurred while logging the exception: " + logEx.Message);
                //}
                //#endregion

                #region  Text file Write Log
                using (StreamWriter sw = new StreamWriter(BugReportFile, true))
                {
                    sw.WriteLine("Date and Time: " + DateTime.Now.ToString());
                    sw.WriteLine("Page : "+ page);
                    sw.WriteLine("Exception: " +Convert.ToString((ex.Message)));
                    sw.WriteLine("User Name: " + userName);
                    sw.WriteLine("Designation: " + designation);
                    sw.WriteLine("FormTypeId: " + Formtypeid);
                    sw.WriteLine("-------------------------------------------------------------------------");
                }
                #endregion
            }
            catch (Exception logEx)
            {
                //CMessageBox.Show("An error occurred while logging the exception: " + logEx.Message);
            }
        }
        public void GridAutoSizeSmart(DataGridView dgv)
        {
            try
            {
                if (dgv.Columns.Count == 0) return;

                // Step 1: Set all columns to AutoSize = AllCells (except checkbox)
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col is DataGridViewCheckBoxColumn)
                        continue;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }

                //dgv.PerformLayout(); // Important to calculate widths

                //// Step 2: Check remaining free width
                //int usedWidth = dgv.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                //int freeWidth = dgv.ClientSize.Width - usedWidth;

                //// Step 3: If free space exists, give it to the second column (Index = 1)

                //    foreach (DataGridViewColumn col in dgv.Columns)
                //        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                    // Give all extra space ONLY to second column
                    if (dgv.Columns.Count > 1)
                    {
                        dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                
            }
            catch 
            { 
            }
        }

        public void GrideAutoSize(DataGridView dataGridView)
        {
            try
            {
                DataGridView gridView = new DataGridView();
                foreach (DataGridViewColumn col in dataGridView.Columns)
                {
                    if (col.GetType() != typeof(DataGridViewCheckBoxColumn))
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                }

                int columnCount = dataGridView.Columns.Count;
                int availableWidth = dataGridView.ClientSize.Width - dataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible);
                if (availableWidth > 0)
                {
                    foreach (DataGridViewColumn col in dataGridView.Columns)
                    {
                        if (col.Index != 0 && col.Frozen != true)
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
                else
                {
                    foreach (DataGridViewColumn col in dataGridView.Columns)
                    {
                        if (col.Index != 0)
                            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                }
            }
            catch (Exception ex)
            {

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
                        CMessageBox.Show("\n" + curTxt.Tag.ToString() + " is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        curTxt.Focus();
                        return true;
                    }
                }
                else if (item.GetType().Name == "MetroComboBox")
                {
                    MetroComboBox curCmbBox = (MetroComboBox)item;
                    if (curCmbBox.SelectedIndex == 0)
                    {
                        CMessageBox.Show("\n" + "Please select " + curCmbBox.Tag.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show("Invalid email format!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                CMessageBox.Show("Invalid GST No format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show("Invalid PAN No format!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    CMessageBox.Show("User Type Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

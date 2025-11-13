using FoodandBeverage.Forms;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Classes
{
    public static class AppSession
    {
        private static readonly Dictionary<int, AutoLogoutManager> activeLogouts = new Dictionary<int, AutoLogoutManager>();

        private static string _currentFormName = string.Empty;
        public static DateTime LastActivityTime { get; private set; } = DateTime.Now;
        public static int CurrentFormNo { get; private set; } = 0;
        public static string CurrentFormName
        {
            get => _currentFormName;
            private set
            {
                _currentFormName = value;
                LastActivityTime = DateTime.Now;
            }
        }

        public static void RegisterForm(Form form, int formNo)
        {
            try
            {
                if (form == null)
                    return;

                foreach (var kvp in activeLogouts.Values)
                {
                    kvp.Stop();
                    (kvp as IDisposable)?.Dispose();
                }

                CurrentFormNo = formNo;
                CurrentFormName = GetFormNameFromEnum(formNo);

                FrmDashboard frmDash = Application.OpenForms.OfType<FrmDashboard>().FirstOrDefault();
                if (frmDash != null)
                {
                    frmDash.lblinactivitytme.Text = ((LogoutFormList)CurrentFormNo).ToString();
                }
                bool isLogoutEnabled = false;
                //if (CurrentFormNo == Convert.ToInt32(LogoutFormList.DashboardCusAccount) ||
                //   CurrentFormNo == Convert.ToInt32(LogoutFormList.DataGridView) ||
                //   CurrentFormNo == Convert.ToInt32(LogoutFormList.ExtraAccessChk) ||
                //   CurrentFormNo == Convert.ToInt32(LogoutFormList.Keyboard) ||
                //   CurrentFormNo == Convert.ToInt32(LogoutFormList.Keypad) ||
                //   CurrentFormNo == Convert.ToInt32(LogoutFormList.KeypadDashboard) ||
                //   CurrentFormNo == Convert.ToInt32(LogoutFormList.NumdisKeypad) ||
                //   CurrentFormNo == Convert.ToInt32(LogoutFormList.Search))
                //    {
                //        isLogoutEnabled = FrmDashboard.logoutformsno.Split(',').Select(x => x.Trim()).Any(x => x == LogoutFormList.Commonpage.ToString());
                //    }
                //    else
                isLogoutEnabled = FrmDashboard.logoutformsno.Split(',').Select(x => x.Trim()).Any(x => x == formNo.ToString());

                if (isLogoutEnabled)
                {
                    if (!activeLogouts.ContainsKey(formNo))
                        activeLogouts[formNo] = new AutoLogoutManager();

                    var lastForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Tag != null && f.Tag.ToString() == formNo.ToString());

                    if (lastForm != null)
                        activeLogouts[formNo].Start(lastForm);

                }
                else
                {
                    activeLogouts.Clear();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void UnregisterForm(Form form)
        {
            try
            {

                if (form == null) return;

                int formNo = form.Tag is int tagInt ? tagInt :
                             int.TryParse(form.Tag?.ToString(), out int parsed) ? parsed : 0;

                if (activeLogouts.TryGetValue(formNo, out var manager))
                {
                    manager.Stop();
                    activeLogouts.Remove(formNo);
                }

                if (activeLogouts.Count == 0 || activeLogouts.Count == 1)
                {
                    Clear();
                    return;
                }
                int lastFormNo = activeLogouts.Keys.Last();
                CurrentFormNo = lastFormNo;
                CurrentFormName = GetFormNameFromEnum(lastFormNo);
                FrmDashboard frmDash = Application.OpenForms.OfType<FrmDashboard>().FirstOrDefault();
                if (frmDash != null)
                {
                    frmDash.lblinactivitytme.Text = ((LogoutFormList)CurrentFormNo).ToString();
                }

                var lastForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Tag != null && f.Tag.ToString() == lastFormNo.ToString());
                if (lastForm != null)
                    activeLogouts[lastFormNo].Start(lastForm);
            }
            catch (Exception ex)
            {

            }
        }

        private static string GetFormNameFromEnum(int formNo)
        {
            try
            {
                switch ((LogoutFormNameuse)formNo)
                {
                    case LogoutFormNameuse.Order: return "FrmOrder";
                    case LogoutFormNameuse.Kitchen_View: return "FrmKitchenView";
                    case LogoutFormNameuse.Category: return "FrmTICategory";
                    case LogoutFormNameuse.Item: return "FrmItemRegistation";
                    case LogoutFormNameuse.Product: return "FrmIngredient";
                    case LogoutFormNameuse.Menu: return "FrmMenu";
                    case LogoutFormNameuse.Unit: return "FrmUnit";
                    case LogoutFormNameuse.Table: return "FrmTable";
                    case LogoutFormNameuse.Purchase: return "FrmPurchase";
                    case LogoutFormNameuse.Purchase_Return: return "FrmPurchaseReturn";
                    case LogoutFormNameuse.Tax: return "FrmTax";
                    case LogoutFormNameuse.Vendor: return "FrmVendor";
                    case LogoutFormNameuse.Discount_And_Offer: return "FrmOfferDiscount";
                    case LogoutFormNameuse.Expence: return "FrmExpence";
                    case LogoutFormNameuse.PromoCode: return "FrmPromo";
                    case LogoutFormNameuse.Company: return "FrmCompany";
                    case LogoutFormNameuse.Reservation: return "FrmReservation";
                    case LogoutFormNameuse.Employee: return "FrmEmployeeShort";
                    case LogoutFormNameuse.Department: return "FrmDepartment";
                    case LogoutFormNameuse.Designation: return "FrmDesignation";
                    case LogoutFormNameuse.User_Account: return "FrmUserAccount";
                    case LogoutFormNameuse.User_Permission: return "FrmUserPermission";
                    case LogoutFormNameuse.OrderView: return "FrmOrderView";
                    case LogoutFormNameuse.Addon: return "FrmAddon";
                    case LogoutFormNameuse.Login: return "FrmLogin";
                    case LogoutFormNameuse.homepage: return "FrmDashboard";
                    case LogoutFormNameuse.company: return "FrmCompany";
                    case LogoutFormNameuse.Keypad: return "FrmKeypad";
                    case LogoutFormNameuse.DashboardChart: return "FrmDashboardReport";
                    case LogoutFormNameuse.TableView: return "FrmTableView";
                    case LogoutFormNameuse.Report: return "FrmReport";
                    case LogoutFormNameuse.CombinedBillpage: return "FrmCombinedBill";
                    case LogoutFormNameuse.Pickuppage: return "FrmPickup";
                    case LogoutFormNameuse.YZout: return "FrmZOut";
                    case LogoutFormNameuse.DataGridView: return "FrmDataGridView";
                    case LogoutFormNameuse.Keyboard: return "FrmKeyboard";
                    case LogoutFormNameuse.NumdisKeypad: return "FrmNumdiskeypad";
                    case LogoutFormNameuse.Search: return "FrmSearch";
                    case LogoutFormNameuse.SaleBill: return "FrmSaleBill";
                    default: return "UnknownForm";
                }
            }
            catch
            {
                return "UnknownForm";
            }
        }

        private static LogoutFormList GetFormEnumValue(string formName)
        {
            return Enum.TryParse(formName.Replace("Frm", ""), out LogoutFormList value)
                ? value
                : LogoutFormList.Home;
        }

        public static void Clear()
        {
            try
            {
                foreach (var kvp in activeLogouts.Values)
                {
                    kvp.Stop();
                    (kvp as IDisposable)?.Dispose();
                }

                activeLogouts.Clear();
                CurrentFormNo = 0;
                CurrentFormName = string.Empty;

                bool isDashboardEnabled = FrmDashboard.logoutformsno
                    .Split(',')
                    .Select(x => x.Trim())
                    .Any(x => x == ((int)LogoutFormList.Home).ToString());

                FrmDashboard frmDash = Application.OpenForms.OfType<FrmDashboard>().FirstOrDefault();

                if (isDashboardEnabled && frmDash != null && !frmDash.IsDisposed && !FrmDashboard.Logout_zout_status)
                {
                    CurrentFormNo = (int)LogoutFormList.Home;
                    CurrentFormName = "FrmDashboard";
                    if (frmDash != null)
                    {
                        frmDash.lblinactivitytme.Text = ((LogoutFormList)CurrentFormNo).ToString();
                    }
                    activeLogouts[(int)LogoutFormList.Home] = new AutoLogoutManager();
                    activeLogouts[(int)LogoutFormList.Home].Start(frmDash);
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}

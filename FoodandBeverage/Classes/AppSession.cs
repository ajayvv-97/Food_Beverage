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
                switch (formNo)
                {
                    case (int)LogoutFormList.Dashboard: return "FrmDashboard";
                    case (int)LogoutFormList.Admin: return "FrmAdmin";
                    case (int)LogoutFormList.Recharge: return "FrmRecharge";
                    case (int)LogoutFormList.Redemption: return "FrmRedemption";
                    case (int)LogoutFormList.CardTransfer: return "FrmCardTransaction";
                    case (int)LogoutFormList.ClearCard: return "FrmClearCard";
                    case (int)LogoutFormList.CustAccount: return "FrmCustAccount";
                    case (int)LogoutFormList.FamilyCard: return "FrmFamilyCard";
                    case (int)LogoutFormList.GroupSale: return "FrmGroupSale";
                    case (int)LogoutFormList.Refund: return "FrmRefund";
                    case (int)LogoutFormList.ThirdPartyCardTransfer: return "FrmThirdPartyCardTransfer";
                    case (int)LogoutFormList.Ticket: return "FrmTicket";
                    case (int)LogoutFormList.TMPanel: return "FrmTMpanel";
                    case (int)LogoutFormList.VoidSale: return "FrmVoidSale";
                    case (int)LogoutFormList.Profile: return "FrmProfile";
                    case (int)LogoutFormList.GeneralConfig: return "FrmGeneralConf";
                    case (int)LogoutFormList.Offer: return "FrmOffer";
                    case (int)LogoutFormList.CardConsolidation: return "FrmCardConsolidation";
                    case (int)LogoutFormList.DashboardCusAccount: return "FrmDashboardCusAccount";
                    case (int)LogoutFormList.DataGridView: return "FrmDataGridView";
                    case (int)LogoutFormList.ExtraAccessChk: return "FrmExtraAccessChk";
                    case (int)LogoutFormList.Keyboard: return "FrmKeyboard";
                    case (int)LogoutFormList.Keypad: return "FrmKeypad";
                    case (int)LogoutFormList.KeypadDashboard: return "FrmKeypadDashboard";
                    case (int)LogoutFormList.NumdisKeypad: return "FrmNumdisKeypad";
                    case (int)LogoutFormList.Search: return "FrmSearch";
                    case (int)LogoutFormList.Complementary: return "FrmComplementary";
                    case (int)LogoutFormList.RequestPoint: return "FrmRequestPoint";
                    default: return "UnknownForm";
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private static LogoutFormList GetFormEnumValue(string formName)
        {
            return Enum.TryParse(formName.Replace("Frm", ""), out LogoutFormList value)
                ? value
                : LogoutFormList.Dashboard;
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
                    .Any(x => x == ((int)LogoutFormList.Dashboard).ToString());

                FrmDashboard frmDash = Application.OpenForms.OfType<FrmDashboard>().FirstOrDefault();

                if (isDashboardEnabled && frmDash != null && !frmDash.IsDisposed && !FrmDashboard.Logout_zout_status)
                {
                    CurrentFormNo = (int)LogoutFormList.Dashboard;
                    CurrentFormName = "FrmDashboard";
                    if (frmDash != null)
                    {
                        frmDash.lblinactivitytme.Text = ((LogoutFormList)CurrentFormNo).ToString();
                    }
                    activeLogouts[(int)LogoutFormList.Dashboard] = new AutoLogoutManager();
                    activeLogouts[(int)LogoutFormList.Dashboard].Start(frmDash);
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}

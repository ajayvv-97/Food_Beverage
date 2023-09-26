using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using System.Configuration;
namespace Game.Forms
{
    public partial class FrmOffer : Form
    {
        public FrmOffer()
        {
            InitializeComponent();
        }
        public const int igame = 0;
        public const int iOfferrate = 1;
        public const int iDiscrate = 2;
        public const int iDiscPer = 3;
        public const int iVIPOfferrate = 4;
        public const int iVIPDiscrate = 5;
        public const int iVIPDiscPer = 6;
        public const int igmid = 7;

        private void FrmOffer_Load(object sender, EventArgs e)
        {
            loadgame();
        }
        public void loadgame()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "ID,mGame";
            objSearch.Table = "tblGame g";
            objSearch.WhereNdGroupBy = "Where g.Status=1 ";
            objSearch.OrderBy = "Order By ID";
            var item = bllSearch.GetSearchData("prdComboFill", objSearch);
            this.cmbgame.DisplayMember = "Value";
            this.cmbgame.ValueMember = "Id";
            this.cmbgame.DataSource = item;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DmnGameOffer dmnGameOffer = new DmnGameOffer();
                for (int i = 0; i < 8; i++)
                {
                    dmnGameOffer.OfferDesc = txttitle.Text;
                    dmnGameOffer.fDt = dtpfrom.Value;
                    dmnGameOffer.tDt = dtpto.Value;
                    dmnGameOffer.gmid = Convert.ToInt32(dgvoffer.Rows[i].Cells[igmid].Value);
                    dmnGameOffer.offerRate = Convert.ToDecimal(dgvoffer.Rows[i].Cells[iOfferrate].Value);
                    dmnGameOffer.DiscRate = Convert.ToDecimal(dgvoffer.Rows[i].Cells[iDiscrate].Value);
                    dmnGameOffer.DiscPer = Convert.ToDecimal(dgvoffer.Rows[i].Cells[iDiscPer].Value);
                    dmnGameOffer.VIPOfferRate = Convert.ToDecimal(dgvoffer.Rows[i].Cells[iVIPOfferrate].Value);
                    dmnGameOffer.VIPDiscRate = Convert.ToDecimal(dgvoffer.Rows[i].Cells[iVIPDiscrate].Value);
                    dmnGameOffer.VIPDiscPer = Convert.ToDecimal(dgvoffer.Rows[i].Cells[iVIPDiscPer].Value);
                    dmnGameOffer.status = 1;
                    dmnGameOffer.BranchId = 1;
                    dmnGameOffer.CreatedBy = 1;
                    BllGameOffer bllGameOffer = new BllGameOffer();
                    bool insertstatus = bllGameOffer.AddOffer("prdInsertGameOffer", dmnGameOffer);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvoffer_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is ComboBox)//this region is for unit editing
            {
                int colIndex = dgvoffer.CurrentCell.ColumnIndex;
                //ComboBox cmb = e.Control as ComboBox;
                //if (cmb != null)
                //{
                //    cmb.SelectedIndex = 0;
                //}

                if (igame == colIndex)
                {

                    #region method 1
                    ComboBox comboBox = e.Control as ComboBox;
                    comboBox.SelectedIndexChanged -= LastColumnComboSelectionChanged;
                    comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
                    #endregion
                }
            }
        }

        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var sendingCB = sender as DataGridViewComboBoxEditingControl;
                var currentcell = dgvoffer.CurrentCellAddress;
                int cmbSelectedIndex = sendingCB.SelectedIndex;
                int CurrentIndex = dgvoffer.CurrentRow.Index;
                int curCel = dgvoffer.CurrentCell.ColumnIndex;

                if (curCel == igame)
                {
                    DataGridViewComboBoxCell cel = (DataGridViewComboBoxCell)dgvoffer.Rows[currentcell.Y].Cells[igame];
                    cel.Value = sendingCB.EditingControlFormattedValue.ToString();

                    if (cmbSelectedIndex != -1)
                    {

                        var cmbValue = ((ComboBox)sender).SelectedValue;
                        if (cmbValue != null)
                        {
                            if (cmbValue is long)
                            {

                            }
                            else
                            {
                                string arr = cmbValue.ToString();
                                string[] values = arr.Split(',');
                                string value = values[0].ToString();
                                string[] unit = value.Split('=');
                                int lgnth = unit.Length;
                                if (lgnth > 1)
                                {
                                    long id = Convert.ToInt32(unit[1].ToString());
                                    cmbValue = id;
                                }

                            }
                            int selectedUnitId = Convert.ToInt32(cmbValue);
                            //long selectedItemId = Convert.ToInt64(dgvoffer.Rows[CurrentIndex].Cells[igame].Value);
                            dgvoffer.Rows[CurrentIndex].Cells[igmid].Value = selectedUnitId;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvoffer_KeyUp(object sender, KeyEventArgs e)
        {
            int c = dgvoffer.CurrentCell.ColumnIndex;
            if (c == iVIPDiscPer)
            {
                //  dgvoffer.Rows.Add(1);
            }
        }

        public void fillgrid()
        {
            DateTime from = dtpfrom.Value;
            DateTime to = dtpto.Value;
            dgvoffer.Rows.Clear();
            BllSearch<DmnGameOffer> bllSearch = new BllSearch<DmnGameOffer>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "g.mGame,gh.htitle,gen.Caption,gh.fTime,gh.tTime,gh.gDur,gh.CardRate,gh.CardDiscRate,gh.CardDiscper,gh.VIPRate,gh.VIPDiscRate,gh.VIPDiscper,gh.Dayid ";
            objSearch.Table = "tblGameHHSett gh left join tblGame g on g.ID = gh.gmid left join tblGeneralOptions gen on gen.GTypeSubId = gh.Dayid";
            objSearch.WhereNdGroupBy = "Where gen.GTypeId=19 and gh.gmid=";
            objSearch.OrderBy = "Order By gen.GTypeSubId";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
        }
    }
}

using BLL;
using Domain;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Forms
{
    public partial class FrmHappyHours : Form
    {
       // public const int ichk = 0;
         public const int  iday=0 ;
        public const int ifrom = 1;
        public const int ito = 2;
        public const int idur = 3;
        public const int icardrate = 4;
        public const int idiscrate = 5;
        public const int idiscper = 6;
        public const int iviprate = 7;
        public const int ivipdiscrate = 8;
        public const int ivipdiscper = 9;
        public const int idayid = 10;

        public FrmHappyHours()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmHappyHours_Load(object sender, EventArgs e)
        {
            loadgame();
            txttitle.Focus();
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
            cmbgame.DataSource = item;
            cmbgame.DisplayMember = "Value";
            cmbgame.ValueMember = "Id";
            cmbgame.Text = "";
        }
        public void fillgrid()
        {
            dgvList.Rows.Clear();
            int id = Convert.ToInt32(cmbgame.SelectedValue);

            BllSearch<DmnHappyHours> bllSearch = new BllSearch<DmnHappyHours>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "g.mGame,gh.htitle,gen.Caption,gh.fTime,gh.tTime,gh.gDur,gh.CardRate,gh.CardDiscRate,gh.CardDiscper,gh.VIPRate,gh.VIPDiscRate,gh.VIPDiscper,gh.Dayid ";
            objSearch.Table = "tblGameHHSett gh left join tblGame g on g.ID = gh.gmid left join tblGeneralOptions gen on gen.GTypeSubId = gh.Dayid";
            objSearch.WhereNdGroupBy = "Where gen.GTypeId=19 and gh.gmid=" + id;
            objSearch.OrderBy = "Order By gen.GTypeSubId";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            dgvList.Rows.Add(8);
            if (itemSource.Count == 0)
            {

                BllSearch<DmnGame> bllSearch1 = new BllSearch<DmnGame>();
                DmnSearch objSearch1 = new DmnSearch();
                objSearch1.Select = "gm.CardRate,VIPRate,g.Caption,g.GTypeSubId  ";
                objSearch1.Table = "tblGame gm left join tblGeneralOptions  g on g.GTypeId=19";
                objSearch1.WhereNdGroupBy = "Where gm.status=1 and gm.Id=" + id;
                objSearch1.OrderBy = "Order By ID";
                var itemSource1 = bllSearch1.GetSearchData("prdSearch", objSearch1);
                
                for (int i = 0; i < 8; ++i)
                {
                    dgvList.Rows[i].Cells[icardrate].Value = itemSource1[i].CardRate;
                    dgvList.Rows[i].Cells[iviprate].Value = itemSource1[i].viprate;
                    dgvList.Rows[i].Cells[iday].Value = itemSource1[i].Caption.ToString();
                    dgvList.Rows[i].Cells[idayid].Value = itemSource1[i].GTypeSubId;

                }
            }
            else
            {
                for (int i = 0; i < 8; ++i)
                {
                    dgvList.Rows[i].Cells[iday].Value = itemSource[i].Caption.ToString();
                    dgvList.Rows[i].Cells[ifrom].Value = itemSource[i].ftime;
                    dgvList.Rows[i].Cells[ito].Value = itemSource[i].ttime;                   
                    dgvList.Rows[i].Cells[idur].Value = itemSource[i].gDur;
                    dgvList.Rows[i].Cells[icardrate].Value = itemSource[i].cardrate;
                    dgvList.Rows[i].Cells[idiscrate].Value = itemSource[i].CardDiscRate;
                    dgvList.Rows[i].Cells[idiscper].Value = itemSource[i].CardDiscPer;
                    dgvList.Rows[i].Cells[iviprate].Value = itemSource[i].VIPRate;
                    dgvList.Rows[i].Cells[ivipdiscrate].Value = itemSource[i].VIPDiscRate;
                    dgvList.Rows[i].Cells[ivipdiscper].Value = itemSource[i].VIPDiscPer;
                    dgvList.Rows[i].Cells[idayid].Value = itemSource[i].Dayid;


                }
                txttitle.Text = itemSource[0].htitle.ToString();
            }
        }     
        private void cmbgame_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fillgrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BllSearch<DmnHappyHours> bllSearch = new BllSearch<DmnHappyHours>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "g.mGame,gh.htitle,gen.Caption,gh.fTime,gh.tTime,gh.gDur,gh.CardRate,gh.CardDiscRate,gh.CardDiscper,gh.VIPRate,gh.VIPDiscRate,gh.VIPDiscper ";
            objSearch.Table = "tblGameHHSett gh left join tblGame g on g.ID = gh.gmid left join tblGeneralOptions gen on gen.GTypeSubId = gh.Dayid";
            objSearch.WhereNdGroupBy = "Where gen.GTypeId=19 and gh.gmid=" + Convert.ToInt32(cmbgame.SelectedValue);
            objSearch.OrderBy = "Order By gen.GTypeSubId";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            DmnHappyHours dmnHappyHours = new DmnHappyHours();
            if (itemSource.Count>0)
            {
                dmnHappyHours.flage = 1;
            }
            else
            {
                dmnHappyHours.flage = 0;
            }
            for (int i = 0; i < 8; i++)
            {
               
                dmnHappyHours.gmid = Convert.ToInt32(cmbgame.SelectedValue);
                dmnHappyHours.htitle = txttitle.Text;
                dmnHappyHours.Dayid= Convert.ToInt32(dgvList.Rows[i].Cells[idayid].Value);
                dmnHappyHours.ftime = Convert.ToDecimal(dgvList.Rows[i].Cells[ifrom].Value);
                dmnHappyHours.ttime = Convert.ToDecimal(dgvList.Rows[i].Cells[ito].Value);
                dmnHappyHours.gDur = Convert.ToInt32(dgvList.Rows[i].Cells[idur].Value);
                dmnHappyHours.cardrate = Convert.ToDecimal(dgvList.Rows[i].Cells[icardrate].Value);
                dmnHappyHours.CardDiscRate = Convert.ToDecimal(dgvList.Rows[i].Cells[idiscrate].Value);
                dmnHappyHours.CardDiscPer = Convert.ToDecimal(dgvList.Rows[i].Cells[idiscper].Value);
                dmnHappyHours.VIPRate = Convert.ToDecimal(dgvList.Rows[i].Cells[iviprate].Value);
                dmnHappyHours.VIPDiscRate = Convert.ToDecimal(dgvList.Rows[i].Cells[ivipdiscrate].Value);
                dmnHappyHours.VIPDiscPer = Convert.ToDecimal(dgvList.Rows[i].Cells[ivipdiscper].Value);
                dmnHappyHours.CreatedBy = 1;
                BllHappyHours bllHappyHours = new BllHappyHours();
                bool insertstatus = bllHappyHours.AddHappyHoures("prdinsertHappyHours", dmnHappyHours);
              
            }       
                MetroMessageBox.Show(this, "Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
            Clearform();


        }



        private void dgvList_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ifrom || e.ColumnIndex == ito)
            {
               // Timefromating(dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                //Duration(e.RowIndex);
            }
        }
        public void Duration(int r)
        {
            decimal from = Convert.ToDecimal(dgvList.Rows[r].Cells[ifrom].Value);
            decimal to = Convert.ToDecimal(dgvList.Rows[r].Cells[ito].Value);
            decimal dur = to - from;
            dur = dur * 60;
            dgvList.Rows[r].Cells[idur].Value = dur;
        }
        private void dgvList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            float Discount, tokenrate = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[icardrate].Value);
            float Discountper;
            float VIPDiscount, VIPtokenrate = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[icardrate].Value);
            float VIPDiscountper;
            if (e.ColumnIndex == idiscper)
            {
                Discountper = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[idiscper].Value);
                Discount = tokenrate * (Discountper / 100);
                dgvList.Rows[e.RowIndex].Cells[idiscrate].Value = Discount;
            }
            else if (e.ColumnIndex == idiscrate)
            {
                Discount = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[idiscrate].Value);
                Discountper = (Discount / tokenrate) * 100;
                dgvList.Rows[e.RowIndex].Cells[idiscper].Value = Discountper;
            }
            else if (e.ColumnIndex == ivipdiscper)
            {
                VIPDiscountper = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[ivipdiscper].Value);
                VIPDiscount = VIPtokenrate * (VIPDiscountper / 100);
                dgvList.Rows[e.RowIndex].Cells[ivipdiscrate].Value = VIPDiscount;
            }
            else if (e.ColumnIndex == ivipdiscrate)
            {
                VIPDiscount = Convert.ToInt32(dgvList.Rows[e.RowIndex].Cells[ivipdiscrate].Value);
                VIPDiscountper = (VIPDiscount / VIPtokenrate) * 100;
                dgvList.Rows[e.RowIndex].Cells[ivipdiscper].Value = VIPDiscountper;
            }
            else if (e.ColumnIndex == ifrom || e.ColumnIndex == ito)
            {                 
                    Duration(e.RowIndex);            
                Timefromating(dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), e);

            }

        }

        public void Timefromating(string fromtime, DataGridViewCellEventArgs e)
        {
            int l = fromtime.Count();           
            int c = fromtime.IndexOf("."); string h, m;        
            if(fromtime.Contains("."))
            {
                string[] arr = new string[4];
                arr = fromtime.Split('.');
                h = arr[0];
                m = arr[1];
                if(h.Count()==1)
                {
                    h = '0' + h;
                }
                else if(h.Count()==0)
                {
                    h = "00";
                }
                if (m.Count() == 1)
                {
                    m = '0' + m;
                }
                else if(m.Count()==0)
                {
                    m = "00";
                }
                dgvList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = h +"."+ m;

            }
           
        }
        public void Clearform()
        {
            cmbgame.SelectedText = "";
            dgvList.Rows.Clear();
            txttitle.Text = "";
        }

        private void s(object sender, EventArgs e)
        {

        }

        private void dgvList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex==idur)
            {
                //Duration(e.RowIndex);
            }
        }

        private void dgvList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvList_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            e.SuppressKeyPress = true;
            if (e.KeyData == Keys.Enter)
            {
                int r = dgvList.CurrentCell.RowIndex;
                int c = dgvList.CurrentCell.ColumnIndex + 1;
                if (r == dgvList.Rows.Count-1)
                {
                    r = 0;
                }

                if (c == 10)
                {
                    dgvList.CurrentCell = dgvList[0, r+1];
                }
                else
                {
                    dgvList.CurrentCell = dgvList[c, r];
                }
            }

        
        }
        private void dgvList_KeyUp(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            if (e.KeyData == Keys.Enter)
            {
                int r = dgvList.CurrentCell.RowIndex;
                int c = dgvList.CurrentCell.ColumnIndex+1 ;
                if (r==dgvList.Rows.Count-1)
                {
                    r = 0;
                }
                
                if (c == 10)
                {
                    dgvList.CurrentCell = dgvList[0,r+1];
                }
                else
                {
                    dgvList.CurrentCell = dgvList[c,r];
                }
            }
        }
     
    }
}

using BLL;
using Domain;
//using MarginPOSNew.Classes;
//using MarginPOSNew.Forms.UserControls;
//using MarginPOSNew.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pLocker.Forms
{
    public partial class FrmDisplayLockers : Form
    {

        //long Locker = 0;
        public static int locid = 0;
        long BranchId = Forms.frmLogin.BranchId;//16
        long UserId = Forms.frmLogin.UserId;//1
        long FinancialYearId = Convert.ToInt64(FrmDashBoard.FinancialYearId);//10

        public FrmDisplayLockers()
        {
            InitializeComponent();
        }
        public bool IsHired(long llockID)
        {
            BllSearch<DmnLocker> bllSearch = new BllSearch<DmnLocker>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblLockerHire";
            objSearch.WhereNdGroupBy = "Where LockID = " + llockID + " and Status =0";
            //objSearch.OrderBy = "Order By I.ItemName ";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void FrmDisplayLockers_Load(object sender, EventArgs e)
        {
            DisplayLockerList();
        }
        private void DisplayLockerList()
        {
            BllSearch<DmnLocker> bllSearch = new BllSearch<DmnLocker>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblLocker";
            objSearch.WhereNdGroupBy = "Where isnull(status,0)=0 and BranchId=" + BranchId;
            objSearch.OrderBy = "Order By ID";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {
                int x = 10;
                int y = 50;
                int ir = 1;
                //for (int i = 0; i < itemSource.Count; i++)
                //for (int i = 0; i < 50; i++)
                for (int i = 0; i < itemSource.Count; i++)
                {

                    if (ir == 11)
                    {
                        y = y + 80;
                        ir = 1;
                        x = 10;
                    }
                    Button newButton = new Button();
                    newButton.Name = itemSource[i].ID.ToString();
                    newButton.Text = itemSource[i].Locker;
                    Color mycolr = ColorTranslator.FromHtml(itemSource[i].LockerColor);
                    newButton.BackColor = ColorTranslator.FromHtml(itemSource[i].LockerColor);
                    newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Locker_png));
                    newButton.BackgroundImageLayout = ImageLayout.Stretch;
                    newButton.Font = new Font("Century Gothic", 10, FontStyle.Bold);
                    newButton.TextAlign = ContentAlignment.BottomCenter;
                    newButton.Click += new EventHandler(this.Button_click);
                    if (IsHired(itemSource[i].ID) == false)
                    {
                        //newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._lock));
                        newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Locker_Hired));
                        newButton.BackgroundImageLayout = ImageLayout.Stretch;
                        newButton.TextAlign = ContentAlignment.BottomCenter;
                        newButton.Font = new Font("Microsoft Sans Serif", 10);

                    }
                    //string hex;
                    //hex=Convert.ToString(itemSource[i].Color);
                    //if (!string.IsNullOrWhiteSpace(hex))
                    //{
                    //   // Color color = System.Drawing.ColorTranslator.FromHtml(hex);
                    //    newButton.BackColor = hex;
                    //}

                    //else
                    //{
                    //    Color color = System.Drawing.ColorTranslator.FromHtml("#40808");
                    //    newButton.BackColor = color;
                    //}




                    this.Controls.Add(newButton);
                    //newButton.Text = "Button" + Convert.ToString(i);
                    newButton.Location = new Point(x, y);
                    newButton.Size = new Size(85, 85);
                    x = x + 85;
                    ir++;
                }
                //if (itemSource.Count == 1)
                //{
                //    ItemId = Convert.ToInt64(itemSource[0].ItemId);
                //    Itemdetails(CurrentIndex, ItemId);
                //    discountPer = Convert.ToDecimal(dgvItemDet.Rows[currentrow].Cells[iDiscPer].Value);
                //    DiscountPerToAmt(currentrow, discountPer);
                //    TotalSummary();
                //}


            }

            //int x=10;
            //int y=50;
            //int ir = 1;
            //for(int i=0;i<112;i++)
            //{

            //    if(ir==15)
            //    {
            //        y = y + 80;
            //        ir = 1;
            //        x = 10;
            //    }
            //    Button newButton = new Button();
            //    newButton.Name = "btn" + i;
            //    this.Controls.Add(newButton);
            //    newButton.Text = "Button"+Convert.ToString(i);
            //    newButton.Location = new Point(x, y );
            //    newButton.Size = new Size(80, 80);
            //    x = x + 85;
            //     ir++;
            //}


        }
        private void Button_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            locid = Convert.ToInt32(btn.Name);
            FrmHireLocker frm = new FrmHireLocker(locid);
            frm.Show();
        }

        private void FrmDisplayLockers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }

        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;


namespace pLocker.Forms
{
    public partial class FrmDisplayLockers : Form
    {
        int lockstatus = -1;        
        long Locker = 0;
        public static int locid = 0;
        long BranchId = Forms.frmLogin.BranchId;//16
        long UserId = Forms.frmLogin.UserId;//1
        long FinancialYearId = Convert.ToInt64(FrmLockerDashBoard.FinancialYearId);//10
        List<int> LocCart = new List<int>();
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
            objSearch.WhereNdGroupBy = "Where LockID = " + llockID + " and Status =1 and hired=1";
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
            // DisplayLockerList();
            FillComboLocker(0);
            mycbo();
            DashBoardStatus();
            comboBox1.SelectedIndex = 0;
        }

        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public ComboboxItem(string texto, double valor)
            {
                this.Text = texto;
                this.Value = valor;
            }
        }
        private void mycbo()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Id");
            dt.Columns.Add("Status");
            DataRow r = dt.NewRow();
            r["Id"] = "0";
            r["Status"] = "All";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Id"] = "1";
            r["Status"] = "Hire";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Id"] = "2";
            r["Status"] = "Vacant";
            dt.Rows.Add(r);



            comboBox1.DataSource = dt;
            comboBox1.DisplayMember = "Status";
            comboBox1.ValueMember = "Id";



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

                    if (ir == 10)
                    {
                        y = y + 130;
                        ir = 1;
                        x = 10;
                    }
                    Button newButton = new Button();
                    newButton.Name = itemSource[i].ID.ToString();
                    newButton.Text = itemSource[i].Locker;
                   
                    Color mycolr = ColorTranslator.FromHtml(itemSource[i].LockerColor);
                    newButton.BackColor = ColorTranslator.FromHtml(itemSource[i].LockerColor);
                    //newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Locker));
                    newButton.BackgroundImageLayout = ImageLayout.Stretch;
                    newButton.Font = new Font("Century Gothic", 10, FontStyle.Bold);
                    newButton.TextAlign = ContentAlignment.MiddleCenter;
                    newButton.Click += new EventHandler(this.Button_click);
                    if (IsHired(itemSource[i].ID) == true)
                    {
                       // newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._lock));
                        newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Locker));
                        newButton.BackgroundImageLayout = ImageLayout.Stretch;
                        newButton.TextAlign = ContentAlignment.MiddleCenter;
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
                    newButton.Size = new Size(80, 120);
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

           


        }
        private void Button_click(object sender, EventArgs e)
        {           
            Button btn = sender as Button;
            if (LocCart.Count > 0)
            {
                FrmHireLocker frm = new FrmHireLocker(LocCart, 1);
                frm.ShowDialog();

            }
			else
			{
                locid = Convert.ToInt32(btn.Name);
                FrmHireLocker frm = new FrmHireLocker(locid, 1);
                frm.ShowDialog();
            }
        }

        private void FrmDisplayLockers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();

            if (e.KeyCode == Keys.F5)
                 FillComboLocker(0);

        }

        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        public void FillComboLocker(int nOpt)
        {
            panel1.Controls.Clear();
            BllSearch<DmnLocker> bllSearch = new BllSearch<DmnLocker>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblLocker";
            if (nOpt == 0)
            {
                objSearch.WhereNdGroupBy = "Where isnull(status,0)=1  and  BranchId=" + BranchId;
            }
            else if(nOpt==1)
			{
                objSearch.Table = "tblLockerHire left join tblLocker on tblLocker.Id=tblLockerHire.LockID  ";
                objSearch.WhereNdGroupBy = "Where isnull(tblLockerHire.status,0)=1 and tblLockerHire.Hired =1" +
                    " and  tblLockerHire.BranchId=" + BranchId;
            }
            else if(nOpt==2)
			{
                objSearch.Table = "tblLocker";
                objSearch.WhereNdGroupBy = "Where isnull(status,0)=1 and Hired = 0 and  BranchId=" + BranchId;
            }
            else if (nOpt == 3)
            {
                objSearch.Table = "tblLockerHire";
                objSearch.WhereNdGroupBy = "where convert(varchar, EntryDate, 105) = convert(varchar, GETDATE(), 105) and Status=1 and BranchId="+BranchId;
            }
            else if (nOpt == 4)
            {
                objSearch.Table = "tblAccountLedger";
                objSearch.WhereNdGroupBy = "where  convert(varchar, CreatedDateTime, 105) = convert(varchar, GETDATE(), 105)";
            }
            else if (nOpt == 5)
            {
                objSearch.Table = "tblLockerHire";
                objSearch.WhereNdGroupBy = "where convert(varchar,ExpTime, 120) > convert(varchar, GETDATE(), 120) and Hired =1 and BranchId=" + BranchId;
            }
            else if (nOpt == 6)
            {
                if (txtLocker != null && !string.IsNullOrWhiteSpace(txtLocker.Text))
                {
                    objSearch.Table = "tblLocker";
                    objSearch.WhereNdGroupBy = "Where Status=1 and BranchId="+BranchId+ " and id>= (select min(id) from tblLocker where Status=1 and BranchId=" + BranchId + " and locker like '%" + txtLocker.Text + "')";
                }
                else
                {
                    objSearch.WhereNdGroupBy = "Where isnull(status,0)=1  and  BranchId=" + BranchId;
                }

            }
            //objSearch.OrderBy = "Order By ID";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {
                int x = 10;
                int y = 10;
                int ir = 1;
                int flage = 0;
                //for (int i = 0; i < itemSource.Count; i++)
                //for (int i = 0; i < 50; i++)
                for (int i = 0; i < itemSource.Count; i++)
                {
                    if (ir == 10)
                    {
                        y = y + 85;
                        ir = 1;
                        x = 10;
                        flage++;
                    }
                    Button newButton = new Button();
                    newButton.Name = itemSource[i].ID.ToString();
                    // newButton.Text = itemSource[i].Locker;
                    if (itemSource[i].Hired == 0)
                    {
                        CheckBox chk = new CheckBox();
                        chk.Text = itemSource[i].Locker;
                        chk.Name = Convert.ToString(itemSource[i].ID);
                        chk.Font = new Font("Helvitica", 7, FontStyle.Bold);
                        chk.CheckAlign = ContentAlignment.MiddleLeft;
                        chk.FlatAppearance.BorderSize = 1;
                        chk.BackColor = Color.Transparent;
                        chk.CheckedChanged += new EventHandler(this.Chk_CheckedChanged);
                        newButton.Controls.Add(chk);
                        chk.Location = new Point(5, 45);
                    }
                    else
                    {
                        Label chk = new Label();
                        chk.Text = itemSource[i].Locker;
                        chk.Name = Convert.ToString(itemSource[i].ID);
                        chk.Font = new Font("Helvitica", 7, FontStyle.Bold);                        
                        chk.BackColor = Color.Transparent;                        
                        newButton.Controls.Add(chk);
                        chk.Location = new Point(15, 50);
                    }
                                    
                    Color mycolr = ColorTranslator.FromHtml(itemSource[i].LockerColor);                    
                    newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.lock_1));
                    newButton.BackgroundImageLayout = ImageLayout.Stretch;
                    newButton.ForeColor = Color.Black;
                    newButton.FlatStyle=FlatStyle.Flat;
                    newButton.FlatAppearance.BorderSize = 0;
                    newButton.Font = new Font("Helvitica", 8, FontStyle.Bold);              
                    newButton.TextAlign = ContentAlignment.MiddleCenter;
                    newButton.Click += new EventHandler(this.Button_click);
                    if (IsHired(itemSource[i].ID) == false)
                    {
                        // newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources._lock));
                        newButton.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.lock_2));
                        newButton.BackgroundImageLayout = ImageLayout.Stretch;
                        newButton.TextAlign = ContentAlignment.BottomCenter;
                        newButton.ForeColor = Color.Red;
                        newButton.Font = new Font("Helvitica,Bold", 10);
                       // newButton.ForeColor = Color.FromArgb(196,196,196);
                    }
                    panel1.Controls.Add(newButton);
                    //newButton.Text = "Button" + Convert.ToString(i);
                    newButton.Location = new Point(x, y);
                    newButton.Size = new Size(80,80);
                    x = x + 85;
                    ir++;
                }                
                panel1.AutoScroll = true;
                int z =flage*10;
                panel1.SetAutoScrollMargin(0, 150);
                DashBoardStatus();
                LocCart.Clear();
            }         

        }
        private void Chk_CheckedChanged(object sender,EventArgs e)
		{
            
            CheckBox chk = sender as CheckBox;
            if(chk.Checked==true)
			{
                if (LocCart.Count < 10)
                    LocCart.Add(Convert.ToInt32(chk.Name));
                else
                {
                    MessageBox.Show("Maximum 10 Locker Can Select ", "Maximum ");
                    chk.Checked = false;
                }
			}
            else
			{
                for(int i=0;i<LocCart.Count;i++)
				{
                    if(LocCart[i]== Convert.ToInt32(chk.Name))
					{
                        LocCart.RemoveAt(i);
					}
				}
			}
		}
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {          
            lockstatus = Convert.ToInt32(comboBox1.SelectedValue);
           // MessageBox.Show(comboBox1.SelectedValue.ToString());            
            FillComboLocker(lockstatus);
        }

       

        private void panel1_Click(object sender, EventArgs e)
        {
            FillComboLocker(0);
        }

       
        public void DashBoardStatus()
        {
            //Today Reg Registerd
            BllSearch<DmnSearch> bllSearch = new BllSearch<DmnSearch>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblAccountLedger";
            objSearch.WhereNdGroupBy = "where  convert(varchar, CreatedDateTime, 105) = convert(varchar, GETDATE(), 105)";
            objSearch.OrderBy = " ";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            btnTodNew.Text = gridSource.Count.ToString();

            //Today Hire           
            objSearch.Select = "*";
            objSearch.Table = "tblLockerHire";
            objSearch.WhereNdGroupBy = "where  convert(varchar, EntryDate, 105) = convert(varchar, GETDATE(), 105) and BranchId="+BranchId;
            objSearch.OrderBy = " ";
            var gridSource1 = bllSearch.GetSearchData("prdSearch", objSearch);
            btnTodHire.Text = gridSource1.Count().ToString();

            //Today Expire
            objSearch.Select = "*";
             objSearch.Table = "tblLockerHire";
            objSearch.WhereNdGroupBy = "where convert(varchar,ExpTime, 120) > convert(varchar, GETDATE(), 120) and Hired =1 and BranchId="+BranchId;
            objSearch.OrderBy = " ";
            var gridSource2 = bllSearch.GetSearchData("prdSearch", objSearch);
            btnTodExp.Text = gridSource2.Count().ToString();

            //Total Hire
            objSearch.Select = "*";
            objSearch.Table = "tblLockerHire";
            objSearch.WhereNdGroupBy = "Where isnull(status,0)=1  and Hired = 1 and BranchId = " + BranchId;
            objSearch.OrderBy = " ";
            var gridSource3 = bllSearch.GetSearchData("prdSearch", objSearch);
            btnTotalHire.Text = gridSource3.Count().ToString();

            //Avail
            objSearch.Select = "*";
            objSearch.Table = "tblLocker";
            objSearch.WhereNdGroupBy = "where Hired = 0 and Status=1 and BranchId="+BranchId;
            objSearch.OrderBy = " ";
            var gridSource4 = bllSearch.GetSearchData("prdSearch", objSearch);
            btnTotalAvail.Text = gridSource4.Count().ToString();

        }

        private void txtLocker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
          

        }

        private void txtLocker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (txtLocker != null && !string.IsNullOrWhiteSpace(txtLocker.Text))
                {
                    FillComboLocker(6);
                }
               // SendKeys.Send("{TAB}");
            }
           
        }

        private void btnTotalHire_Click(object sender, EventArgs e)
        {
            FillComboLocker(1);
        }

        private void btnTotalAvail_Click(object sender, EventArgs e)
        {
            FillComboLocker(2);
        }

        private void btnTodHire_Click(object sender, EventArgs e)
        {
            FillComboLocker(3);
        }

        private void btnTodNew_Click(object sender, EventArgs e)
        {
            //FillComboLocker(4);
        }

        private void btnTodExp_Click(object sender, EventArgs e)
        {
            FillComboLocker(5);
        }

		private void button1_Click(object sender, EventArgs e)
		{
            FillComboLocker(0);
        }

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{


		}

		private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
		{

		}
	}
}
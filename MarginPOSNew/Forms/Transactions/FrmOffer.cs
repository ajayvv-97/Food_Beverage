using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
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

namespace MarginPOSNew.Forms.Transactions
{

    public partial class FrmOffer : Form
    {
        //private int iQuantity;
        //private int iTotalAmount;
        private object dgvGeneral;
        private string _retValue;
        private object rowIndexforGridviewCBox;

        long slno = 0;
        long Id = 0;


        //grid
        const int isno = 0;
        const int iItemId = 1;
        const int iItemCode = 2;
        const int iBarcode = 3;
        const int iName = 4;
        const int iPurchaseRate = 5;
        const int iSalesRate = 6;
        const int iMRP = 7;
        const int iOfferRate = 8;
        const int iDiscRate = 9;
        const int iDiscPer = 10;

        const int fDt = 10;
        const int tDt = 11;
        const int iEntryDate = 12;
        const int iOfferDesc = 13;
        const int iCreatedBy = 15;
        const int iModifiedBy = 15;
        const int iStatus = 16;
        const int iBranchId = 17;
        const int iRemarks = 18;
        DateTime iCreatedDateTime;
        DateTime iModifiedDateTime;


        long BranchId = Forms.UserAccounts.FrmLogin.BranchId;//16
        long UserId = Forms.UserAccounts.FrmLogin.UserId;//1
        long FinancialYearId = Convert.ToInt64(FrmDashboard.FinancialYearId);//10

        long retval = 0;

        //public int SlNo { get; private set; }

        public FrmOffer()
        {
            InitializeComponent();
        }

        private void dgvnewgrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void dgvnewgrid_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            ItemPOPup();
        }


        public void ItemPOPup()
        {
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            dtColumnAlign.Columns.Add();
            dtColumnAlign.Rows.Add("6", "R");
            dtColumnAlign.Rows.Add("7", "R");
            dtColumnAlign.Rows.Add("8", "R");
            dtColumnAlign.Rows.Add("9", "R");
            dtColumnAlign.Rows.Add("10", "R");

            string returnValue = string.Empty;
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "Id,Name,Code,Barcode,Category,Unit,SalesRate,MRP,Tax(%),QOH";
            _objSearch.Select = "I.ItemId as Id,I.SearchName as Field1,I.ItemCode as Field2,I.Barcode as Field3,C.CategoryName as Field4,U.UnitName as Field5," +
                " CAST((I.PurchaseRate1*(100+I.SellingPer)/100) as decimal) as Field6, I.MRP1 as Field7, I.IGSTPer as Field8, I.QuantityOnHand as Field9";
            _objSearch.Table = " tblItem I LEFT JOIN tblCategory C  ON I.CategoryId = C.CategoryId LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
            _objSearch.WhereNdGroupBy = " WHERE I.Status=0 And I.BranchId = " + BranchId;
            _objSearch.OrderBy = "order by I.SearchName ASC ";
            //  using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 1200, 400, 100, 100))
            using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.AllCharacters, this.Width - 90, (this.Height - 200), this.Location.Y + 70, this.Location.X + 100, dtColumnAlign))

            {
                var dialogResult = search.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    returnValue = Convert.ToString(search._retValue);
                }
            }
            if (returnValue != string.Empty)
            {

                if (dgvnewgrid.CurrentCell == null)
                {
                    MessageBox.Show(this, "Select Row!");
                }
                else
                {
                    int CurrentIndex = dgvnewgrid.CurrentRow.Index;
                    retval = Convert.ToInt64(returnValue.ToString());


                    Itemdetails(CurrentIndex, retval);

                    //dgvnewgrid.Rows[CurrentIndex + 1].Cells[isno].Value = "0";

                    //TotalSummary();

                    // dgvnewgrid.CurrentCell = dgvnewgrid.Rows[CurrentIndex].Cells[iQuantity];
                }
            }

        }

        //private void TotalSummary()
        //{
        //    throw new NotImplementedException();
        //}

        private void Itemdetails(int CurrentIndex, long retval)
        {
            rowIndexforGridviewCBox = CurrentIndex;//this is to store value for grid viewcombo box control
            slno = CurrentIndex;
            BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "I.ItemId,I.ItemCode,I.Barcode,I.ItemName,I.PurchaseRate1,I.SaleRate,I.MRP1";
            objSearch.Table = " tblItem I LEFT JOIN tblUnit U ON I.MainUnitId=U.UnitId";
            objSearch.WhereNdGroupBy = "Where I.ItemId =" + retval;
            objSearch.OrderBy = "";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);

            dgvnewgrid.Rows[CurrentIndex].Cells[isno].Value = slno + 1;

            dgvnewgrid.Rows[CurrentIndex].Cells[iItemId].Value = Convert.ToString(itemSource[0].ItemId);
            dgvnewgrid.Rows[CurrentIndex].Cells[iItemCode].Value = Convert.ToString(itemSource[0].ItemCode);
            dgvnewgrid.Rows[CurrentIndex].Cells[iBarcode].Value = Convert.ToString(itemSource[0].Barcode);
            dgvnewgrid.Rows[CurrentIndex].Cells[iName].Value = Convert.ToString(itemSource[0].ItemName);
            dgvnewgrid.Rows[CurrentIndex].Cells[iPurchaseRate].Value = Convert.ToString(itemSource[0].PurchaseRate1);
            dgvnewgrid.Rows[CurrentIndex].Cells[iSalesRate].Value = Convert.ToString(itemSource[0].SaleRate);
            dgvnewgrid.Rows[CurrentIndex].Cells[iMRP].Value = Convert.ToString(itemSource[0].MRP1);




        }

        //private void dgvnewgrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        ItemPOPup();
        //    }

        //    catch (Exception ex)
        //    {
        //        MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);


        //    }
        //}

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                DeleteRecord();

                bool savestatus = true;
                for (int i = 0; i < dgvnewgrid.Rows.Count; i++)
                {
                    string itId = Convert.ToString(dgvnewgrid.Rows[i].Cells[iItemId].Value);
                    if (itId != "")
                    {
                        DmnOfferDetails dmnOffrDetails = new DmnOfferDetails();
                        Bllofferdetails offrdetails = new Bllofferdetails();

                        dmnOffrDetails.EntryDate = DateTime.Now;
                        dmnOffrDetails.OfferDesc = Convert.ToString(txtoffer.Text);
                        dmnOffrDetails.ItemId = Convert.ToInt32(dgvnewgrid.Rows[i].Cells[iItemId].Value);
                        dmnOffrDetails.ItemCode = Convert.ToInt32(dgvnewgrid.Rows[i].Cells[iItemCode].Value);
                        dmnOffrDetails.IBarCode = long.Parse(Convert.ToString(dgvnewgrid.Rows[i].Cells[iBarcode].Value));
                        dmnOffrDetails.ItemName = Convert.ToString(dgvnewgrid.Rows[i].Cells[iName].Value);
                        dmnOffrDetails.pRate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[i].Cells[iPurchaseRate].Value));
                        dmnOffrDetails.sRate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[i].Cells[iSalesRate].Value));
                        dmnOffrDetails.MRP = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[i].Cells[iMRP].Value));
                        dmnOffrDetails.OffrRate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[i].Cells[iOfferRate].Value));
                        dmnOffrDetails.DiscRate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[i].Cells[iDiscRate].Value));
                        dmnOffrDetails.DiscPer = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[i].Cells[iDiscPer].Value) == string.Empty ? "0.00" : Convert.ToString(dgvnewgrid.Rows[i].Cells[iDiscPer].Value));

                        //dTPfrm.Format = DateTimePickerFormat.Custom;
                        //dTPfrm.CustomFormat = "yyyy-MM-dd";
                        dmnOffrDetails.fDt = dTPfrm.Value;

                        //dTPto.Format = DateTimePickerFormat.Custom;
                        //dTPto.CustomFormat = "yyyy-MM-dd";
                        dmnOffrDetails.tDt = dTPto.Value;

                        dmnOffrDetails.CreatedBy = Forms.UserAccounts.FrmLogin.UserId;
                        dmnOffrDetails.CreatedDateTime = DateTime.Now;
                        dmnOffrDetails.ModifiedBy = Forms.UserAccounts.FrmLogin.UserId;
                        dmnOffrDetails.ModifiedDateTime = DateTime.Now;
                        dmnOffrDetails.BranchId = Forms.UserAccounts.FrmLogin.BranchId;
                        dmnOffrDetails.Remarks = "";

                        dmnOffrDetails.Status = 0;
                        dmnOffrDetails.CreatedBy = Forms.UserAccounts.FrmLogin.UserId;


                        var DetlstData = offrdetails.AddData("prdInsertOfferDetails", dmnOffrDetails);
                        savestatus = DetlstData;
                        if (!savestatus)
                        {
                            MetroMessageBox.Show(this, " Save failed ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                            break;

                        }

                    }

                }
                if (savestatus)
                {

                    MetroMessageBox.Show(this, " Successfully saved ", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                    btnRefresh_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }



        }

        //try
        //{
        //    dTPfrm.Focus();
        //    dgvnewgrid.Columns.Clear();
        //    dgvnewgrid.DataSource = null;

        //    DmnSearch dmnSearch = new DmnSearch();
        //    BllSearch<DmnOfferDetails> bllSearch = new BllSearch<DmnOfferDetails>();

        //    dTPfrm.Format = DateTimePickerFormat.Custom;
        //    // Display the date as "Mon 27 Feb 2012".  
        //    dTPfrm.CustomFormat = "yyyy-MM-dd hh:MM:ss";
        //    DateTime fromdate = Convert.ToDateTime(dTPfrm.Text.ToString());

        //    dTPto.Format = DateTimePickerFormat.Custom;
        //    // Display the date as "Mon 27 Feb 2012".  
        //    dTPto.CustomFormat = "yyyy-MM-dd hh:MM:ss";
        //    DateTime todate = Convert.ToDateTime(dTPto.Text.ToString());

        //    dmnSearch.Select = "ItemId,IBarCode,PRate,SRate,MRP,DiscRate,OffrRate,DiscPer,BillAmt";
        //    dmnSearch.Table = "tblOffer";
        //    dmnSearch.WhereNdGroupBy = "where fDt>='01/29/2020 ' and tDt<='02/05/2020'";

        //    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
        //    if (gridSource.Count > 0)
        //    {
        //        //dgvnewgrid.Rows.Add(gridSource.Count);
        //        for (int i = 0; i < (gridSource.Count); i++)
        //        {
        //            dgvnewgrid.Rows[i].Cells[iItemId].Value = Convert.ToString(gridSource[i].ItemId);
        //            dgvnewgrid.Rows[i].Cells[iIBarCode].Value = Convert.ToString(gridSource[i].IBarCode);
        //            dgvnewgrid.Rows[i].Cells[iPRate].Value = Convert.ToString(gridSource[i].pRate);
        //            dgvnewgrid.Rows[i].Cells[iSRate].Value = Convert.ToString(gridSource[i].sRate);
        //            dgvnewgrid.Rows[i].Cells[iMRP].Value = Convert.ToString(gridSource[i].MRP);
        //            dgvnewgrid.Rows[i].Cells[iDiscPer].Value = Convert.ToString(gridSource[i].DiscPer);
        //            dgvnewgrid.Rows[i].Cells[iBillAmt].Value = Convert.ToString(gridSource[i].BillAmt);
        //            dgvnewgrid.Rows[i].Cells[iOffrRate].Value = Convert.ToString(gridSource[i].OffrRate);
        //            dgvnewgrid.Rows[i].Cells[iDiscRate].Value = Convert.ToString(gridSource[i].DiscRate);

        //            ;
        //        }

        //    }
        //}
        //catch (Exception ex)
        //{
        //    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
        //}


        private void FrmOffer_Load(object sender, EventArgs e)
        {
            ActiveControl = dTPfrm;
            dTPfrm.Focus();

        }
        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                using (var search = new FrmListOffers())
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue.ToString() != "")
                {
                    // Id = Convert.ToInt32(returnValue.ToString());
                    DmnSearch dmnSearch = new DmnSearch();
                    BllSearch<DmnOfferDetails> bllSearch = new BllSearch<DmnOfferDetails>();
                    dmnSearch.Select = " o.id,o.ItemId,o.ItemCode,o.IBarCode,I.ItemName,o.pRate,o.sRate,o.MRP,o.OffrRate,o.DiscRate,o.DiscPer,o.fDt,o.tDt,o.OfferDesc";
                    dmnSearch.Table = "tblOffer o LEFT JOIN tblItem I ON o.ItemId = I.ItemId";
                    dmnSearch.WhereNdGroupBy = "where  OfferDesc in (select OfferDesc from tblOffer where id =" + returnValue + ")";
                    // + gridSource1[0].OfferDesc;
                    dmnSearch.OrderBy = "Order By o.id DESC ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);



                    for (int i = 0; i < (gridSource.Count); i++)
                    {
                        dgvnewgrid.Rows.Add();


                        dTPfrm.Text = Convert.ToString(gridSource[i].fDt);
                        dTPto.Text = Convert.ToString(gridSource[i].tDt);
                        txtoffer.Text = Convert.ToString(gridSource[i].OfferDesc);

                        dgvnewgrid.Rows[i].Cells[isno].Value = (i + 1).ToString("#");
                        dgvnewgrid.Rows[i].Cells[iItemId].Value = Convert.ToString(gridSource[i].ItemId);
                        dgvnewgrid.Rows[i].Cells[iItemCode].Value = Convert.ToString(gridSource[i].ItemCode);
                        dgvnewgrid.Rows[i].Cells[iBarcode].Value = Convert.ToString(gridSource[i].IBarCode);
                        dgvnewgrid.Rows[i].Cells[iName].Value = Convert.ToString(gridSource[i].ItemName);
                        dgvnewgrid.Rows[i].Cells[iPurchaseRate].Value = Convert.ToString(gridSource[i].pRate);
                        dgvnewgrid.Rows[i].Cells[iSalesRate].Value = Convert.ToString(gridSource[i].sRate);
                        dgvnewgrid.Rows[i].Cells[iMRP].Value = Convert.ToString(gridSource[i].MRP);
                        dgvnewgrid.Rows[i].Cells[iOfferRate].Value = Convert.ToString(gridSource[i].OffrRate);
                        dgvnewgrid.Rows[i].Cells[iDiscRate].Value = Convert.ToString(gridSource[i].DiscRate);
                        dgvnewgrid.Rows[i].Cells[iDiscPer].Value = Convert.ToString(gridSource[i].DiscPer);

                    }




                }
            }

            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }




        }


        public void DeleteRecord()
        {
            try
            {
                DmnSearch dmnSearch = new DmnSearch();
                BllSearch<DmnOfferDetails> bllSearch = new BllSearch<DmnOfferDetails>();
                dmnSearch.Table = "tblOffer";
                dmnSearch.WhereNdGroupBy = "where convert(date, fDt, 105) = (SELECT convert(varchar(10), Convert(date, '" + dTPfrm.Text + "', 105), 23))" +
                                               "and convert(date, tDt,105)= (SELECT convert(varchar(10), Convert(date, '" + dTPto.Text + "', 105), 23))";

                var gridSource = bllSearch.GetSearchData("prdDeleteRecord", dmnSearch);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }





        public void fillGridView()
        {
            try
            {
                dgvnewgrid.Columns.Clear();
                dgvnewgrid.DataSource = null;

                FrmDataGridView<DmnOfferDetails> frmGridSearch = new FrmDataGridView<DmnOfferDetails>();
                DmnSearch dmnSearch = new DmnSearch();

                dTPfrm.Format = DateTimePickerFormat.Custom;
                dTPfrm.CustomFormat = "dd-MM-yyyy";


                dTPto.Format = DateTimePickerFormat.Custom;
                dTPto.CustomFormat = "dd-MM-yyyy";
                //todate = Convert.ToDateTime(dTPto.Text.ToString());

                //dTPfrm.Format = DateTimePickerFormat.Custom;
                //dTPfrm.CustomFormat = "dd/MM/yyyy";
                //dTPto.Format = DateTimePickerFormat.Custom;

                //dTPto.CustomFormat = "dd/MM/yyyy";

                dmnSearch.ColumnHeaders = "ItemId,ItemCode,IBarCode,ItemName,PRate,SRate,MRP,OffrRate,DiscRate,DiscPer";
                dmnSearch.Select = "ItemId,ItemCode,IBarCode,ItemName,PRate,SRate,MRP,OffrRate,DiscRate,DiscPer";
                dmnSearch.Table = "tblOffer";
                dmnSearch.WhereNdGroupBy = "where convert(date, fDt, 105) = (SELECT convert(varchar(10), Convert(date, '" + dTPfrm.Text + "', 105), 23))" +
                                           "and convert(date, tDt,105)= (SELECT convert(varchar(10), Convert(date, '" + dTPto.Text + "', 105), 23))";


                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvnewgrid.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvnewgrid.DataSource = gridSource.DataSource;



            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }





        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //dTPto.MinDate = dTPfrm.Value;

            // fillDateTimePicker();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // fillDateTimePicker();
        }


        private void btnList_Click(object sender, EventArgs e)
        {

        }


        public void fillDateTimePicker()
        {
            try
            {
                dgvnewgrid.Columns.Clear();
                dgvnewgrid.DataSource = null;

                FrmDataGridView<DmnOfferDetails> frmGridSearch = new FrmDataGridView<DmnOfferDetails>();
                DmnSearch dmnSearch = new DmnSearch();

                dTPfrm.Format = DateTimePickerFormat.Custom;
                dTPfrm.CustomFormat = "dd-MM-yyyy";


                dTPto.Format = DateTimePickerFormat.Custom;
                dTPto.CustomFormat = "dd-MM-yyyy";
                //todate = Convert.ToDateTime(dTPto.Text.ToString());

                //dTPfrm.Format = DateTimePickerFormat.Custom;
                //dTPfrm.CustomFormat = "dd/MM/yyyy";
                //dTPto.Format = DateTimePickerFormat.Custom;

                //dTPto.CustomFormat = "dd/MM/yyyy";

                dmnSearch.ColumnHeaders = "ItemId,ItemCode,IBarCode,ItemName,pRate,sRate,MRP,OffrRate,DiscRate,DiscPer";
                dmnSearch.Select = "o.ItemId,o.ItemCode,o.IBarCode,I.ItemName,o.pRate,o.sRate,o.MRP,o.OffrRate,o.DiscRate,o.DiscPer";
                dmnSearch.Table = "tblOffer o LEFT JOIN tblItem I ON o.ItemId = I.ItemId";
                dmnSearch.WhereNdGroupBy = "where convert(date, fDt, 105) = (SELECT convert(varchar(10), Convert(date, '" + dTPfrm.Text + "', 105), 23))" +
                                           "and convert(date, tDt,105)= (SELECT convert(varchar(10), Convert(date, '" + dTPto.Text + "', 105), 23))";


                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);

                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvnewgrid.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvnewgrid.DataSource = gridSource.DataSource;



            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }




        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dTPfrm.Text = Convert.ToString(DateTime.Now.Date);
            dTPto.Text = Convert.ToString(DateTime.Now.Date);
            txtoffer.Text = "";
            dgvnewgrid.Rows.Clear();
            dgvnewgrid.Refresh();

        }

        private void dgvnewgrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvnewgrid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvnewgrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvnewgrid.CurrentCell.ColumnIndex == 8)//OfferRate
                {
                   

                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[9].Value = 0;
                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[10].Value = 0;


                    decimal Salerate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[6].Value));
                    decimal OfferRate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[8].Value));
                    decimal PurchaseRate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[5].Value));

                    if (OfferRate > Salerate)
                    {
                        MetroMessageBox.Show(this, "", "Offer Rate Can't Exceed than Sale Rate !!! ", MessageBoxButtons.OK, 155);

                        dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[8].Value = 0;
                        return;
                    }
                    else if (OfferRate < PurchaseRate)
                    {
                        MetroMessageBox.Show(this, "", "Offer Rate Can't less than PurchaseRate !!! ", MessageBoxButtons.OK, 155);

                        dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[8].Value = 0;
                        return;
                    }

                    else
                    {

                    }



                    decimal disrate = Salerate - OfferRate;
                    decimal discper = Math.Round(disrate / OfferRate * 100,2);


                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[9].Value = disrate;
                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[10].Value = discper;

                }

                if (dgvnewgrid.CurrentCell.ColumnIndex == 9)//DiscRate
                {
                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[10].Value = 0;
                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[8].Value = 0;

                    decimal Salerate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[6].Value));
                    decimal disrate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[9].Value));

                    decimal OfferRate = Salerate - disrate;
                    decimal discper = Math.Round(disrate / OfferRate * 100)/2;


                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[10].Value = discper;
                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[8].Value = OfferRate;

                }


                if (dgvnewgrid.CurrentCell.ColumnIndex == 10)//Discper
                {
                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[9].Value = 0;
                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[8].Value = 0;

                    decimal Salerate = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[6].Value));
                    decimal discper = decimal.Parse(Convert.ToString(dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[10].Value));

                    decimal disrate = Salerate * (discper / 100);
                    decimal OfferRate = Salerate - disrate;

                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[9].Value = disrate;
                    dgvnewgrid.Rows[dgvnewgrid.CurrentCell.RowIndex].Cells[8].Value = OfferRate;

                }

                SendKeys.Send("{up}");
                SendKeys.Send("{right}");
            }
           

            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }

        private void dTPfrm_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void dTPto_KeyUp(object sender, KeyEventArgs e)
        {


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dTPfrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void dTPto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void txtoffer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                System.Windows.Forms.SendKeys.Send("{TAB}");
            }
        }

        private void FrmOffer_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void FrmOffer_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void FrmOffer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.F6)
                    btnSave_Click(sender, e);
                if (e.KeyData == Keys.F3)
                    btnsearch_Click(sender, e);
                if (e.KeyData == Keys.F5)
                    btnRefresh_Click(sender, e);
               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void dgvnewgrid_KeyDown(object sender, KeyEventArgs e)
        
        {
            //try
            //{
            //    e.SuppressKeyPress = true;
            //    int iColumn = dgvnewgrid.CurrentCell.ColumnIndex;
            //    int iRow = dgvnewgrid.CurrentCell.RowIndex;

            //    if (iColumn == dgvnewgrid.ColumnCount - 1)
            //    {
            //        if (dgvnewgrid.RowCount > (iRow + 1))
            //        {
            //            dgvnewgrid.CurrentCell = dgvnewgrid[3, iRow + 1];
            //        }
            //        else
            //        {
            //            //focus next control
            //        }
            //    }
            //    else
            //    {
            //        if (iColumn == 3)
            //        {
            //            dgvnewgrid.CurrentCell = dgvnewgrid[iColumn + 1, iRow];
            //        }
            //        else
            //        {
            //            //focus next control
            //            dgvnewgrid.CurrentCell = dgvnewgrid[iColumn + 1, iRow];
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            //}

            //e.SuppressKeyPress = true;
            //int iColumn = dgvnewgrid.CurrentCell.ColumnIndex;
            //int iRow = dgvnewgrid.CurrentCell.RowIndex;
            //if (iColumn == dgvnewgrid.ColumnCount - 1)
            //{
            //    if (dgvnewgrid.RowCount > (iRow + 1))
            //    {
            //        dgvnewgrid.CurrentCell = dgvnewgrid[1, iRow + 1];
            //    }
            //    else
            //    {
            //        //focus next control

            //    }
            //}
            //else
            //    dgvnewgrid.CurrentCell = dgvnewgrid[iColumn + 1, iRow];

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                int iColumn = dgvnewgrid.CurrentCell.ColumnIndex;
                int iRow = dgvnewgrid.CurrentCell.RowIndex;
                if (iColumn == dgvnewgrid.Columns.Count - 1)
                    dgvnewgrid.CurrentCell = dgvnewgrid[0, iRow + 1];
                else
                    dgvnewgrid.CurrentCell = dgvnewgrid[iColumn + 1, iRow];

            }
            if (e.KeyCode == Keys.F8)
            {
                ItemPOPup();

            }


        }

         
    }   
}
    


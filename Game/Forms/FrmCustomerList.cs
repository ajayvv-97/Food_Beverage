using Domain;
using BLL;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pLocker.Forms
{
    public partial class FrmCustomerList : Form
    {

        long BranchId = 0;// Forms.frmLogin.BranchId;//16
        const int gAccID = 0;
        const int gEnrollDate = 1;
        const int gAccNo = 2;
        const int gAccName = 3;
        const int gMobile = 4;
        const int gAccEmail = 5;
        const int gEmergency = 6;
        const int gAccAddress = 7;
        const int gZipcode = 8;
        const int gState = 9;
        const int gCity = 10;
        const int gCountry = 11;
        const int  gdob = 12;
        const int gProofNo = 13;
        const int gInstitute = 14;
        const int gOccupation = 15;
        
        public FrmCustomerList()
        {
            InitializeComponent();          


        }

        private void FrmLockerList_Load(object sender, EventArgs e)
        {
            try
            {


                cboField.DisplayMember = "Text";
                cboField.ValueMember = "Value";

                var items = new[] {
                new { Text = "-Select-", Value = "0" },
                new { Text = "AccNo", Value = "1" },
                new { Text = "AccName", Value = "2" },
                new { Text = "Mobile", Value = "3" },
                new { Text = "Country", Value = "4" },
                new { Text = "State", Value = "5" },
                new { Text = "Zipcode", Value = "6" },
                new { Text = "Occupation", Value = "7" },
                new { Text = "ProofNo", Value = "8" },


            };
                cboField.DataSource = items;
                mtxtFieldValue.Text = "";
                mtxtFieldValue.Focus();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }

        }
       



        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BranchId = 1;
                FillGridList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }



        private void FillGridList()
        {
            try
            {
                BllSearch<DmnCustAccount> _bllSearch = new BllSearch<DmnCustAccount>();
                DmnSearch _objSearch = new DmnSearch();

                _objSearch.Select = "LedgerId as id,EnrollDate,LedgerCode as AccNo, LedgerName as AccName, dob,cInstitute,Occupation,Zipcode,Mobile," +
                                   "cEmergency,cCountry,State,City,ProofNo,LedgerAddress as AccAddress,Emailid as AccEmail";
                _objSearch.Table = " tblAccountLedger";
                //_objSearch.Select = "dbo.tblLocker.*, isnull(dbo.tblLockerLocation.LocationName,'')";
                //_objSearch.Table = " dbo.tblLocker left JOIN dbo.tblLockerLocation ON dbo.tblLocker.LocationID = dbo.tblLockerLocation.LocationId";

                if (!string.IsNullOrWhiteSpace(mtxtFieldValue.Text))
                {
                    switch (Convert.ToInt32(cboField.SelectedValue))
                    {
                        case 1:
                            _objSearch.WhereNdGroupBy = "WHERE LedgerCode Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;
                            break;
                        case 2:
                            _objSearch.WhereNdGroupBy = "WHERE LedgerName Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;
                            break;
                        case 3:
                            _objSearch.WhereNdGroupBy = "WHERE Mobile Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId; 
                            break;
                        case 4:
                            _objSearch.WhereNdGroupBy = "WHERE cCountry Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;
                            break;
                        case 5:
                            _objSearch.WhereNdGroupBy = "WHERE State Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId; 
                            break;
                        case 6:
                            _objSearch.WhereNdGroupBy = "WHERE Zipcode Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;
                            break;
                        case 7:
                            _objSearch.WhereNdGroupBy = "WHERE Occupation Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;
                            break;
                        case 8:
                            _objSearch.WhereNdGroupBy = "WHERE ProofNo Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;
                            break;

                        case 9:
                            _objSearch.WhereNdGroupBy = "WHERE LedgerAddress Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;
                            break;
                        case 10:
                            _objSearch.WhereNdGroupBy = "WHERE EmailId Like '%" + mtxtFieldValue.Text + "%' and EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;
                            break;

                        default:
                            _objSearch.WhereNdGroupBy = "WHERE EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;
                            break;

                    }
                }
                else

                    _objSearch.WhereNdGroupBy = "WHERE EnrollDate BETWEEN (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollFrom.Text + "', 105), 23)) and (SELECT convert(varchar(10), Convert(date, '" + dtpEnrollTo.Text + "', 105), 23)) and isnull(status,0)=0  and BranchId=" + BranchId;


                _objSearch.OrderBy = "";
                var gridSource = _bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count > 0)
                {

                    dgvList.Rows.Clear();
                    dgvList.Rows.Add(gridSource.Count);
                    for (int i = 0; i < gridSource.Count(); i++)
                    {

                        dgvList.Rows[i].Cells[gAccID].Value = gridSource[i].AccID.ToString();

                        dgvList.Rows[i].Cells[gEnrollDate].Value = gridSource[i].EnrollDate.ToString();

                        if (gridSource[i].AccNo != null)
                            dgvList.Rows[i].Cells[gAccNo].Value = gridSource[i].AccNo.ToString();
                        else
                            dgvList.Rows[i].Cells[gAccNo].Value = "";

                        if (gridSource[i].AccName != null)
                            dgvList.Rows[i].Cells[gAccName].Value = gridSource[i].AccName.ToString();
                        else
                            dgvList.Rows[i].Cells[gAccName].Value = "";

                        if (gridSource[i].DOB != null)
                            dgvList.Rows[i].Cells[gdob].Value = gridSource[i].DOB.ToString();

                        else
                            dgvList.Rows[i].Cells[gdob].Value = "";


                        if (gridSource[i].CInstitute != null)
                            dgvList.Rows[i].Cells[gInstitute].Value = gridSource[i].CInstitute.ToString();

                        else
                            dgvList.Rows[i].Cells[gInstitute].Value = "";


                        if (gridSource[i].Occupation != null)
                            dgvList.Rows[i].Cells[gOccupation].Value = gridSource[i].Occupation.ToString();

                        else
                            dgvList.Rows[i].Cells[gOccupation].Value = "";

                        if (gridSource[i].ZipCode != null)
                            dgvList.Rows[i].Cells[gZipcode].Value = gridSource[i].ZipCode.ToString();

                        else
                            dgvList.Rows[i].Cells[gZipcode].Value = "";

                        if (gridSource[i].AccPhone != null)
                            dgvList.Rows[i].Cells[gMobile].Value = gridSource[i].AccPhone.ToString();

                        else
                            dgvList.Rows[i].Cells[gMobile].Value = "";


                        if (gridSource[i].cEmergency != null)
                            dgvList.Rows[i].Cells[gEmergency].Value = gridSource[i].cEmergency.ToString();

                        else
                            dgvList.Rows[i].Cells[gEmergency].Value = "";

                        if (gridSource[i].cCountry != null)
                            dgvList.Rows[i].Cells[gCountry].Value = gridSource[i].cCountry.ToString();

                        else
                            dgvList.Rows[i].Cells[gCountry].Value = "";

                        if (gridSource[i].State != null)
                            dgvList.Rows[i].Cells[gState].Value = gridSource[i].State.ToString();

                        else
                            dgvList.Rows[i].Cells[gState].Value = "";

                        if (gridSource[i].City != null)
                            dgvList.Rows[i].Cells[gCity].Value = gridSource[i].City.ToString();

                        else
                            dgvList.Rows[i].Cells[gCity].Value = "";

                        if (gridSource[i].ProofNo != null)
                            dgvList.Rows[i].Cells[gProofNo].Value = gridSource[i].ProofNo.ToString();

                        else
                            dgvList.Rows[i].Cells[gProofNo].Value = "";

                        if (gridSource[i].AccAddress != null)
                            dgvList.Rows[i].Cells[gAccAddress].Value = gridSource[i].AccAddress.ToString();

                        else
                            dgvList.Rows[i].Cells[gAccAddress].Value = "";

                        if (gridSource[i].AccEmail != null)
                            dgvList.Rows[i].Cells[gAccEmail].Value = gridSource[i].AccEmail.ToString();

                        else
                            dgvList.Rows[i].Cells[gAccEmail].Value = "";



                    }
                    lblTotLocker.Text = gridSource.Count() + " Records";
                    dgvList.Columns[gAccNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gAccName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gdob].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gInstitute].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gOccupation].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gCountry].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gState].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gCity].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gAccAddress].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gAccEmail].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                    dgvList.Columns[gZipcode].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvList.Columns[gProofNo].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                  

                    dgvList.Columns[gAccNo].Width = 150;
                    dgvList.Columns[gAccName].Width = 150;
                    dgvList.Columns[gInstitute].Width = 150;
                    dgvList.Columns[gOccupation].Width = 150;
                    dgvList.Columns[gCountry].Width = 80;
                    dgvList.Columns[gAccEmail].Width = 150;
                    dgvList.Columns[gAccAddress].Width = 200;
                    dgvList.Columns[gProofNo].Width = 100;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.ToString());


            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmCustAccount frmCustAccount = new FrmCustAccount();
            frmCustAccount.ShowDialog();
        }
    }
}

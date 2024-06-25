using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
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

namespace FoodandBeverage.Forms
{
	public partial class FrmPromo : Form
	{
		string dtfrom = "";
		string dtto = "";

		ClsGeneral clsgen = new ClsGeneral();

		public FrmPromo()
		{
			InitializeComponent();
		}

		private void FrmPromo_Load(object sender, EventArgs e)
		{
			Clear();

			///dtpfrom.Format = DateTimePickerFormat.Custom;
			//dtpfrom.CustomFormat = "dd-MM-yyyy hh:mm:ss";
			//dgvProCode.BorderStyle = BorderStyle.None;
			//dgvProCode.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
			//dgvProCode.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			//dgvProCode.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
			//dgvProCode.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
			//dgvProCode.BackgroundColor = Color.White;
			//dgvProCode.EnableHeadersVisualStyles = false;
			//dgvProCode.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
			//dgvProCode.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
			//dgvProCode.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				dtfrom = dtpfrom.Value.ToString("yyyy-MM-dd") + " " + dtpfromtime.Value.ToString("HH:mm:ss");
				dtto = dtpto.Value.ToString("yyyy-MM-dd") + " " + dtptotime.Value.ToString("HH:mm:ss");
				if (MandatoryCheck())
				{
					bool flage = true;
					DmnPromoMaster dmnProMaster = new DmnPromoMaster();
					BllPromoMaster bllPromo = new BllPromoMaster();
					dmnProMaster.PromoMId = Convert.ToInt32(txtPromoMId.Text);
					dmnProMaster.Caption = txtprocap.Text;
					dmnProMaster.Description = txtprodesc.Text;
					dmnProMaster.StartDate = Convert.ToDateTime(dtfrom);
					dmnProMaster.EndDate = Convert.ToDateTime(dtto);
					dmnProMaster.MinBillAmt = Convert.ToDecimal(txtgrt.Text);
					dmnProMaster.MaxDiscount = Convert.ToDecimal(txtmaxdis.Text);
					dmnProMaster.DiscountPer = Convert.ToDecimal(txtdiscper.Text);
					dmnProMaster.PromoLabel = txtproLbl.Text;
					dmnProMaster.TotalPromo = Convert.ToInt32(txttotPro.Text);
					dmnProMaster.Status = 1;
					dmnProMaster.CreatedBy = FrmDashboard.UserId;
					var data = bllPromo.AddPromo("prdInsertPromoMaster", dmnProMaster);
					if (data.PromoMId != 0)
					{
						DmnPromoDetails dmnPromoDetails = new DmnPromoDetails();
						dmnPromoDetails.PromoMId = data.PromoMId;
						for (int i = 0; i < dgvProCode.Rows.Count; i++)
						{
							if (dgvProCode.Rows[i].Cells[2].Value != "" && dgvProCode.Rows[i].Cells[2].Value != null)
							{
								dmnPromoDetails.Code = Convert.ToString(dgvProCode.Rows[i].Cells[2].Value);
								dmnPromoDetails.Status = 1;
								dmnPromoDetails.CreatedBy = Convert.ToInt32(FrmLogin.UserId);
								bool details = bllPromo.AddPromoDetails("PrdInsertUpdPromoDetails", dmnPromoDetails);
								if (details == false)
								{
									flage = false;
									break;
								}
							}
						}
						if (flage == true)
						{
							CMessageBox.Show("Promo Details Saved Successfuly", "Save Success", MessageBoxButtons.OK);
							Clear();
						}
						else
						{
							CMessageBox.Show("Promo Details Save Failed", "Save Failed", MessageBoxButtons.OK);
						}
					}
					else
					{
						CMessageBox.Show("Promo Details Save Failed", "Save Failed", MessageBoxButtons.OK);
					}
				}
			}
			catch (Exception ex)
			{
				CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsgen.LogException("PromoCode page btnSave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.PromoCode));
			}
		}

		private void btnrefresh_Click(object sender, EventArgs e)
		{
			Clear();
		}
		private void btngen_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(txttotPro.Text) && Convert.ToInt32(txttotPro.Text) > 0)
				{
					dgvProCode.Rows.Clear();
					int val = Convert.ToInt32(txttotPro.Text);
					string Lbl = txtproLbl.Text;
					Random random = new Random();
					dgvProCode.Rows.Add(val);
					for (int i = 0; i < val; i++)
					{
						int c = i + random.Next(0, 99999);
						dgvProCode.Rows[i].Cells[1].Value = i + 1;
						dgvProCode.Rows[i].Cells[2].Value = Lbl + c;
					}
				}
			}
			catch (Exception ex)
			{
				CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsgen.LogException("PromoCode page btngen_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.PromoCode));
			}

		}

		private void btncolse_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		public void Clear()
		{
			try
			{
				dgvProCode.Rows.Clear();
				txtprocap.Text = "";
				txtprodesc.Text = "";
				dtpfrom.MinDate = DateTime.Now;
				dtpto.MinDate = DateTime.Now;
				dtpfrom.Value = DateTime.Now;
				dtpto.Value = DateTime.Now;
				txtgrt.Text = "0";
				txtmaxdis.Text = "0";
				txtdiscper.Text = "0";
				txtproLbl.Text = "";
				txttotPro.Text = "0";
				txtPromoMId.Text = "0";
				btnSave.Visible = true;
			}
			catch (Exception ex)
			{
				CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsgen.LogException("PromoCode page Clear()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.PromoCode));
			}
		}
		public bool MandatoryCheck()
		{
			try
			{
				if (string.IsNullOrEmpty(txtprocap.Text))
				{
					CMessageBox.Show("Promo Caption Is A Mandatory Field", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtprocap.Focus();
					return false;
				}
				else if (string.IsNullOrEmpty(txtmaxdis.Text) || Convert.ToDecimal(txtmaxdis.Text) <= 0)
				{
					CMessageBox.Show("Promo Maximum Discount Amount Is A Mandatory Field", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtmaxdis.Focus();
					return false;
				}
				else if (string.IsNullOrEmpty(txtdiscper.Text) || Convert.ToDecimal(txtdiscper.Text) <= 0)
				{
					CMessageBox.Show("Promo Discount Percentage Is A Mandatory Field", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtdiscper.Focus();
					return false;
				}
				else if (string.IsNullOrEmpty(txtproLbl.Text))
				{
					CMessageBox.Show("Promo Label Is A Mandatory Field", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtproLbl.Focus();
					return false;
				}
				else if (string.IsNullOrEmpty(txttotPro.Text) || Convert.ToDecimal(txttotPro.Text) <= 0)
				{
					CMessageBox.Show("Promo Count Must Be Greater Than Zero", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txttotPro.Focus();
					return false;
				}
				else if (dgvProCode.Rows.Count <= 0)
				{
					CMessageBox.Show("Promos Must Be Greater Than Zero", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					btngen.Focus();
					return false;
				}
				//else if(Convert.ToDateTime(dtfrom)<=DateTime.Now)
				//{
				//	CMessageBox.Show("From Time Must Be Greater Than Current DateTime ", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				//	dtpfrom.Focus();
				//	return false;
				//}	
				else if (Convert.ToDateTime(dtto) <= Convert.ToDateTime(dtfrom))
				{
					CMessageBox.Show("To Time Must Be Greater Than From Time ", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					dtpto.Focus();
					return false;
				}
				else
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsgen.LogException("PromoCode page MandatoryCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.PromoCode));
				return false;
			}
		}
		private void dtpfrom_ValueChanged(object sender, EventArgs e)
		{
			dtpto.MinDate = DateTime.Now;
		}
		public void CheckPromoLabel(string val)
		{
			try
			{
				DmnSearch dmnSearch = new DmnSearch();
				BllSearch<DmnPromoMaster> bllSearch = new BllSearch<DmnPromoMaster>();
				dmnSearch.Select = "*";
				dmnSearch.Table = "tblPromoMaster ";
				dmnSearch.WhereNdGroupBy = "where PromoLabel='" + val + "' and Status=1";
				var data = bllSearch.GetSearchData("prdSearch", dmnSearch);
				if (data.Count > 0)
				{
					CMessageBox.Show("Promo Lable Is Alredy Exist", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					txtproLbl.Focus();
					return;
				}
			}
			catch (Exception ex)
			{
				CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsgen.LogException("PromoCode page CheckPromoLabel()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.PromoCode));
			}
		}

		private void txtproLbl_Leave(object sender, EventArgs e)
		{
			CheckPromoLabel(txtproLbl.Text);
		}


		private void txttotPro_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void btnserach_Click(object sender, EventArgs e)
		{
			try
			{
				string returnValue = string.Empty;
				DmnSearch _objSearch = new DmnSearch();
				_objSearch.ColumnHeaders = "Id,Caption,Minimum BillAmt,PromoLabel,TotalPromo,MaxDiscount";
				_objSearch.Select = "PromoMId Id,Caption Field1,MinBillAmt Field2,PromoLabel Field3,TotalPromo Field4,MaxDiscount Field6";
				_objSearch.Table = "tblPromoMaster";
				_objSearch.WhereNdGroupBy = " where Status=1";
				_objSearch.OrderBy = " ";
				using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
				{
					var dialogResult = search.ShowDialog();
					if (dialogResult == DialogResult.OK)
					{
						returnValue = Convert.ToString(search._retValue);
					}
				}
				if (returnValue.ToString() != null && returnValue.ToString() != "")
				{
					BllSearch<DmnPromoMaster> bllSearch = new BllSearch<DmnPromoMaster>();
					DmnSearch objSearch = new DmnSearch();
					objSearch.Select = "*";
					objSearch.Table = "tblPromoMaster";
					objSearch.WhereNdGroupBy = " where Status=1 and PromoMId=" + returnValue;
					var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
					if (gridSource.Count > 0)
					{
						txtPromoMId.Text = Convert.ToString(gridSource[0].PromoMId);
						txtprocap.Text = gridSource[0].Caption;
						txtprodesc.Text = gridSource[0].Description;
						dtpto.MinDate = Convert.ToDateTime(gridSource[0].StartDate.ToString("dd-MM-yyyy"));
						dtpfrom.MinDate = Convert.ToDateTime(gridSource[0].StartDate.ToString("dd-MM-yyyy"));
						dtpto.Value = Convert.ToDateTime(gridSource[0].EndDate.ToString("dd-MM-yyyy"));
						dtptotime.Value = Convert.ToDateTime(gridSource[0].EndDate.ToString("hh:mm:ss"));
						dtpfrom.Value = Convert.ToDateTime(gridSource[0].StartDate.ToString("dd-MM-yyyy"));
						dtpfromtime.Value = Convert.ToDateTime(gridSource[0].StartDate.ToString("hh:mm:ss"));
						txtgrt.Text = Convert.ToString(gridSource[0].MinBillAmt);
						txtmaxdis.Text = Convert.ToString(gridSource[0].MaxDiscount);
						txtdiscper.Text = Convert.ToString(gridSource[0].DiscountPer);
						txtproLbl.Text = Convert.ToString(gridSource[0].PromoLabel);
						txttotPro.Text = Convert.ToString(gridSource[0].TotalPromo);
						BllSearch<DmnPromoDetails> bllSearch1 = new BllSearch<DmnPromoDetails>();
						objSearch.Select = "*";
						objSearch.Table = "tblPromoDetails";
						objSearch.WhereNdGroupBy = " where Status>0 and PromoMId=" + returnValue;
						objSearch.OrderBy = " order by Status ";
						var gridSource1 = bllSearch1.GetSearchData("prdSearch", objSearch);
						dgvProCode.Rows.Clear();
						dgvProCode.Rows.Add(gridSource1.Count());
						for (int i = 0; i < dgvProCode.Rows.Count; i++)
						{
							if (gridSource1[i].Status == 2)
							{
								btnSave.Visible = false;
								dgvProCode.Rows[i].DefaultCellStyle.BackColor = Color.LightGray;
							}
							dgvProCode.Rows[i].Cells[1].Value = i + 1;
							dgvProCode.Rows[i].Cells[2].Value = gridSource1[i].Code;
						}
					}
				}
			}
			catch (Exception ex)
			{
				CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsgen.LogException("PromoCode page btnserach_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.PromoCode));
			}

		}
		private void btncancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void txtmaxdis_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		private void txtdiscper_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		private void txtgrt_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
			{
				e.Handled = true;
			}
		}

		private void txtgrt_Validating(object sender, CancelEventArgs e)
		{
			decimal value;
			if (!decimal.TryParse(txtgrt.Text, out value))
			{
				txtgrt.Text = "0";
			}
		}

		private void txtmaxdis_Validating(object sender, CancelEventArgs e)
		{
			decimal value;
			if (!decimal.TryParse(txtmaxdis.Text, out value))
			{
				txtmaxdis.Text = "0";
			}
		}

		private void txtdiscper_Validating(object sender, CancelEventArgs e)
		{
			decimal value;
			if (!decimal.TryParse(txtdiscper.Text, out value))
			{
				txtdiscper.Text = "0";
			}
		}
		private void txttotPro_Validating(object sender, CancelEventArgs e)
		{
			decimal value;
			if (!decimal.TryParse(txttotPro.Text, out value))
			{
				txttotPro.Text = "0";
			}
		}

		#region TexBox Round function
		public void textb_shape()
		{
			try
			{
				// Apply rounded corners to text boxes based on the list
				foreach (TextBox textBox in new TextBox[] { txtprocap, txtprodesc, txtgrt, txtmaxdis, txtdiscper, txtproLbl, txttotPro })
				{
					ApplyRoundedTextBox(textBox, 10);
					textBox.Font = new Font(textBox.Font.FontFamily, 12);
					textBox.BorderStyle = BorderStyle.None;
				}

			}
			catch (Exception ex)
			{
				CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsgen.LogException("PromoCode page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.PromoCode));
			}

		}

		private void ApplyRoundedTextBox(TextBox textBox, int radius)
		{
			try
			{
				// Create a new GraphicsPath object
				GraphicsPath path = new GraphicsPath();
				// Define the shape of the path using a rounded rectangle
				Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
				path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
				path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
				path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
				path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
				path.CloseAllFigures();
				// Set the Region property of the textbox to the new path
				textBox.Region = new Region(path);
			}
			catch (Exception ex)
			{
				CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsgen.LogException("PromoCode page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.PromoCode));
			}
		}

		#endregion

		private void FrmPromo_Resize(object sender, EventArgs e)
		{
			textb_shape();
		}

		private void txtdiscper_Leave(object sender, EventArgs e)
		{
			try
			{
				decimal value;
				if (!decimal.TryParse(txtdiscper.Text, out value))
				{
					txtdiscper.Text = "0";
				}
				if (Convert.ToDecimal(txtdiscper.Text) > 100 && Convert.ToDecimal(txtdiscper.Text) <= 0)
				{
					CMessageBox.Show("Discount % Must Be Less Than 100 and Greater Than Zero", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				clsgen.LogException("PromoCode page txtdiscper_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.PromoCode));
			}

		}

		private void txtprocap_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				e.SuppressKeyPress = true;
				SendKeys.Send("{TAB}");
			}
		}
	}
}

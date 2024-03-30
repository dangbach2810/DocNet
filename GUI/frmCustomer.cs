using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class frmCustomer : Form
    {
        private int id;
        private string name;
        BUS_Customer busCustomer = new BUS_Customer();
        public frmCustomer()
        {
            InitializeComponent();
        }
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            
            gvCustomer.DataSource = busCustomer.ListOfCustomers();
            LoadGridView();
            SetValue(true, false);
            txtCusName.Focus();
        }
        private void LoadGridView()
        {
            gvCustomer.Columns[0].HeaderText = "Mã khách hàng";
            gvCustomer.Columns[1].HeaderText = "Họ tên";
            gvCustomer.Columns[2].HeaderText = "Địa chỉ";
            gvCustomer.Columns[3].HeaderText = "Số điện thoại";
            
            foreach (DataGridViewColumn item in gvCustomer.Columns)
            {
                item.DividerWidth = 1;
            }
            gvCustomer.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            gvCustomer.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            
        }
        private void SetValue(bool param, bool isLoad)
        {
            
            txtCusAddress.Text = null;
            txtCusPhoneNumber.Text = null;
            btnCusInsert.Enabled = param;
            txtCusName.Text = null;
            txtCusName.Focus();
            if (isLoad)
            {
                btnCusUpdate.Enabled = false;
                btnCusDelete.Enabled = false;
            }
            else
            {
                btnCusUpdate.Enabled = !param;
                btnCusDelete.Enabled = !param;
            }
            
        }
        private void MsgBox(string message, bool isError = false)
        {
            if (isError)
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            // Kiểm tra null hoặc rỗng
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
               
            }

            // Kiểm tra độ dài
            if (phoneNumber.Length != 10)
            {
                return false;
            }

            // Kiểm tra ký tự đầu tiên
            if (phoneNumber[0] != '0')
            {
                return false;
            }

            // Kiểm tra các ký tự còn lại
            foreach (char c in phoneNumber.Substring(1))
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
        
        private void gvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCusUpdate.Enabled = true;
            btnCusDelete.Enabled = true;
            btnCusInsert.Enabled = false;
            
            txtCusName.Text = gvCustomer.CurrentRow.Cells[1].Value.ToString();
            txtCusAddress.Text = gvCustomer.CurrentRow.Cells[2].Value.ToString();
            txtCusPhoneNumber.Text = gvCustomer.CurrentRow.Cells[3].Value.ToString();
        }
        private void btnCusInsert_Click(object sender, EventArgs e)
        {
            if (txtCusAddress.Text != "" && txtCusName.Text != "" && txtCusPhoneNumber.Text != "")
            {
                if (IsValidPhoneNumber(txtCusPhoneNumber.Text))
                {
                    tblCustomer tblCustomer = new tblCustomer();
                    tblCustomer.Name = txtCusName.Text;
                    tblCustomer.Address = txtCusAddress.Text;
                    tblCustomer.PhoneNumber = txtCusPhoneNumber.Text;
                    if (busCustomer.InsertCustomer(tblCustomer))
                    {
                        SetValue(true, false);
                        gvCustomer.DataSource = busCustomer.ListOfCustomers();
                        LoadGridView();
                        MsgBox("Thêm khách hàng thành công!", false);

                    }
                    else
                        MsgBox("Không thể thêm khách hàng được!", true);

                }
                else MsgBox("Số điện thoại không đúng định dạng!");
            }
            else MsgBox("Thiếu trường thông tin!", true);

        }
        private void btnCusDelete_Click(object sender, EventArgs e)
        {
            id = int.Parse(gvCustomer.CurrentRow.Cells[0].Value.ToString());
            if (busCustomer.DeleteCustomer(id))
            {
                SetValue(true, false);
                gvCustomer.DataSource = busCustomer.ListOfCustomers();
                LoadGridView();
                MsgBox("Xóa khách hàng thành công", false);
            }
            else
                MsgBox("Xóa khách hàng không thành công", true);
        }
        private void btnCusUpdate_Click(object sender, EventArgs e)
        {
            if (txtCusAddress.Text != "" && txtCusName.Text != ""
               && txtCusPhoneNumber.Text != "")
            {

                tblCustomer tblCustomer = new tblCustomer();
                tblCustomer.Name = txtCusName.Text;
                tblCustomer.Address = txtCusAddress.Text;

                tblCustomer.PhoneNumber = txtCusPhoneNumber.Text;
                if (IsValidPhoneNumber(txtCusPhoneNumber.Text))
                    {
                if (busCustomer.UpdateCustomer(tblCustomer))
                {
                   
                        gvCustomer.DataSource = busCustomer.ListOfCustomers();
                        SetValue(true, false);
                        LoadGridView();
                        MsgBox("Sửa thông tin khách hàng thành công!", false);
                   
                }
                    else
                        MsgBox("Không được phép sửa tên!", true);
                }
                    else MsgBox("Số điện thoại không đúng định dạng!", true);
            }
            else MsgBox("Thiếu thông tin !", true);
        }
        private void txtCusSearch_TextChanged(object sender, EventArgs e)
        {
            name = txtCusSearch.Text.Trim();
            if (name == "")
            {
                frmCustomer_Load(sender, e);
                txtCusSearch.Focus();
            }
            else
            {
                gvCustomer.DataSource = busCustomer.SearchCustomer(name);
            }
        }
        private void btnCusRefesh_Click(object sender, EventArgs e)
        {   
           
            SetValue(true, false);
            MsgBox("Làm mới thành công",false);
            
        }
        private void frmCustomer_Shown(object sender, EventArgs e)
        {
            txtCusName.Focus();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MsgBox("Xin chào nhe!!");
        }
    }
}

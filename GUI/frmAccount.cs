using System;
using BUS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace GUI
{
    public partial class frmAccount : Form
    {
        BUS_Employee busEmployee = new BUS_Employee();
        private string email;

        public frmAccount(string email)
        {
            InitializeComponent();
            this.email = email;
        }

        private void LoadData()
        {
            if (busEmployee != null)
            {
                tblEmployee nv = busEmployee.TimNVByEmail(email);
                if (nv != null)
                {
                    txtName.Text = nv.Name ?? "";
                    txtAddress.Text = nv.Address ?? "";
                    txtPhoneNumber.Text = nv.PhoneNumber ?? "";
                    txtEmail.Text = nv.Email ?? "";
                }
                else
                {
                    MessageBox.Show("Employee not found!");
                }
            }
            else
            {
                MessageBox.Show("Employee data service is not available!");
            }
        }

        private void frmAccount_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text; // Địa chỉ được nhập trong một TextBox có tên txtAddress
            string phoneNumber = txtPhoneNumber.Text; // Số điện thoại được nhập trong một TextBox có tên txtPhoneNumber

            // Tạo một đối tượng tblEmployee để chứa thông tin mới
            tblEmployee employee = new tblEmployee
            {
                Email = email, 
                Address = address,
                PhoneNumber = phoneNumber
                
            };

            // Gọi phương thức UpdateEmployeeAddressPhoneNumber để cập nhật thông tin
            bool isSuccess = busEmployee.UpdateEmployeeAddressPhoneNumber(employee);

            // Kiểm tra kết quả và hiển thị thông báo tương ứng
            if (isSuccess)
            {
                MessageBox.Show("Employee information updated successfully.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to update employee information.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}

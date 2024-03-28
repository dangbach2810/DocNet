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
                MessageBox.Show("Sửa thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Không thể sửa được thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool isRetypingPassword = false;

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!isRetypingPassword)
            {
                if (txtOldPassword.Text != "")
                {
                    if (txtNewPassword.Text == txtRepeatPassword.Text)
                    {
                        busEmployee = new BUS_Employee();
                        if (busEmployee.ChangePassword(txtEmail.Text, txtOldPassword.Text, txtNewPassword.Text))
                        {
                            MessageBox.Show("Đổi mật khẩu thành công, vui lòng đăng nhập lại.", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Properties.Settings.Default.password = "";
                            Properties.Settings.Default.Save();
                            Application.Restart();
                        }
                        else MessageBox.Show("Mật khẩu cũ không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        isRetypingPassword = true;
                        txtRepeatPassword.Clear();
                        MessageBox.Show("Mật khẩu mới không trùng nhau! Vui lòng nhập lại mật khẩu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else MessageBox.Show("Vui lòng nhập mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (txtRepeatPassword.Text == txtNewPassword.Text)
                {
                    isRetypingPassword = false; // Reset biến để cho lần sau không nhảy vào đoạn code này
                    btnChangePassword_Click(sender, e); // Gọi lại sự kiện để thực hiện cập nhật mật khẩu
                }
                else
                {
                    txtRepeatPassword.Clear();
                    MessageBox.Show("Mật khẩu mới không trùng nhau! Vui lòng nhập lại mật khẩu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

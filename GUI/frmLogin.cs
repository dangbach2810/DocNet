using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
namespace GUI
{
    public partial class frmLogin : Form
    {
        BUS_Employee busEmployee = new BUS_Employee();
        public frmLogin()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "" && txtPassword.Text != "")
            {
                if (busEmployee.Login(txtEmail.Text, txtPassword.Text))
                {

                    //MessageBox.Show("Đăng nhập thành công!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Properties.Settings.Default.isSave = tglRememberMe.Checked;
                    if (tglRememberMe.Checked)
                    {
                        Properties.Settings.Default.email = txtEmail.Text;
                        Properties.Settings.Default.password = txtPassword.Text;
                    }
                    Properties.Settings.Default.Save();
                    frmMain fMain = new frmMain(txtEmail.Text);
                    this.Hide();
                    fMain.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai email hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Text = "";

                    txtEmail.Focus();
                }
            }
            else if (txtEmail.Text == "" && txtPassword.Text == "")
            {
                MessageBox.Show("Không được để trống thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Text = "";
                txtPassword.Text = "";
                txtEmail.Focus();
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.isSave)
            {
                txtEmail.Text = Properties.Settings.Default.email;
                txtPassword.Text = Properties.Settings.Default.password;
                tglRememberMe.Checked = true;
            }
        }
        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "")
            {
                busEmployee = new BUS_Employee();
                if (busEmployee.IsExistEmail(txtEmail.Text))
                {
                    frmResetPassword resetForm = new frmResetPassword(txtEmail.Text);
                    resetForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Email bạn vừa nhập không có trong hệ thống", "Thông báo");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập Email!", "Thông báo");
            }
        }
        private void guna2ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmResetPassword : Form
    {
        private string _email;
        BUS_Employee busEmployee = new BUS_Employee();

        public frmResetPassword(string email)
        {
            InitializeComponent();
            _email = email;
        }

        private void Form_ResetPassword(object sender, EventArgs e)
        {

        }

        private void Form_ConfirmPassword (object sender, EventArgs e)
        {

        }

       

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtComfirmPassword.Text;
            if(newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (busEmployee.UpdatePassword(_email, newPassword))
                {
                    /*SendMail loader = new SendMail(txtEmail.Text, password, true);
                    loader.ShowDialog();*/
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
                    this.Close();
                }
                else
                    MessageBox.Show("Không thực hiện được", "Thông báo"); 
            }
        }

       
    }
}

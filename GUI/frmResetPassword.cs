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
        BUS_Mail busMail;

        public frmResetPassword(string email)
        {
            InitializeComponent();
            _email = email;

        }

        private void Form_ResetPassword(object sender, EventArgs e)
        {

        }

        private void Form_ConfirmPassword(object sender, EventArgs e)
        {

        }



        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            
            busMail = new BUS_Mail(txtEmail.ToString());
            if (busMail.Send(_email))
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo");
                    this.Close();
                }
                else
                    MessageBox.Show("Không thực hiện được", "Thông báo");
        }
    }
}
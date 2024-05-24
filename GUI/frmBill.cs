﻿using System;
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
    public partial class frmBill : Form
    {
        frmBillInfo fBillInfo = new frmBillInfo();
        BUS_Bill busBill = new BUS_Bill();

        public frmBill()
        {
            InitializeComponent();
           // fBillInfo = new frmBillInfo(email);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.Hide();
            fBillInfo.ShowDialog();
            this.Show();
            /*gvBill.DataSource = busBill.ListOfBills();
            LoadGridView();*/
        }

        private void LoadGridView()
        {
            gvBill.Columns[0].HeaderText = "Mã HD";
            gvBill.Columns[1].HeaderText = "Tên KH";
            gvBill.Columns[2].HeaderText = "Thời gian";
            gvBill.Columns[3].HeaderText = "Tổng tiền";
            foreach (DataGridViewColumn item in gvBill.Columns)
            {
                item.DividerWidth = 1;
            }
            gvBill.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gvBill.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gvBill.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            string name = txtSearch.Text.Trim();
            if (name == "")
            {
                frmBill_Load(sender, e);
                txtSearch.Focus();
            }
            else
            {
                gvBill.DataSource  = busBill.SearchCustomerInBill(txtSearch.Text);
            }
        }
        private void MsgBox(string message, bool isError = false)
        {
            if (isError)
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
       
        private void frmBill_Load(object sender, EventArgs e)
        {
            gvBill.DataSource = busBill.ListOfBills();
            LoadGridView();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            int id = int.Parse(gvBill.CurrentRow.Cells[0].Value.ToString());
            if (busBill.DeleteBill(id))
            {
                MsgBox("Xóa hóa đơn thành công", false);
                gvBill.DataSource = busBill.ListOfBills();
                LoadGridView();
            }
            else
                MsgBox("Xóa hóa đơn không thành công", true);
        }
    }
}

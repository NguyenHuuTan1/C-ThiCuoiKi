﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QUANLYHOSONHANVIEN.DataLayer;
using QUANLYHOSONHANVIEN.Controller;

namespace QUANLYHOSONHANVIEN.GiaoDien
{
    public partial class frmThemHinhThucDaoTao : DevComponents.DotNetBar.Office2007Form
    {
        public frmThemHinhThucDaoTao()
        {
            InitializeComponent();
        }
        HinhThucDaoTaoCtr ctr = new HinhThucDaoTaoCtr();
        HinhThucDaoTaoData data = new HinhThucDaoTaoData();
        private void frmThemHinhThucDaoTao_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            btnThemMoi.Enabled = false;
            ctr.HienthiHINHTHUCDTDataGridview(dataGridViewNganhHoc);
            if (dataGridViewNganhHoc.Rows.Count > 0)
            {
                btnCapNhat.Enabled = true;
                btnXoa.Enabled = true;
            }
            else
            {
                btnCapNhat.Enabled = false;
                btnXoa.Enabled = false;
            }
        }
        private bool kiemtra()
        {
            if (txttenngoai.Text == "")
            {
                txttenngoai.BackColor = Color.Yellow;
                txttenngoai.Focus();
                return false;
            }
            return true;
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            btnThemMoi.Enabled = true;
            long ma = ThamSo.TaoMaHINHTHUCDAOTAO;
            ThamSo.TaoMaHINHTHUCDAOTAO = ma + 1;
            string maso = "HTDT" + ma.ToString();
            txtmangoai.Text = maso;
            txttenngoai.Text = "";
            btnThemMoi.Enabled = true;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (kiemtra())
            {
                data.ThemHINHTHUCDAOTAO(txtmangoai.Text, txttenngoai.Text.ToString());
                loadData();
                btnThemMoi.Enabled = false;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridViewNganhHoc.CurrentRow;
            string manh = row.Cells["colmanh"].Value.ToString();
            if (kiemtra())
            {
                data.UpdateHINHTHUCDAOTAO(txttenngoai.Text, manh);
                loadData();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            DataGridViewRow row = dataGridViewNganhHoc.CurrentRow;
            string manh = row.Cells["colmanh"].Value.ToString();
            if (MessageBox.Show("Bạn muốn xoá Hình thức đào tạo này ?", "Thông Báo !", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                data.DeleteHINHTHUCDAOTAO(manh);
                loadData();
            }
        }

        private void dataGridViewNganhHoc_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridViewNganhHoc.Rows[e.RowIndex];
            //Thong tin chính
            txtmangoai.Text = row.Cells["colmanh"].Value.ToString();
            txttenngoai.Text = row.Cells["coltennh"].Value.ToString();
        }

        private void txttenngoai_TextChanged(object sender, EventArgs e)
        {
            txttenngoai.BackColor = Color.White;
        }
    }
}

﻿using PBL.BLL;
using PBL.DAL;
using PBL.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBL
{
    public partial class FormNhanVien : Form
    {
        public string Username { get; set; }
        public Boolean UserClosing = false;

        public FormNhanVien(string Username)
        {
            InitializeComponent();
            SetCBBSortDG();
            SetCBBSach();

            this.Username = Username;
            SetHoSo();
        }

        #region Logout

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            UserClosing = true;
            DialogResult dr;
            dr = MessageBox.Show("Bạn có muốn đăng xuất khỏi chương trình không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Dispose();
            }
            else
            {
                UserClosing = false;
            }
        }

        private void FormNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.ApplicationExitCall:
                    break;

                case CloseReason.FormOwnerClosing:
                    break;

                case CloseReason.MdiFormClosing:
                    break;

                case CloseReason.None:
                    break;

                case CloseReason.TaskManagerClosing:
                    break;

                case CloseReason.UserClosing:
                    if (!UserClosing)
                    {
                        DialogResult TL;
                        TL = MessageBox.Show("Bạn Có Muốn Thoát không", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (TL == DialogResult.No)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                    break;

                case CloseReason.WindowsShutDown:
                    break;

                default:
                    break;
            }
            UserClosing = false;
        }

        #endregion Logout

        #region Sach

        private void SetCBBSach()
        {
            using (DHP_07Entities db = new DHP_07Entities())
            {
                cbbLoaiTL.Items.Add(new CBBItem { Value = 0, Text = "ALL" });
                cbbLoaiTL.Text = "ALL";
                foreach (LoaiTaiLieu i in db.LoaiTaiLieux)
                {
                    cbbLoaiTL.Items.Add(new CBBItem { Value = i.MaLTL, Text = i.TenLoai });
                }
            }
        }

        private void ShowSach(string TenTL, int MaLTL)
        {
            dataGridViewQLSach.DataSource = QLTL_BLL.Instance.GetListTL(TenTL, MaLTL);
        }

        private void btnAddSach_Click(object sender, EventArgs e)
        {
            FormDuLieuSach f = new FormDuLieuSach(null);
            f.d = new FormDuLieuSach.MyDel(this.ShowSach);
            f.ShowDialog();
        }

        private void btnEditS_Click(object sender, EventArgs e)
        {
            if (dataGridViewQLSach.SelectedRows.Count == 1)
            {
                FormDuLieuSach f = new FormDuLieuSach(dataGridViewQLSach.SelectedRows[0].Cells["MaTL"].Value.ToString());
                f.d = new FormDuLieuSach.MyDel(this.ShowSach);
                f.ShowDialog();
            }
        }

        private void btnSearchS_Click(object sender, EventArgs e)
        {
            ShowSach(txtTenTaiLieuS.Text, ((CBBItem)cbbLoaiTL.SelectedItem).Value);
        }

        private void btnShowS_Click(object sender, EventArgs e)
        {
            ShowSach("", 0);
        }

        private void btnDelS_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewQLSach.SelectedRows)
            {
                QLTL_BLL.Instance.DeleteTL(i.Cells["MaTL"].Value.ToString());
            }
            ShowSach(txtTenTaiLieuS.Text, ((CBBItem)cbbLoaiTL.SelectedItem).Value);
        }

        private void btnSortS_Click(object sender, EventArgs e)
        {
            dataGridViewQLSach.DataSource = QLTL_BLL.Instance.SortTL(cbbSortS.Text.ToString());
        }

        private void btnViTri_Click(object sender, EventArgs e)
        {
            if (dataGridViewQLSach.SelectedRows.Count == 1)
            {
                string s = dataGridViewQLSach.SelectedRows[0].Cells["MaTL"].Value.ToString();
                MessageBox.Show("Ke Sach : " + s[0]
                    + "\nTang : " + s.Substring(1, 2)
                    + "\nHang : " + s.Substring(3, 2)
                    + "\nCot : " + s.Substring(5, 2));
            }
        }

        #endregion Sach

        #region DocGia

        private void SetCBBSortDG()
        {
            cbbSortDG.Items.Add(new CBBItem { Value = 0, Text = "MaDocGia Tang" });
            cbbSortDG.Items.Add(new CBBItem { Value = 1, Text = "MaDocGia Giam" });
            cbbSortDG.Items.Add(new CBBItem { Value = 2, Text = "MSSV Tang" });
            cbbSortDG.Items.Add(new CBBItem { Value = 3, Text = "MSSV Giam" });
            cbbSortDG.Items.Add(new CBBItem { Value = 4, Text = "HoTen Tang" });
            cbbSortDG.Items.Add(new CBBItem { Value = 5, Text = "HoTen Giam" });
            cbbSortDG.Items.Add(new CBBItem { Value = 6, Text = "NgaySinh Tang" });
            cbbSortDG.Items.Add(new CBBItem { Value = 7, Text = "NgaySinh Giam" });
            cbbSortDG.Items.Add(new CBBItem { Value = 8, Text = "MaLop Tang" });
            cbbSortDG.Items.Add(new CBBItem { Value = 9, Text = "MaLop Giam" });
            cbbSortDG.Items.Add(new CBBItem { Value = 10, Text = "NgayDK Tang" });
            cbbSortDG.Items.Add(new CBBItem { Value = 11, Text = "NgayDK Giam" });
        }

        private void ShowDG(string sothe, string name)
        {
            dataGridViewDocGia.DataSource = QLDG_BLL.Instance.GetListDGSelect(sothe, name);
        }

        private void btnSearchDG_Click(object sender, EventArgs e)
        {
            ShowDG(txtSoTheDG.Text, txtHoTenDG.Text);
        }

        private void btnShowDG_Click(object sender, EventArgs e)
        {
            ShowDG("", "");
        }

        private void btnAddDocGia_Click(object sender, EventArgs e)
        {
            FormDuLieuDocGia f = new FormDuLieuDocGia(null);
            f.d = new FormDuLieuDocGia.Mydel(this.ShowDG);
            f.ShowDialog();
        }

        private void btnEditDocGia_Click(object sender, EventArgs e)
        {
            if (dataGridViewDocGia.SelectedRows.Count == 1)
            {
                FormDuLieuDocGia f = new FormDuLieuDocGia(dataGridViewDocGia.SelectedRows[0].Cells["MSSV"].Value.ToString());
                f.d = new FormDuLieuDocGia.Mydel(this.ShowDG);
                f.ShowDialog();
            }
        }

        private void btnDelDG_Click(object sender, EventArgs e)
        {
            if (dataGridViewDocGia.SelectedRows.Count == 1)
            {
                string m = dataGridViewDocGia.SelectedRows[0].Cells["MSSV"].Value.ToString();
                QLDG_BLL.Instance.DelDG(QLDG_BLL.Instance.GetDGByMSSV(m));
            }
            ShowDG("", "");
        }

        private void btnSearchTKVP_Click(object sender, EventArgs e)
        {
            FormViPham f = new FormViPham();
            f.ShowDialog();
        }

        private void btnSortDG_Click(object sender, EventArgs e)
        {
            string[] arrListStr = cbbSortDG.Text.Split(' ');
            string m1 = arrListStr[0];
            string m2 = arrListStr[1];
            dataGridViewDocGia.DataSource = QLDG_BLL.Instance.SortDG(m1, m2);
        }

        #endregion DocGia

        #region PhieuMuon

        private void ShowPM(string TenDG, string MSSV)
        {
            dataGridViewPhieuMuon.DataSource = QLPM_BLL.Instance.GetListPM(TenDG, MSSV);
        }

        private void btnAddPhieuMuon_Click(object sender, EventArgs e)
        {
            FormAddDuLieuPhieuMuon f = new FormAddDuLieuPhieuMuon(Username);
            f.d = new FormAddDuLieuPhieuMuon.MyDel(this.ShowPM);
            f.ShowDialog();
        }

        private void btnReturnPM_Click(object sender, EventArgs e)
        {
            if (dataGridViewPhieuMuon.SelectedRows.Count == 1)
            {
                if (QLPM_BLL.Instance.CheckReturned(Convert.ToInt32(dataGridViewPhieuMuon.SelectedRows[0].Cells[0].Value))) MessageBox.Show("Phieu muon da tra roi !");
                else
                {
                    FormTraDuLieuPhieuMuon f = new FormTraDuLieuPhieuMuon(Convert.ToInt32(dataGridViewPhieuMuon.SelectedRows[0].Cells[0].Value));
                    f.d = new FormTraDuLieuPhieuMuon.MyDel(this.ShowPM);
                    f.ShowDialog();
                }
            }
        }

        private void btnSearchPM_Click(object sender, EventArgs e)
        {
            ShowPM(txtTenDocGiaPM.Text, txtMSSVPM.Text);
        }

        private void btnShowPM_Click(object sender, EventArgs e)
        {
            ShowPM("", "");
        }

        private void btnDelPM_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridViewPhieuMuon.SelectedRows)
            {
                QLPM_BLL.Instance.DeletePM(Convert.ToInt32(i.Cells["MaPhieuMuon"].Value));
            }
            ShowPM(txtTenDocGiaPM.Text, txtMSSVPM.Text);
        }

        private void btnSortPM_Click(object sender, EventArgs e)
        {
            dataGridViewPhieuMuon.DataSource = QLPM_BLL.Instance.SortPM(cbbSortPM.Text);
        }

        #endregion PhieuMuon

        #region HoSo

        public void SetHoSo()
        {
            txtTenNguoiDung.Text = QLNV_BLL.Instance.GetUserByUsername(Username).HoTen;
            txtTenNguoiDung.Enabled = false;
            dtpNgaySinhNV.Value = Convert.ToDateTime(QLNV_BLL.Instance.GetUserByUsername(Username).NgaySinh);
            dtpNgaySinhNV.Enabled = false;
            txtEmail.Text = QLNV_BLL.Instance.GetUserByUsername(Username).Email;
            txtEmail.Enabled = false;
            txtSDT.Text = QLNV_BLL.Instance.GetUserByUsername(Username).DienThoai;
            txtSDT.Enabled = false;
            txtUsername.Text = QLNV_BLL.Instance.GetUserByUsername(Username).Username;
            txtUsername.Enabled = false;
            txtOldPW.Hide();
            txtNewPW.Hide();
            txtConfirmPW.Hide();
            btnSavePassword.Hide();
            lbOldPW.Hide();
            lbNewPW.Hide();
            lbConfirmPW.Hide();
            btnCancelEditPW.Hide();
        }

        private void btnEditTenNguoiDung_Click(object sender, EventArgs e)
        {
            txtTenNguoiDung.Enabled = true;
        }

        private void btnSaveTenNguoiDung_Click(object sender, EventArgs e)
        {
            QLNV_BLL.Instance.EditHoTenNV(QLNV_BLL.Instance.GetUserByUsername(Username), QLNV_BLL.Instance.GetUserByUsername(Username).ID, txtTenNguoiDung.Text);
            MessageBox.Show("Thông tin đã được cập nhật");
            txtTenNguoiDung.Enabled = false;
        }

        private void btnEditNgaySinh_Click(object sender, EventArgs e)
        {
            dtpNgaySinhNV.Enabled = true;
        }

        private void btnSaveNgaySinh_Click(object sender, EventArgs e)
        {
            QLNV_BLL.Instance.EditNgaySinhNV(QLNV_BLL.Instance.GetUserByUsername(Username), QLNV_BLL.Instance.GetUserByUsername(Username).ID, dtpNgaySinhNV.Value);
            MessageBox.Show("Thông tin đã được cập nhật");
            dtpNgaySinhNV.Enabled = false;
        }

        private void btnEditEmail_Click(object sender, EventArgs e)
        {
            txtEmail.Enabled = true;
        }

        private void btnSaveEmail_Click(object sender, EventArgs e)
        {
            QLNV_BLL.Instance.EditEmailNV(QLNV_BLL.Instance.GetUserByUsername(Username), QLNV_BLL.Instance.GetUserByUsername(Username).ID, txtEmail.Text);
            MessageBox.Show("Thông tin đã được cập nhật");
            txtEmail.Enabled = false;
        }

        private void btnEditSDT_Click(object sender, EventArgs e)
        {
            txtSDT.Enabled = true;
            txtSDT.MaxLength = 10;
        }

        private void btnSaveSDT_Click(object sender, EventArgs e)
        {
            QLNV_BLL.Instance.EditSDTNV(QLNV_BLL.Instance.GetUserByUsername(Username), QLNV_BLL.Instance.GetUserByUsername(Username).ID, txtSDT.Text);
            MessageBox.Show("Thông tin đã được cập nhật");
            txtSDT.Enabled = false;
        }

        private void btnEditPassword_Click(object sender, EventArgs e)
        {
            txtOldPW.Show();
            txtNewPW.Show();
            txtConfirmPW.Show();
            btnSavePassword.Show();
            lbOldPW.Show();
            lbNewPW.Show();
            lbConfirmPW.Show();
            btnCancelEditPW.Show();
        }

        private void btnCancelEditPW_Click(object sender, EventArgs e)
        {
            txtOldPW.Hide();
            txtNewPW.Hide();
            txtConfirmPW.Hide();
            btnSavePassword.Hide();
            lbOldPW.Hide();
            lbNewPW.Hide();
            lbConfirmPW.Hide();
            txtOldPW.Text = "";
            txtNewPW.Text = "";
            txtConfirmPW.Text = "";
            btnCancelEditPW.Hide();
        }

        private void btnSavePassword_Click(object sender, EventArgs e)
        {
            if (QLNV_BLL.Instance.GetUserByUsername(Username).Password == txtOldPW.Text)
            {
                if (txtNewPW.Text == txtConfirmPW.Text)
                {
                    QLNV_BLL.Instance.ChangePass(QLNV_BLL.Instance.GetUserByUsername(Username), txtConfirmPW.Text);
                    MessageBox.Show("Đổi mật khẩu thành công !");
                    txtOldPW.Text = "";
                    txtNewPW.Text = "";
                    txtConfirmPW.Text = "";
                    txtOldPW.Hide();
                    txtNewPW.Hide();
                    txtConfirmPW.Hide();
                    btnSavePassword.Hide();
                    lbOldPW.Hide();
                    lbNewPW.Hide();
                    lbConfirmPW.Hide();
                    btnCancelEditPW.Hide();
                }
                else
                {
                    MessageBox.Show("Nhập sai mật khẩu mới");
                }
            }
            else
            {
                MessageBox.Show("Nhập sai mật khẩu cũ");
            }
        }

        #endregion HoSo
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HRM_VTHP.DanhMuc
{
    public partial class frmTamUng : Form
    {
        public frmTamUng()
        {
            InitializeComponent();
        }
        int kt = 0;
        int TamUngID = 0;
        void Load_DL()
        {
            string sql = "Select * from TamUng";
            DataTable dt = Core.Core.GetData(sql);
            grdTamUng.DataSource = dt;
        }
        void Reset()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnCapNhat.Enabled = false;
            btnHuyBo.Enabled = false;
            btnXoa.Enabled = false;
            txtTamUng.Text = "";
            txtTamUng.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            kt = 1;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnCapNhat.Enabled = true;
            btnHuyBo.Enabled = true;
            btnXoa.Enabled = false;
            txtTamUng.Text = "";
            txtTamUng.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            kt = 2;
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnCapNhat.Enabled = true;
            btnHuyBo.Enabled = true;
            btnXoa.Enabled = false;
            txtTamUng.Enabled = true;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if(kt == 1)
            {
                if (txtTamUng.Text == "")
                {
                    MessageBox.Show("Bạn chưa nhập tạm ứng");
                }
                else
                {
                    string sql = "Insert into TamUng(TenTamUng) values(N'" + txtTamUng.Text + "')";
                    if (Core.Core.RunSql(sql) == -1)
                    {
                        MessageBox.Show("Lỗi khi thêm mới");
                    }

                    else
                    {
                        Load_DL();
                    }
                }
            }
            else
            {
                string sql = "Update TamUng set TenTamUng =N'" + txtTamUng.Text + "' where TamUngID ='" + TamUngID + "'";
                Core.Core.RunSql(sql);
                Load_DL();
            }
            Reset();
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "Select * from ChiTietTamUng where TamUngID ='" + TamUngID + "'";
            DataTable dt = Core.Core.GetData(sql);
            if(dt.Rows.Count > 0)
            {
                MessageBox.Show("Bạn phải xóa bản ghi trong ChiTietTamUng");
            }
            else
            {
                sql = "Delete TamUng where TamUngID ='" + TamUngID + "'";
                Core.Core.RunSql(sql);
                Load_DL();
                Reset();
            }
            
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            TamUngID = int.Parse(gridView1.GetFocusedRowCellValue("TamUngID").ToString());
            txtTamUng.Text = gridView1.GetFocusedRowCellValue("TenTamUng").ToString();
        }

        private void frmTamUng_Load(object sender, EventArgs e)
        {
            Load_DL();
            Reset();
        }

        private void txtTamUng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Bạn không được phép nhập số");
            }
        }

        private void btnHuyBo_Click_1(object sender, EventArgs e)
        {
            Reset();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LK_CSDL
{
    public partial class CTDHform : Form
    {
        private string cnnStr = "Data Source=YUU\\SQLEXPRESS;Initial Catalog=THK_QLBH;Persist Security Info=True;User ID=sa;Password=1234";
        string _id1, _id2;
        Form1 form1;
        public CTDHform(string Id1, string Id2, Form1 form1)
        {
            this._id1 = Id1;
            this._id2 = Id2;
            InitializeComponent();
            this.txtSHD.Text = _id1.ToString();
            this.txtMH.Text = _id2.ToString();
            this.form1 = form1;
        }
        //hiện lên form
        private void CTDHform_Load(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(cnnStr);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM CHITIETDATHANG WHERE SoHD = '{_id1}' and MaH = '{_id2}'";
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            DataRow row = dt.Rows[0];
            this.txtSHD.Text = row["SoHD"].ToString();
            this.txtMH.Text = row["MaH"].ToString();
            this.txtGB.Text = row["GiaBan"].ToString();
            this.txtSL.Text = row["SoLuong"].ToString();


        }
        //sửa thông tin
        private void button1_Click(object sender, EventArgs e)
        {
            string giaBanDa = this.txtGB.Text;
            string soLuong = this.txtSL.Text;
            string query = $@" UPDATE CHITIETDATHANG SET 
                            CHITIETDATHANG.GiaBan = N'{giaBanDa}', 
                            CHITIETDATHANG.SoLuong = {soLuong} 
                            WHERE SoHD = '{_id1}' AND MaH = '{_id2}'";
            SqlConnection conn = new SqlConnection(cnnStr);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            int kq = cmd.ExecuteNonQuery();
            if (kq > 0)
            {
                MessageBox.Show("Đã sửa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.form1.ShowData("sp_select", "@tenBang", "CHITIETDATHANG");
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Không sửa được", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //xóa thông tin
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(cnnStr);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"DELETE FROM CHITIETDATHANG WHERE SoHD = '{_id1}' and MaH = '{_id2}'";
                int kq = cmd.ExecuteNonQuery();
                if (kq > 0)
                {
                    MessageBox.Show("Da xoa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.form1.ShowData("sp_select", "@tenBang", "CHITIETDATHANG");
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Không xóa được", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

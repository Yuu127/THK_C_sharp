using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LK_CSDL
{
    public partial class KhachHangDialogForm : Form
    {
        private int Id;
        private string cnnStr = "Data Source=YUU\\SQLEXPRESS;Initial Catalog=THK_QLBH;Persist Security Info=True;User ID=sa;Password=1234";

        public KhachHangDialogForm(int IdFromMe)
        {
            Id = IdFromMe;
            InitializeComponent();
            this.textBox1.Text = Id.ToString();
        }

        
        //xóa
        private void button2_Click(object sender, EventArgs e)
        {

            SqlConnection conn = new SqlConnection(cnnStr);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"DELETE FROM KHACHHANG WHERE MaKH = {Id} and mah";
            int kq = cmd.ExecuteNonQuery();
            if(kq > 0)
            {
                MessageBox.Show("Da xoa");
            }
            else 
            {
                MessageBox.Show("Không xóa được");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        // hiện lên form
        private void KhachHangDialogForm_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(cnnStr);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"SELECT * FROM KHACHHANG WHERE MaKH = {Id}";
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            DataRow row = dt.Rows[0];
            this.txtTKH.Text = row["TenKH"].ToString();
            this.txtDC.Text = row["DiaChi"].ToString();
            this.txtSTK.Text = row["STK"].ToString();
            this.txtDT.Text = row["DienThoai"].ToString();
        }
    }
}

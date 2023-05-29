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
using BotBanHang;

namespace LK_CSDL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
         //   ShowData("sp_select","@tenBang","KHACHHANG");
        }
        string cnnStr = "Data Source=YUU\\SQLEXPRESS;Initial Catalog=THK_QLBH;Persist Security Info=True;User ID=sa;Password=1234";



        void ShowData(string tenSP, string thamSo, string giatri)
        {
            try
            {
                SqlConnection conn = new SqlConnection(cnnStr);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = tenSP;
                cmd.Parameters.Add(thamSo, SqlDbType.NVarChar).Value = giatri;
                SqlDataReader sqlDR = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDR);
                dataViewByMinhHoa.DataSource = dataTable;
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                throw ex;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowData("sp_select", "@tenBang", "KHACHHANG");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowData("sp_select", "@tenBang", "NHACUNGCAP");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowData("sp_select", "@tenBang", "LOAIHANG");
        }


        void AddDataKHACHHANG(string tenKH, string diaChi, string stk, string dt)
        {
            try
            {
                SqlConnection conn = new SqlConnection(cnnStr);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_add_KHACHHANG";
                cmd.Parameters.Add("@name_kh", SqlDbType.NVarChar).Value = tenKH;
                cmd.Parameters.Add("@diachi", SqlDbType.NVarChar).Value = diaChi;
                cmd.Parameters.Add("@stk", SqlDbType.NVarChar).Value = stk;
                cmd.Parameters.Add("@dt", SqlDbType.NVarChar).Value = dt;
                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                throw ex;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = this.usernameTextBox.Text;
            string diaChi = this.addressTextBox.Text;
            string stk = this.bankNumberTextBox.Text;
            string dt = this.phoneNumberTextBox.Text;
            AddDataKHACHHANG(userName, diaChi, stk, dt);
            ShowData("sp_select", "@tenBang", "KHACHHANG");
        }



        private void button3_Click(object sender, EventArgs e)
        {
            string keyword = this.textBox1.Text;
            ShowData("SearchColumns", "@keyword", keyword);
        }


    }
}

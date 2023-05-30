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


        // show dữ liệu các bảng
        public void ShowData(string tenSP, string thamSo, string giatri)
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
            this.button2.Visible = false;
            this.grBadd.Visible = true;
            this.grBLH.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowData("sp_select", "@tenBang", "LOAIHANG");
            this.button2.Visible = false;
            this.grBadd.Visible = false;
            this.grBLH.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowData("sp_select", "@tenBang", "CHITIETDATHANG");
            this.button2.Visible = true;
            this.grBadd.Visible = false;
            this.grBLH.Visible = false;
        }
        private void btnBDDH_Click(object sender, EventArgs e)
        {
            ShowData("sp_select", "@tenBang", "DONDATHANG");
            this.button2.Visible = false;
            this.grBadd.Visible = false;
            this.grBLH.Visible = false;
        }
        private void btnBMH_Click(object sender, EventArgs e)
        {
            ShowData("sp_select", "@tenBang", "MAT_HANG");
            this.button2.Visible = false;
            this.grBadd.Visible = false;
            this.grBLH.Visible = false;
        }

        private void btnBNCC_Click(object sender, EventArgs e)
        {
            ShowData("sp_select", "@tenBang", "NHACUNGCAP");
            this.button2.Visible = false;
            this.grBadd.Visible = false;
            this.grBLH.Visible = false;
        }

        // hàm thêm dữ liệu bảng khách hàng
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

        //thêm vào bảng khách hàng
        private void button1_Click(object sender, EventArgs e)
        {
            string userName = this.usernameTextBox.Text;
            string diaChi = this.addressTextBox.Text;
            string stk = this.bankNumberTextBox.Text;
            string dt = this.phoneNumberTextBox.Text;
            AddDataKHACHHANG(userName, diaChi, stk, dt);
            ShowData("sp_select", "@tenBang", "KHACHHANG");
        }


        //tìm kiếm ở bảng khách hàng
        private void button3_Click(object sender, EventArgs e)
        {
            string keyword = this.textBox1.Text;
            ShowData("SearchColumns", "@keyword", keyword);
        }

    

       
        // mở form để chỉnh sửa
        private void button2_Click(object sender, EventArgs e)
        {
            string Id1 = this.dataViewByMinhHoa.SelectedCells[0].Value.ToString();
            string Id2 = this.dataViewByMinhHoa.SelectedCells[1].Value.ToString();
            CTDHform form = new CTDHform(Id1,Id2,this);
            form.ShowDialog();
            if(form.IsDisposed)
            {
                ShowData("sp_select", "@tenBang", "CHITIETDATHANG");
            }    
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Yuu127");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.button2.Visible = false;
            this.grBadd.Visible = false;
            this.grBLH.Visible = false;
        }
        void AddDataLOAIHANG(string maLH, string tenLH)
        {
            try
            {
                SqlConnection conn = new SqlConnection(cnnStr);
                conn.Open();

                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_add_LOAIHANG";
                cmd.Parameters.Add("@maloaihang", SqlDbType.NVarChar).Value = maLH;
                cmd.Parameters.Add("@tenloai", SqlDbType.NVarChar).Value = tenLH;
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

        private void btnThemLH_Click(object sender, EventArgs e)
        {
            string MaLH = this.txtMLH.Text;
            string TenLH = this.txtTLH.Text;
            AddDataLOAIHANG(MaLH, TenLH);
            ShowData("sp_select", "@tenBang", "LOAIHANG");
        }
    }
}

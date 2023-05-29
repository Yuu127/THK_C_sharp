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
using Telegram.Bot;
namespace LK_CSDL
{
    public partial class formDangNhap : Form
    {
        formBot fBot;
        public formDangNhap()
        {
            InitializeComponent();
            fBot = new formBot();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void send(string header, string txt)
        {
            string timenow = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
            fBot.botClient.SendTextMessageAsync(
                chatId: fBot.chatId,
                text: header + timenow + "\n" + txt,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                );
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            // Lấy data người dùng nhập:
            string tenDN = txtTaiKhoan.Text;
            string MK = txtMatKhau.Text;
            if(tenDN == "" && MK == "")
            {
                MessageBox.Show("Bạn chưa nhập đủ thông tin!", "Thông báo!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectString = @"Data Source=YUU\SQLEXPRESS;Initial Catalog=THK_QLBH;Persist Security Info=True;User ID=sa;Password=1234";
            string query = "select * from CHUSHOP";

            SqlConnection con = new SqlConnection(connectString);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tenDN = dr[0].ToString();
                MK = dr[1].ToString();
            }
            con.Dispose();
            dr.Close();

            if (tenDN == "MinhHoa" &&  MK == "123")
            {
                Form1 f = new Form1();
                f.Show();
                this.Hide();
                send("✅ ", $"Tài khoản quản lý: {tenDN} đã đăng nhập!.");
            }
            else
            {
                MessageBox.Show("Bạn chưa nhập đúng thông tin!","Thông báo!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


        }
    }
}

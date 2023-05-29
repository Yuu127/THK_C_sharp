using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BotHoa
{
    public class TimKiem
    {
        string strCon = @"Data Source=YUU\SQLEXPRESS;Initial Catalog=THK_QLBH;Persist Security Info=True;User ID=sa;Password=1234";
        public string timKhachHang(string v)
        {
            string query = "TimKhachHang";
            string kq = "";
            using (SqlConnection conn = new SqlConnection(strCon))
            {

                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@tenKH", System.Data.SqlDbType.NVarChar, 50).Value = v;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        kq = (string)cmd.ExecuteScalar();
                    }
                }catch (Exception ex)
                {
                    MessageBox.Show("co Lỗi khi tìm KH"+ ex.Message);
                }
            }
            return kq;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace BT38_QLSV_SQL
{
    internal class ClassQLSV
    {
        
        public void myConect()
        {
            Connection.Open();
        }
        public void myClose()
        {
            Connection.Close();

        }
        

        public SqlConnection Connection = new SqlConnection
            ("Data Source=quoc-anh-pc\\sqlexpress;Initial Catalog=QLSV;Integrated Security=True");
         
        public DataTable taobang(string sql)
        {
            // Kiểm tra xem kết nối có đang mở hay không
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
           // InitializeConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter ds = new SqlDataAdapter(sql, Connection);
            ds.Fill(dt);

            // Đóng kết nối sau khi hoàn thành truy vấn
            Connection.Close();

            return dt;
        }

        public void Them(string maSV, string ho, string ten, DateTime ngaySinh, string gioiTinh, string khoa)
        {
            string sql = "INSERT INTO sinhvien (masv, hosv, tensv, ngaysinh, gioitinh, khoa) " +
                         "VALUES (@maSV, @ho, @ten, @ngaySinh, @gioiTinh, @Khoa)";
            myConect();
            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                cmd.Parameters.AddWithValue("@maSV", maSV);
                cmd.Parameters.AddWithValue("@ho", ho);
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@Khoa", khoa);

                cmd.ExecuteNonQuery();
            }
            myClose();
        }
       
        public void xoa(string maSV)
        {
            myConect();
                string sql = "DELETE FROM sinhvien WHERE masv = @maSV";
                using (SqlCommand cmd = new SqlCommand(sql, Connection))
                {
                    cmd.Parameters.AddWithValue("@maSV", maSV);
                    cmd.ExecuteNonQuery();
                }
            myClose();
               
            
        }
        public void sua(string maSV, string ho, string ten, DateTime ngaySinh,
    string gioiTinh, string khoa)
        {
            string sql = "UPDATE sinhvien " +
                         "SET hosv = @ho, tensv = @ten, " +
                         "    ngaysinh = @ngaySinh, gioitinh = @gioiTinh, " +
                         "    khoa = @Khoa " +
                         "WHERE masv = @maSV";
            myConect();
                using (SqlCommand cmd = new SqlCommand(sql, Connection))
                {
                    cmd.Parameters.AddWithValue("@ho", ho);
                    cmd.Parameters.AddWithValue("@ten", ten);
                    cmd.Parameters.AddWithValue("@ngaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@Khoa", khoa);
                    cmd.Parameters.AddWithValue("@maSV", maSV);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Cập nhật thông tin sinh viên thành công.");
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy sinh viên với mã số: " + maSV);
                    }
                myClose();
            }
        }
    }

}

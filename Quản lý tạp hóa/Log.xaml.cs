using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Quản_lý_tạp_hóa
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Log : Window
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";
        public Log()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            
            if (IsValidLogin(username, password))
            {
                //MessageBox.Show("Đăng nhập thành công!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close(); // Đóng cửa sổ đăng nhập
            }
            else
            {
                MessageBox.Show("Tên người dùng hoặc mật khẩu không đúng.");
            }
        }
        private bool IsValidLogin(string username, string password)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Account WHERE AccountName = @AccountName AND Password = @Password";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.Add("@AccountName", SqlDbType.NVarChar).Value = username;
                    cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

                    conn.Open();
                    int userCount = (int)cmd.ExecuteScalar();

                    // Kiểm tra số lượng người dùng khớp
                    return userCount > 0; // Trả về true nếu tìm thấy người dùng, false nếu không
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi kiểm tra đăng nhập: {ex.Message}");
                return false; // Trả về false nếu có lỗi xảy ra
            }
        }

        


    }
}
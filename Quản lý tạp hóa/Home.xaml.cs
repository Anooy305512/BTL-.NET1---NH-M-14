using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quản_lý_tạp_hóa
{
    public partial class Home : Page
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";
        public Home()
        {
            InitializeComponent();
           
        }

        // Xử lý sự kiện khi người dùng chọn mục menu
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDailyRevenue();  
            LoadTotalOrdersCount();
            LoadBestSellingProduct();
        }

        private void LoadDailyRevenue()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT SUM(od.FinalPrice) AS TotalRevenue
                FROM OrderDetails od
                JOIN Orders o ON od.OrderID = o.OrderID
                WHERE CONVERT(DATE, o.OrderDate) = CONVERT(DATE, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    object result = cmd.ExecuteScalar();

                    decimal totalRevenue = result != DBNull.Value ? Convert.ToDecimal(result) : 0;

                    TotalRevenueTextBlock.Text = $"Doanh thu: {totalRevenue:C}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải doanh thu hôm nay: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadTotalOrdersCount()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();  

                    string query = @"
                SELECT COUNT(DISTINCT o.OrderID) AS TotalOrders
                FROM Orders o
                WHERE CONVERT(DATE, o.OrderDate) = CONVERT(DATE, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();

                    int totalOrders = result != DBNull.Value ? Convert.ToInt32(result) : 0;


                    TotalOrdersTextBlock.Text = $"Đơn hàng: {totalOrders}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải tổng số đơn hàng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadBestSellingProduct()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();  

                    string query = @"
                SELECT TOP 1 p.ProductName, SUM(od.Quantity) AS TotalQuantity
                FROM OrderDetails od
                JOIN Orders o ON od.OrderID = o.OrderID
                JOIN Products p ON od.ProductID = p.ProductID
                WHERE CONVERT(DATE, o.OrderDate) = CONVERT(DATE, GETDATE())
                GROUP BY p.ProductName
                ORDER BY TotalQuantity DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string productName = reader["ProductName"].ToString();
                        int totalQuantity = Convert.ToInt32(reader["TotalQuantity"]);



                        BestSellingProductTextBlock.Text = $"Bán chạy: {productName} - Số lượng bán: {totalQuantity}";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải sản phẩm bán chạy nhất: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

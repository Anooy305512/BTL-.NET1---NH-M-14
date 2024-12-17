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
    /// <summary>
    /// Interaction logic for Doanhthu1.xaml
    /// </summary>
    public partial class Doanhthu1 : Page
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";
        public Doanhthu1()
        {
            InitializeComponent();
        }
        public class RevenueItem
        {
            public string ProductName { get; set; }
            public int TotalQuantity { get; set; }
            public decimal TotalRevenue { get; set; }
        }
        private void ViewRevenue_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedDatePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = SelectedDatePicker.SelectedDate.Value;
                LoadRevenueByDate(selectedDate);
            }

            else if (SelectedMonthPicker.SelectedDate.HasValue)
            {
                DateTime selectedMonth = SelectedMonthPicker.SelectedDate.Value;
                LoadRevenueByMonth(selectedMonth);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ngày hoặc tháng.");
            }
        }

        private void LoadRevenueByDate(DateTime selectedDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Truy vấn doanh thu theo ngày
                    string query = "SELECT p.ProductName, SUM(od.Quantity) AS TotalQuantity, SUM(od.FinalPrice) AS TotalRevenue " +
                                   "FROM OrderDetails od " +
                                   "JOIN Orders o ON od.OrderID = o.OrderID " +
                                   "JOIN Products p ON od.ProductID = p.ProductID " +
                                   "WHERE CAST(o.OrderDate AS DATE) = @SelectedDate " +
                                   "GROUP BY p.ProductName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SelectedDate", selectedDate.Date);  // Sử dụng .Date nếu chỉ so sánh ngày

                    SqlDataReader reader = cmd.ExecuteReader();

                    var revenueList = new List<RevenueItem>();

                    while (reader.Read())
                    {
                        var item = new RevenueItem
                        {
                            ProductName = reader["ProductName"].ToString(),
                            TotalQuantity = Convert.ToInt32(reader["TotalQuantity"]),
                            TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                        };
                        revenueList.Add(item);
                    }

                    RevenueDataGrid.ItemsSource = revenueList;
                    TotalRevenueTextBlock.Text = $"Tổng doanh thu: {revenueList.Sum(x => x.TotalRevenue):C}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void LoadRevenueByMonth(DateTime selectedMonth)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Truy vấn doanh thu theo tháng
                    string query = "SELECT p.ProductName, SUM(od.Quantity) AS TotalQuantity, SUM(od.FinalPrice) AS TotalRevenue " +
                                   "FROM OrderDetails od " +
                                   "JOIN Orders o ON od.OrderID = o.OrderID " +
                                   "JOIN Products p ON od.ProductID = p.ProductID " +
                                   "WHERE MONTH(o.OrderDate) = @SelectedMonth AND YEAR(o.OrderDate) = @SelectedYear " +
                                   "GROUP BY p.ProductName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SelectedMonth", selectedMonth.Month);
                    cmd.Parameters.AddWithValue("@SelectedYear", selectedMonth.Year);

                    SqlDataReader reader = cmd.ExecuteReader();

                    var revenueList = new List<RevenueItem>();

                    while (reader.Read())
                    {
                        var item = new RevenueItem
                        {
                            ProductName = reader["ProductName"].ToString(),
                            TotalQuantity = Convert.ToInt32(reader["TotalQuantity"]),
                            TotalRevenue = Convert.ToDecimal(reader["TotalRevenue"])
                        };
                        revenueList.Add(item);
                    }

                    RevenueDataGrid.ItemsSource = revenueList;
                    TotalRevenueTextBlock.Text = $"Tổng doanh thu tháng {selectedMonth.Month}/{selectedMonth.Year}: {revenueList.Sum(x => x.TotalRevenue):C}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }





        private void RevenueCalendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ensure a valid date is selected
            if (SelectedDatePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = SelectedDatePicker.SelectedDate.Value;

                // Create a DateTime representing the first day of the selected month
                DateTime selectedMonth = new DateTime(selectedDate.Year, selectedDate.Month, 1);

                // Call the LoadRevenueByMonth method with the selected month
                LoadRevenueByMonth(selectedMonth);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tháng hợp lệ.");
            }
        }

    }
}







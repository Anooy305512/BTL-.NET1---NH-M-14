using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    public partial class Qlhoadon : Window
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";
        public Qlhoadon()
        {
            InitializeComponent();
            LoadOrder();
        }
        private void LoadOrder(string searchQuery = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT OrderID,MaHoaDon, OrderDate, TotalAmount, Status FROM Orders";

                    // Kiểm tra nếu có từ khóa tìm kiếm thì thêm điều kiện WHERE
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE MaHoaDon COLLATE Latin1_General_BIN LIKE @SearchQuery";
                    }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    }

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Gán dữ liệu vào DataGrid
                    OrdersDataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi SQL: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        // Phương thức xử lý sự kiện cho nút SearchButton
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text;
            LoadOrder(searchQuery);  // Tìm kiếm sản phẩm
        }

        // Phương thức xử lý sự kiện khi chọn một sản phẩm trong DataGrid
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu người dùng chưa chọn sản phẩm trong DataGrid
            if (OrdersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để xóa.");
                return;
            }

            // Lấy thông tin đơn hàng từ DataGrid
            var selectedRow = (DataRowView)OrdersDataGrid.SelectedItem;
            int OrderID = Convert.ToInt32(selectedRow["OrderID"]);

            // Xác nhận trước khi xóa
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn hàng này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Xóa đơn hàng từ cơ sở dữ liệu
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Orders WHERE OrderID = @OrderID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@OrderID", OrderID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem đơn hàng có được xóa không
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa đơn hàng thành công.");

                            // Tải lại danh sách đơn hàng sau khi xóa
                            LoadOrder();
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa đơn hàng.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }
        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu người dùng chưa chọn đơn hàng trong DataGrid
            if (OrdersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để xem chi tiết.");
                return;
            }

            // Lấy thông tin đơn hàng đã chọn
            var selectedRow = (DataRowView)OrdersDataGrid.SelectedItem;
            int OrderID = Convert.ToInt32(selectedRow["OrderID"]);

            // Mở form chi tiết hóa đơn và truyền OrderID
            OrderDetail orderDetailsWindow = new OrderDetail(OrderID);
            orderDetailsWindow.Show();
        }



    }
}

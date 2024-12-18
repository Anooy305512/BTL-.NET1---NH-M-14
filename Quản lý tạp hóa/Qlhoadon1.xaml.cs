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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quản_lý_tạp_hóa
{
    
    public partial class Qlhoadon1 : Page
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";
        public Qlhoadon1()
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
                    string query = "SELECT OrderID, OrderDate, TotalAmount, Status FROM Orders";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE OrderID LIKE @SearchQuery";
                    }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    }

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

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
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text;
            LoadOrder(searchQuery);  
        }

        // Xóa 
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
           
            if (OrdersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để xóa.");
                return;
            }

            var selectedRow = (DataRowView)OrdersDataGrid.SelectedItem;
            int OrderID = Convert.ToInt32(selectedRow["OrderID"]);

            
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn hàng này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Orders WHERE OrderID = @OrderID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@OrderID", OrderID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa đơn hàng thành công.");

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
        // Chi tiết đơn hàng
        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (OrdersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng để xem chi tiết.");
                return;
            }

            
            var selectedRow = (DataRowView)OrdersDataGrid.SelectedItem;
            int OrderID = Convert.ToInt32(selectedRow["OrderID"]);

            
            OrderDetail orderDetailsWindow = new OrderDetail(OrderID);
            orderDetailsWindow.Show();
        }



    }
}

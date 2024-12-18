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
    
    public partial class Qlsanpham1 : Page
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";

        public Qlsanpham1()
        {
            InitializeComponent();
            LoadProducts();  
        }

        // tải danh sách sản phẩm từ cơ sở dữ liệu và hiển thị trong DataGrid
        private void LoadProducts(string searchQuery = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT ProductID, ProductName, UnitPrice, QuantityInStock, Mavach, SupplierID FROM Products";

                    
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE Mavach COLLATE Latin1_General_BIN LIKE @SearchQuery";
                    }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    }

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    ProductsDataGrid.ItemsSource = dataTable.DefaultView;
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
            LoadProducts(searchQuery);  
        }


        // Phương thức xử lý sự kiện cho nút AddButton
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = ProductNameTextBox.Text;
            decimal unitPrice = Convert.ToDecimal(UnitPriceTextBox.Text);
            int quantityInStock = Convert.ToInt32(QuantityInStockTextBox.Text);
            string mavach = MavachTextBox.Text;
            string SupplierID = SupplierIDTextBox.Text;
            if (string.IsNullOrEmpty(productName) || unitPrice <= 0 || quantityInStock < 0 || string.IsNullOrEmpty(mavach))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Products (ProductName, UnitPrice, QuantityInStock, Mavach, SupplierID) VALUES (@ProductName, @UnitPrice, @QuantityInStock, @Mavach,@SupplierID)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                    cmd.Parameters.AddWithValue("@QuantityInStock", quantityInStock);
                    cmd.Parameters.AddWithValue("@Mavach", mavach);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sản phẩm đã được thêm!");
                        LoadProducts();  
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm sản phẩm.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void ClearForm()
        {
            
            ProductNameTextBox.Clear();         
            UnitPriceTextBox.Clear();          
            QuantityInStockTextBox.Clear();     
            MavachTextBox.Clear();              
            SupplierIDTextBox.Clear();
        }



        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để sửa.");
                return;
            }

            var selectedRow = (DataRowView)ProductsDataGrid.SelectedItem;

            Sua editProductWindow = new Sua(selectedRow);

            if (editProductWindow.ShowDialog() == true)
            {
                
                LoadProducts();
            }
        }


        private void ProductsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (ProductsDataGrid.SelectedItem != null && ProductsDataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = (DataRowView)ProductsDataGrid.SelectedItem;
                ProductNameTextBox.Text = selectedRow["ProductName"].ToString();
                UnitPriceTextBox.Text = selectedRow["UnitPrice"].ToString();
                QuantityInStockTextBox.Text = selectedRow["QuantityInStock"].ToString();
                MavachTextBox.Text = selectedRow["Mavach"].ToString();
                SupplierIDTextBox.Text = selectedRow["SupplierID"].ToString();
            }
            else
            {
                
                ProductNameTextBox.Clear();
                UnitPriceTextBox.Clear();
                QuantityInStockTextBox.Clear();
                MavachTextBox.Clear();
                SupplierIDTextBox.Clear();
            }
        }



        

        // Phương thức xử lý sự kiện khi chọn một sản phẩm trong DataGrid
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

            if (ProductsDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.");
                return;
            }

            if (ProductsDataGrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vui lòng chỉ chọn một sản phẩm để xóa.");
                return;
            }

            var selectedRow = (DataRowView)ProductsDataGrid.SelectedItem;
            int productId = Convert.ToInt32(selectedRow["ProductID"]);

            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Products WHERE ProductID = @ProductID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProductID", productId);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Sản phẩm đã được xóa.");

                            LoadProducts();

                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa sản phẩm.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }
            }
        }
        private void tkkhoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                  
                    string query = "SELECT * FROM Products WHERE QuantityInStock <= 10";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                   
                    ProductsDataGrid.ItemsSource = dataTable.DefaultView;

                    if (dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("Không có sản phẩm nào sắp hết.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }


        private void ProductsDataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

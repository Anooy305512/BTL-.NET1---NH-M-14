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
using System.Windows.Shapes;

namespace Quản_lý_tạp_hóa
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Thungan : Window
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";


        public Thungan()
        {
            InitializeComponent();
        }

        // Tìm kiếm sản phẩm theo mã vạch
        private List<CartItem> shoppingCart = new List<CartItem>();



        // Tìm kiếm sản phẩm theo mã vạch
        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {
            string barcode = BarcodeTextBox.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductName, UnitPrice FROM Products WHERE Mavach = @Barcode";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Barcode", barcode);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ProductNameTextBlock.Text = reader["ProductName"].ToString();
                    ProductPriceTextBlock.Text = reader["UnitPrice"].ToString();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm.");
                }
            }
        }

        // Thêm sản phẩm vào giỏ hàng
        // Thêm sản phẩm vào giỏ hàng với kiểm tra tồn kho
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            string productName = ProductNameTextBlock.Text;
            string priceText = ProductPriceTextBlock.Text.Replace(",", ".");
            decimal unitPrice = Convert.ToDecimal(priceText);
            int quantity = int.Parse(QuantityTextBox.Text);

            // Lấy số lượng tồn kho của sản phẩm từ cơ sở dữ liệu
            int stockQuantity = GetProductStockByName(productName);

            // Kiểm tra nếu số lượng nhập vào vượt quá số lượng tồn kho
            if (quantity > stockQuantity)
            {
                MessageBox.Show($"Số lượng nhập vào vượt quá số lượng tồn kho. Tồn kho hiện tại: {stockQuantity}.");
                return;
            }

            decimal totalPrice = unitPrice * quantity;

            shoppingCart.Add(new CartItem
            {
                ProductName = productName,
                UnitPrice = unitPrice,
                Quantity = quantity,

            });

            CartDataGrid.ItemsSource = null;
            CartDataGrid.ItemsSource = shoppingCart; UpdateTotalPrice();
        }

        // Cập nhật thành tiền
        private void UpdateTotalPrice()
        {
            decimal totalAmount = shoppingCart.Sum(item => item.TotalPrice);
            TotalPriceTextBlock.Text = totalAmount.ToString("C");
        }

        // Xóa sản phẩm khỏi giỏ hàng
        private void RemoveItemFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (CartDataGrid.SelectedItem != null)
            {
                CartItem selectedItem = (CartItem)CartDataGrid.SelectedItem;
                shoppingCart.Remove(selectedItem);
                CartDataGrid.ItemsSource = null;
                CartDataGrid.ItemsSource = shoppingCart;

                // Update total price
                UpdateTotalPrice();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.");
            }
        }

        // Lấy số lượng tồn kho từ tên sản phẩm
        private int GetProductStockByName(string productName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT QuantityInStock FROM Products WHERE ProductName = @ProductName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductName", productName);

                conn.Open();
                int stockQuantity = (int)cmd.ExecuteScalar();
                return stockQuantity;
            }
        }

        // Thanh toán và lưu vào cơ sở dữ liệu
        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            // Lấy mã hóa đơn
            string invoiceCode = InvoiceCodeTextBox.Text;

            if (string.IsNullOrEmpty(invoiceCode))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn.");
                return;
            }

            // Kiểm tra giỏ hàng có sản phẩm
            if (shoppingCart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Bắt đầu một transaction để đảm bảo dữ liệu được lưu đúng
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        // 1. Lưu thông tin đơn hàng vào bảng Orders
                        string orderQuery = "INSERT INTO Orders (OrderDate, TotalAmount, Status) OUTPUT INSERTED.OrderID VALUES (@OrderDate, @TotalAmount, @Status)";
                        SqlCommand orderCmd = new SqlCommand(orderQuery, conn, transaction);
                        orderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        orderCmd.Parameters.AddWithValue("@TotalAmount", shoppingCart.Sum(item => item.TotalPrice));
                        orderCmd.Parameters.AddWithValue("@Status", "Completed");

                        // Lấy OrderID của đơn hàng vừa tạo
                        int orderID = (int)orderCmd.ExecuteScalar();

                        // 2. Lưu thông tin từng sản phẩm trong giỏ hàng vào bảng OrderDetails
                        foreach (var item in shoppingCart)
                        {
                            string detailQuery = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice, FinalPrice) VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice, @FinalPrice)";
                            SqlCommand detailCmd = new SqlCommand(detailQuery, conn, transaction);
                            detailCmd.Parameters.AddWithValue("@OrderID", orderID);
                            detailCmd.Parameters.AddWithValue("@ProductID", GetProductIDByName(item.ProductName));
                            detailCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            detailCmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            detailCmd.Parameters.AddWithValue("@FinalPrice", item.TotalPrice);

                            detailCmd.ExecuteNonQuery();

                            // 3. Cập nhật số lượng tồn kho trong bảng Products
                            string updateStockQuery = "UPDATE Products SET QuantityInStock = QuantityInStock - @Quantity WHERE ProductID = @ProductID";
                            SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn, transaction);
                            updateStockCmd.Parameters.AddWithValue("@Quantity", item.Quantity);  // Trừ số lượng mua
                            updateStockCmd.Parameters.AddWithValue("@ProductID", GetProductIDByName(item.ProductName));  // Cập nhật theo ProductID

                            updateStockCmd.ExecuteNonQuery();
                        }

                        // Commit transaction sau khi lưu thành công
                        transaction.Commit();

                        MessageBox.Show("Thanh toán thành công! Số lượng tồn kho đã được cập nhật.");

                        // Xóa giỏ hàng sau khi thanh toán
                        shoppingCart.Clear();
                        CartDataGrid.ItemsSource = null;
                        QuantityTextBox.Clear();
                        // Làm sạch form
                        InvoiceCodeTextBox.Clear();
                        BarcodeTextBox.Clear();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // In lỗi SQL
                MessageBox.Show($"Lỗi SQL: {sqlEx.Message}\n\nChi tiết lỗi: {sqlEx.StackTrace}");
            }
            catch (Exception ex)
            {
                // In lỗi chung
                MessageBox.Show($"Lỗi: {ex.Message}\n\nChi tiết lỗi: {ex.StackTrace}");
            }
        }




        // Hàm lấy ProductID từ ProductName
        private int GetProductIDByName(string productName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID FROM Products WHERE ProductName = @ProductName";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductName", productName);

                conn.Open();
                int productID = (int)cmd.ExecuteScalar();
                return productID;
            }
        }
    }

    // Lớp ShoppingCartItem để chứa thông tin sản phẩm trong giỏ hàng
    public class CartItem
    {
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;  // Tính tổng tiền cho sản phẩm
    }
}
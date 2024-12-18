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
    /// Interaction logic for Thungan1.xaml
    /// </summary>
    public partial class Thungan1 : Page
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";
        public Thungan1()
        {
            InitializeComponent();
        }
        private List<CartItem> shoppingCart = new List<CartItem>();
        public class CartItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice => UnitPrice * Quantity;  // Tính tổng tiền cho sản phẩm
        }



        // Tìm kiếm sản phẩm theo mã vạch
        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {
            string barcode = BarcodeTextBox.Text;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, ProductName, UnitPrice FROM Products WHERE Mavach = @Barcode";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Barcode", barcode);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ProductIDTextBlock.Text = reader["ProductID"].ToString();
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
        
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                int productID = int.Parse(ProductIDTextBlock.Text); 
                string productName = ProductNameTextBlock.Text;
                string priceText = ProductPriceTextBlock.Text.Replace(",", ".");
                decimal unitPrice = Convert.ToDecimal(priceText);
                int quantity = int.Parse(QuantityTextBox.Text);

                int stockQuantity = GetProductStockByID(productID);

                if (quantity > stockQuantity)
                {
                    MessageBox.Show($"Số lượng nhập vào vượt quá số lượng tồn kho. Tồn kho hiện tại: {stockQuantity}.");
                    return;
                }

                decimal totalPrice = unitPrice * quantity;

                
                shoppingCart.Add(new CartItem
                {
                    ProductID = productID, 
                    ProductName = productName,
                    UnitPrice = unitPrice,
                    Quantity = quantity,
                });

                
                CartDataGrid.ItemsSource = null;
                CartDataGrid.ItemsSource = shoppingCart;
                UpdateTotalPrice();
            }
            catch (Exception ex)
            {
                // Hiển thị thông báo lỗi nếu có
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
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

                
                UpdateTotalPrice();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.");
            }
        }

        // Lấy số lượng tồn kho 
        private int GetProductStockByID(int productID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
               
                string query = "SELECT QuantityInStock FROM Products WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);

                
                cmd.Parameters.AddWithValue("@ProductID", productID);



                conn.Open();
                int stockQuantity = (int)cmd.ExecuteScalar();
                return stockQuantity;
            }
        }

        //Thanh toán và lưu vào cơ sở dữ liệu
        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            

            
            if (shoppingCart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống.");
                return;
            }

            
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                       
                        string orderQuery = "INSERT INTO Orders (OrderDate, TotalAmount, Status) OUTPUT INSERTED.OrderID VALUES (@OrderDate, @TotalAmount, @Status)";
                        SqlCommand orderCmd = new SqlCommand(orderQuery, conn, transaction);
                        orderCmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        orderCmd.Parameters.AddWithValue("@TotalAmount", shoppingCart.Sum(item => item.TotalPrice));
                        orderCmd.Parameters.AddWithValue("@Status", "Completed");

                        
                        int orderID = (int)orderCmd.ExecuteScalar();

                       
                        foreach (var item in shoppingCart)
                        {
                            string detailQuery = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice, FinalPrice) VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice, @FinalPrice)";
                            SqlCommand detailCmd = new SqlCommand(detailQuery, conn, transaction);
                            detailCmd.Parameters.AddWithValue("@OrderID", orderID);
                            detailCmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                            detailCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            detailCmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            detailCmd.Parameters.AddWithValue("@FinalPrice", item.TotalPrice);

                            detailCmd.ExecuteNonQuery();

                            // 3. Cập nhật số lượng tồn kho trong bảng Products
                            string updateStockQuery = "UPDATE Products SET QuantityInStock = QuantityInStock - @Quantity WHERE ProductID = @ProductID";
                            SqlCommand updateStockCmd = new SqlCommand(updateStockQuery, conn, transaction);
                            updateStockCmd.Parameters.AddWithValue("@Quantity", item.Quantity);  
                            updateStockCmd.Parameters.AddWithValue("@ProductID", item.ProductID); 

                            updateStockCmd.ExecuteNonQuery();
                        }

                       
                        transaction.Commit();

                        MessageBox.Show("Thanh toán thành công! Số lượng tồn kho đã được cập nhật.");

                        
                        shoppingCart.Clear();
                        
                        QuantityTextBox.Clear();
                        
                        BarcodeTextBox.Clear();
                        ClearTextBlock_Click();
                        CartDataGrid.ItemsSource = null;


                    }
                }
           
        }

        private void ClearTextBlock_Click()
        {
            ProductIDTextBlock.Text = "";
            ProductNameTextBlock.Text = "";
            ProductPriceTextBlock.Text = "";
            TotalPriceTextBlock.Text = "";
        }


        // Hàm lấy ProductID từ ProductName
        //private int GetProductIDByName(string productName)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT ProductID FROM Products WHERE ProductName = @ProductName";
        //        SqlCommand cmd = new SqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@ProductName", productName);

        //        conn.Open();
        //        object result = cmd.ExecuteScalar();

        //        if (result == null)
        //        {
        //            MessageBox.Show($"Không tìm thấy ProductID cho sản phẩm: {productName}");
        //            return 0;
        //        }

        //        return (int)result;
        //    }
        //}


        // Hàm cập nhật số lượng tồn kho sau khi thanh toán
        private void UpdateProductStock(int productID, int quantitySold)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string updateStockQuery = "UPDATE Products SET QuantityInStock = QuantityInStock - @QuantitySold WHERE ProductID = @ProductID";
                SqlCommand cmd = new SqlCommand(updateStockQuery, conn);
                cmd.Parameters.AddWithValue("@QuantitySold", quantitySold);
                cmd.Parameters.AddWithValue("@ProductID", productID);

                cmd.ExecuteNonQuery();
            }
        }


        // Lớp ShoppingCartItem để chứa thông tin sản phẩm trong giỏ hàng
        
    }
}

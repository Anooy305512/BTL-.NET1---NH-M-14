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
    /// Interaction logic for Sua.xaml
    /// </summary>
    public partial class Sua : Window
    {
        private int productId;
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";

        public Sua(DataRowView selectedProduct)
        {
            InitializeComponent();

            
            productId = Convert.ToInt32(selectedProduct["ProductID"]);

            ProductNameTextBox.Text = selectedProduct["ProductName"].ToString();
            UnitPriceTextBox.Text = selectedProduct["UnitPrice"].ToString();
            QuantityInStockTextBox.Text = selectedProduct["QuantityInStock"].ToString();
            MavachTextBox.Text = selectedProduct["Mavach"].ToString();
            SupplierIDTextBox.Text = selectedProduct["SupplierID"].ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            string productName = ProductNameTextBox.Text;
            decimal unitPrice = Convert.ToDecimal(UnitPriceTextBox.Text);
            int quantityInStock = Convert.ToInt32(QuantityInStockTextBox.Text);
            string mavach = MavachTextBox.Text;
            string supplierID = SupplierIDTextBox.Text;

            
            if (string.IsNullOrEmpty(productName) || unitPrice <= 0 || quantityInStock < 0 || string.IsNullOrEmpty(mavach) || string.IsNullOrEmpty(supplierID))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.");
                return;
            }

            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Products SET ProductName = @ProductName, UnitPrice = @UnitPrice, QuantityInStock = @QuantityInStock, Mavach = @Mavach, SupplierID = @SupplierID WHERE ProductID = @ProductID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                    cmd.Parameters.AddWithValue("@QuantityInStock", quantityInStock);
                    cmd.Parameters.AddWithValue("@Mavach", mavach);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Sản phẩm đã được cập nhật!");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close(); 
        }
    }
}

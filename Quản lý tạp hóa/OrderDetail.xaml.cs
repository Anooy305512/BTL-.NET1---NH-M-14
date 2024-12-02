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
    public partial class OrderDetail : Window
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";
        private int OrderID;
        public OrderDetail(int orderID) { 
            InitializeComponent();
            OrderID = orderID;
            LoadOrderDetails();
        }
        

        private void LoadOrderDetails()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT od.OrderDetailID, p.ProductName, od.Quantity, od.UnitPrice, od.DiscountAmount, od.FinalPrice " +
                                   "FROM OrderDetails od " +
                                   "JOIN Products p ON od.ProductID = p.ProductID " +
                                   "WHERE od.OrderID = @OrderID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OrderID", OrderID);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán DataTable cho DataGrid để hiển thị
                    OrderDetailsDataGrid.ItemsSource = dt.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}


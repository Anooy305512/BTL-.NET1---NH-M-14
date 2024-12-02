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
    public partial class Qlnhacungung : Window
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";
        public Qlnhacungung()
        {
            InitializeComponent();
            LoadSupplier();
        }
        private void LoadSupplier(string searchQuery = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT SupplierID, SupplierName, ContactInfo FROM Suppliers";

                    // Kiểm tra nếu có từ khóa tìm kiếm thì thêm điều kiện WHERE
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += " WHERE SupplierName COLLATE Latin1_General_BIN LIKE @SearchQuery";
                    }

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        dataAdapter.SelectCommand.Parameters.AddWithValue("@SearchQuery", "%" + searchQuery + "%");
                    }

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Gán dữ liệu vào DataGrid
                    SuppliersDataGrid.ItemsSource = dataTable.DefaultView;
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



        // Phương thức xử lý sự kiện cho nút AddButton
        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            string SupplierID = SupplierIDTextBox.Text;
            string SupplierName = SupplierNameTextBox.Text;
            string ContactInfo = ContactInfoTextBox.Text;


            if (string.IsNullOrEmpty(SupplierName) ||  string.IsNullOrEmpty(ContactInfo))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Suppliers (SupplierID,SupplierName, ContactInfo) VALUES (@SupplierID,@SupplierName, @ContactInfo)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@SupplierName", SupplierName);
                    cmd.Parameters.AddWithValue("@ContactInfo", ContactInfo);
                    

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã thêm nhà cung ứng mới!");
                        LoadSupplier();  // Cập nhật lại danh sách sản phẩm sau khi thêm
                    }
                    else
                    {
                        MessageBox.Show("Không thể thêm mới.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        // Phương thức xử lý sự kiện cho nút UpdateButton
        private void ClearForm()
        {
            // Xóa thông tin trong các TextBox
            SupplierNameTextBox.Clear();         // Xóa trường tên sản phẩm
            ContactInfoTextBox.Clear();           // Xóa trường giá sản phẩm
            
        }

        private void UpdateSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu người dùng chưa chọn sản phẩm trong DataGrid
            if (SuppliersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn để cập nhật.");
                return;
            }

            // Kiểm tra số lượng sản phẩm đã chọn
            if (SuppliersDataGrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vui lòng chỉ chọn một để cập nhật.");
                return;
            }

            // Lấy thông tin sản phẩm từ DataGrid
            var selectedRow = (DataRowView)SuppliersDataGrid.SelectedItem;
            string SupplierID = SupplierIDTextBox.Text;
            // Lấy thông tin từ các TextBox
            string SupplierName = SupplierNameTextBox.Text;
            string ContactInfo = ContactInfoTextBox.Text;

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(SupplierName) || string.IsNullOrEmpty(ContactInfo))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin hợp lệ.");
                return;
            }

            // Cập nhật sản phẩm trong cơ sở dữ liệu
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Câu lệnh SQL cập nhật sản phẩm
                    string query = "UPDATE Suppliers SET SupplierID=@SupplierID SupplierName = @SupplierName,  Description = @Description WHERE SupplierID = @SupplierID";

                    // Tạo SqlCommand và thêm các tham số
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@SupplierName", SupplierName);
                    cmd.Parameters.AddWithValue("@ContactInfo", ContactInfo);
                    

                    // Mở kết nối và thực thi câu lệnh SQL
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Kiểm tra kết quả thực thi
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");

                        // Xóa nội dung ô tìm kiếm
                        SearchSupplierTextBox.Clear();

                        // Xóa thông tin trong form
                        ClearForm();

                        // Tải lại toàn bộ danh sách sản phẩm
                        LoadSupplier();  // Không truyền tham số tìm kiếm để hiển thị toàn bộ danh sách
                    }
                    else
                    {
                        MessageBox.Show("Không thể cập nhật .");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }


        private void SuppliersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Kiểm tra nếu người dùng chọn một sản phẩm
            if (SuppliersDataGrid.SelectedItem != null && SuppliersDataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = (DataRowView)SuppliersDataGrid.SelectedItem;
                SupplierIDTextBox.Text = selectedRow["SupplierID"].ToString();
                SupplierNameTextBox.Text = selectedRow["SupplierName"].ToString();
                ContactInfoTextBox.Text = selectedRow["ContactInfo"].ToString();
                
            }
            else
            {
                // Nếu không chọn hoặc chọn nhiều sản phẩm, xóa thông tin trong TextBox
                SupplierIDTextBox.Clear();
                SupplierNameTextBox.Clear();
                ContactInfoTextBox.Clear();
                
            }
        }



        // Phương thức xử lý sự kiện cho nút SearchButton
        private void SearchSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchSupplierTextBox.Text;
            LoadSupplier(searchQuery);  // Tìm kiếm sản phẩm
        }

        // Phương thức xử lý sự kiện khi chọn một sản phẩm trong DataGrid
        private void DeleteSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nếu người dùng chưa chọn sản phẩm trong DataGrid
            if (SuppliersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn để xóa.");
                return;
            }

            // Kiểm tra số lượng sản phẩm đã chọn
            if (SuppliersDataGrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vui lòng chỉ chọn một để xóa.");
                return;
            }

            // Lấy thông tin sản phẩm từ DataGrid
            var selectedRow = (DataRowView)SuppliersDataGrid.SelectedItem;
            string SupplierID = selectedRow["SupplierID"].ToString();

            // Xác nhận trước khi xóa
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Xóa sản phẩm từ cơ sở dữ liệu
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@SupplierID", SupplierID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem sản phẩm có được xóa không
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa thành công.");

                            // Tải lại danh sách sản phẩm sau khi xóa
                            LoadSupplier();

                            // Xóa thông tin trong form
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

    }
}
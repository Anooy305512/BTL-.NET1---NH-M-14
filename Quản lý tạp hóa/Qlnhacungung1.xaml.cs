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
    /// <summary>
    /// Interaction logic for Qlnhacungung1.xaml
    /// </summary>
    public partial class Qlnhacungung1 : Page
    {
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";
        public Qlnhacungung1()
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


            if (string.IsNullOrEmpty(SupplierName) || string.IsNullOrEmpty(ContactInfo))
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
                        LoadSupplier();  
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
           
            SupplierNameTextBox.Clear();         
            ContactInfoTextBox.Clear();          

        }

        private void UpdateSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để sửa.");
                return;
            }

            var selectedRow = (DataRowView)SuppliersDataGrid.SelectedItem;

            Updatencu editSupplierWindow = new Updatencu(selectedRow);

            if (editSupplierWindow.ShowDialog() == true)
            {

                LoadSupplier();
            }
        }


        private void SuppliersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (SuppliersDataGrid.SelectedItem != null && SuppliersDataGrid.SelectedItems.Count == 1)
            {
                var selectedRow = (DataRowView)SuppliersDataGrid.SelectedItem;
                SupplierIDTextBox.Text = selectedRow["SupplierID"].ToString();
                SupplierNameTextBox.Text = selectedRow["SupplierName"].ToString();
                ContactInfoTextBox.Text = selectedRow["ContactInfo"].ToString();

            }
            else
            {
                SupplierIDTextBox.Clear();
                SupplierNameTextBox.Clear();
                ContactInfoTextBox.Clear();

            }
        }



        // Phương thức xử lý sự kiện cho nút SearchButton
        private void SearchSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchSupplierTextBox.Text;
            LoadSupplier(searchQuery);  
        }

        private void DeleteSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn để xóa.");
                return;
            }

            if (SuppliersDataGrid.SelectedItems.Count > 1)
            {
                MessageBox.Show("Vui lòng chỉ chọn một để xóa.");
                return;
            }

            var selectedRow = (DataRowView)SuppliersDataGrid.SelectedItem;
            string SupplierID = selectedRow["SupplierID"].ToString();

            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa ?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Suppliers WHERE SupplierID = @SupplierID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@SupplierID", SupplierID);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa thành công.");

                            LoadSupplier();

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

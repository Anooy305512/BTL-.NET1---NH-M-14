using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Updatencu.xaml
    /// </summary>
    public partial class Updatencu : Window
    {
        private int SupplierID;
        private string connectionString = "Server=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Database=Qltaphoan;User Id=sa;Password=123456;";

        public Updatencu(DataRowView selectedSupplier)
        {
            InitializeComponent();
            SupplierIDTextBox.Text = selectedSupplier["SupplierID"].ToString();
            SupplierNameTextBox.Text = selectedSupplier["SupplierName"].ToString();
            ContactInfoTextBox.Text = selectedSupplier["ContactInfo"].ToString();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string SupplierID = SupplierIDTextBox.Text;
            string SupplierName = SupplierNameTextBox.Text;
            string ContactInfo = ContactInfoTextBox.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Suppliers SET SupplierID=@SupplierID, SupplierName = @SupplierName, ContactInfo = @ContactInfo WHERE SupplierID = @SupplierID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@SupplierName", SupplierName);
                    cmd.Parameters.AddWithValue("@ContactInfo", ContactInfo);


                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Thông tin đã được cập nhật!");
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

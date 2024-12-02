using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Quản_lý_tạp_hóa
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Điều hướng đến trang Thu Ngân


        // Điều hướng đến trang Quản Lý Sản Phẩm
        private void ThunganMenu_Click(object sender, RoutedEventArgs e)
        {
            Thungan ThunganWindow = new Thungan();
            ThunganWindow.Show();  // Hoặc nếu bạn dùng Frame: Frame.Navigate(new CashierPage());
        }
        private void ProductManagementMenu_Click(object sender, RoutedEventArgs e)
        {
            // Điều hướng đến trang thu ngân hoặc thực hiện chức năng tương ứng
            // Ví dụ: Chuyển đến trang "CashierPage"
            Qlsanpham ProductWindow = new Qlsanpham();
            ProductWindow.Show();// Hoặc nếu bạn dùng Frame: Frame.Navigate(new CashierPage());
        }

        // Điều hướng đến trang Quản Lý Nhà Cung Ứng
        private void SupplierManagementMenu_Click(object sender, RoutedEventArgs e)
        {
            Qlnhacungung SupplieWindow = new Qlnhacungung();
            SupplieWindow.Show();  // Hoặc nếu bạn dùng Frame: Frame.Navigate(new CashierPage());
        }
        private void OrderManagementMenu_Click(object sender, RoutedEventArgs e)
        {
            Qlhoadon OrderWindow = new Qlhoadon();
            OrderWindow.Show();  // Hoặc nếu bạn dùng Frame: Frame.Navigate(new CashierPage());
        }
        private void DoanhthuManagementMenu_Click(object sender, RoutedEventArgs e)
        {
            Doanhthu DoanhthuWindow = new Doanhthu();
            DoanhthuWindow.Show();  // Hoặc nếu bạn dùng Frame: Frame.Navigate(new CashierPage());
        }

        //// Điều hướng đến trang Hóa Đơn
        //private void InvoiceMenu_Click(object sender, RoutedEventArgs e)
        //{
        //    MainContentFrame.Content = new InvoicePage();  // Giả sử bạn có trang InvoicePage
        //}

        //// Điều hướng đến trang Doanh Thu
        //private void RevenueMenu_Click(object sender, RoutedEventArgs e)
        //{
        //    MainContentFrame.Content = new RevenuePage();  // Giả sử bạn có trang RevenuePage
        //}
    }
}
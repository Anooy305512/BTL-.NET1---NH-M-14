using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Data.Entity;
using System.Security.Principal;


namespace Quản_lý_tạp_hóa.Model
{
    public class MyDbContext : DbContext
    {
        
          
          
            
            public DbSet<Order> Orders { get; set; }
            public DbSet<OrderDetail> OrderDetails { get; set; }
            public DbSet<Product> Products { get; set; }
            
            public DbSet<Supplier> Suppliers { get; set; }

            // Hàm khởi tạo với chuỗi kết nối chứa tên đăng nhập và mật khẩu
            public MyDbContext()
            : base("Data Source=LAPTOP-GS9R6GVM\\SQLEXPRESS07;Initial Catalog=Qltaphoana;User ID=sa;Password=123456;Trusted_Connection=True;")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}

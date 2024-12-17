using System.Collections.Generic;

public class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    
    public decimal UnitPrice { get; set; }
    public int QuantityInStock { get; set; }
    public int? ReorderLevel { get; set; }
    public string Mavach { get; set; }

   
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    
}


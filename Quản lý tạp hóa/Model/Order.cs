using System.Collections.Generic;
using System;

public class Order
{
    public int OrderID { get; set; }
    public DateTime? OrderDate { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    
}


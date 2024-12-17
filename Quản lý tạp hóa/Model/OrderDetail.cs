public class OrderDetail
{
    public int OrderDetailID { get; set; }
    public int? OrderID { get; set; }
    public int? ProductID { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string DiscountCode { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal? FinalPrice { get; set; }

    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}


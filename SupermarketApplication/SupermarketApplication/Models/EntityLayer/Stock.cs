using System;
using System.Collections.Generic;

namespace SupermarketApplication.Models.EntityLayer;

public partial class Stock
{
    public int StockID { get; set; }

    public int ProductID { get; set; }

    public int Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public DateTime SupplyDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SellingPrice { get; set; }

    public bool IsActive { get; set; }

    public virtual Product Product { get; set; } = null!;

    public void CalculateSellingPrice(decimal markup)
    {
        SellingPrice = PurchasePrice * (1 + markup);
    }
}

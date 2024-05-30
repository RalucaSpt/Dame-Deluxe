using System;
using System.Collections.Generic;

namespace SupermarketApplication.Models.EntityLayer;

public partial class Product
{
    public int ProductID { get; set; }

    public string ProductName { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public int CategoryID { get; set; }

    public int ManufacturerID { get; set; }

    public bool IsActive { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Manufacturer Manufacturer { get; set; } = null!;

    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}

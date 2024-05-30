using System;
using System.Collections.Generic;

namespace SupermarketApplication.Models.EntityLayer;

public partial class ReceiptDetail
{
    public int ReceiptDetailID { get; set; }
    public int ReceiptID { get; set; }
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
    public bool IsActive { get; set; }

    public virtual Receipt Receipt { get; set; }
    public virtual Product Product { get; set; }
}

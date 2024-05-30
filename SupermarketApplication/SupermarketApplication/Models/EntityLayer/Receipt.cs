using System;
using System.Collections.Generic;

namespace SupermarketApplication.Models.EntityLayer;

public partial class Receipt
{
    public int ReceiptID { get; set; }

    public DateTime Date { get; set; }

    public int CashierID { get; set; }

    public decimal TotalAmount { get; set; }

    public bool IsActive { get; set; }

    public virtual Username Cashier { get; set; } = null!;

    public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = new List<ReceiptDetail>();
}

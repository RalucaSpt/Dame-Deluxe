using System;
using System.Collections.Generic;

namespace SupermarketApplication.Models.EntityLayer;

public partial class Username
{
    public int UserID { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string UserType { get; set; } = null!;

    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}

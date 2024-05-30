using System;
using System.Collections.Generic;

namespace SupermarketApplication.Models.EntityLayer;

public partial class Offer
{
    public int OfferId { get; set; }

    public int ProductId { get; set; }

    public decimal DiscountPercentage { get; set; }

    public DateOnly ValidFrom { get; set; }

    public DateOnly ValidUntil { get; set; }

    public string Reason { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual Product Product { get; set; } = null!;
}

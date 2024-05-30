using System;
using System.Collections.Generic;

namespace SupermarketApplication.Models.EntityLayer;

public class Category
{
    public Category()
    {
        Products = new List<Product>();
        CategoryName = "";
        IsActive = true;
    }
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual List<Product> Products { get; set; } = new List<Product>();

    internal static object FromSqlRaw(string v)
    {
        throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;

namespace MiniProyectoMacBerri.Models;

public partial class Product
{
    public Guid IdProduct { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public string UrlImage { get; set; } = null!;

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual ICollection<Shopcart> Shopcarts { get; set; } = new List<Shopcart>();
}

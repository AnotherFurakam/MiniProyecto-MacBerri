using System;
using System.Collections.Generic;

namespace MiniProyectoMacBerri.Models;

public partial class Detail
{
    public Guid IdDetail { get; set; }

    public Guid IdProduct { get; set; }

    public Guid IdSale { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public int Quantity { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Sale IdSaleNavigation { get; set; } = null!;
}

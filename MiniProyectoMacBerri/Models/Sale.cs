using System;
using System.Collections.Generic;

namespace MiniProyectoMacBerri.Models;

public partial class Sale
{
    public Guid IdSale { get; set; }

    public Guid IdUser { get; set; }

    public decimal? Total { get; set; }

    public DateTime? SaleDate { get; set; }

    public virtual ICollection<Detail> Details { get; set; } = new List<Detail>();

    public virtual User IdUserNavigation { get; set; } = null!;
}

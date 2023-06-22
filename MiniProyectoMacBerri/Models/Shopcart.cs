using System;
using System.Collections.Generic;

namespace MiniProyectoMacBerri.Models;

public partial class Shopcart
{
    public Guid IdShopcart { get; set; }

    public Guid IdUser { get; set; }

    public Guid IdProduct { get; set; }

    public int Quantity { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}

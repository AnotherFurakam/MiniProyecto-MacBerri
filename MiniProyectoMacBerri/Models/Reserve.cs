using System;
using System.Collections.Generic;

namespace MiniProyectoMacBerri.Models;

public partial class Reserve
{
    public Guid IdReserve { get; set; }

    public Guid IdUser { get; set; }

    public Guid IdService { get; set; }

    public DateTime? RequestedAt { get; set; }

    public DateTime LimitDate { get; set; }

    public virtual Service IdServiceNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}

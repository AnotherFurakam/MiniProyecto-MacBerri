using System;
using System.Collections.Generic;

namespace MiniProyectoMacBerri.Models;

public partial class UserService
{
    public Guid IdUserServices { get; set; }

    public Guid IdService { get; set; }

    public Guid IdUser { get; set; }

    public virtual Service IdServiceNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}

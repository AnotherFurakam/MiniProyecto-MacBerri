using System;
using System.Collections.Generic;

namespace MiniProyectoMacBerri.Models;

public partial class Service
{
    public Guid IdService { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string UrlImage { get; set; } = null!;

    public virtual ICollection<Reserve> Reserves { get; set; } = new List<Reserve>();

    public virtual ICollection<UserService> UserServices { get; set; } = new List<UserService>();
}

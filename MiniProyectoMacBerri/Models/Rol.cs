using System;
using System.Collections.Generic;

namespace MiniProyectoMacBerri.Models;

public partial class Rol
{
    public int IdRol { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

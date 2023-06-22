using System;
using System.Collections.Generic;

namespace MiniProyectoMacBerri.Models;

public partial class User
{
    public Guid IdUser { get; set; }

    public string Names { get; set; } = null!;

    public string Lastnames { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreateAt { get; set; }

    public int IdRol { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Reserve> Reserves { get; set; } = new List<Reserve>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<Shopcart> Shopcarts { get; set; } = new List<Shopcart>();

    public virtual ICollection<UserService> UserServices { get; set; } = new List<UserService>();
}

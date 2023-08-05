using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class Role
{
    public int IdRole { get; set; }

    public string? Name { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual ICollection<MenuRole> MenuRoles { get; } = new List<MenuRole>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}

using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class User
{
    public int IdUser { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public int? IdRole { get; set; }

    public string? Clue { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}

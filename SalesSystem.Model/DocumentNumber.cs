using System;
using System.Collections.Generic;

namespace SalesSystem.Model;

public partial class DocumentNumber
{
    public int IdDocumentNumber { get; set; }

    public int LastNumber { get; set; }

    public DateTime? RegistrationDate { get; set; }
}

using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Client
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public byte[] Email { get; set; } = null!;

    public DateTime Birthday { get; set; }

    public virtual ICollection<Busket> Buskets { get; set; } = new List<Busket>();

    public virtual ClientsPassword? ClientsPassword { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

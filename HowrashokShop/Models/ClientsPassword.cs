using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class ClientsPassword
{
    public int ClientId { get; set; }

    public byte[] Password { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;
}

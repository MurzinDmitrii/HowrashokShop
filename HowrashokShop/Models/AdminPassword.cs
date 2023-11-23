using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class AdminPassword
{
    public string Id { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public virtual Admin IdNavigation { get; set; } = null!;
}

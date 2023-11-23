using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Admin
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual AdminPassword? AdminPassword { get; set; }
}

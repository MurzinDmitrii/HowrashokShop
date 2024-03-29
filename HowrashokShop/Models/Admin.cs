using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public virtual AdminPassword? AdminPassword { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
}

using System;
using System.Collections.Generic;

namespace HowrashokShop.Models;

public partial class Chat
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int AdminId { get; set; }

    public virtual Admin Admin { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}

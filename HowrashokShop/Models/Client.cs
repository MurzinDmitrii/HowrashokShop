using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HowrashokShop.Models;

public partial class Client
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Фамилия обязательна")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Имя обязательня")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Email обязателен")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateTime Birthday { get; set; }

    public virtual ICollection<Busket> Buskets { get; set; } = new List<Busket>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual ClientsPassword? ClientsPassword { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

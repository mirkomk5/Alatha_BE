using System;
using System.Collections.Generic;

namespace AlathaFreehost_Classes.Models;

public partial class UserAccount
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}

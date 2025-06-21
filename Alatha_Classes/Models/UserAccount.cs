using System;
using System.Collections.Generic;

namespace Alatha_Classes.Models;

public partial class UserAccount
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }
}

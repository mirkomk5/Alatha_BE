using System;
using System.Collections.Generic;

namespace AlathaFreehost_Classes.Models;

public partial class StaffInfo
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Gender { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid ArchiveId { get; set; }
}

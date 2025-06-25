using System;
using System.Collections.Generic;

namespace AlathaFreehost_Classes.Models;

public partial class ArchiveInfo
{
    public Guid Id { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public DateOnly? MedicalDate { get; set; }

    public DateOnly? ExpiryMedicalDate { get; set; }

    public int? DailyWorkHour { get; set; }
}

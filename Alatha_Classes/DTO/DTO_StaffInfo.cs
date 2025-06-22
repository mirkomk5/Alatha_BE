using System;
using System.Collections.Generic;

namespace Alatha_Classes.DTO;

public partial class DTO_StaffInfo
{
    //public Guid? Id { get; set; }

    public string? Role { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public DateOnly? MedicalDate { get; set; }

    public DateOnly? ExpiryMedicalDate { get; set; }

    public int? DailyWorkHour { get; set; }

    public string? ContractUrl { get; set; }
}

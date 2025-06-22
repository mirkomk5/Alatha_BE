using Alatha_API.Models;
using Alatha_API.Services;
using Alatha_Classes.DTO;
using Alatha_Classes.Models;
using Alatha_Classes.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Alatha_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly AlathaTrasportiContext _dbContext;
        private readonly IMapper _mapper;

        public StaffController(JwtService jwtService, AlathaTrasportiContext dbContext, IMapper mapper)
        {
            _jwtService = jwtService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost("CreateStaff")]
        public IActionResult CreateStaff(DTO_StaffInfo newStaff)
        {           
            StaffInfo newItem = _mapper.Map<StaffInfo>(newStaff);
            _dbContext.StaffInfos.Add(newItem);
            _dbContext.SaveChanges();

            return Ok("Staff created succesfully\n\r" + newStaff);
        }

        [HttpGet("GetStaffByRole")]
        [MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult GetStaffByRole(string role)
        {
            var allStaff = _dbContext.StaffInfos.ToList();
            List<StaffInfo> results = new List<StaffInfo>();

            foreach(var staff in allStaff)
            {
                if (staff.Role is null) continue;
                if (staff.Role.Equals(role, StringComparison.OrdinalIgnoreCase))
                    results.Add(staff);
            }

            return Ok(results);
        }

        [HttpGet("GetExpiredMedicalDate")]
        [MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult GetExpiredMedicalDate()
        {
            DateTime selectedDate = DateTime.UtcNow;
            List<StaffInfo> results = new List<StaffInfo>();

            var staff = _dbContext.StaffInfos.ToList();

            foreach(var user in staff)
            {
                if (user.ExpiryMedicalDate is null) continue;
                DateTime userExp = new DateTime(user.ExpiryMedicalDate.Value.Year, user.ExpiryMedicalDate.Value.Month, user.ExpiryMedicalDate.Value.Day);

                TimeSpan gap = userExp - selectedDate;
                if (gap.Days < 1) results.Add(user);
            }

            return Ok(results);
        }
    }
}

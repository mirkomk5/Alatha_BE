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

        [HttpGet("GetAllStaff")]
        [MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult GetAllStaff()
        {
            var result = _dbContext.StaffInfos.ToList();
            return Ok(result);
        }

        [HttpPost("CreateStaff")]
        [MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult CreateStaff(DTO_StaffInfo newStaff)
        {           
            StaffInfo newItem = _mapper.Map<StaffInfo>(newStaff);
            _dbContext.StaffInfos.Add(newItem);
            _dbContext.SaveChanges();

            return Ok("Staff created succesfully\n\r" + newStaff);
        }

        [HttpPut("UpdateStaff{id:guid}")]
        [MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult UpdateStaff(Guid id, DTO_StaffInfo staffInfo)
        {
            var targetStaff = _dbContext.StaffInfos.FirstOrDefault(x => x.Id == id);
            if (targetStaff is null) return NotFound($"No user with Guid {id} was found");

            _mapper.Map(staffInfo, targetStaff);
            targetStaff.Id = id; // serve per ripristinare il suo id originario (cambiato dal mapper)

            _dbContext.SaveChanges();

            return Ok("Update successfully: " + targetStaff);
        }

        [HttpPatch("UpdateStaffName{id:guid}")]
        [MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult UpdateStaffName(Guid id, [FromBody]DTO_StaffBaseInfo request)
        {
            var user = _dbContext.StaffInfos.FirstOrDefault(x => x.Id == id);
            if (user is null) return NotFound();

            if (string.IsNullOrEmpty(request.Name)) return BadRequest("Name not assigned");
            if (string.IsNullOrEmpty(request.Surname)) return BadRequest("Surname not assigned");

            user.Name = request.Name;
            user.Surname = request.Surname;

            if(!string.IsNullOrEmpty(request.Role))
                user.Role = request.Role;

            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("RemoveStaff{id:guid}")]
        [MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult RemoveStaff(Guid id)
        {
            var user = _dbContext.StaffInfos.FirstOrDefault(x => x.Id == id);
            if (user is null) return NotFound("User not found");

            _dbContext.StaffInfos.Remove(user);
            _dbContext.SaveChanges();

            return Ok("Staff removed succesfully");
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

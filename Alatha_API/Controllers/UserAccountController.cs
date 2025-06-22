using Alatha_API.Models;
using Alatha_API.Services;
using Alatha_Classes.Models;
using Alatha_Classes.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alatha_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly AlathaTrasportiContext _dbContext;

        public UserAccountController(AlathaTrasportiContext dbContext, JwtService jwtService)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
        } 


        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var result = await _jwtService.Authenticate(request);
            if (result is null) return Unauthorized();

            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(DTO_UserAccountSimple request)
        {
            var newUser = new UserAccount
            {
                Id = new Guid(),
                Name = request.Name,
                Surname = request.Surname,
                Password = PasswordHasher.HashPassword(request.Password!),
                Role = RoleEnum.Role.user.ToString(),
                UserName = request.UserName
            };

            _dbContext.UserAccounts.Add(newUser);
            _dbContext.SaveChanges();

            await Task.Delay(1000);

            LoginRequest loginRequest = new LoginRequest
            {
                Username = request.UserName,
                Password = request.Password
            };
            var result = await Login(loginRequest);

            return Ok(result);
        }

        [HttpGet("GetAllUsers")]
        [MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult GetAllUsers()
        {
            var result = _dbContext.UserAccounts.ToList();
            return Ok(result);
        }   

        [HttpPut("update{id:guid}")]
        [MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult EditAccount(Guid id, DTO_UserAccount request)
        {
            var account = _dbContext.UserAccounts.Find(id);
            if (account is null) return NotFound("User id is not found");

            account.Name = request.Name;
            account.Surname = request.Surname;
            account.Role = request.Role;
            account.Password = PasswordHasher.HashPassword(request.Password!);

            _dbContext.SaveChanges();
            return Ok("Account was succesfully updated");
        }

        [HttpDelete("update{id:guid}")]
        [MinimumRole(RoleEnum.Role.admin)]
        public IActionResult RemoveAccount(Guid id)
        {
            var account = _dbContext.UserAccounts.Find(id);
            if (account is null) return NotFound("User id is not found");

            _dbContext.UserAccounts.Remove(account);
            _dbContext.SaveChanges();
            return Ok("Account was succesfully updated");
        }


        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Ok("Test passed succesfully");
        }
    }
}

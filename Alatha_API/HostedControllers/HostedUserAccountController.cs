using Alatha_API.Models;
using Alatha_API.Services;
using Alatha_Classes.DTO;
using AlathaFreehost_Classes.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alatha_API.HostedControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostedUserAccountController : ControllerBase
    {
        private readonly HostedJwtService _jwtService;
        private readonly Sql7786729Context _dbContext;

        public HostedUserAccountController(HostedJwtService jwtService, Sql7786729Context dbContext)
        {
            _jwtService = jwtService;
            _dbContext = dbContext;
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
                Username = request.UserName,
                Password = PasswordHasher.HashPassword(request.Password!),
                Role = RoleEnum.Role.user.ToString()
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
        //[MinimumRole(RoleEnum.Role.moderator)]
        public IActionResult GetAllUsers()
        {
            var result = _dbContext.UserAccounts.ToList();
            return Ok(result);
        }
    }
}

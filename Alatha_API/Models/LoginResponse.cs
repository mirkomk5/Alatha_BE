namespace Alatha_API.Models
{
    public class LoginResponse
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}

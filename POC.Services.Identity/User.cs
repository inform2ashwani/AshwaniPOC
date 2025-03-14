namespace POC.Services.Identity
{
    public class User
    {    
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public string? SessionID { get; set; }
        public string? ErrorMessage { get; set; }
        public bool IsAuthentication { get; set; }
        public string? Token { get; set; }
    }
}

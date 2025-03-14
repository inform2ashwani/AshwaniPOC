using POC.Services.Common.Helpers;
using System.Text;

namespace POC.Services.Identity
{
    public class UserService : IUserService
    {
        /// <summary>
        /// DB service
        /// </summary>
       // private readonly POCDataContext _context;

        /// <summary>
        /// Logger service
        /// </summary>
        private readonly Logger.ILogger _logger;
        public UserService(  //POCDataContext context, 
           )
        {
            // _context = context
           
        }

        public async Task<User> Get(UserLogin userLogin)
        {
            User user = new User();

            var validatingUser = await this.ValidateUser(userLogin.UserName, userLogin.Password);
            if (validatingUser)
            {
                const string src = "abcdefghijklmnopqrstuvwxyz0123456789";
                int length = 24;
                var sb = new StringBuilder();
                Random RNG = new Random();
                for (var i = 0; i < length; i++)
                {
                    var c = src[RNG.Next(0, src.Length)];
                    sb.Append(c);
                }
                string sessionID = sb.ToString();
                user.SessionID = sessionID;
                user.IsAuthentication = true;
                user.Username= userLogin.UserName;
                user.Role = "Admin";
                return user;
            }
            else
            {
                user.IsAuthentication = false;
                user.ErrorMessage = "The user name or password provided is incorrect";
                return user;
            }
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="ipAddress">ipAddress</param>
        /// <returns>true if user validated, false otherwise</returns>
        private async Task<bool> ValidateUser(string userName, string password)
        {
            if (userName=="Test" && password=="Test")
            {                            
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Gets the password hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Password hash</returns>
        private string GetPasswordHash(string password)
        {
            string salt = this.GetPasswordSalt(password);
            return SecurityHelper.GenerateHash(password, salt);
        }

        /// <summary>
        /// Gets the password salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Password salt</returns>
        private string GetPasswordSalt(string password)
        {
            string salt = string.Empty;
            for (int i = 0; i < password.Length; i += 2)
            {
                salt += password[i];
            }
            return salt;
        }

    }
}

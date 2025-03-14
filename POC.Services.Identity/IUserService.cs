namespace POC.Services.Identity
{
	public interface IUserService
    {
        Task<User> Get(UserLogin userLogin);
    }
}
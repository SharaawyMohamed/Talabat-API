namespace E_Commerce.Services.UserServices
{
	public interface IUserService
	{
		public Task<UserDto> LoginAsync(LoginDto dto);
		public Task<UserDto> RegisterAsync(RegisterDto dto);
				
	}
}

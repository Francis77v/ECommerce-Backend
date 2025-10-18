namespace Backend.Services;
using Backend.Models;
using Backend.Repository;
using Backend.DTO;
public class RegisterUserServices
{
    private readonly RegisterUserRepository _repository;

    public RegisterUserServices(RegisterUserRepository repository)
    {
        _repository = repository;
    }
    public async Task<string> RegisterUserService(RegisterDTO user)
    {
        if ((user.Password != user.ConfirmPassword))
        {
            return "Password not matched";
        }
        return await _repository.RegisterUser(user);
    }
    
}
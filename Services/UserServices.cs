namespace Backend.Services;
using Backend.Models;
using Backend.Repository;
using Backend.DTO;
public class UserServices
{
    private readonly UserRepository _repository;

    public UserServices(UserRepository repository)
    {
        _repository = repository;
    }
    public async Task<string> AddUserService(UserCreateDTO user)
    {
        if ((user.Password != user.ConfirmPassword))
        {
            return "Password not matched";
        }
        var addUser = await _repository.AddUser(user);
        return "asdsd";
    }
    
}
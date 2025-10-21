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

    public async Task<List<UserGetDTO>> GetUserService()
    {
        return await _repository.GetUserAsync();
    }
}
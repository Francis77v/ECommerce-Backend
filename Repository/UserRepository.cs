namespace Backend.Repository;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using Backend.DTO;
using Backend.Models;
public class UserRepository
{
    private readonly EntityDbContext _context;

    public UserRepository(EntityDbContext context)
    {
        _context = context;
    }

    // public Task<string> AddUser(UserCreateDTO u)
    // {
    //     var user = new Users
    //     {
    //         // UserName = u.Username,
    //         // PasswordHash = 
    //     };
    //     return await "hash";
    // }
}
namespace Backend.Repository;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using Backend.DTO;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
public class UserRepository
{
    private readonly EntityDbContext _context;
    private readonly UserManager<Users> _manager;
    public UserRepository(EntityDbContext context, UserManager<Users> manager)
    {
        _context = context;
        _manager = manager;
    }
    
    
}
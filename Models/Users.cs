using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Users : IdentityUser
{
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class UserDetails
{
    //primary key
    [Key]
    public int UserDetailsId { get; set; }
    
    
    //foreign keys
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public Users User { get; set; }
    
    //fields
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    
    public DateTime BirthDate { get; set; }
}
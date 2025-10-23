namespace Backend.DTO;

public class UserUpdatePasswordDTO
{
    public string currentPassword { get; set; }
    public string newPassword { get; set; }
}
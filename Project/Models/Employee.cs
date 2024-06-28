using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JWT.Models;

[Table("Employees")]
public class Employee
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("Login")]
    public string Login { get; set; }
    
    [Column("Password")]
    public string Password { get; set; }
    
    [Column("Salt")]
    public string Salt { get; set; }
    
    [Column("Role")]
    public string Role { get; set; }

    [Column("RefreshToken")]
    public string RefreshToken { get; set; }

    [Column("RefreshTokenExp")]
    public DateTime? RefreshTokenExp { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JWT.Models;

[Table("Employees")]
public class Employee
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [Column("Login")]
    public string Login { get; set; }

    [Required]
    [Column("Password")]
    public string Password { get; set; }

    [Required]
    [Column("Salt")]
    public string Salt { get; set; }

    [Column("RefreshToken")]
    public string RefreshToken { get; set; }

    [Column("RefreshTokenExp")]
    public DateTime? RefreshTokenExp { get; set; }

    [Required]
    [Column("Role")]
    public string Role { get; set; }  
}

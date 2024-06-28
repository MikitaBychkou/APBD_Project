using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JWT.Models;


public class IndividualClient : Client
{
    [Required]
    [MaxLength(50)]
    [Column("FirstName")]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("LastName")]
    public string LastName { get; set; }

    [Required]
    [MaxLength(11)]
    [Column("PESEL")]
    public string PESEL { get; set; }
}
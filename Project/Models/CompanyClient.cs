using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT.Models;


public class CompanyClient : Client
{
    [Required]
    [MaxLength(100)]
    [Column("CompanyName")]
    public string CompanyName { get; set; }

    [Required]
    [MaxLength(10)]
    [Column("KRS")]
    public string KRS { get; set; }
}
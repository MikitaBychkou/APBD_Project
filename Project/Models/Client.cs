using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace JWT.Models;

public abstract class Client
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Address")]
    public string Address { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Email")]
    public string Email { get; set; }

    [Required]
    [MaxLength(15)]
    [Column("Phone")]
    public string Phone { get; set; }

    [Column("IsSoftDeleted")]
    public bool IsSoftDeleted { get; set; }

    public ICollection<Contract> Contracts { get; set; }
    public ICollection<Payment> Payments { get; set; }
    
    [Required]
    [Column("ClientType")]
    public string ClientType { get; set; }
}
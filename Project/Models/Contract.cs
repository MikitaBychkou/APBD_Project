using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT.Models;

[Table("Contracts")]
public class Contract
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Client))]
    [Column("ClientId")]
    public int ClientId { get; set; }
    public Client Client { get; set; }

    [Required]
    [ForeignKey(nameof(Software))]
    [Column("SoftwareId")]
    public int SoftwareId { get; set; }
    public Software Software { get; set; }

    [Required]
    [Column("StartDate")]
    public DateTime StartDate { get; set; }

    [Required]
    [Column("EndDate")]
    public DateTime EndDate { get; set; }

    [Required]
    [Column("Price")]
    public decimal Price { get; set; }

    [Required]
    [Column("IsSigned")]
    public bool IsSigned { get; set; }

    [Required]
    [Column("IsPaid")]
    public bool IsPaid { get; set; }

    [Required]
    [Column("IsActive")]
    public bool IsActive { get; set; }

    [Column("AdditionalSupportYears")]
    public int? AdditionalSupportYears { get; set; }

    public ICollection<Payment> Payments { get; set; }
}
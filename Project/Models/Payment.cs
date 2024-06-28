using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT.Models;

[Table("Payments")]
public class Payment
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
    [ForeignKey(nameof(Contract))]
    [Column("ContractId")]
    public int ContractId { get; set; }
    public Contract Contract { get; set; }

    [Required]
    [Column("Amount")]
    public decimal Amount { get; set; }

    [Required]
    [Column("PaymentDate")]
    public DateTime PaymentDate { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace JWT.Models;

[Table("Discounts")]
public class Discount
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Name")]
    public string Name { get; set; }

    [Required]
    [Column("Percentage")]
    public decimal Percentage { get; set; }

    [Required]
    [Column("StartDate")]
    public DateTime StartDate { get; set; }

    [Required]
    [Column("EndDate")]
    public DateTime EndDate { get; set; }

    public ICollection<Software> Softwares { get; set; }
}
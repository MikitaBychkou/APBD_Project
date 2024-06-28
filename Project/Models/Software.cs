using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWT.Models;
[Table("Softwares")]
public class Software
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("Name")]
    public string Name { get; set; }

    [Required]
    [Column("Description")]
    public string Description { get; set; }

    [Required]
    [Column("Version")]
    public string Version { get; set; }

    [Required]
    [Column("Category")]
    public string Category { get; set; }

    [Required]
    [Column("Price")]
    public decimal Price { get; set; }

    public ICollection<Contract> Contracts { get; set; }
    public ICollection<Discount> Discounts { get; set; }
}
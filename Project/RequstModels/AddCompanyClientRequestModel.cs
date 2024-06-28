using System.ComponentModel.DataAnnotations;

namespace Project.RequstModels;

public class AddCompanyClientRequestModel
{
    [Required]
    [MaxLength(100)]
    public string Address { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(15)]
    public string Phone { get; set; }

    [Required]
    [MaxLength(100)]
    public string CompanyName { get; set; }

    [Required]
    [MaxLength(10)]
    public string KRS { get; set; }
}
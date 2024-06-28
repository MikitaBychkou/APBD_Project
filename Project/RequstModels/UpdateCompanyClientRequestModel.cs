using System.ComponentModel.DataAnnotations;

namespace Project.RequstModels;

public class UpdateCompanyClientRequestModel
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
}
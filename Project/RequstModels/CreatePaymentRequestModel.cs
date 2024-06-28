using System.ComponentModel.DataAnnotations;

namespace Project.RequstModels;

public class CreatePaymentRequestModel
{
    [Required]
    public int ClientId { get; set; }

    [Required]
    public int ContractId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }
}
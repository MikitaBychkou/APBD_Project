namespace Project.ResponceModels;

public class ContractResponseModel
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Price { get; set; }
    public bool IsSigned { get; set; }
    public bool IsPaid { get; set; }
    public bool IsActive { get; set; }
    public int? AdditionalSupportYears { get; set; }
}
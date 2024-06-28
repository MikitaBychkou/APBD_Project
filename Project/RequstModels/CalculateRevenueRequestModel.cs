namespace Project.RequstModels;

public class CalculateRevenueRequestModel
{
    public int? SoftwareId { get; set; } // if null -> calculate all revenue
}
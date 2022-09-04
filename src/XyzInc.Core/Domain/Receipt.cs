namespace XyzInc.Core.Domain;

public class Receipt
{
    public DateTime DateIssued { get; set; }
    public decimal AmountToPay { get; set; }
    public string Description { get; set; }
}
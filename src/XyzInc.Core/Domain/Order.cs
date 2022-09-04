namespace XyzInc.Core.Domain;

public class Order
{
    public int OrderNumber { get; set; }
    public Guid UserId { get; set; }
    public decimal PayableAmount { get; set; }
    public string Description { get; set; }
}
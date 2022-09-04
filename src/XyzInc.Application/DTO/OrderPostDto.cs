namespace XyzInc.Application.DTO;

public record OrderPostDto(int OrderNumber, Guid UserId, decimal PayableAmount, int PaymentGatewayId, string Description);
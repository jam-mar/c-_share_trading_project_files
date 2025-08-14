namespace WebApi.Models;

public record BuyStockRequest(
    Guid UserId,
    string StockTicker,
    decimal Quantity
);
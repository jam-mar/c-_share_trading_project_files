namespace Core;

public record Holding(
    User User,
    Stock Stock,
    decimal Quantity,
    decimal AveragePurchasePrice
);

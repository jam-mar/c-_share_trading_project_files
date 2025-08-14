namespace Core;

public record Trade(
    Guid Id,
    User User,
    Stock Stock,
    OrderType OrderType,
    decimal Quantity,
    DateTimeOffset Timestamp
);

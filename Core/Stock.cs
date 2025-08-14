namespace Core;

public record Stock(
    string TickerSymbol,
    string CompanyName,
    decimal CurrentPrice
);
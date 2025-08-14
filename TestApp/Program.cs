using Core;
using Application;

var portfolioService = new PortfolioService();

try
{
    Console.WriteLine("=== Testing Portfolio Service ===\n");
    
    // Get our test user
    var testUser = portfolioService.GetTestUser();
    Console.WriteLine($"Using test user: {testUser.Username}");
    
    // Buy some Microsoft stock
    Console.WriteLine("\n1. Buying 10 shares of MSFT...");
    var portfolio = await portfolioService.BuyStockAsync(testUser.Id, "MSFT", 10);
    
    Console.WriteLine($"✓ Portfolio created with ID: {portfolio.Id}");
    Console.WriteLine($"✓ Holdings count: {portfolio.Holdings.Count}");
    
    var holding = portfolio.Holdings.First();
    Console.WriteLine($"✓ Stock: {holding.Stock.CompanyName} ({holding.Stock.TickerSymbol})");  // ← Fixed this line
    Console.WriteLine($"✓ Quantity: {holding.Quantity}");
    Console.WriteLine($"✓ Average Price: ${holding.AveragePurchasePrice}");
    
    // Buy more of the same stock
    Console.WriteLine("\n2. Buying 5 more shares of MSFT...");
    portfolio = await portfolioService.BuyStockAsync(testUser.Id, "MSFT", 5);
    
    holding = portfolio.Holdings.First();
    Console.WriteLine($"✓ Updated Quantity: {holding.Quantity}");
    Console.WriteLine($"✓ New Average Price: ${holding.AveragePurchasePrice:F2}");
    
    // Buy a different stock
    Console.WriteLine("\n3. Buying 20 shares of AAPL...");
    portfolio = await portfolioService.BuyStockAsync(testUser.Id, "AAPL", 20);
    
    Console.WriteLine($"✓ Total holdings: {portfolio.Holdings.Count}");
    foreach (var h in portfolio.Holdings)
    {
        Console.WriteLine($"  - {h.Stock.TickerSymbol}: {h.Quantity} shares @ ${h.AveragePurchasePrice:F2}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Error: {ex.Message}");
}

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();
using Core;

namespace Application;

public class PortfolioService : IPortfolioService
{

    private readonly List<User> _users = new()
    {
        new User(Guid.NewGuid(), "testuser", "test@example.com")
    };

    private readonly List<Portfolio> _portfolios = new();

    private readonly List<Stock> _stocks = new()
    {
        new Stock("MSFT", "Microsoft Corp", 300.50m),
        new Stock("AAPL", "Apple Inc", 175.25m)
    };

    public async Task<Portfolio> BuyStockAsync(Guid userId, string stockTicker, decimal quantity)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId) ?? throw new ArgumentException("User not found.");
        var stock = _stocks.FirstOrDefault(s => s.TickerSymbol == stockTicker) ?? throw new ArgumentException("Stock not found.");
        var portfolio = _portfolios.FirstOrDefault(p => p.User.Id == userId);
        if (portfolio == null)
        {
            portfolio = new Portfolio(Guid.NewGuid(), user, new List<Holding>());
            _portfolios.Add(portfolio);
        }

        var existingHolding = portfolio.Holdings.FirstOrDefault(h => h.Stock.TickerSymbol == stockTicker);

        if (existingHolding != null)
        {
            var oldValue = existingHolding.Quantity * existingHolding.AveragePurchasePrice;
            var newValue = quantity * stock.CurrentPrice;
            var totalQuantity = existingHolding.Quantity + quantity;
            var newAveragePrice = (oldValue + newValue) / totalQuantity;

            var updatedHolding = existingHolding with
            {
                Quantity = totalQuantity,
                AveragePurchasePrice = newAveragePrice
            };

            portfolio.Holdings.Remove(existingHolding);
            portfolio.Holdings.Add(updatedHolding);
        }
        else
        {
            var newHolding = new Holding(user, stock, quantity, stock.CurrentPrice);
            portfolio.Holdings.Add(newHolding);
        }

        return await Task.FromResult(portfolio);
    }
    public async Task<Portfolio?> GetPortfolioAsync(Guid userId)
    {
        var portfolio = _portfolios.FirstOrDefault(p => p.User.Id == userId);
        return await Task.FromResult(portfolio);
    }

    public async Task<List<Stock>> GetAvailableStocksAsync()
    {
        return await Task.FromResult(_stocks);
    }

    public User GetTestUser()
    {
        return _users.First();
    }
}
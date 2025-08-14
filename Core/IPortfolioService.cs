namespace Core
{
    public interface IPortfolioService
    {
        Task<Portfolio> BuyStockAsync(Guid userId, string stockTicker, decimal quantity);

    }
}
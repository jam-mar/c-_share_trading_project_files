using Microsoft.AspNetCore.Mvc;
using Core;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Portfolio?>> GetPortfolio(Guid userId)
        {
            // We'll need to add this method to our service
            var portfolio = await _portfolioService.GetPortfolioAsync(userId);

            if (portfolio == null)
            {
                return NotFound("Portfolio not found");
            }

            return Ok(portfolio);
        }

        [HttpGet("stocks")]
        public async Task<ActionResult<List<Stock>>> GetAvailableStocks()
        {
            var stocks = await _portfolioService.GetAvailableStocksAsync();
            return Ok(stocks);
        }
    }
}
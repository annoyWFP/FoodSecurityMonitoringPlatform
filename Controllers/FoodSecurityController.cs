using FoodSecurityMonitoringPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodSecurityMonitoringPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodSecurityController : ControllerBase
	{
        private readonly IFoodSecurityService _foodSecurityService;
        private readonly ILogger<FoodSecurityController> _logger;

        public FoodSecurityController(IFoodSecurityService foodSecurityService, ILogger<FoodSecurityController> logger)
        {
            _foodSecurityService = foodSecurityService;
            _logger = logger;
        }

        /// <summary>
        /// Gets alerts for a given country (ISO3) where CFII exceeds the threshold.
        /// Query parameters: startDate, endDate.
        /// Example: GET /api/foodsecurity/alerts/YEM?startDate=2025-01-01&endDate=2025-01-31
        /// </summary>
        [HttpGet("alerts/{iso3}")]
        public async Task<IActionResult> GetAlerts(string iso3, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var alerts = await _foodSecurityService.GetAlertsAsync(iso3, startDate, endDate);
                return Ok(alerts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving alerts for ISO: {Iso3}", iso3);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Returns the time series of CFII (daily average across regions) for a given country.
        /// Example: GET /api/FoodSecurity/timeseries/YEM?startDate=2024-01-01&endDate=2024-01-31
        /// </summary>
        [HttpGet("timeseries/{iso3}")]
        public async Task<IActionResult> GetTimeSeries(string iso3, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var indicators = await _foodSecurityService.GetRegionIndicatorsAsync(iso3, startDate, endDate);
                // Group by date (formatted as yyyy-MM-dd) and compute average CFII per day.
                var timeSeries = indicators
                    .GroupBy(i => i.Date.ToString("yyyy-MM-dd"))
                    .Select(g => new
                    {
                        Date = g.Key,
                        AverageCFII = g.Average(x => x.CFII)
                    })
                    .OrderBy(ts => ts.Date)
                    .ToList();
                return Ok(timeSeries);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving time series for ISO: {Iso3}", iso3);
                return StatusCode(500, "Internal server error");
            }
        }

    }
}


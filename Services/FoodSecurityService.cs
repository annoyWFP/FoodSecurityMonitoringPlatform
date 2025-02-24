using System;
using FoodSecurityMonitoringPlatform.Models;

namespace FoodSecurityMonitoringPlatform.Services
{
    public class FoodSecurityService : IFoodSecurityService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FoodSecurityService> _logger;
        private readonly IConfiguration _configuration;

        public FoodSecurityService(HttpClient httpClient, IConfiguration configuration, ILogger<FoodSecurityService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<RegionIndicator>> GetAlertsAsync(string iso3, DateTime startDate, DateTime endDate, double threshold = 1)
        {
            var indicators = await GetRegionIndicatorsAsync(iso3, startDate, endDate);
            // Return regions where the computed CFII exceeds the threshold.
            return indicators.Where(r => r.CFII > threshold);
        }

        public async Task<IEnumerable<RegionIndicator>> GetRegionIndicatorsAsync(string iso3, DateTime startDate, DateTime endDate)
        {
            string? baseUrl = _configuration["FoodSecurityApi:BaseUrl"];
            string url = $"{baseUrl}/v1/foodsecurity/country/{iso3}/region?date_start={startDate:yyyy-MM-dd}&date_end={endDate:yyyy-MM-dd}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode && response.Content.Headers.ContentLength > 53) // Check for success before deserialization
                {
                    // Deserialize into the ApiResponse model.
                    var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (apiResponse?.Body != null)
                    {
                        // Convert the wrapped items to RegionIndicator.
                        return apiResponse.Body.Select(item => new RegionIndicator
                        {
                            Date = item.Date,
                            RegionName = item.Region.Name,
                            FCS = item.Metrics.Fcs.Prevalence,
                            RCSI = item.Metrics.Rcsi.Prevalence
                        });
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Handle 404 specifically.  
                    return Enumerable.Empty<RegionIndicator>();
                }
                else
                {
                    // Handle other error codes (e.g., 500, 400, etc.)
                    _logger.LogError($"API request failed with status code {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");

                    // Return an appropriate result or throw an exception.
                    return Enumerable.Empty<RegionIndicator>();
                }
                return Enumerable.Empty<RegionIndicator>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching data for ISO: {Iso3}", iso3);
                throw;
            }
        }
    }
}


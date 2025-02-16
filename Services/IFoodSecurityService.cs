using System;
using FoodSecurityMonitoring.Models;

namespace FoodSecurityMonitoring.Services
{
	public interface IFoodSecurityService
	{
        /// <summary>
        /// Retrieves food security indicators for a country between two dates.
        /// </summary>
        Task<IEnumerable<RegionIndicator>> GetRegionIndicatorsAsync(string iso3, DateTime startDate, DateTime endDate);

        /// <summary>
        /// Filters and returns regions where CFII exceeds a given threshold.
        /// </summary>
        Task<IEnumerable<RegionIndicator>> GetAlertsAsync(string iso3, DateTime startDate, DateTime endDate, double threshold = 1.0);
    }
}


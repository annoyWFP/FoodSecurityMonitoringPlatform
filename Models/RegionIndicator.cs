using System;
namespace FoodSecurityMonitoringPlatform.Models
{
    /// <summary>
    /// Represents food security data for an administrative region.
    /// </summary>
	public class RegionIndicator
	{
        public DateTime Date { get; set; }
        public string RegionName { get; set; }
        public double FCS { get; set; }   // e.g., using the prevalence value from Fcs metric
        public double RCSI { get; set; }  // using the prevalence value from Rcsi metric


        /// <summary>
        /// Composite Food Insecurity Index (CFII) computed as 0.5 * FCS + 1.5 * RCSI.
        /// </summary>
        public double CFII => 0.5 * FCS + 1.5 * RCSI;
    }
}


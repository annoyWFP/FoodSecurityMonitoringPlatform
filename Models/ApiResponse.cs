using System;
namespace FoodSecurityMonitoring.Models
{
	public class ApiResponse
	{
        public string StatusCode { get; set; }
        public List<RegionIndicatorWrapper> Body { get; set; }
    }

    public class RegionIndicatorWrapper
    {
        public CountryInfo Country { get; set; }
        public RegionInfo Region { get; set; }
        public DateTime Date { get; set; }
        public string DataType { get; set; }
        public Metrics Metrics { get; set; }
    }

    public class CountryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iso3 { get; set; }
        public string Iso2 { get; set; }
    }

    public class RegionInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
    }

    public class Metrics
    {
        public MetricDetail Fcs { get; set; }
        public MetricDetail Rcsi { get; set; }
    }

    public class MetricDetail
    {
        public int People { get; set; }
        public double Prevalence { get; set; }
    }
}


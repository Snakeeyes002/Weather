﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.BusinessModels;
using WeatherApp.Interfaces;
using WeatherApp.Models.Geocoding;
using WeatherApp.Models.LocationIQGeocoding;

namespace WeatherApp.Services
{
    public class LocationIQGeocodingException : Exception
    {
        public LocationIQGeocodingException() : base()
        {
        }
        public LocationIQGeocodingException(string message) : base(message)
        {
        }
    }

    public class LocationIQGeocodingService : IGeocodingService
    {
        public async Task<IList<FormattedAddress>> GetFormattedAddressAsync(string city_adrress)
        {
            string requestUri = string.Format("https://eu1.locationiq.com/v1/search.php?key=a43d65b56a1a62&q={0}&format=json&addressdetails=1", Uri.EscapeDataString(city_adrress));
            try
            {
                WebClient webClient = new WebClient();
                string result = await webClient.DownloadStringTaskAsync(requestUri);
                List<LocationIQGeocodeResult> locationIQGeocodeResults = JsonConvert.DeserializeObject<List<LocationIQGeocodeResult>>(result);
                List<FormattedAddress> formattedAddresses = (List<FormattedAddress>)locationIQGeocodeResults.Select(r => new FormattedAddress
                {
                    CityName = r.Address.City,
                    CountryName =r.Address.Country,
                    Latitude = Double.Parse(r.Lat),
                    Longtitude = Double.Parse(r.Lon) 
                });
                return formattedAddresses;
            }
            catch (WebException e)
            {
                throw new LocationIQGeocodingException(e.Message);
            }

            
        }
    }
}
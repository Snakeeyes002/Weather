﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.BusinessModels;

namespace WeatherApp.Interfaces
{
    interface IWeatherService
    {
        BusinessWeatherCondition GetWeather();
    }
}
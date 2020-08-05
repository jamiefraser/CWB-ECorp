using ECorp.UI.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECorp.UI.Services
{
    public interface IForecastService
    {
        Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
    }
}

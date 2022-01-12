using BlazorProject.Server.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BlazorProject.Server.Services
{
    public class CalculatorService : ICalculatorService
    {
        public double CalculatePriceAverage(IEnumerable<double> prices)
        {
            return prices.Average();
        }
    }
}

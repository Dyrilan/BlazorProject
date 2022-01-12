using System.Collections.Generic;

namespace BlazorProject.Server.Services.Interfaces
{
    public interface ICalculatorService
    {
        public double CalculatePriceAverage(IEnumerable<double> prices);
    }
}

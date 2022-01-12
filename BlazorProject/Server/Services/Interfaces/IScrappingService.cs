using System.Collections.Generic;

namespace BlazorProject.Server.Services.Interfaces
{
    public interface IScrappingService
    {
        public IEnumerable<double> FetchPricesFromFutbin(string link);
        
    }
}

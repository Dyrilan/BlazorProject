using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorProject.Server.Services.Interfaces
{
    public interface IScrappingService
    {
        public Task<IEnumerable<double>> FetchPricesFromFutbinAsync(string link);
    }
}

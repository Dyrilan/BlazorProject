using BlazorProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorProject.Server.Services.Interfaces
{
    public interface IFileService
    {
        public Task<List<FifaCard>> ReadFifaFileAsync();
        public Task WriteToFifaFileAsync(IEnumerable<FifaCard> input);
    }
}

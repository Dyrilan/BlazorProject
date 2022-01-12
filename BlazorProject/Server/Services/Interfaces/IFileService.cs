using BlazorProject.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorProject.Server.Services.Interfaces
{
    public interface IFileService
    {
        public Task<List<FifaCard>> ReadFifaFile();
        public Task WriteToFifaFile(IEnumerable<FifaCard> input);
    }
}

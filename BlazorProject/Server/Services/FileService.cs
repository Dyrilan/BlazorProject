using BlazorProject.Server.Services.Interfaces;
using BlazorProject.Shared.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorProject.Server.Services
{
    public class FileService : IFileService
    {
        public async Task<List<FifaCard>> ReadFifaFileAsync()
        {
            string fileName = @"..\Server\FifaPrices.json";

            using FileStream openStream = File.OpenRead(fileName);
            var fifaCards = await JsonSerializer.DeserializeAsync<List<FifaCard>>(openStream);

            return fifaCards;
        }

        public async Task WriteToFifaFileAsync(IEnumerable<FifaCard> input)
        {
            string fileName = @"..\Server\FifaPrices.json";
            using FileStream stream = File.Create(fileName);
            {
                await JsonSerializer.SerializeAsync(stream, input);
            }
            
            await stream.DisposeAsync();
        }
    }
}

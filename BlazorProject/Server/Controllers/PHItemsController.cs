using BlazorProject.Server.Data;
using BlazorProject.Shared.Enums;
using BlazorProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProject.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PHItemsController : ControllerBase
    {
        private readonly ILogger<PHItemsController> _logger;
        private readonly IDbContextFactory<PHContext> _contextFactory;

        public PHItemsController(ILogger<PHItemsController> logger, IDbContextFactory<PHContext> contextFactory)
        {
            _logger = logger;
            _contextFactory = contextFactory;

        }

        [HttpGet]
        public async Task<IEnumerable<PHItem>> Get()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.PHItems.ToListAsync();
        }

        [HttpPost("/api/pH/Add")]
        public async Task<string> Add(PHItem pHItem)
        {
            if (pHItem.Type == new phItemType())
            {
                pHItem.Type = phItemType.unknown;
            }

            if(!await ItemExistsInDatabase(pHItem.Name))
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    await context.PHItems.AddAsync(pHItem);
                    await context.SaveChangesAsync();
                }

                return $"{pHItem.Name} added";
            }

            return $"{pHItem.Name} already exists";
        }

        [HttpPost("/api/pH/Delete")]
        public async Task<string> Delete(PHItem pHItem)
        {
            //there is little to no chance to actually not have item in DB
            if (await ItemExistsInDatabase(pHItem.Name))
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    context.PHItems.Attach(pHItem);
                    context.PHItems.Remove(pHItem);
                    await context.SaveChangesAsync();
                }

                return $"{pHItem.Name} deleted";
            }

            return $"{pHItem.Name} doesnt exist";
        }

        [HttpPost("/api/pH/Edit")]
        public async Task<string> Edit(PHItem pHItem)
        {
            //there is little to no chance to actually not have item in DB
            if (await ItemExistsInDatabase(pHItem.Name))
            {
                using (var context = _contextFactory.CreateDbContext())
                {
                    context.Update(pHItem);
                    await context.SaveChangesAsync();
                }

                return $"{pHItem.Name} edited";
            }

            return $"{pHItem.Name} doesnt exist";
        }

        public async Task<bool> ItemExistsInDatabase(string name)
        {
            var values = await _contextFactory.CreateDbContext().PHItems.Where(x => x.Name == name).ToListAsync();

            return values.Any();
        }
    }
}

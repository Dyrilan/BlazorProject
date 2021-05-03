using BlazorProject.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorProject.Shared.Enums;

namespace BlazorProject.Server.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PHContext context)
        {
            context.Database.EnsureCreated();

            // Look for any PHItems.
            if (context.PHItems.Any())
            {
                return;   // DB has been seeded
            }

            var pHItems = new PHItem[]
            {
                new PHItem{Name = "Uhorka", Type=phItemType.vegetable, Value=8},
            };

            foreach (PHItem item in pHItems)
            {
                context.PHItems.Add(item);
            }

            context.SaveChanges();
        }
    }
}

using BlazorProject.Server.Services.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BlazorProject.Server.Services
{
    public class ScrappingService : IScrappingService
    {
        public async Task<IEnumerable<double>> FetchPricesFromFutbinAsync(string link)
        {
            Random r = new();
            int rInt = r.Next(4000, 6000);
            await Task.Delay(rInt);


            var unparsedPrices = new List<string>();

            HtmlWeb web = new();
            HtmlDocument document = await web.LoadFromWebAsync(link);

            var nodes = document.DocumentNode.SelectNodes("//*[@id=\"repTb\"]/tbody/tr[position() < 6]/td[5]/span");

            Parallel.ForEach(nodes, item =>
            {
                unparsedPrices.Add(item.InnerText.Trim(new Char[] { ' ', '.' }));
            });

            var parsedPrices = GetPrices(unparsedPrices);

            return parsedPrices;
        }

        private static IEnumerable<double> GetPrices(IEnumerable<string> unparsedPrices)
        {
            var parsedPrices = new List<double>();

            foreach (var price in unparsedPrices)
            {
                var fPrice = ParsePrices(price);

                parsedPrices.Add(Math.Round(fPrice, 0));
            }

            return parsedPrices;

        }

        private static double ParsePrices(string inputPrice)
        {
            if (inputPrice.Contains('K'))
            {
                inputPrice = inputPrice.Replace("K", "");
                _ = double.TryParse(inputPrice, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double result);
                return result * 1000;
            }

            if (inputPrice.Contains('M'))
            {
                inputPrice = inputPrice.Replace("M", "");
                _ = double.TryParse(inputPrice, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out double result);
                return result * 1000000;
            }

            _ = double.TryParse(inputPrice, out double value);
            return value;
        }
    }
}

using BlazorProject.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorProject.Client.Pages
{
    public partial class Fifa : ComponentBase
    {
        private FifaCard[] _fifaCards;

        double v91 = 49000;
        double v90 = 40000;
        double v89 = 33000;
        double v88 = 24000;
        double v87 = 17000;
        double v86 = 12000;
        double v85 = 6000;
        double v84 = 2000;
        double v83 = 850;
        double v82 = 750;
        double v81 = 550;
        double v80 = 500;
        double v79 = 500;
        double v78 = 500;

        readonly List<(int, string, double)> results = new();

        protected override async Task OnInitializedAsync()
        {
            _fifaCards = await Http.GetFromJsonAsync<FifaCard[]>("api/Fifa/FifaCard");
            SetupPrices();

            results.Clear();
            Calculate83();
            Calculate84();
            Calculate85();
            Calculate86();
            Calculate87();
            Calculate88();
        }

        private async Task ChangeFifaCardPrices()
        {
            _fifaCards = null;
            Dictionary<int, string> data = new Dictionary<int, string>()
            {
                {91,  "https://www.futbin.com/players?page=1&player_rating=91-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {90,  "https://www.futbin.com/players?page=1&player_rating=90-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {89,  "https://www.futbin.com/players?page=1&player_rating=89-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {88,  "https://www.futbin.com/players?page=1&player_rating=88-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {87,  "https://www.futbin.com/players?page=1&player_rating=87-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {86,  "https://www.futbin.com/players?page=1&player_rating=86-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {85,  "https://www.futbin.com/players?page=1&player_rating=85-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {84,  "https://www.futbin.com/players?page=1&player_rating=84-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {83,  "https://www.futbin.com/players?page=1&player_rating=83-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {82,  "https://www.futbin.com/players?page=1&player_rating=82-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {81,  "https://www.futbin.com/players?page=1&player_rating=81-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {80,  "https://www.futbin.com/players?page=1&player_rating=80-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {79,  "https://www.futbin.com/players?page=1&player_rating=79-99&pc_price=200-15000000&sort=pc_price&order=asc"},
                {78,  "https://www.futbin.com/players?page=1&player_rating=78-99&pc_price=200-15000000&sort=pc_price&order=asc"},
            };

            StartTimer();

            await Http.PostAsJsonAsync("api/Fifa/ChangePrices", data);            
            await OnInitializedAsync();            
        }

        private string _name = "";
        private double _value = 0;        
        private void AddValues(double value, string name)
        {
            _name += $"{name}, ";
            _value += value;
        }

        private void ResetValues()
        {
            _name = "";
            _value = 0;
        }
        
        private void SetupPrices()
        {
            v91 = _fifaCards.Where(x => x.Rating == 91).Select(r => r.Price).FirstOrDefault();
            v90 = _fifaCards.Where(x => x.Rating == 90).Select(r => r.Price).FirstOrDefault();
            v89 = _fifaCards.Where(x => x.Rating == 89).Select(r => r.Price).FirstOrDefault();
            v88 = _fifaCards.Where(x => x.Rating == 88).Select(r => r.Price).FirstOrDefault();
            v87 = _fifaCards.Where(x => x.Rating == 87).Select(r => r.Price).FirstOrDefault();
            v86 = _fifaCards.Where(x => x.Rating == 86).Select(r => r.Price).FirstOrDefault();
            v85 = _fifaCards.Where(x => x.Rating == 85).Select(r => r.Price).FirstOrDefault();
            v84 = _fifaCards.Where(x => x.Rating == 84).Select(r => r.Price).FirstOrDefault();
            v83 = _fifaCards.Where(x => x.Rating == 83).Select(r => r.Price).FirstOrDefault();
            v82 = _fifaCards.Where(x => x.Rating == 82).Select(r => r.Price).FirstOrDefault();
            v81 = _fifaCards.Where(x => x.Rating == 81).Select(r => r.Price).FirstOrDefault();
            v80 = _fifaCards.Where(x => x.Rating == 80).Select(r => r.Price).FirstOrDefault();
            v79 = _fifaCards.Where(x => x.Rating == 79).Select(r => r.Price).FirstOrDefault();
        }

        private void Calculate83()
        {
            Dictionary<string, double> patterns = new()
            {
                { "1x84, 6x83, 3x82, 1x81", (1 * v84 + 6 * v83 + 3 * v82 + 1 * v81) },
                { "9x83, 2x82", (9 * v83 + 2 * v82) },
                { "2x84, 3x83, 6x82", (2 * v84 + 3 * v83 + 6 * v82) },
                { "10x83, 1x78", (10 * v83 + 1 * v78) },
                { "3x84, 1x83, 6x82, 1x81 ", (3 * v84 + 1 * v83 + 6 * v82 + 1 * v81) },
                { "3x84, 2x83, 3x82, 3x81 ", (3 * v84 + 2 * v83 + 3 * v82 + 3 * v81) },
                { "1x85, 1x84, 2x83, 6x82, 1x81", (1 * v85 + 1 * v84 + 2 * v83 + 6 * v82 + 1 * v81) },
                { "1x85, 4x83, 6x82", (1 * v85 + 4 * v83 + 6 * v82) },
                { "2x85, 9x82", (2 * v85 + 9 * v82) },
                { "4x84, 1x83, 6x81", (4 * v84 + 1 * v83 + 6 * v81) },
                { "4x84, 1x83, 1x82, 4x81, 1x80", (4 * v84 + 1 * v83 + 1 * v82 + 4 * v81 + 1 * v80) },
                { "1x86, 3x83, 6x82, 1x81", (1 * v86 + 3 * v83 + 6 * v82 + 1 * v81) },
                { "1x86, 4x83, 3x82, 3x81", (1 * v86 + 4 * v83 + 3 * v82 + 3 * v81) },
                { "1x86, 1x85, 7x82, 2x81", (1 * v86 + 1 * v85 + 7 * v82 + 2 * v81) },
                { "1x86, 1x85, 8x82, 1x80", (1 * v86 + 1 * v85 + 8 * v82 + 1 * v80) },
                { "1x86, 4x84, 1x82, 1x80, 4x78", (1 * v86 + 4 * v84 + 1 * v82 + 1 * v80 + 4 * v78) }
            };

            var result = patterns.OrderBy(x => x.Value);

            results.Add((83, result.First().Key, result.First().Value));
        }

        private void Calculate84()
        {
            Dictionary<string, double> patterns = new()
            {
                { "1x85, 6x84, 3x83, 1x82", (1 * v85 + 6 * v84 + 3 * v83 + 1 * v82) },
                { "9x84, 2x83", (9 * v84 + 2 * v83) },
                { "2x85, 3x84, 6x83", (2 * v85 + 3 * v84 + 6 * v83) },
                { "10x84, 1x79", (10 * v84 + 1 * v79) },
                { "3x85, 1x84, 6x83, 1x82 ", (3 * v85 + 1 * v84 + 6 * v83 + 1 * v82) },
                { "3x85, 2x84, 3x83, 3x82 ", (3 * v85 + 2 * v84 + 3 * v83 + 3 * v82) },
                { "1x86, 1x85, 2x84, 6x83, 1x82", (1 * v86 + 1 * v85 + 2 * v84 + 6 * v83 + 1 * v82) },
                { "1x86, 4x84, 6x83", (1 * v86 + 4 * v84 + 6 * v83) },
                { "2x86, 9x83", (2 * v86 + 9 * v83) },
                { "4x85, 1x84, 6x82", (4 * v85 + 1 * v84 + 6 * v82) },
                { "4x85, 1x84, 1x83, 4x82, 1x81", (4 * v85 + 1 * v84 + 1 * v83 + 4 * v82 + 1 * v81) },
                { "1x87, 3x84, 6x83, 1x82", (1 * v87 + 3 * v84 + 6 * v83 + 1 * v82) },
                { "1x87, 4x84, 3x83, 3x82", (1 * v87 + 4 * v84 + 3 * v83 + 3 * v82) },
                { "1x87, 1x86, 7x83, 2x82", (1 * v87 + 1 * v86 + 7 * v83 + 2 * v82) },
                { "1x87, 1x86, 8x83, 1x81", (1 * v87 + 1 * v86 + 8 * v83 + 1 * v81) },
                { "1x87, 4x85, 1x83, 1x81, 4x80", (1 * v87 + 4 * v85 + 1 * v83 + 1 * v81 + 4 * v80) }
            };

            var result = patterns.OrderBy(x => x.Value);

            results.Add((84, result.First().Key, result.First().Value));
        }

        private void Calculate85()
        {
            Dictionary<string, double> patterns = new()
            {
                { "1x86, 6x85, 3x84, 1x83", (1 * v86 + 6 * v85 + 3 * v84 + 1 * v83) },
                { "9x85, 2x84", (9 * v85 + 2 * v84) },
                { "2x86, 3x85, 6x84", (2 * v86 + 3 * v85 + 6 * v84) },
                { "10x85, 1x80", (10 * v85 + 1 * v80) },
                { "3x86, 1x85, 6x84, 1x83 ", (3 * v86 + 1 * v85 + 6 * v84 + 1 * v83) },
                { "3x86, 2x85, 3x84, 3x83 ", (3 * v86 + 2 * v85 + 3 * v84 + 3 * v83) },
                { "1x87, 1x86, 2x85, 6x84, 1x83", (1 * v87 + 1 * v86 + 2 * v85 + 6 * v84 + 1 * v83) },
                { "1x87, 4x85, 6x84", (1 * v87 + 4 * v85 + 6 * v84) },
                { "2x87, 9x84", (2 * v87 + 9 * v84) },
                { "4x86, 1x85, 6x83", (4 * v86 + 1 * v85 + 6 * v83) },
                { "4x86, 1x85, 1x84, 4x83, 1x82", (4 * v86 + 1 * v85 + 1 * v84 + 4 * v83 + 1 * v82) },
                { "1x88, 3x85, 6x84, 1x83", (1 * v88 + 3 * v85 + 6 * v84 + 1 * v83) },
                { "1x88, 4x85, 3x84, 3x83", (1 * v88 + 4 * v85 + 3 * v84 + 3 * v83) },
                { "1x88, 1x87, 7x84, 2x83", (1 * v88 + 1 * v87 + 7 * v84 + 2 * v83) },
                { "1x88, 1x87, 8x84, 1x82", (1 * v88 + 1 * v87 + 8 * v84 + 1 * v82) },
                { "1x88, 4x86, 1x84, 1x82, 4x81", (1 * v88 + 4 * v86 + 1 * v84 + 1 * v82 + 4 * v81) }
            };

            var result = patterns.OrderBy(x => x.Value);

            results.Add((85, result.First().Key, result.First().Value));
        }

        private void Calculate86()
        {
            Dictionary<string, double> patterns = new()
            {
                { "1x87, 6x86, 3x85, 1x84", (1 * v87 + 6 * v86 + 3 * v85 + 1 * v84) },
                { "9x86, 2x85", (9 * v86 + 2 * v85) },
                { "2x87, 3x86, 6x85", (2 * v87 + 3 * v86 + 6 * v85) },
                { "10x86, 1x81 ", (10 * v86 + 1 * v81) },
                { "3x87, 1x86, 6x85, 1x84 ", (3 * v87 + 1 * v86 + 6 * v85 + 1 * v84) },
                { "3x87, 2x86, 3x85, 3x84 ", (3 * v87 + 2 * v86 + 3 * v85 + 3 * v84) },
                { "1x88, 1x87, 2x86, 6x85, 1x84", (1 * v88 + 1 * v87 + 2 * v86 + 6 * v85 + 1 * v84) },
                { "1x88, 4x86, 6x85", (1 * v88 + 4 * v86 + 6 * v85) },
                { "2x88, 9x85", (2 * v88 + 9 * v85) },
                { "4x87, 1x86, 6x84", (4 * v87 + 1 * v86 + 6 * v84) },
                { "4x87, 1x86, 1x85, 4x84, 1x83", (4 * v87 + 1 * v86 + 1 * v85 + 4 * v84 + 1 * v83) },
                { "1x89, 3x86, 6x85, 1x84", (1 * v89 + 3 * v86 + 6 * v85 + 1 * v84) },
                { "1x89, 4x86, 3x85, 3x84", (1 * v89 + 4 * v86 + 3 * v85 + 3 * v84) },
                { "1x89, 1x88, 7x85, 2x84", (1 * v89 + 1 * v88 + 7 * v85 + 2 * v84) },
                { "1x89, 1x88, 8x85, 1x83", (1 * v89 + 1 * v88 + 8 * v85 + 1 * v83) },
                { "1x89, 4x87, 1x85, 1x83, 4x82", (1 * v89 + 4 * v87 + 1 * v85 + 1 * v83 + 4 * v82) }
            };

            var result = patterns.OrderBy(x => x.Value);

            results.Add((86, result.First().Key, result.First().Value));
        }

        private void Calculate87()
        {
            Dictionary<string, double> patterns = new()
            {
                { "1x88, 6x87, 3x86, 1x85", (1 * v88 + 6 * v87 + 3 * v86 + 1 * v85) },
                { "9x87, 2x86", (9 * v87 + 2 * v86) },
                { "2x88, 3x87, 6x86", (2 * v88 + 3 * v87 + 6 * v86) },
                { "10x87, 1x82 ", (10 * v87 + 1 * v82) },
                { "3x88, 1x87, 6x86, 1x85 ", (3 * v88 + 1 * v87 + 6 * v86 + 1 * v85) },
                { "3x88, 2x87, 3x86, 3x85 ", (3 * v88 + 2 * v87 + 3 * v86 + 3 * v85) },
                { "1x89, 1x88, 2x87, 6x86, 1x85", (1 * v89 + 1 * v88 + 2 * v87 + 6 * v86 + 1 * v85) },
                { "1x89, 4x87, 6x86", (1 * v89 + 4 * v87 + 6 * v86) },
                { "2x89, 9x86", (2 * v89 + 9 * v86) },
                { "4x88, 1x87, 6x85", (4 * v88 + 1 * v87 + 6 * v85) },
                { "4x88, 1x87, 1x86, 4x85, 1x84", (4 * v88 + 1 * v87 + 1 * v86 + 4 * v85 + 1 * v84) },
                { "1x90, 3x87, 6x86, 1x85", (1 * v90 + 3 * v87 + 6 * v86 + 1 * v85) },
                { "1x90, 4x87, 3x86, 3x85", (1 * v90 + 4 * v87 + 3 * v86 + 3 * v85) },
                { "1x90, 1x89, 7x86, 2x85", (1 * v90 + 1 * v89 + 7 * v86 + 2 * v85) },
                { "1x90, 1x89, 8x86, 1x84", (1 * v90 + 1 * v89 + 8 * v86 + 1 * v84) },
                { "1x90, 4x88, 1x86, 1x84, 4x83", (1 * v90 + 4 * v88 + 1 * v86 + 1 * v84 + 4 * v83) }
            };

            var result = patterns.OrderBy(x => x.Value);

            results.Add((87, result.First().Key, result.First().Value));
        }

        private void Calculate88()
        {
            Dictionary<string, double> patterns = new()
            {
                { "1x89, 6x88, 3x87, 1x86", (1 * v89 + 6 * v88 + 3 * v87 + 1 * v86) },
                { "9x88, 2x87", (9 * v88 + 2 * v87) },
                { "2x89, 3x88, 6x87", (2 * v89 + 3 * v88 + 6 * v87) },
                { "10x88, 1x83 ", (10 * v88 + 1 * v83) },
                { "3x89, 1x88, 6x87, 1x86 ", (3 * v89 + 1 * v88 + 6 * v87 + 1 * v86) },
                { "3x89, 2x88, 3x87, 3x86 ", (3 * v89 + 2 * v88 + 3 * v87 + 3 * v86) },
                { "1x90, 1x89, 2x88, 6x87, 1x86", (1 * v90 + 1 * v89 + 2 * v88 + 6 * v87 + 1 * v86) },
                { "1x90, 4x88, 6x86", (1 * v90 + 4 * v88 + 6 * v86) },
                { "2x90, 9x87", (2 * v90 + 9 * v87) },
                { "4x89, 1x88, 6x86", (4 * v89 + 1 * v88 + 6 * v86) },
                { "4x89, 1x88, 1x87, 4x86, 1x85", (4 * v89 + 1 * v88 + 1 * v87 + 4 * v86 + 1 * v85) },
                { "1x91, 3x88, 6x87, 1x86", (1 * v91 + 3 * v88 + 6 * v87 + 1 * v86) },
                { "1x91, 4x88, 3x87, 3x86", (1 * v91 + 4 * v88 + 3 * v87 + 3 * v86) },
                { "1x91, 1x90, 7x87, 2x86", (1 * v91 + 1 * v90 + 7 * v87 + 2 * v86) },
                { "1x91, 1x90, 8x87, 1x85", (1 * v91 + 1 * v90 + 8 * v87 + 1 * v85) },
                { "1x91, 4x89, 1x87, 1x85, 4x84", (1 * v91 + 4 * v89 + 1 * v87 + 1 * v85 + 4 * v84) }
            };

            var result = patterns.OrderBy(x => x.Value);

            results.Add((88, result.First().Key, result.First().Value));
        }

        private static System.Timers.Timer aTimer;
        private int counter = 78;

        public void StartTimer()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += CountDownTimer;
            aTimer.Enabled = true;
        }

        public void CountDownTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (counter > 0)
            {
                counter -= 1;
            }
            else
            {
                aTimer.Enabled = false;
            }
            InvokeAsync(StateHasChanged);
        }
    }
}

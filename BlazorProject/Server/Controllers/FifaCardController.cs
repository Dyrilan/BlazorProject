﻿using BlazorProject.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BlazorProject.Server.Services.Interfaces;

namespace BlazorProject.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FifaCardController : ControllerBase
    {
        private readonly ILogger<FifaCardController> _logger;
        private readonly IScrappingService _scrappingService;
        private readonly IFileService _fileService;
        private readonly ICalculatorService _calculator;

        public FifaCardController(ILogger<FifaCardController> logger, IScrappingService scrappingService, IFileService fileService, ICalculatorService calculator)
        {
            _logger = logger;
            _scrappingService = scrappingService;
            _fileService = fileService;
            _calculator = calculator;
        }

        [HttpGet("/api/Fifa/FifaCard")]
        public async Task<IEnumerable<FifaCard>> GetFifaCards()
        {            
            var fifaCards = await ReadFromFile();

            return fifaCards;
        }

        [HttpPost("/api/Fifa/ChangePrices")]
        public async Task ChangeFifaCardPrices(IDictionary<int, string> data)
        {
            var fifaCards = GetFifaCards(data);

            await WriteToFile(fifaCards);
        }


        private async Task<List<FifaCard>> ReadFromFile()
        {
            var fifaCards = new List<FifaCard>();

            try
            {
                fifaCards = await _fileService.ReadFifaFile();
            }
            catch (Exception)
            {

                _logger.LogError("Unable to read from FifaCards to file");
            }

            return fifaCards;
        }

        private async Task WriteToFile(List<FifaCard> fifaCards)
        {
            try
            {
                await _fileService.WriteToFifaFile(fifaCards);
            }
            catch (Exception)
            {

                _logger.LogError("Unable to write FifaCards to file");
            }
        }

        private List<FifaCard> GetFifaCards(IDictionary<int, string> data)
        {
            var fifaCards = new List<FifaCard>();

            foreach (var (rating, link) in data)
            {
                var prices = _scrappingService.FetchPricesFromFutbin(link);
                var averagePrice = _calculator.CalculatePriceAverage(prices);

                fifaCards.Add(new FifaCard
                {
                    Rating = rating,
                    Price = averagePrice
                });
            }

            return fifaCards;
        }
    }
}
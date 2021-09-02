
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StockExchange.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockExchange
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private IStockServiceControl _stockService;
        private ICompanyServiceControl _companyService;
        public Worker(ILogger<Worker> logger, IStockServiceControl stockService, ICompanyServiceControl companyService)
        {
            _logger = logger;
            _stockService = stockService;
            _companyService = companyService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //Console.WriteLine("Printing from worker");

                _companyService.SetVal();
                _stockService.SetVal();

                await Task.Delay(60*1000, stoppingToken);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HackAzureYakinikuApp.Models;
using Stripe;

namespace HackAzureYakinikuApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BalanceService balanceService;

        public HomeController(ILogger<HomeController> logger, BalanceService balanceService)
        {
            _logger = logger;
            this.balanceService = balanceService;
        }

        public IActionResult Index()
        {
            var balance = balanceService.Get(new RequestOptions()
            {
            });
            var yakiniku = new YakinikuViewModel()
            {
                Balance = balance.Pending.Sum(x => x.Amount),
            };

            ViewBag.Yakiniku = yakiniku;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackAzureYakinikuApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Stripe;

namespace HackAzureYakinikuApp.Controllers
{
    public class PaymentController : Controller
    {
        private PaymentIntentService paymentIntentService;
        private IConfiguration configuration;

        public PaymentController(PaymentIntentService paymentIntentService, IConfiguration configuration)
        {
            this.paymentIntentService = paymentIntentService;
            this.configuration = configuration;
        }

        // GET: PaymentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PaymentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PaymentController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = 1099,
                    Currency = "jpy",
                    // Verify your integration in this guide by including this parameter
                    Metadata = new Dictionary<string, string>
                    {
                      { "integration_check", "accept_a_payment" },
                    },
                };

                var paymentIntent = paymentIntentService.Create(options);

                return RedirectToAction(nameof(Pay), new
                {
                    clientSecret = paymentIntent.ClientSecret,
                });
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("Pay/{clientSecret}")]
        public ActionResult Pay(string clientSecret)
        {
            var model = new CollectCardModel()
            {
                ClientSecret = clientSecret,
            };
            return View(model);
        }

        // GET: PaymentController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaymentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaymentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

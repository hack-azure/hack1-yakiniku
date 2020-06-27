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
        {data:image/pjpeg;base64,/9j/4AAQSkZJRgABAQEAYABgAAD/2wBDAAEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/2wBDAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQEBAQH/wAARCABAAEADASIAAhEBAxEB/8QAHwAAAQUBAQEBAQEAAAAAAAAAAAECAwQFBgcICQoL/8QAtRAAAgEDAwIEAwUFBAQAAAF9AQIDAAQRBRIhMUEGE1FhByJxFDKBkaEII0KxwRVS0fAkM2JyggkKFhcYGRolJicoKSo0NTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqDhIWGh4iJipKTlJWWl5iZmqKjpKWmp6ipqrKztLW2t7i5usLDxMXGx8jJytLT1NXW19jZ2uHi4+Tl5ufo6erx8vP09fb3+Pn6/8QAHwEAAwEBAQEBAQEBAQAAAAAAAAECAwQFBgcICQoL/8QAtREAAgECBAQDBAcFBAQAAQJ3AAECAxEEBSExBhJBUQdhcRMiMoEIFEKRobHBCSMzUvAVYnLRChYkNOEl8RcYGRomJygpKjU2Nzg5OkNERUZHSElKU1RVVldYWVpjZGVmZ2hpanN0dXZ3eHl6goOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK0tPU1dbX2Nna4uPk5ebn6Onq8vP09fb3+Pn6/9oADAMBAAIRAxEAPwD+riiiivpE1po+n/tnl/XzR8eFFFFCa00fT/2zy/r5oAooooTWmj6f+2eX9fNAFFFFCa00fT/2zy/r5oAooopp7fLp/g/r/hkAVz/irxb4V8CeHtU8W+N/E3h/wd4U0SBbrWvE3irWdO8PeH9Itnmit0uNT1nVrm006wga4mhgWW6uYo2mljiDF3VT0FfnF/wV0/5RvftX/wDYgaZ/6mvhanBc0oR/mlFbLq6a7ef9aWzrVHSo1aqV3Tp1JpPZuEXJJ+tj9AfDXibw34z0HSvFXg/xBonivwxrtnFqOh+I/Deq2GuaDrOnzgmG+0rV9LnutP1CzmAJiubS4mhkwdrnFblfBv8AwS9/5R7/ALIv/ZFvCv8A6Kmr7yokuWTjvaVr2XRwX9f8BDpTdSlSqNWdSnTqNLpzwUrfK9gooopJ7fLp/g/r/hkWFFFFCW3y6/4P8v6ugCvzi/4K6f8AKN79q/8A7EDTP/U18LV+jtc14w8GeEPiF4a1fwZ4+8K+HfG3g/X7dbTXfCvi3RdO8ReHNatEniuUttV0TV7a803ULdLmCC4WG7tpY1mhilCh41YOD5ZQla/LKLtda2dN2/D8u6Mq0HVo1aSaTqUqkE3snODim/JXPjL/AIJe/wDKPf8AZF/7It4V/wDRU1feVYHhXwp4X8C+HNH8H+CvDmheEPCfh6xi0zQPDPhjSbDQvD+iabACILDSdH0uC10/TrKEEiK1tLeGCME7EGTW/RJ80nLvK+66uD/r/goqlF06VKm9XTp04NrZuEFFtetrhRRRSS2+XX/B/l/V0WFFFFJJab9P/bPP+vkgCiiihJab9P8A2zz/AK+SAKKKKElpv0/9s8/6+SAKKKKElpv0/wDbPP8Ar5ID/9k=
            //try
            //{
            var options = new PaymentIntentCreateOptions
            {
                Amount = long.Parse(collection["amount"]),
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
            //}
            //catch
            //{
            //    return View();
            //}
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

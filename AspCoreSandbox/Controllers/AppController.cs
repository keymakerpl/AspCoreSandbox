using AspCoreSandbox.Data;
using AspCoreSandbox.Data.Repositories;
using AspCoreSandbox.Services;
using AspCoreSandbox.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreSandbox.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IProductRepository _repository;

        public AppController(IMailService mailService, IProductRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("Contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("mail@mail.pl", model.Subject, model.Message);
                ViewBag.UserMessage = "Mail Sent!";
                ModelState.Clear();
            }

            return View();
        }

        [HttpGet("About")]
        public IActionResult About()
        {
            ViewBag.Title = "About";

            return View();
        }

        
        public IActionResult Shop()
        {
            //var products = await _repository.GetAllAsync();

            //return View(products);

            return View();
        }
    }
}

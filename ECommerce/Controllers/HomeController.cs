using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Hoşgeldiniz";
            return View();
        }
        public IActionResult Help()
        {
            ViewData["Title"] = "Yardım Masası";
            return View();
        }
        [Route("/Contact")]
        public IActionResult Contact()
        {
            
            ViewData["Title"] = "İletişim";
            return View();
        }
    }
}

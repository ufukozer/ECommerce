using System;
using System.Collections.Generic;
using ECommerce.Data.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;



namespace ECommerce.Controllers
{
    public class CategoryController : Controller
    {
        [Route ("/kategori/{id}")]
        public IActionResult Index(int id)
        {
            Category category = new Category();
            using (Data.ECommerceContext eCommerce = new Data.ECommerceContext())
            {
                category = eCommerce.Categories.SingleOrDefault(a => a.Id == id);
                //select * from Categories where Id == 3
            }
            ViewData["Title"] = category.Name;
            return View(category);
        }

    }
}
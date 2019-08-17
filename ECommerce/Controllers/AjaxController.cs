using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class AjaxController : Controller
    {
        [Route("/api")]
        public IActionResult Handle()
        {
            string json = HttpContext.Request.Form["JSON"].ToString();
            DTO.ProductSaveDto productSave = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductSaveDto>(json);
            
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                eCommerceContext.Products.Add(new Models.Product()
                {
                    Name = productSave.ProductName,
                    Description = "boş",
                    StateId = (int) Enums.State.Active,
                    CategoryId = productSave.CategoryId,
                    CreateDate = DateTime.UtcNow,
                });
                eCommerceContext.SaveChanges();
            }
            return View();
        }
    }
}
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
            DTO.ProductSaveDto categorySave = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductSaveDto>(json);
            
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                eCommerceContext.Products.Add(new Models.Product()
                {
                    Name = categorySave.productName,
                    Description = "boş",
                    State = eCommerceContext.States.Single(a => a.Id == (int)Enums.State.Active),
                   // Category = "???",
                    CreateDate = DateTime.UtcNow,
                });
            }
            return View();
        }
    }
}
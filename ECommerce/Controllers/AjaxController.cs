using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class AjaxController : Controller
    {
        private static readonly AjaxMethod AjaxMethod = new AjaxMethod();
        [Route("/api")]
        public JsonResult Handle()
        {
            DTO.AjaxResponseDto ajaxResponse = new DTO.AjaxResponseDto();
            string json = HttpContext.Request.Form["JSON"].ToString();
            DTO.AjaxRequestDto ajaxRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.AjaxRequestDto>(json);
            if (ajaxRequest.Method == "SaveProduct")
            {
                AjaxMethod.SaveProduct(ajaxRequest.Json);
            }
            else if (ajaxRequest.Method == "ProductsByCategoryId")
            {
                ajaxResponse.Dynamic = AjaxMethod.ProductsByCategoryId(ajaxRequest.Json);
            }
            
            return new JsonResult(ajaxResponse);
        }
    }
    public class AjaxMethod
    {
        public void SaveProduct(string json)
        {
            DTO.ProductSaveDto productSave = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductSaveDto>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                eCommerceContext.Products.Add(new Models.Product()
                {
                    Name = productSave.ProductName,
                    Description = productSave.ProductDescription,
                    StateId = (int)Enums.State.Active,
                    CategoryId = productSave.CategoryId,
                    CreateDate = DateTime.UtcNow,
                });
                eCommerceContext.SaveChanges();
            }
        }
        public List<Models.Product> ProductsByCategoryId(string json)
        {
            List<Models.Product> result = new List<Models.Product>();
            DTO.ProductsByCategoryId productsByCategoryId = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductsByCategoryId>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                result = eCommerceContext.Products.Where(a => a.CategoryId == productsByCategoryId.CategoryId).ToList();
            }
            return result;
        }
    }
}
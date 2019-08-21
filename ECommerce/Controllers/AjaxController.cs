using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PagedList;

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
            else if (ajaxRequest.Method == "RemoveProduct")
            {
                ajaxResponse.Dynamic = AjaxMethod.RemoveProduct(ajaxRequest.Json);
            }
            else if (ajaxRequest.Method == "ContactSubmit")
            {
                AjaxMethod.ContactSubmit(ajaxRequest.Json);
            }
            else if (ajaxRequest.Method == "UpdateProduct")
            {
                AjaxMethod.ProductUpdate(ajaxRequest.Json);
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
        private List<Models.Product> pagedList;
        public List<Models.Product> ProductsByCategoryId(string json)
        {

            List<Models.Product> result = new List<Models.Product>();
            DTO.ProductsByCategoryId productsByCategoryId = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductsByCategoryId>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                result = eCommerceContext.Products.Where(a => a.CategoryId == productsByCategoryId.CategoryId).ToList();
                PagedList<Models.Product> pagedList = new PagedList<Models.Product>(result, 1, 2);
            }

            return pagedList;
        }
        public bool RemoveProduct(string json)
        {
            bool result = false;
            DTO.ProductRemoveDto productRemove = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductRemoveDto>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                Models.Product product = eCommerceContext.Products.SingleOrDefault(a => a.Id == productRemove.ProductId);
                if (product != null)
                {
                    eCommerceContext.Products.Remove(product);
                    eCommerceContext.SaveChanges();
                    result = true;
                }
            }
            return result;
        }
        public void ContactSubmit(string json)
        {
            DTO.ContactSubmitDto contactSubmit = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ContactSubmitDto>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                eCommerceContext.Contacts.Add(new Models.Contact()
                {
                    NameSurname = contactSubmit.NameSurname,
                    EMail = contactSubmit.EMail,
                    Message = contactSubmit.Message,
                });
                eCommerceContext.SaveChanges();
            }
        }
        public void ProductUpdate(string json)
        {
            DTO.ProductUpdateDto productUpdate = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductUpdateDto>(json);
            using (ECommerceContext eCommerceContext = new ECommerceContext())
            {
                Models.Product product = eCommerceContext.Products.SingleOrDefault(a => a.Id == productUpdate.ProductId);

                product.Description = productUpdate.ProductDescription;
                product.Name = productUpdate.ProductName;

                eCommerceContext.Products.Update(product);
                eCommerceContext.SaveChanges();
            }
        }
    }
}
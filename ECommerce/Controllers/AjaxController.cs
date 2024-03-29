﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Data.Models;
//using PagedList;
using Microsoft.EntityFrameworkCore;

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
        private static readonly Adapter.ProductAdapter productAdapter = new Adapter.ProductAdapter();
        public void SaveProduct(string json)
        {
            DTO.ProductSaveDto productSave = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductSaveDto>(json);
            Data.Models.Product product = new Data.Models.Product()
            {
                Name = productSave.ProductName,
                Description = productSave.ProductDescription,
                StateId = (int)Enums.State.Active,
                CategoryId = productSave.CategoryId,
                CreateDate = DateTime.UtcNow
            }; 
            productAdapter.Insert<Data.Models.Product>(product);
            
        }

        public List<Data.Models.Product> ProductsByCategoryId(string json)
        {

            List<Data.Models.Product> result = new List<Data.Models.Product>();
            DTO.ProductsByCategoryId productsByCategoryId = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductsByCategoryId>(json);
            IQueryable<Data.Models.Product> products = productAdapter.Get<Data.Models.Product>();

            result = products.Include(a => a.Category).Where(a => a.CategoryId == productsByCategoryId.CategoryId).ToList();


            return result;
        }
        public bool RemoveProduct(string json)
        {
            bool result = false;
            DTO.ProductRemoveDto productRemove = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductRemoveDto>(json);
            productAdapter.Delete<Data.Models.Product>(productRemove.ProductId);

            return result;
        }
        private static readonly Adapter.ContactAdapter contactAdapter = new Adapter.ContactAdapter();
        public void ContactSubmit(string json)
        {
            DTO.ContactSubmitDto contactSubmit = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ContactSubmitDto>(json);
            IQueryable<Data.Models.Contact> contacts = contactAdapter.Get<Data.Models.Contact>();

            //using (ECommerceContext eCommerceContext = new ECommerceContext())
            //{
            //    eCommerceContext.Contacts.Add(new Models.Contact()
            //    {
            //        NameSurname = contactSubmit.NameSurname,
            //        EMail = contactSubmit.EMail,
            //        Message = contactSubmit.Message,
            //    });
            //    eCommerceContext.SaveChanges();
            //}
        }
        public void ProductUpdate(string json)
        {
            DTO.ProductUpdateDto productUpdate = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO.ProductUpdateDto>(json);
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                Data.Models.Product product = eCommerceContext.Products.SingleOrDefault(a => a.Id == productUpdate.ProductId);

                product.Description = productUpdate.ProductDescription;
                product.Name = productUpdate.ProductName;

                eCommerceContext.Products.Update(product);
                eCommerceContext.SaveChanges();
            }
        }
    }
}
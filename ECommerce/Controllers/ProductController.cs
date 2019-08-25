using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Data.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [Route("/urun/{id}")]
        public IActionResult Index(int id)
        {
            
            Data.Models.Product product = _productRepository.Find(id);
            return View(product);
        }
        [Route("urun/{id}/guncelle")]
        public IActionResult Update(int id)
        {
            Data.Models.Product product;
            using (Data.ECommerceContext eCommerceContext = new Data.ECommerceContext())
            {
                product = eCommerceContext.Products.SingleOrDefault(a => a.Id == id);
            }
            return View(product);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.API.Model;
using Products.API.Services;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = productService.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            return Ok(productService.GetProduct(id));
        }
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var existingProduct = productService.GetProduct(id);
            if (existingProduct == null)
            {
                return BadRequest($"{id} id'li eleman yok.");
            }
            product.Id = id;
            return Ok(productService.Edit(product));
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            var newProduct = productService.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, null);

        }
    }
}

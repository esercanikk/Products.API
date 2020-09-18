﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Schema;
using Products.API.Data;
using Products.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.API.Services
{
    public class ProductService : IProductService
    {
        private ProductDbContext dbContext;
        public ProductService(ProductDbContext dbContext)
        {
            this.dbContext = dbContext;
        } 

        public Product Add(Product product)
        {
            var entity = dbContext.Products.Add(product).Entity;
            dbContext.SaveChanges();
            return entity;
        }

        public Product Delete(int id)
        {
            var product = dbContext.Products.Find(id);
            var deletedProduct = dbContext.Products.Remove(product).Entity;
            return deletedProduct;
        }

        public Product Edit(Product product)
        {
            dbContext.Entry(product).State = EntityState.Modified;
            dbContext.SaveChanges();
            return product;
        }

        public Product GetProduct(int id)
        {
            return dbContext.Products.Find(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return dbContext.Products.ToList();
        }
    }
}

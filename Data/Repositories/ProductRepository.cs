﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Helpers;
using ECommerceCMS.Models;

namespace ECommerceCMS.Data.Repositories
{
    public class ProductRepository : EntityBaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public ProductsByListingModel GetProductsByListingId(int listingId)
        {
            var returnModel = new ProductsByListingModel();
            var queryable = _context.Set<ProductListing>().Include(c => c.Product).Include(c=>c.Listing);
            queryable.Load();
            var productlistings = queryable.Where(t => t.ListingId == listingId).Select(c=>new { Product = c.Product,Listing=c.Listing }).ToList();
            int index = 0;
            if(productlistings!=null && productlistings.Count > 0)
            {
                returnModel.Listing = new ListingViewModel() { Name = productlistings[0].Listing.Name, Description = productlistings[0].Listing.Description };
                returnModel.Products = new List<ProductViewModel>();
            }
            foreach (var product in productlistings)
            {
               

                returnModel.Products.Add(new ProductViewModel()
                {
                    BaseImageUrl = product.Product.BaseImageUrl,
                    Id = product.Product.Id,
                    isSale = index % 2 == 0,
                    Name = product.Product.Name,
                    Price = product.Product.Price,
                    SalePrice = index % 2 == 0 ? product.Product.Price * 0.85 : product.Product.Price,
                    isNew = index % 3 == 0,
                    Pictures=new List<string>() { product.Product.BaseImageUrl}
                    
                }
                );
                index++;
            }
            return returnModel;


        }
        public ProductsByCategoryModel GetProductsByCategoryId(int categoryId, int itemCount, int pageNumber)
        {
            var returnModel = new ProductsByCategoryModel();
            var queryable = _context.Set<Product>().Include(c => c.ProductCategory);
            queryable.Load();
            var products = queryable.Where(t => t.ProductCategoryId == categoryId).Skip((pageNumber-1)*itemCount).Take(itemCount).Select(c => new { Product = c, ProductCategory = c.ProductCategory }).ToList();
            int index = 0;
            if (products != null && products.Count > 0)
            {
                returnModel.Category = new ProductCategoryViewModel() { CategoryName = products[0].ProductCategory.CategoryName};
                returnModel.Products = new List<ProductViewModel>();
            }
            foreach (var product in products)
            {


                returnModel.Products.Add(new ProductViewModel()
                {
                    BaseImageUrl = product.Product.BaseImageUrl,
                    Id = product.Product.Id,
                    isSale = index % 2 == 0,
                    Name = product.Product.Name,
                    Price = product.Product.Price,
                    SalePrice = index % 2 == 0 ? product.Product.Price * 0.85 : product.Product.Price,
                    isNew = index % 3 == 0,
                    Pictures = new List<string>() { product.Product.BaseImageUrl }

                }
                );
                index++;
            }
            return returnModel;


        }
        public IQueryable<Product> GetProductsQueryable()
        {
            IQueryable<Product> query = _context.Set<Product>().AsQueryable();
            return query;

        }
    }
}
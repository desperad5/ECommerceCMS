﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Models
{
    public class ProductsByCategoryModel
    {
        [JsonProperty("products")]
        public ProductsWithCategoryModel Products { get; set; }
        
        [JsonProperty("brands")]
        public List<BrandViewModel> Brands { get; set; }
    }
}

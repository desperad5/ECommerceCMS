using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Data.Repositories
{
    public class ProductCategoryRepository : EntityBaseRepository<ProductCategory>, IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public List<ProductCategoryTreeModel> GetProductCategoryTree(int tenantId)
        {
            var model = new List<ProductCategoryTreeModel>();
            //path: '/home/left-sidebar/collection/all', title: 'makeup', type: 'link'
            AsQueryable().Include(c => c.ChildCategories).Load();
            var categories = FindBy(t => t.TenantId==tenantId &&!t.IsDeleted && t.IsActive);

            if (categories != null && categories.Count() > 0)
            {

                var parentCategories = categories.Where(t => !t.ParentCategoryId.HasValue).ToList();
                foreach (var parentCategory in parentCategories)
                {
                    var parentCategoryModel = new ProductCategoryTreeModel() { Id = parentCategory.Id, CategoryName = parentCategory.CategoryName };
                    model.Add(parentCategoryModel);
                    if (parentCategory.ChildCategories != null && parentCategory.ChildCategories.Count > 0)
                    {
                        parentCategoryModel.ChildCategories = new List<ProductCategoryTreeModel>();
                        foreach (var childCategory in parentCategory.ChildCategories)
                        {
                            var childCategoryModel = new ProductCategoryTreeModel() { Id = childCategory.Id, CategoryName = childCategory.CategoryName };
                            parentCategoryModel.ChildCategories.Add(childCategoryModel);
                            if (childCategory.ChildCategories != null && childCategory.ChildCategories.Count > 0)
                            {
                                childCategoryModel.ChildCategories = new List<ProductCategoryTreeModel>();
                                foreach (var childsChildCategory in childCategory.ChildCategories)
                                {
                                    var childschildCategoryModel = new ProductCategoryTreeModel() { Id = childsChildCategory.Id, CategoryName = childsChildCategory.CategoryName };
                                    childCategoryModel.ChildCategories.Add(childschildCategoryModel);
                                }
                            }
                        }
                    }
                }
            }
            return model;

        }
        //public List<ProductViewModel> GetProductsByCategoryId(int ProductCategoryId)
        //{

        //}
    }

}

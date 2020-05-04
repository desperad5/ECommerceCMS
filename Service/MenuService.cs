using AutoMapper;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceCMS.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(ListingService));
        public MenuService()
        {

        }
        public Menu GetMenuById(int menuId)
        {
             return _menuRepository.FindBy(l => l.Id == menuId && !l.IsDeleted).FirstOrDefault();
            
        }
    }
}

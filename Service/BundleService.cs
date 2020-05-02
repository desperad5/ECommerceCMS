using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Helpers;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using ECommerceCMS.Services;

namespace ECommerceCMS.Service
{
    public class BundleService : IBundleService
    {
        private readonly IBundleRepository _bundleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(BundleService));

        public BundleService(IBundleRepository bundleRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _bundleRepository = bundleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        //Calling from Controller
       
    }
}
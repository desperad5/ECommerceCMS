using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using log4net;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Helpers;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;

namespace SorubankCMS.Service
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
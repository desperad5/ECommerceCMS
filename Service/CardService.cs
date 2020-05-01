using AutoMapper;
using SorubankCMS.Data.Abstract;
using SorubankCMS.Data.Entity;
using SorubankCMS.Helpers;
using SorubankCMS.Models;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using SorubankCMS.Models.ElasticSearch;
using SorubankCMS.Data.Repositories;
using System.Collections;
using log4net;

namespace SorubankCMS.Service
{
    public class CardService : ICardService
    {
        
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IElasticClient _elasticClient;
        private readonly ITenantRepository _tenantRepository;
        private static readonly ILog logger = Logger.GetLogger(typeof(CardService));
        public CardService(IProductRepository productRepository, 
                             IMapper mapper, IElasticClient elasticClient,
                              ITenantRepository tenantRepository)
        {
            
            _productRepository = productRepository;
            _mapper = mapper;
            _elasticClient = elasticClient;
            
        }
       

    }
}
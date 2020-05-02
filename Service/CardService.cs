using AutoMapper;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Helpers;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using ECommerceCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using ECommerceCMS.Models.ElasticSearch;
using ECommerceCMS.Data.Repositories;
using System.Collections;
using log4net;

namespace ECommerceCMS.Service
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
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Models.ElasticSearch;

public static class ElasticsearchExtensions
{
    public static void AddElasticsearch(
        this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["elasticsearch:url"];
        var defaultIndex = configuration["elasticsearch:index"];

        var settings = new ConnectionSettings(new Uri(url))
            .DefaultIndex(defaultIndex)
            .DefaultMappingFor<ProductELModel>(m => m
                .PropertyName(p => p.Description, "Description")
                
            );
            

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);
    }
}
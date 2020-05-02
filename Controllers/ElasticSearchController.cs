using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using ECommerceCMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using ECommerceCMS.Models.ElasticSearch;

namespace ECommerceCMS.Controllers
{
    /*
        [EnableCors("EnableCors")]
        [Authorize]
    */

    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class ElasticSearchController : Controller
    {

        private readonly IElasticClient _elasticClient;
        public ElasticSearchController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Find([FromBody] ElasticSearchViewModel model)
        {

            if (model != null && !String.IsNullOrEmpty(model.Query))
            {
                var query = model.Query;

                var response = await _elasticClient.SearchAsync<ProductELModel>(
                                s => s.Query(q => q.QueryString(d => d.Query(query)))
                                .From((model.Page - 1) * model.PageSize)
                            .Size(model.PageSize));

                if (!response.IsValid)
                {
                    // We could handle errors here by checking response.OriginalException or response.ServerError properties
                    return BadRequest("Internal error!");
                }

                return Ok(response.Documents);
            }
            else
            {
                return BadRequest("Empty query string is not allowed!");
            }
        }

    }
}
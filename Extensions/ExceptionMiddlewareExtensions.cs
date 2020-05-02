﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ECommerceCMS.Services;
using ECommerceCMS;

namespace ECommerceCMS.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Logger.Error($"Something went wrong: {contextFeature.Error}");
                        ServiceResult<string> result = new ServiceResult<string>();
                        result.resultType = ServiceResultType.Fail;
                        result.message = "Internal Server Error";                        
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
                    }
                });
            });
        }
    }
}

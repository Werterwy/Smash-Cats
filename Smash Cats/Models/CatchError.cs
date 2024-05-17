﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Smash_Cats.Models
{
    public class CatchError : Attribute, IExceptionFilter
    {

        private readonly ILogger<CatchError> _logger;

        public CatchError(ILogger<CatchError> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.Message);
            context.Result = new ViewResult
            {
                ViewName = "Error"
            };

        }
    }
}

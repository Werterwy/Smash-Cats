using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Smash_Cats.Models
{
    public class IEFillerAttribute : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();

            if (Regex.IsMatch(userAgent, "Mothilla"))
            {
                context.Result = new ContentResult
                {
                    Content = "Ваш браузер не поддерживает"
                };
            }
        }
    }
}

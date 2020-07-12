using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Web.Framework.Mvc.Results.Wrapping
{
    public class AppActionResultWrapperFactory : IAppActionResultWrapperFactory
    {
        public IAppActionResultWrapper CreateFor(FilterContext context)
        {
            switch (context)
            {
                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is ObjectResult:
                    return new AppObjectActionResultWrapper();

                case ResultExecutingContext resultExecutingContext when resultExecutingContext.Result is JsonResult:
                    return new AppJsonActionResultWrapper();

                default:
                    return new NullAppActionResultWrapper();
            }
        }
    }
}

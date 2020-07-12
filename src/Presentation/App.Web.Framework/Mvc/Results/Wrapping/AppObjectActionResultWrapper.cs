using App.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Web.Framework.Mvc.Results.Wrapping
{
    public class AppObjectActionResultWrapper : IAppActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {
            ObjectResult objectResult = null;

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    objectResult = resultExecutingContext.Result as ObjectResult;
                    break;

            }

            if (objectResult == null)
            {
                throw new ArgumentException("Action Result should be JsonResult!");
            }

            if (!(objectResult.Value is JsonResponseBase))
            {
                objectResult.Value = new JsonResponse(objectResult.Value);
                objectResult.DeclaredType = typeof(JsonResponse);
            }
        }
    }
}

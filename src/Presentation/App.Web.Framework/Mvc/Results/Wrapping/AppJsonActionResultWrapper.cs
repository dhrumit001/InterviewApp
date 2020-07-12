using App.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace App.Web.Framework.Mvc.Results.Wrapping
{
    public class AppJsonActionResultWrapper : IAppActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {
            JsonResult jsonResult = null;

            switch (context)
            {
                case ResultExecutingContext resultExecutingContext:
                    jsonResult = resultExecutingContext.Result as JsonResult;
                    break;

            }

            if (jsonResult == null)
            {
                throw new ArgumentException("Action Result should be JsonResult!");
            }

            if (!(jsonResult.Value is JsonResponseBase))
            {
                jsonResult.Value = new JsonResponse(jsonResult.Value);
            }
        }
    }
}

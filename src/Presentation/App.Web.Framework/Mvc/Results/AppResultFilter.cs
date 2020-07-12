using App.Web.Framework.Models;
using App.Web.Framework.Mvc.Results.Wrapping;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Reflection;

namespace App.Web.Framework.Mvc.Results
{
    public class AppResultFilter : IResultFilter
    {
        private readonly IAppActionResultWrapperFactory _actionResultWrapperFactory;

        public AppResultFilter(IAppActionResultWrapperFactory actionResultWrapper)
        {
            _actionResultWrapperFactory = actionResultWrapper;
        }

        public virtual void OnResultExecuting(ResultExecutingContext context)
        {

            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor == null)
            {
                return;
            }

            var methodInfo = controllerActionDescriptor.MethodInfo;

            var wrapResultAttribute =
                 methodInfo.GetCustomAttributes(true).OfType<WrapResultAttribute>().FirstOrDefault()
                   ?? methodInfo.ReflectedType?.GetTypeInfo().GetCustomAttributes(true).OfType<WrapResultAttribute>().FirstOrDefault()
                   ?? new WrapResultAttribute();

            if (!wrapResultAttribute.WrapOnSuccess)
            {
                return;
            }

            _actionResultWrapperFactory.CreateFor(context).Wrap(context);
        }

        public virtual void OnResultExecuted(ResultExecutedContext context)
        {
            //no action
        }
    }
}

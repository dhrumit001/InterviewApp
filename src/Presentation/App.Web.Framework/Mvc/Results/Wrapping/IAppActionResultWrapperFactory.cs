using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Web.Framework.Mvc.Results.Wrapping
{
    public interface IAppActionResultWrapperFactory 
    {
        IAppActionResultWrapper CreateFor(FilterContext context);
    }
}

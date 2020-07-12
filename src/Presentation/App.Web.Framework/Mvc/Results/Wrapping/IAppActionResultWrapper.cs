using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Web.Framework.Mvc.Results.Wrapping
{
    public interface IAppActionResultWrapper
    {
        void Wrap(FilterContext context);
    }
}

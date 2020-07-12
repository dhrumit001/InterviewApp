using Microsoft.AspNetCore.Mvc.Filters;

namespace App.Web.Framework.Mvc.Results.Wrapping
{
    public class NullAppActionResultWrapper : IAppActionResultWrapper
    {
        public void Wrap(FilterContext context)
        {

        }

    }
}

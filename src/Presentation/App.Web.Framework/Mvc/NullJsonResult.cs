﻿using Microsoft.AspNetCore.Mvc;

namespace App.Web.Framework.Mvc
{
    /// <summary>
    /// Null JSON result
    /// </summary>
    public class NullJsonResult : JsonResult
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public NullJsonResult() : base(null)
        {
        }
    }
}

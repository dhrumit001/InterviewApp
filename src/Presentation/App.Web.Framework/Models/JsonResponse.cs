﻿using System;
using System.Collections.Generic;
using System.Text;

namespace App.Web.Framework.Models
{
    /// <summary>
    /// This class is used to create standard responses for AJAX/remote requests.
    /// </summary>
    [Serializable]
    public class JsonResponse : JsonResponse<object>
    {
        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        public JsonResponse()
        {

        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Success"/> specified.
        /// </summary>
        /// <param name="success">Indicates success status of the result</param>
        public JsonResponse(bool success)
            : base(success)
        {

        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponse{TResult}.Result"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as true.
        /// </summary>
        /// <param name="result">The actual result object</param>
        public JsonResponse(object result)
            : base(result)
        {

        }

        /// <summary>
        /// Creates an <see cref="AjaxResponse"/> object with <see cref="AjaxResponseBase.Error"/> specified.
        /// <see cref="AjaxResponseBase.Success"/> is set as false.
        /// </summary>
        /// <param name="error">Error details</param>
        /// <param name="unAuthorizedRequest">Used to indicate that the current user has no privilege to perform this request</param>
        public JsonResponse(ErrorInfo error, bool unAuthorizedRequest = false)
            : base(error, unAuthorizedRequest)
        {

        }
    }
}
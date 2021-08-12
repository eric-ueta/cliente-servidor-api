using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationServer.Responses
{
    public class ErrorObjectResult : ObjectResult
    {
        #region Constructors

        public ErrorObjectResult() : base(null)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public ErrorObjectResult(object value, int statusCode) : base(value)
        {
            StatusCode = statusCode;
        }

        public ErrorObjectResult(int statusCode) : this(null, statusCode)
        {
        }

        #endregion Constructors
    }
}
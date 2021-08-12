using DonationServer.Responses;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DonationServer.Middlewares
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        #region Methods

        public override void OnException(ExceptionContext context)
        {
            try
            {
                if (context.Exception is not null)
                {
                    var response = new ErrorResponse(context.Exception.Message, 500);

                    context.Result = new ErrorObjectResult(response, 500);
                }

                context.ExceptionHandled = true;
            }
            catch
            {
                context.Result = new ErrorObjectResult();
            }
        }

        #endregion Methods
    }
}
using System;

namespace DonationServer.Responses
{
    public class ErrorResponse
    {
        #region Properties

        public int Codigo { get; set; }
        public string Mensagem { get; set; }

        #endregion Properties

        #region Constructors

        public ErrorResponse(string message, int code)
        {
            this.Mensagem = message;
            this.Codigo = code;
        }

        #endregion Constructors
    }
}
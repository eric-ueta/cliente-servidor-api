using System.Text.Json.Serialization;

namespace DonationServer.Domain
{
    public class Solicitacao
    {
        #region Properties

        public string Data { get; set; }

        public int? DoacaoId { get; set; }

        public int? Id { get; set; }

        public int? ReceptorId { get; set; }

        public bool Status { get; set; }

        [JsonPropertyName("tipo_doacao")]
        public string TipoDoacao { get; set; }

        #endregion Properties
    }
}
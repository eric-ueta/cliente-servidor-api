using System.Text.Json.Serialization;

namespace DonationServer.Domain
{
    public class Doacao
    {
        #region Properties

        public string Data { get; set; }

        public int DoadorId { get; set; }

        public int? Id { get; set; }

        public string Local { get; set; }

        [JsonPropertyName("quantidade_restante")]
        public int? QuantidadeRestante { get; set; }

        [JsonPropertyName("quantidade_total")]
        public int? QuantidadeTotal { get; set; }

        [JsonPropertyName("tipo_doacao")]
        public string TipoDoacao { get; set; }

        #endregion Properties
    }
}
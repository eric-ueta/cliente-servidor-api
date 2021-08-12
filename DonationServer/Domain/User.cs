using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationServer.Domain
{
    public class User
    {
        #region Properties

        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public int Tipo { get; set; }

        [JsonPropertyName("tipo_doacao")]
        public string TipoDoacao { get; set; }

        #endregion Properties
    }
}
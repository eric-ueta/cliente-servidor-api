using DonationServer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationServer.Dtos
{
    public class UserLoginDto
    {
        #region Properties

        public string Cpf { get; set; }
        public string Senha { get; set; }

        #endregion Properties
    }
}
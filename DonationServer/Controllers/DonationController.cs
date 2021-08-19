using DonationServer.Domain;
using DonationServer.Responses;
using DonationServer.Sqlite;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationServer.Controllers
{
    [Route("doacoes")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        #region Fields

        private readonly SQLiteManager _sqliteManager = new();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Adiciona uma nova doacao
        /// </summary>
        /// <param name="donation"> Informações da doacao </param>
        /// <returns> 204 caso criado com sucesso </returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Não foi possível realizar a operação </response>
        /// <response code="500"> Erro desconhecido </response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<Doacao>> Create([FromBody] Doacao donation)
        {
            try
            {
                Doacao created = await _sqliteManager.SaveDonation(donation);

                return created is not null
                    ? Ok(created)
                    : BadRequest(new ErrorResponse("Erro ao criar solicitacao", 400));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{doacaoId}")]
        public async Task<ActionResult<Doacao>> Delete([FromBody] int doacaoId)
        {
            try
            {
                bool deleted = await _sqliteManager.DeleteDonation(doacaoId);

                return deleted
                    ? NoContent()
                    : BadRequest(new ErrorResponse("Erro ao editar a doacao", 400));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{doacaoId}")]
        public async Task<ActionResult<Doacao>> Get(int doacaoId)
        {
            try
            {
                Doacao donation = await _sqliteManager.GetDonationById(doacaoId);

                return donation is not null
                    ? Ok(donation)
                    : NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca a lista de doacoes
        /// </summary>
        /// <returns> Lista de doacoes </returns>
        /// <response code="200"> Lista de solicitacoes </response>
        /// <response code="204"> Nenhum usuario encontrado </response>
        /// <response code="500"> Erro desconhecido </response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet("/usuarios/{userId}/doacoes")]
        public async Task<ActionResult<IEnumerable<Doacao>>> GetAllByUser(int userId)
        {
            try
            {
                IEnumerable<Doacao> donations = await _sqliteManager.GetAllDonations(userId);

                return donations?.Count() > 0
                    ? Ok(donations)
                    : NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<ActionResult<Doacao>> Put([FromBody] Doacao donation)
        {
            try
            {
                Doacao updated = await _sqliteManager.UpdateDonation(donation);

                return updated is not null
                    ? Ok(updated)
                    : BadRequest(new ErrorResponse("Erro ao editar a doacao", 400));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Methods
    }
}
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
    [Route("solicitacoes")]
    [ApiController]
    public class SolicitationController : ControllerBase
    {
        #region Fields

        private readonly SQLiteManager _sqliteManager = new();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Adiciona uma nova solicitação
        /// </summary>
        /// <param name="solicitation"> Informações da solicitação </param>
        /// <returns> 204 caso criado com sucesso </returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Não foi possível realizar a operação </response>
        /// <response code="500"> Erro desconhecido </response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<Solicitacao>> Create([FromBody] Solicitacao solicitation)
        {
            try
            {
                Solicitacao created = await _sqliteManager.SaveSolicitation(solicitation);

                return created is not null
                    ? Ok(created)
                    : BadRequest(new ErrorResponse("Erro ao criar solicitacao", 400));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{solicitacaoId}")]
        public async Task<ActionResult<Solicitacao>> Delete(int solicitacaoId)
        {
            try
            {
                bool deleted = await _sqliteManager.DeleteSolicitation(solicitacaoId);

                return deleted
                    ? NoContent()
                    : BadRequest(new ErrorResponse("Erro ao editar solicitacao", 400));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{solicitacaoId}")]
        public async Task<ActionResult<Solicitacao>> Get(int solicitacaoId)
        {
            try
            {
                Solicitacao solicitation = await _sqliteManager.GetSolicitationById(solicitacaoId);

                return solicitation is not null
                    ? Ok(solicitation)
                    : NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca a lista de solicitacoes
        /// </summary>
        /// <returns> Lista de solicitacoes </returns>
        /// <response code="200"> Lista de solicitacoes </response>
        /// <response code="204"> Nenhum usuario encontrado </response>
        /// <response code="500"> Erro desconhecido </response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet("/usuarios/{userId}/solicitacoes")]
        public async Task<ActionResult<IEnumerable<Solicitacao>>> GetAllByUser(int userId)
        {
            try
            {
                IEnumerable<Solicitacao> solicitations = await _sqliteManager.GetAllSolcitations(userId);

                return solicitations?.Count() > 0
                    ? Ok(solicitations)
                    : NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public async Task<ActionResult<Solicitacao>> Put([FromBody] Solicitacao solicitation)
        {
            try
            {
                Solicitacao updated = await _sqliteManager.UpdateSolicitation(solicitation);

                return updated is not null
                    ? Ok(updated)
                    : BadRequest(new ErrorResponse("Erro ao editar solicitacao", 400));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Methods
    }
}
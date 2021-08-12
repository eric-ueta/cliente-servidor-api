using DonationServer.Domain;
using DonationServer.Responses;
using DonationServer.Sqlite;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonationServer.Controllers
{
    [Route("usuarios")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Fields

        private readonly SQLiteManager _sqliteManager = new();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Adiciona u novo usuário
        /// </summary>
        /// <param name="user"> Informações do usuario </param>
        /// <returns> 204 caso criado com sucesso </returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="400"> Não foi possível realizar a operação </response>
        /// <response code="500"> Erro desconhecido </response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            try
            {
                User createdUser = await _sqliteManager.SaveUser(user);

                return createdUser is not null
                    ? Ok(createdUser)
                    : BadRequest(new ErrorResponse("Erro ao criar usuario", 400));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Busca a lista de usuarios
        /// </summary>
        /// <returns> Lista de usuarios </returns>
        /// <response code="200"> Lista de usuarios </response>
        /// <response code="204"> Nenhum usuario encontrado </response>
        /// <response code="500"> Erro desconhecido </response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                var users = await _sqliteManager.GetAllUser();

                return users?.Count() > 0
                    ? Ok(User)
                    : NoContent();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion Methods
    }
}
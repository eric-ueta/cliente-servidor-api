using DonationServer.Domain;
using DonationServer.Dtos;
using DonationServer.Factories;
using DonationServer.Middlewares;
using DonationServer.Responses;
using DonationServer.Sqlite;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DonationServer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class AuthController : ControllerBase
    {
        #region Fields

        private readonly SQLiteManager _sqliteManager = new();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Realiza o login no sistema
        /// </summary>
        /// <param name="unknowUser"></param>
        /// <returns>Usuario e token</returns>
        /// <response code="200">Usuário e token</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="500">Erro desconhecido</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserLoginDto unknowUser)
        {
            try
            {
                // Recupera o usuário
                User user = await _sqliteManager.GetUserByDocAndPass(unknowUser.Cpf, unknowUser.Senha);

                // Verifica se o usuário existe
                if (user is null)
                    return NotFound(new ErrorResponse("Verify your user and password!", 404));

                // Gera o Token
                var token = TokenFactory.GenerateToken(user);

                // Oculta a senha
                user.Senha = null;

                AuthManager.LoggedUsers.AddOrUpdate(user.Id.Value, (us) => token, (k, v1) => { return token; });

                // Retorna os dados
                return Ok(new UserToken
                {
                    Usuario = user,
                    Token = token
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Realiza o logout do sistema
        /// </summary>
        /// <returns>204</returns>
        /// <response code="204">usuário deslogado com sucesso</response>
        [ProducesResponseType(204)]
        [Auth]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Request.HttpContext.User = null;

            return NoContent();
        }

        #endregion Methods
    }

    public class UserToken
    {
        #region Properties

        public string Token { get; set; }
        public User Usuario { get; set; }

        #endregion Properties
    }
}
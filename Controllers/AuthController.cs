using Microsoft.AspNetCore.Mvc;
using Projeto_SIT.Models;
using Projeto_SIT.Services;

namespace Projeto_SIT.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly AuthService _authService;
        public AuthController(AuthService authService) {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult login(UserLogin1 login) {
            var user = _authService.Login(login);
            if (user == null) {
                return Unauthorized(new { message = "Credenciais inválidas." });
            }
            return Ok(new {
                idUser = user.IdUser,
                userName = user.UserName,
                email = user.Email,
                telefone = user.Telefone,
                token = user.Token
            });
        }
    }
}

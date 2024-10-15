using Microsoft.AspNetCore.Mvc;
using Projeto_SIT.Models;
using Projeto_SIT.Services;

namespace Projeto_SIT.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly UserService _userService;
        public UserController(UserService userService) {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public ActionResult get() {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        [HttpGet("GetUserEquip")]
        public ActionResult getequip() { 
            var userEquip = _userService.GetUsersEquipamentos();
            return Ok(userEquip);
        }

        [HttpGet("GetUserCargo")]
        public ActionResult GetUserCargo(int id) { 
            var command = _userService.GetUserCargo(id);
            return Ok(command);
        }

        [HttpGet("{idUser}")]
        public ActionResult getId(int idUser) {
            if (idUser == 0) {
                return BadRequest(new { message = "Usuário não encontrado!" });
            }
            var Users = _userService.GetUserId(idUser);
            if (Users == null) {
                return NotFound(new { message = "Usuário não encontrado!" });
            }
            return Ok(Users);
        }

        [HttpPost]
        public ActionResult post(PostUser idUser) {
            try {
                var success = _userService.Registrar(idUser);
                if (success) {
                    return Ok(new { message = "Usuário registrado com sucesso!" });
                } else {
                    return BadRequest(new { message = "Falha ao registrar o usuário" });
                }
            } catch (Exception ex) {
                if (ex.Message == "Esse usuário já existe!") {
                    return Conflict(new { message = ex.Message }); // Retorna 409 Conflict com a mensagem
                }
                return StatusCode(500, new { message = "Erro interno no servidor" }); // Para outros erros
            }
        }

        [HttpPut("{id}")]
        public IActionResult put(int id, UserUpdate user) {
            if (user == null || id != user.IdUser) {
                return BadRequest("Dados incorretos.");
            }
            try {
                _userService.UpdateUser(id, user.UserName, user.Email, user.Telefone, user.Password, user.Cidade, user.IdFilial);
                return Ok(new { message = "Usuário atualizada com sucesso!" });
            } catch (Exception ex) {
                return StatusCode(500, "Erro interno ao atualizar o usuário.");
            }
        }

        [HttpDelete("{idUser}")]
        public IActionResult delete(int idUser) {
            var delete = _userService.Delete(idUser);

            if (delete) {
                return Ok(new { message = "Usuário excluido com sucesso!" });
            }
            return NotFound(new { message = "Usuário não encontrado!" });
        }
    }
}

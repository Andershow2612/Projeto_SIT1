using Microsoft.AspNetCore.Mvc;
using Projeto_SIT.Models;
using Projeto_SIT.Services;

namespace Projeto_SIT.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class CargosController : ControllerBase {
        private readonly CargosService _cargosService;
        public CargosController(CargosService cargosService) {
            _cargosService = cargosService;
        }

        [HttpGet]
        public IActionResult get() { 
            var command = _cargosService.GetAll();
            return Ok(command);
        }

        [HttpGet("cargo/{nomeCargo}")]
        public IActionResult GetUserByCargo(string nomeCargo) {
            if ( string.IsNullOrEmpty(nomeCargo) || nomeCargo.All(char.IsDigit)) {
                return BadRequest("O nome do cargo é inválido. Apenas letras são aceitas.");
            }
            var command = _cargosService.GetCargoUser(nomeCargo);

            //implementação de operador ternário.(if bonito)
            return command == null || command.Count == 0
                ? NotFound() 
                : Ok(command);
        }

        [HttpPost]
        public IActionResult post(PostCargos id) {
            var command = _cargosService.register(id);
            if (command) {
                return Ok(new { message = "Cargo adicionado com sucesso!" });
            }
            return BadRequest(new { message = "Não foi possivel adicionar" });
        }

        [HttpDelete("{id}")]
        public IActionResult delete(int id) { 
            if(id == null || id <= 0) {
                return BadRequest(new { message = "Erro ao excluir cargo." });
            }
            var command = _cargosService.Delete(id);
            return Ok($"Cargo excluido com sucesso!");
        }
    }
}

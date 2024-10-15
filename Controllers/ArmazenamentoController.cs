using Microsoft.AspNetCore.Mvc;
using Projeto_SIT.Models;
using Projeto_SIT.Services;

namespace Projeto_SIT.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class ArmazenamentoController : ControllerBase {
        private readonly ArmazenamentoService _armazenamentoService;

        public ArmazenamentoController(ArmazenamentoService armazenamentoService) {
            _armazenamentoService = armazenamentoService;
        }

        [HttpGet("GetAll")]
        public IActionResult Index() { 
            var lista = _armazenamentoService.getArmazenamento();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            if (id == 0) {
                return BadRequest(new { message = "Erro ao encontrar este armazenamento!" });
            }

            var armz = _armazenamentoService.getId(id);
           
            if (armz == null) { 
                return NotFound(new { message = "Armazenamento não encontrado!" });
            }
            return Ok(armz);
        }

        [HttpPost]
        public IActionResult post(PostArmazenamento equip) {
            var post = _armazenamentoService.Registrar(equip);

            if (!post) {
                return BadRequest(new { message = "Erro ao criar registro!" });
            }
            return Ok(new { message = $"Registro {equip.NomeArmazenamento} criado com sucesso!" });
        }

        [HttpPut("{id}")]
        public IActionResult put(int id, UpdateArmz armz) {
            if (armz == null || id != armz.IdArmazenamento) {
                return BadRequest("Dados incorretos.");
            }
            _armazenamentoService.UpdateArmz(id, armz.NomeArmazenamento, armz.Descricao, armz.CapacidadeMaxima, armz.IdFilial);
            return Ok(new { message = "Atualização feita com sucesso!" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) { 
            var delete = _armazenamentoService.DeleteArmz(id);

            if (!delete) { 
                return BadRequest(new { message = "Erro ao excluir!" });
            }
            return Ok(new { message = "Excluido com sucesso!" });
        }
    }
}

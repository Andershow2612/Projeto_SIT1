using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_SIT.Models;
using Projeto_SIT.Services;

namespace Projeto_SIT.Controllers {
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class EquipamentoController : ControllerBase {
        private readonly EquipamentoService _equipamentoService;
        public EquipamentoController(EquipamentoService equipamentoService) {
            _equipamentoService = equipamentoService;
        }

        [HttpGet("GetAll")]
        public IActionResult Get() {
            var command = _equipamentoService.GetEquip();
            return Ok(command);
        }

        [HttpGet("{id}")]
        public IActionResult GetEquipbyId(int id) {
            if (id == 0 || id == null) { 
                return BadRequest();
            }
            var command = _equipamentoService.getById(id);
            return Ok(command);

        }

        [HttpPost]
        public IActionResult Post(PostEquipamento equip) {
            var command = _equipamentoService.RegistarEquip(equip);
            if (command) { 
                return Ok(new { message = $"{equip.Tipo} adicionado com sucesso!" });
            }
            return BadRequest(new { message = "Não foi possivel adicionar" });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateEquip equip) {
            if (equip == null || !ModelState.IsValid) { 
                return NotFound(new { message = "Não equipamento não encontrado" });
            }
            _equipamentoService.UpdateEquip(id, equip.NumeroDeSerie, equip.Modelo, equip.Marca, equip.Tipo, equip.Cor, equip.idArmazenamento, equip.idFilial);
            return Ok(new { message = "Atualizado com sucesso!"});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            if (id <= 0) {
                return BadRequest(new { message = "ID inválido!" });
            }
            //acessa o serviço de get por id, trazendo várias informações sobre o equipamento que esta sendo excluido!
            var equipamento = _equipamentoService.getById(id);

            if (equipamento == null) {
                return NotFound(new { message = "Equipamento não encontrado!" });
            }

            _equipamentoService.deleteEquip(id);

            // Retorna o nome do equipamento excluído na mensagem
            return Ok(new { message = $"Equipamento '{equipamento.Modelo}' excluído com sucesso!" });
        }
    }
}

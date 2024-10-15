using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_SIT.Models;
using Projeto_SIT.Services;

namespace Projeto_SIT.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class FiliaisController : ControllerBase {

        private readonly FiliaisService _filiaisService;
        public FiliaisController(FiliaisService filiaisService) {
            _filiaisService = filiaisService;
        }

        [HttpGet("GetAll")]
        public IActionResult Get() { 
            var lista = _filiaisService.getAll();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id) { 
            if (id == 0) {
                return BadRequest(new { message = "Filial não encontrada!" });
            }
            var filial = _filiaisService.getId(id);
            if (filial == null) { 
                return NotFound(new { message = "Filial não encontrada!" });
            }
            return Ok(filial);
        }

        [HttpPost]
        public IActionResult Post(PostFiliais filiais) { 
            var registro = _filiaisService.registar(filiais);
            if (registro) {
                return Ok(new { message = $"Filial {filiais.NomeFilial} registrada com sucesso!"});
            }
            return BadRequest(new { message = "Erro ao registrar filial" });
        }
         
        [HttpPut("{id}")]
        public IActionResult put(int id, [FromBody] Filiais filial) {
            if (filial == null || id != filial.IdFilial) {
                return BadRequest("Dados incorretos.");
            }
            try {
                _filiaisService.UpdateFilial(id, filial.NomeFilial, filial.UF, filial.Cidade, filial.Rua, filial.Numero);
                return Ok(new { message = "Filial atualizada com sucesso!" });
            } catch (Exception ex) {
                return StatusCode(500, "Erro interno ao atualizar a filial.");
            }
        }

        [HttpDelete("{idFilial}")]
        public IActionResult Delete(int idFilial) { 
            var delete = _filiaisService.deletar(idFilial);

            if (delete) {
                return Ok(new { message = "Filial excluido com sucesso!" });
            }
            return NotFound(new { message = "Filial não encontrado!" });
        }
    }
}

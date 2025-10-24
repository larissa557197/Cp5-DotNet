using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using VisionHive.Application.DTO.Request;
using VisionHive.Domain.Entities;
using VisionHive.Infrastructure.Repositories.Mongo;

namespace VisionHive.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/patios")]
    [ApiController]
    [ApiVersion(2.0)]
    public class PatioControllerV2 : ControllerBase
    {
        private readonly PatioMongoRepository _repository;

        public PatioControllerV2(PatioMongoRepository repository)
        {
            _repository = repository;
        }
        
        // método pra criar um pátio - POST
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PatioRequest request)
        {
            var patio = new Patio
            {
                Nome = request.Nome,
                LimiteMotos = request.LimiteMotos,
                FilialId = request.FilialId
            };

            await _repository.CreateAsync(patio);
            
            return Ok(new
            {
                Mensagem = "Pátio Inserido com sucesso (MongoDB)",
                Patio = patio
            });
        }

        // GET - Lista todos os pátios
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patios = await _repository.GetAllAsync();
            return Ok(patios);
        }
        
        // GET - Buscar por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var patio = await _repository.GetByIdAsync(id);
            if (patio == null)
                return NotFound(new { Mensagem = $"Pátio com ID {id} não encontrado." });

            return Ok(patio);
        }
        
        // UPDATE - Atualiza 
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] PatioRequest request)
        {
            var patioExitente = await _repository.GetByIdAsync(id);
            if(patioExitente == null)
                return NotFound(new { Mensagem = "Pátio não encontrado."});
            
            patioExitente.Nome = request.Nome;
            patioExitente.LimiteMotos = request.LimiteMotos;
            patioExitente.FilialId = request.FilialId;
            
            var atualizado = await _repository.UpdateAsync(patioExitente);
            if (!atualizado)
                return BadRequest(new { Mensagem = "Falha ao atualizar pátio" });
            
            return Ok(new
            {
                Mensagem = "Pátio atualizado com sucesso!",
                Patio = patioExitente
            });
        }
        
        // DELETE
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletado = await _repository.DeleteAsync(id);
            if (!deletado)
                return NotFound(new { Mensagem = "Pátio não encontrado" });

            return Ok(new
            {
                Mensagem = "Pátio excluído com sucesso!"
            });
        }
    }
    
    
}


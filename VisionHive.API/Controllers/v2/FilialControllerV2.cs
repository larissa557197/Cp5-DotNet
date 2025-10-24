
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using VisionHive.Application.DTO.Request;
using VisionHive.Domain.Entities;
using VisionHive.Infrastructure.Repositories.Mongo;

namespace VisionHive.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/filiais")]
    [ApiController]
    [ApiVersion(2.0)]
    public class FilialControllerV2: ControllerBase
    {
        private readonly FilialMongoRepository _repository;

        public FilialControllerV2(FilialMongoRepository repository)
        {
            _repository = repository;
        }
        
        // POST - Criar uma filial
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FilialRequest request)
        {
            var filial = new Filial
            {
                Nome = request.Nome,
                Bairro = request.Bairro,
                Cnpj = request.Cnpj
            };

            await _repository.CreateAsync(filial);
            
            return Ok(new
            {
                Mensagem = "Filial inserida com sucesso (MongoDB)",
                    Filial = filial
            });
        }

        // GET - Listar todas as filiais
        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var filiais = await _repository.GetAllAsync();
            return Ok(filiais);
        }
        
        // GET - Buscar por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var filial = await _repository.GetByIdAsync(id);

            if (filial == null)
                return NotFound(new { Mensagem = $"Filial com ID {id} não encontrada." });

            return Ok(filial);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] FilialRequest request)
        {
            var filialExistente = await _repository.GetByIdAsync(id);
            if (filialExistente == null)
                return NotFound(new { Mensagem = "Filial não encontrada." });

            filialExistente.Nome = request.Nome;
            filialExistente.Bairro = request.Bairro;
            filialExistente.Cnpj = request.Cnpj;

            var atualizado = await _repository.UpdateAsync(filialExistente);
            if (!atualizado)
                return BadRequest(new { Mensagem = "Falha ao atualizar filial." });

            return Ok(new
            {
                Mensagem = "Filial atualizada com sucesso!",
                Filial = filialExistente
            });
        }

        // DELETE - Remover uma filial
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletado = await _repository.DeleteAsync(id);
            if (!deletado)
                return NotFound(new { Mensagem = "Filial não encontrada." });

            return Ok(new { Mensagem = "Filial excluída com sucesso!" });
        }
        

    }
}



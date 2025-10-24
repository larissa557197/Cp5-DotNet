using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using VisionHive.Application.DTO.Request;
using VisionHive.Domain.Entities;
using VisionHive.Infrastructure.Repositories.Mongo;

namespace VisionHive.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/motos")]
    [ApiController]
    [ApiVersion(2.0)]
    public class MotoControllerV2 : ControllerBase
    {
        private readonly MotoMongoRepository _repository;

        public MotoControllerV2(MotoMongoRepository repository)
        {
            _repository = repository;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotoRequest request)
        {
            var moto = new Moto()
            {
                Placa = request.Placa,
                Chassi = request.Chassi,
                NumeroMotor = request.NumeroMotor,
                Prioridade = request.Prioridade,
                PatioId = request.PatioId
            };

            await _repository.CreateAsync(moto);
            
            return Ok(new
            {
                Mensagem = "Moto cadastrado com sucesso (MongoDB)!",
                Moto = moto
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var motos = await _repository.GetAllAsync();
           return Ok(motos);
        }
        
        // GET - Busca por ID
        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById(Guid id)
        {
            var moto = await _repository.GetByIdAsync(id);
            if (moto == null)
                return NotFound(new { Mensagem = $"Moto com ID {id} não encontrado." });

            return Ok(moto);
        }
        
        // UPDATE
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MotoRequest request)
        {
            var motoExitente = await _repository.GetByIdAsync(id);
            if(motoExitente == null)
                return NotFound(new { Mensagem = "Moto não encontrada."});
            
            motoExitente.Placa = request.Placa;
            motoExitente.Chassi = request.Chassi;
            motoExitente.NumeroMotor = request.NumeroMotor;
            motoExitente.Prioridade = request.Prioridade;
            motoExitente.PatioId = request.PatioId;
            
            var atualizado = await _repository.UpdateAsync(motoExitente);
            if (!atualizado)
                return BadRequest(new { Mensagem = "Falha ao atualizar moto" });
            
            return Ok(new
            {
                Mensagem = "Moto atualizada com sucesso!",
                Moto = motoExitente
            });
        }
        
        // DELETE
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletado = await _repository.DeleteAsync(id);
            if (!deletado)
                return NotFound(new { Mensagem = "Moto não encontrada" });

            return Ok(new
            {
                Mensagem = "Moto excluída com sucesso!"
            });
        }
    
    }
}
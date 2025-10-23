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
    
    }
}
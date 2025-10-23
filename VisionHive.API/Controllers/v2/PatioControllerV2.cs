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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var patios = await _repository.GetAllAsync();
            return Ok(patios);
        }
    }
}


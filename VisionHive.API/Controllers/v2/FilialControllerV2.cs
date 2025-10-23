
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
        
        // refazer
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

        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var filiais = await _repository.GetAllAsync();
            return Ok(filiais);
        }

    }
}



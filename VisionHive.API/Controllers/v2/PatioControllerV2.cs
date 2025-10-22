using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using VisionHive.Application.DTO.Request;
using VisionHive.Domain.Entities;

namespace VisionHive.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/patios")]
    [ApiController]
    [ApiVersion(2.0)]
    public class PatioControllerV2 : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] PatioRequest request)
        {
            Console.WriteLine("Versão da API: 2.0 - MongoDB");
            Console.WriteLine($"Placa Recebida:  {request.Nome}");
            return Ok(new { Mensagem = "Pátio inserido com sucesso (MongoDB)", request });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Console.WriteLine("Versão da API: 2.0 - MongoDB");
            return Ok(new { Mensagem = "Listando pátios do MongoDB" });
        }
    }
}


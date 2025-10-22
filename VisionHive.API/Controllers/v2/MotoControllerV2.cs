using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using VisionHive.Application.DTO.Request;
using VisionHive.Domain.Entities;

namespace VisionHive.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/motos")]
    [ApiController]
    [ApiVersion(2.0)]
    public class MotoControllerV2 : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] MotoRequest request)
        {
            Console.WriteLine("Versão da API: 2.0 - MongoDB");
            Console.WriteLine($"Placa Recebida:  {request.Placa}");
            return Ok(new {Mensagem = "Moto inserida com sucesso (MongoDB)", request});
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Console.WriteLine("Versão da API: 2.0 - MongoDB");
            return Ok(new { Mensagem = "Listanod motos do MongoDB" });
        }
    
    }
}
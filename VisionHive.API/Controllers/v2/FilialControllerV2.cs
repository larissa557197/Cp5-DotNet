
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using VisionHive.Domain.Entities;

namespace VisionHive.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/filiais")]
    [ApiController]
    [ApiVersion(2.0)]
    public class FilialControllerV2: ControllerBase
    {
        
        private readonly IMongoCollection<Filial> _filiais;

        public FilialControllerV2(IMongoDatabase database)
        {
            _filiais = database.GetCollection<Filial>("filiais");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _filiais.Find(_ => true).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var filial = await _filiais.Find(f => f.Id == id).FirstOrDefaultAsync();
            return filial is null ? NotFound() : Ok(filial);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Filial filial)
        {
            filial.Id = Guid.NewGuid();
            await _filiais.InsertOneAsync(filial);
            return CreatedAtAction(nameof(GetById), new { id = filial.Id }, filial);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Filial filial)
        {
            var result = await _filiais.ReplaceOneAsync(f => f.Id == id, filial);
            return result.MatchedCount == 0 ? NotFound() : Ok(filial);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _filiais.DeleteOneAsync(f => f.Id == id);
            return result.DeletedCount == 0 ? NotFound() : NoContent();
        }
    
    }
}



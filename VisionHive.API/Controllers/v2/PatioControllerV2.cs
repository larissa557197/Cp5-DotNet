using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using VisionHive.Domain.Entities;

namespace VisionHive.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/patios")]
    [ApiController]
    [ApiVersion(2.0)]
    public class PatioControllerV2 : ControllerBase
    {
        private readonly IMongoCollection<Patio> _patios;

        public PatioControllerV2(IMongoDatabase database)
        {
            _patios = database.GetCollection<Patio>("patios");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _patios.Find(_ => true).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var patio = await _patios.Find(p => p.Id == id).FirstOrDefaultAsync();
            return patio is null ? NotFound() : Ok(patio);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patio patio)
        {
            patio.Id = Guid.NewGuid();
            await _patios.InsertOneAsync(patio);
            return CreatedAtAction(nameof(GetById), new { id = patio.Id }, patio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Patio patio)
        {
            var result = await _patios.ReplaceOneAsync(p => p.Id == id, patio);
            return result.MatchedCount == 0 ? NotFound() : Ok(patio);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _patios.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount == 0 ? NotFound() : NoContent();
        }
    }
}


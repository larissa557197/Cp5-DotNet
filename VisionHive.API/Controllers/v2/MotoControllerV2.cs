using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using VisionHive.Domain.Entities;

namespace VisionHive.API.Controllers.v2
{
    [Route("api/v{version:apiVersion}/motos")]
    [ApiController]
    [ApiVersion(2.0)]
    public class MotoControllerV2 : ControllerBase
    {
        private readonly IMongoCollection<Moto> _motos;

        public MotoControllerV2(IMongoDatabase database)
        {
            _motos = database.GetCollection<Moto>("motos");
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var list = await _motos.Find(m => true).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var moto = await _motos.Find(m => m.Id == id).FirstOrDefaultAsync();
            return moto is  null ? NotFound() : Ok(moto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Moto moto)
        {
            moto.Id = Guid.NewGuid();
            await _motos.InsertOneAsync(moto);
            return CreatedAtAction(nameof(GetById), new { id = moto.Id }, moto);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Moto moto)
        {
            var result = await _motos.ReplaceOneAsync(m => m.Id == id, moto);
            return result.MatchedCount == 0 ? NotFound() : Ok(moto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _motos.DeleteOneAsync(m => m.Id == id);
            return result.DeletedCount == 0 ? NotFound() : NoContent();
        }
    
    }
}
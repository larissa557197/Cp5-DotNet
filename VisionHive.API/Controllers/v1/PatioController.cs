using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Asp.Versioning;
using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Application.DTO.Validators;
using VisionHive.Application.UseCases;
using VisionHive.Infrastructure.Contexts;

namespace VisionHive.API.Controllers
{
    [Route("api/v{version:apiVersion}/patios")]
    [ApiController]
    [ApiVersion(1.0)]
    //[SwaggerTag("Gerencia os pátios e seus limites de motos.")]
    public class PatioController(IPatioUseCase patioUseCase) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] PatioRequest request)
        {
            var validator = new PatioRequestValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var patio = await patioUseCase.PostAsync(request);
            return StatusCode((int)HttpStatusCode.Created, patio);
        }
        
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPaginate([FromQuery] PaginatedRequest paginatedRequest)
        {
            var validator = new PaginatedRequestValidator();
            var result = await validator.ValidateAsync(paginatedRequest);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var patios = await patioUseCase.GetPaginationAsync(paginatedRequest);
            return Ok(patios);
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var patio = await patioUseCase.GetByIdAsync(id);
            return patio is null ? NotFound() : Ok(patio);
        }
        
        [HttpPut("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromBody] PatioRequest request)
        {
            var validator = new PatioRequestValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var updated = await patioUseCase.UpdateAsync(id, request);
            return updated ? NoContent() : NotFound();
        }
        
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await patioUseCase.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }

    }
}

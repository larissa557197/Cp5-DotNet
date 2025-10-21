using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Asp.Versioning;
using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Application.DTO.Validators;
using VisionHive.Application.UseCases;
using VisionHive.Domain.Pagination;
using VisionHive.Infrastructure.Contexts;

namespace VisionHive.API.Controllers
{
    [Route("api/v{version:apiVersion}/motos")]
    [ApiController]
    [ApiVersion(1.0)]
    //[SwaggerTag("Gerencia as motos registradas no sistema.")]
    public class MotoController(IMotoUseCase motoUseCase) : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromBody] MotoRequest request)
        {
            var validator = new MotoRequestValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var moto = await motoUseCase.PostAsync(request);
            return StatusCode((int)HttpStatusCode.Created, moto);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] PaginatedRequest paginatedRequest)
        {
            var validator = new PaginatedRequestValidator();
            var result = await validator.ValidateAsync(paginatedRequest);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var motos = await motoUseCase.GetPaginationAsync(paginatedRequest);
            return Ok(motos);
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var moto = await motoUseCase.GetByIdAsync(id);
            return moto is null ? NotFound() : Ok(moto);
        }
        
        [HttpPut("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put(Guid id, [FromBody] MotoRequest request)
        {
            var validator = new MotoRequestValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var updated = await motoUseCase.UpdateAsync(id, request);
            return updated ? NoContent() : NotFound();
        }
        
        
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await motoUseCase.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        
    }
}

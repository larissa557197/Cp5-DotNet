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
    [Route("api/v{version:apiVersion}/filiais")]
    [ApiController]
    [ApiVersion(1.0)]
    //[SwaggerTag("Gerencia as filiais cadastradas no sistema.")]
    public class FilialController(IFilialUseCase filialUseCase) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] FilialRequest request)
        {
            var validator = new FilialRequestValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var filial = await filialUseCase.PostAsync(request);
            return StatusCode((int)HttpStatusCode.Created, filial);
        }
        
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPaginate([FromQuery] PaginatedRequest paginatedRequest)
        {
            var validator = new PaginatedRequestValidator();
            var result = await validator.ValidateAsync(paginatedRequest);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var filiais = await filialUseCase.GetPaginationAsync(paginatedRequest);
            return Ok(filiais);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var filial = await filialUseCase.GetByIdAsync(id);
            return filial is null ? NotFound() : Ok(filial);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Put(Guid id, [FromBody] FilialRequest request)
        {
            var validator = new FilialRequestValidator();
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
                return BadRequest(result.Errors);

            var updated = await filialUseCase.UpdateAsync(id, request);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await filialUseCase.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Asp.Versioning;
using VisionHive.Application.DTO.Request;
using VisionHive.Application.DTO.Response;
using VisionHive.Application.UseCases;
using VisionHive.Domain.Pagination;
using VisionHive.Infrastructure.Contexts;

namespace VisionHive.API.Controllers
{
    [Route("api/v{version:apiVersion}/motos")]
    [ApiController]
    [ApiVersion(1.0)]
    // [SwaggerTag("Gerencia o cadastro, consulta, atualização e remoção de motos (v1 - Oracle).")]
    public class MotoController(IMotoUseCase motoUseCase) : ControllerBase
    {
        ///<summary> Lista paginada de motos </summary>
        /// <remarks> validação + retorna a Task diretamente (sem await).</remarks>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<MotoResponse>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetPaginate([FromQuery] PaginatedRequest request)
        {
            
        }
    }
}

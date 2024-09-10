using Application.Files.Commands;
using Application.Files.Dto;
using Application.Files.Queries;
using Common.Models;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prodcut.API;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ApiControllerBase
    {
        // POST: api/Files
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFileCommand command)
        {
            return await Mediator.Send(command);
        }

        // PUT: api/Files/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateFileCommand command)
        {
            return await Mediator.Send(command);
        }

        // DELETE: api/Files/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return await Mediator.Send(command);
        }
        [HttpDelete]
        public async Task<ActionResult<Domain.Entities.File>> Delete(DeleteFileCommand command)
        {
            return await Mediator.Send(command);
        }
        // GET: api/Files
        [HttpGet]
        public async Task<ActionResult<PaginatedList<Domain.Entities.File>>> GetAll([FromQuery] GetFilesByFilterQuery command )
        {
            return await Mediator.Send(command);
        }
    }
}

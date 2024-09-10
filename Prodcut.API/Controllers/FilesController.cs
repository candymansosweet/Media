
using Application.Files.Commands;
using Application.Files.Dto;
using Application.Files.Queries;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Prodcut.API;

namespace Product.API.Controllers
{
    public class FilesController : ApiControllerBase
    {
        // POST: api/Files
        [HttpPost]
        public async Task<ActionResult<FileDto>> Create([FromBody] CreateFileCommand command)
        {
            return await Mediator.Send(command);
        }

        // PUT: api/Files/{id}
        [HttpPut]
        public async Task<ActionResult<FileDto>> Update([FromBody] UpdateFileCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult<FileDto>> Delete(DeleteFileCommand command)
        {
            return await Mediator.Send(command);
        }
        // GET: api/Files
        [HttpGet]
        public async Task<ActionResult<PaginatedList<FileDto>>> GetAll([FromQuery] GetFilesByFilterQuery command )
        {
            return await Mediator.Send(command);
        }
    }
}

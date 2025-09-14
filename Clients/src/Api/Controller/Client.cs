using Microsoft.AspNetCore.Mvc;
using Application.UseCases.Clients;
using Domain.Entities;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController(
        CreateClientHandler createClientHandler,
        GetClientByIdHandler getClientByIdHandler,
        UpdateClientHandler updateClientHandler,
        DeleteClientHandler deleteClientHandler
        ) : ControllerBase
    {
        private readonly CreateClientHandler _createClientHandler = createClientHandler;
        private readonly GetClientByIdHandler _getClientByIdHandler = getClientByIdHandler;
        private readonly UpdateClientHandler _updateClientHandler = updateClientHandler;
        private readonly DeleteClientHandler _deleteClientHandler = deleteClientHandler;

        // CREATE
        [HttpPost]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            var client = _createClientHandler.Handle(request);
            return Ok(client);
        }

        // READ BY ID
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var client = _getClientByIdHandler.Handle(id);
            return Ok(client);
        }

        // READ ALL
        [HttpGet]
        public IActionResult GetAll([FromServices] GetAllClientsHandler getAllClientsHandler)
        {
            var clients = getAllClientsHandler.Handle();
            return Ok(clients);
        }

        // UPDATE
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] UpdateClientRequest request)
        {
            var updatedRequest = request with { Id = id };
            var client = _updateClientHandler.Handle(updatedRequest);
            return Ok(client);
        }

        // DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _deleteClientHandler.Handle(id);
            return NoContent();
        }
    }
}

using Domain.Entities;
using Domain.Repositories;
using System;

namespace Application.UseCases.Clients
{
    public class CreateClientHandler(IClientRepository clientRepository)
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public Client Handle(CreateClientRequest request)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };
            _clientRepository.AddClient(client);
            return client;
        }
    }

    public record CreateClientRequest(string FirstName, string LastName, string Email, string PhoneNumber);


    public class GetClientByIdHandler(IClientRepository clientRepository)
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public Client Handle(Guid id)
        {
            var client = _clientRepository.GetClientById(id) ?? throw new Exception("Client not found");
            return client;
        }
    }

    public class GetAllClientsHandler(IClientRepository clientRepository)
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public IEnumerable<Client> Handle()
        {
            return _clientRepository.GetAllClients();
        }
    }

    public class UpdateClientHandler(IClientRepository clientRepository)
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public Client Handle(UpdateClientRequest request)
        {
            var client = _clientRepository.GetClientById(request.Id) ?? throw new Exception("Client not found");

            client.FirstName = request.FirstName ?? client.FirstName;
            client.LastName = request.LastName ?? client.LastName;
            client.Email = request.Email ?? client.Email;
            client.PhoneNumber = request.PhoneNumber ?? client.PhoneNumber;
            client.UpdatedAt = DateTime.UtcNow;

            _clientRepository.UpdateClient(client);
            return client;
        }
    }

    public record UpdateClientRequest(Guid Id, string? FirstName, string? LastName, string? Email, string? PhoneNumber);

    public class DeleteClientHandler(IClientRepository clientRepository)
    {
        private readonly IClientRepository _clientRepository = clientRepository;

        public void Handle(Guid id)
        {
            var client = _clientRepository.GetClientById(id) ?? throw new Exception("Client not found");
            _clientRepository.DeleteClient(id);
        }
    }

}
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class InMemoryClientRepository : IClientRepository
    {
        private readonly List<Client> _clients = new();

        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public Client GetClientById(Guid id)
        {
            return _clients.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clients;
        }

        public void UpdateClient(Client client)
        {
            // Nada especial, a referência já está na lista
            // Se quisesse, poderia validar se existe antes
        }

        public void DeleteClient(Guid id)
        {
            var client = GetClientById(id);
            if (client != null)
                _clients.Remove(client);
        }
    }
}

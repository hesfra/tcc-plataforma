using Domain.Entities;
using System;
namespace Domain.Repositories
{
    public interface IClientRepository
    {
        void AddClient(Client client);
        Client GetClientById(Guid id);
        IEnumerable<Client> GetAllClients();
        void UpdateClient(Client client);
        void DeleteClient(Guid id);
    }
}
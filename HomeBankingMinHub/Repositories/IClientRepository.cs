
using HomeBankingMindHub.Models;
using System.Collections.Generic;
using HomebankingMindHub.Models;
using System.Linq;

namespace HomeBankingMindHub.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        void Save(Client client);
        Client FindById(long id);
        Client FindByEmail(string email);

    }
}
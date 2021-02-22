using Contacts.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.UI.Data
{
    public interface IContactDataService
    {
        Task<List<Contact>> GetAllAsync();
    }
}

using Contacts.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contacts.UI.Data
{
    public interface IContactLookUpDataService
    {
        Task<IEnumerable<LookUpItem>> GetContactLookUpAsync();
    }
}
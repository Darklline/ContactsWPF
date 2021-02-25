using System.Collections.Generic;
using System.Threading.Tasks;
using Contacts.Model;

namespace Contacts.UI.Data.Lookups
{
    public interface IContactLookUpDataService
    {
        Task<IEnumerable<LookUpItem>> GetContactLookUpAsync();
    }
}
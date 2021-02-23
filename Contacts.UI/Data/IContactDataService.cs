using Contacts.Model;
using System.Threading.Tasks;

namespace Contacts.UI.Data
{
    public interface IContactDataService
    {
        Task<Contact> GetByIdAsync(int contactId);
    }
}

using System.Threading.Tasks;
using Contacts.Model;

namespace Contacts.UI.Data.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> GetByIdAsync(int contactId);
        Task SaveAsync(Contact contact);
    }
}

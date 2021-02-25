using Contacts.DataAccess;
using Contacts.Model;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Contacts.UI.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _context;

        public ContactRepository(ContactDbContext contextCreator)
        {
            _context = contextCreator;
        }
        public async Task<Contact> GetByIdAsync(int contactId)
        {
            return await _context.Contacts.SingleAsync(c => c.Id == contactId);

        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
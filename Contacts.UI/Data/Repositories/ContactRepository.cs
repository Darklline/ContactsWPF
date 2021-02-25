using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Contacts.DataAccess;
using Contacts.Model;

namespace Contacts.UI.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly Func<ContactDbContext> _contextCreator;

        public ContactRepository(Func<ContactDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }
        public async Task<Contact> GetByIdAsync(int contactId)
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Contacts.AsNoTracking().SingleAsync(c => c.Id == contactId);
            }
        }

        public async Task SaveAsync(Contact contact)
        {
            using (var ctx = _contextCreator())
            {
                ctx.Contacts.Attach(contact);
                ctx.Entry(contact).State = EntityState.Modified;
                await ctx.SaveChangesAsync();
            }
        }
    }
}

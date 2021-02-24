using Contacts.DataAccess;
using Contacts.Model;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Contacts.UI.Data
{
    public class ContactDataService : IContactDataService
    {
        private readonly Func<ContactDbContext> _contextCreator;

        public ContactDataService(Func<ContactDbContext> contextCreator)
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

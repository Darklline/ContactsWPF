using Contacts.DataAccess;
using Contacts.Model;
using System;
using System.Collections.Generic;
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
        public async Task<List<Contact>> GetAllAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Contacts.AsNoTracking().ToListAsync();
            }
        }
    }
}

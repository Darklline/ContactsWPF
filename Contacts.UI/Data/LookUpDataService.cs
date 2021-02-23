using Contacts.DataAccess;
using Contacts.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.UI.Data
{
    public class LookUpDataService : IContactLookUpDataService
    {
        private Func<ContactDbContext> _contextCreator;

        public LookUpDataService(Func<ContactDbContext> contextCreator)
        {
            _contextCreator = contextCreator;
        }

        public async Task<IEnumerable<LookUpItem>> GetContactLookUpAsync()
        {
            using (var ctx = _contextCreator())
            {
                return await ctx.Contacts.AsNoTracking()
                     .Select(c =>
                     new LookUpItem
                     {
                         Id = c.Id,
                         DisplayMember = c.FirstName + " " + c.LastName
                     })
                     .ToListAsync();
            }
        }
    }
}

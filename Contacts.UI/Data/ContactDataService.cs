using Contacts.Model;
using System.Collections.Generic;

namespace Contacts.UI.Data
{
    public class ContactDataService : IContactDataService
    {
        public IEnumerable<Contact> GetAll()
        {
            yield return new Contact { Id = 0, FirstName = "Wojciech", LastName = "Preneta", Email = "wojciech.preneta@icloud.com" };
        }
    }
}

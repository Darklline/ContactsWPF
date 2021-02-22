using Contacts.Model;
using System.Collections.Generic;

namespace Contacts.UI.Data
{
    public interface IContactDataService
    {
        IEnumerable<Contact> GetAll();
    }
}

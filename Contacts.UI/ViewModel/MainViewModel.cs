using Contacts.Model;
using Contacts.UI.Data;
using System.Collections.ObjectModel;

namespace Contacts.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IContactDataService _contactDataService;
        private Contact _selectedContact;

        public MainViewModel(IContactDataService contactDataService)
        {
            Contacts = new ObservableCollection<Contact>();
            _contactDataService = contactDataService;
        }

        public void Load()
        {
            var contacts = _contactDataService.GetAll();

            Contacts.Clear();
            foreach (var contact in contacts)
            {
                Contacts.Add(contact);
            }
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
            }
        }
    }
}

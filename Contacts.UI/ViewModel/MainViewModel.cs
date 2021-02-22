using Contacts.Model;
using Contacts.UI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

        public async Task LoadAsync()
        {
            var contacts = await _contactDataService.GetAllAsync();

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

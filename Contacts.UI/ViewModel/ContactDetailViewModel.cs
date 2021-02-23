using Contacts.Model;
using Contacts.UI.Data;
using System.Threading.Tasks;

namespace Contacts.UI.ViewModel
{
    public class ContactDetailViewModel : ViewModelBase, IContactDetailViewModel
    {
        private IContactDataService _dataService;

        public ContactDetailViewModel(IContactDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task LoadAsync(int friendId)
        {
            Contact = await _dataService.GetByIdAsync(friendId);
        }

        private Contact _contact;

        public Contact Contact
        {
            get { return _contact; }
            private set
            {
                _contact = value;
                OnPropertyChanged();
            }
        }
    }
}

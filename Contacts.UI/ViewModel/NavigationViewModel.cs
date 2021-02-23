using Contacts.Model;
using Contacts.UI.Data;
using Contacts.UI.Event;
using Contacts.UI.ViewModel;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IContactLookUpDataService _friendLookupService;
        private readonly IEventAggregator _eventAggregator;

        public NavigationViewModel(IContactLookUpDataService friendLookupService, IEventAggregator eventAggregator)
        {
            _friendLookupService = friendLookupService;
            _eventAggregator = eventAggregator;
            Contacts = new ObservableCollection<LookUpItem>();
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupService.GetContactLookUpAsync();
            Contacts.Clear();
            foreach (var item in lookup)
            {
                Contacts.Add(item);
            }
        }

        public ObservableCollection<LookUpItem> Contacts { get; }
        private LookUpItem _selectedContact;

        public LookUpItem SelectedContact
        {
            get { return _selectedContact; }
            set
            {
                _selectedContact = value;
                OnPropertyChanged();
                if (_selectedContact != null)
                {
                    _eventAggregator.GetEvent<OpenContactDetailViewEvent>()
                        .Publish(_selectedContact.Id);
                }
            }
        }

    }
}

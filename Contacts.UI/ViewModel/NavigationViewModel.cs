using Contacts.Model;
using Contacts.UI.Data;
using Contacts.UI.Event;
using Contacts.UI.ViewModel;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Contacts.UI.Data.Lookups;

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
            Contacts = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterContactSavedEvent>().Subscribe(AfterContactSaved);
        }

        private void AfterContactSaved(AfterContactSavedEventArgs obj)
        {
            var lookupItem = Contacts.Single(l => l.Id == obj.Id);
            lookupItem.DisplayMember = obj.DisplayMember;
        }

        public async Task LoadAsync()
        {
            var lookup = await _friendLookupService.GetContactLookUpAsync();
            Contacts.Clear();
            foreach (var item in lookup)
            {
                Contacts.Add(new NavigationItemViewModel(item.Id, item.DisplayMember));
            }
        }

        public ObservableCollection<NavigationItemViewModel> Contacts { get; }
        private NavigationItemViewModel _selectedContact;

        public NavigationItemViewModel SelectedContact
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

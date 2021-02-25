using Contacts.UI.Data.Lookups;
using Contacts.UI.Event;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private readonly IContactLookUpDataService _contactLookupService;
        private readonly IEventAggregator _eventAggregator;

        public NavigationViewModel(IContactLookUpDataService contactLookupService, IEventAggregator eventAggregator)
        {
            _contactLookupService = contactLookupService;
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
            var lookup = await _contactLookupService.GetContactLookUpAsync();
            Contacts.Clear();
            foreach (var item in lookup)
            {
                Contacts.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, _eventAggregator));
            }
        }

        public ObservableCollection<NavigationItemViewModel> Contacts { get; }
    }
}

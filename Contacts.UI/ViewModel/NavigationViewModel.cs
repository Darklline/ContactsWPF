using Contacts.Model;
using Contacts.UI.Data;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.ViewModel
{
    public class NavigationViewModel : INavigationViewModel
    {
        private IContactLookUpDataService _friendLookupService;

        public NavigationViewModel(IContactLookUpDataService friendLookupService)
        {
            _friendLookupService = friendLookupService;
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
    }
}

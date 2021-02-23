using FriendOrganizer.UI.ViewModel;
using System.Threading.Tasks;

namespace Contacts.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationViewModel navigationViewModel, IContactDetailViewModel contactDetailViewModel)
        {
            NavigationViewModel = navigationViewModel;
            ContactDetailViewModel = contactDetailViewModel;
        }

        public INavigationViewModel NavigationViewModel { get; }
        public IContactDetailViewModel ContactDetailViewModel { get; }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }
    }
}

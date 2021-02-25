using Contacts.UI.Event;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Contacts.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Func<IContactDetailViewModel> _contactDetailViewModelCreator;
        private IContactDetailViewModel _contactDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IContactDetailViewModel> contactDetailViewModelCreator, IEventAggregator eventAggregator)
        {
            _contactDetailViewModelCreator = contactDetailViewModelCreator;

            eventAggregator.GetEvent<OpenContactDetailViewEvent>()
                .Subscribe(OnOpenContactDetailView);

            NavigationViewModel = navigationViewModel;
        }

        public INavigationViewModel NavigationViewModel { get; }

        public IContactDetailViewModel ContactDetailViewModel
        {
            get => _contactDetailViewModel;
            private set
            {
                _contactDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private async void OnOpenContactDetailView(int contactId)
        {
            if (ContactDetailViewModel != null && ContactDetailViewModel.HasChanges)
            {
                var result = MessageBox.Show("Changes were made. Do u wanna leave ?", "Question", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }
            ContactDetailViewModel = _contactDetailViewModelCreator();

            await ContactDetailViewModel.LoadAsync(contactId);
        }
    }
}

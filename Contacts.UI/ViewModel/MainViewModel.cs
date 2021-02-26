using Contacts.UI.Event;
using Contacts.UI.View.Services;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;

namespace Contacts.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Func<IContactDetailViewModel> _contactDetailViewModelCreator;
        private readonly IMessageDialogService _messageDialogService;
        private IContactDetailViewModel _contactDetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel, Func<IContactDetailViewModel> contactDetailViewModelCreator, IEventAggregator eventAggregator, IMessageDialogService messageDialogService)
        {
            _contactDetailViewModelCreator = contactDetailViewModelCreator;
            _messageDialogService = messageDialogService;

            eventAggregator.GetEvent<OpenContactDetailViewEvent>()
                .Subscribe(OnOpenContactDetailView);

            CreateNewContactCommand = new DelegateCommand(OnCreateNewContactExecute);

            NavigationViewModel = navigationViewModel;
        }

        public INavigationViewModel NavigationViewModel { get; }
        public ICommand CreateNewContactCommand { get; }

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

        private async void OnOpenContactDetailView(int? contactId)
        {
            if (ContactDetailViewModel != null && ContactDetailViewModel.HasChanges)
            {
                var result = _messageDialogService.ShowOkCancelDialog("Changes were made. Do u wanna leave ?", "Question");
                if (result == MessageDialogResult.Cancel)
                {
                    return;
                }
            }
            ContactDetailViewModel = _contactDetailViewModelCreator();
            await ContactDetailViewModel.LoadAsync(contactId);
        }

        private void OnCreateNewContactExecute()
        {
            OnOpenContactDetailView(null);
        }
    }
}

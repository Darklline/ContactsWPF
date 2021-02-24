using Contacts.Model;
using Contacts.UI.Data;
using Contacts.UI.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.UI.ViewModel
{
    public class ContactDetailViewModel : ViewModelBase, IContactDetailViewModel
    {
        private IContactDataService _dataService;
        private readonly IEventAggregator _eventAggregator;

        public ContactDetailViewModel(IContactDataService dataService, IEventAggregator eventAggregator)
        {
            _dataService = dataService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenContactDetailViewEvent>()
                .Subscribe(OnOpenContactDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private bool OnSaveCanExecute()
        {
            return true;
        }

        private async void OnSaveExecute()
        {
            await _dataService.SaveAsync(Contact);
            _eventAggregator.GetEvent<AfterContactSavedEvent>().Publish(new AfterContactSavedEventArgs
            {
                Id = Contact.Id,
                DisplayMember = $"{Contact.FirstName} {Contact.LastName}"
            });
        }

        private async void OnOpenContactDetailView(int friendId)
        {
            await LoadAsync(friendId);
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
        public ICommand SaveCommand { get; }
    }
}

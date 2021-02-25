using Contacts.UI.Data;
using Contacts.UI.Event;
using Contacts.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;
using Contacts.UI.Data.Repositories;

namespace Contacts.UI.ViewModel
{
    public class ContactDetailViewModel : ViewModelBase, IContactDetailViewModel
    {
        private IContactRepository _repository;
        private readonly IEventAggregator _eventAggregator;
        private ContactWrapper _contact;
        public ICommand SaveCommand { get; }

        public ContactDetailViewModel(IContactRepository repository, IEventAggregator eventAggregator)
        {
            _repository = repository;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenContactDetailViewEvent>()
                .Subscribe(OnOpenContactDetailView);

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public async Task LoadAsync(int friendId)
        {
            var contact = await _repository.GetByIdAsync(friendId);

            Contact = new ContactWrapper(contact);
            Contact.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Contact.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public ContactWrapper Contact
        {
            get { return _contact; }
            private set
            {
                _contact = value;
                OnPropertyChanged();
            }
        }

        private bool OnSaveCanExecute()
        {
            return Contact != null && !Contact.HasErrors;
        }

        private async void OnSaveExecute()
        {
            await _repository.SaveAsync(Contact.Model);
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
    }
}

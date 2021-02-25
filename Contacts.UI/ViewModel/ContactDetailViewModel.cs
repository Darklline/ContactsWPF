using Contacts.UI.Data.Repositories;
using Contacts.UI.Event;
using Contacts.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contacts.UI.ViewModel
{
    public class ContactDetailViewModel : ViewModelBase, IContactDetailViewModel
    {
        private readonly IContactRepository _contactRepository;
        private readonly IEventAggregator _eventAggregator;
        private bool _hasChanges;
        private ContactWrapper _contact;
        public ICommand SaveCommand { get; }

        public ContactDetailViewModel(IContactRepository contactRepository, IEventAggregator eventAggregator)
        {
            _contactRepository = contactRepository;
            _eventAggregator = eventAggregator;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        public async Task LoadAsync(int contactId)
        {
            var contact = await _contactRepository.GetByIdAsync(contactId);

            Contact = new ContactWrapper(contact);
            Contact.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _contactRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Contact.HasErrors))
                {
                    ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
                }
            };

            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }

        public bool HasChanges
        {
            get => _hasChanges;
            set
            {
                if (_hasChanges == value) return;
                _hasChanges = value;
                OnPropertyChanged();
                ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
            }
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
            return Contact != null && !Contact.HasErrors && HasChanges;
        }

        private async void OnSaveExecute()
        {
            await _contactRepository.SaveAsync();
            HasChanges = _contactRepository.HasChanges();
            _eventAggregator.GetEvent<AfterContactSavedEvent>().Publish(new AfterContactSavedEventArgs
            {
                Id = Contact.Id,
                DisplayMember = $"{Contact.FirstName} {Contact.LastName}"
            });
        }
    }
}
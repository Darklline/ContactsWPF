﻿using Contacts.UI.Data.Repositories;
using Contacts.UI.Event;
using Contacts.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;
using Contacts.Model;
using Contacts.UI.View.Services;

namespace Contacts.UI.ViewModel
{
    public class ContactDetailViewModel : ViewModelBase, IContactDetailViewModel
    {
        private readonly IContactRepository _contactRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMessageDialogService _messageDialogService;
        private bool _hasChanges;
        private ContactWrapper _contact;
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public ContactDetailViewModel(IContactRepository contactRepository, IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _contactRepository = contactRepository;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        public async Task LoadAsync(int? contactId)
        {
            var contact = contactId.HasValue
                ? await _contactRepository.GetByIdAsync(contactId.Value)
                : CreateNewContact();

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
            if (Contact.Id == 0)
            {
                Contact.FirstName = "";
            }
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

        private Contact CreateNewContact()
        {
            var contact = new Contact();
            _contactRepository.Add(contact);

            return contact;
        }

        private async void OnDeleteExecute()
        {
            var result =
                _messageDialogService.ShowOkCancelDialog(
                    $"Are u sure u wanna delete {Contact.FirstName} {Contact.LastName} ?", "Question");
            if (result == MessageDialogResult.Ok)
            {
                _contactRepository.Remove(Contact.Model);
                await _contactRepository.SaveAsync();
                _eventAggregator.GetEvent<AfterContactDeletedEvent>().Publish(Contact.Id);
            }
        }
    }
}
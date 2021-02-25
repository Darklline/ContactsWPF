using Contacts.UI.Event;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace Contacts.UI.ViewModel
{
    public class NavigationItemViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private string _displayMember;

        public NavigationItemViewModel(int id, string displayMember, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            OpenContactDetailViewCommand = new DelegateCommand(OnOpenContactDetailView);
        }

        public int Id { get; }

        public string DisplayMember
        {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        public ICommand OpenContactDetailViewCommand { get; }

        private void OnOpenContactDetailView()
        {
            _eventAggregator.GetEvent<OpenContactDetailViewEvent>()
                .Publish(Id);
        }
    }
}

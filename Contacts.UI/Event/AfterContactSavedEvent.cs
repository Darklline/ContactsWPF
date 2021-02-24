using Prism.Events;

namespace Contacts.UI.Event
{
    public class AfterContactSavedEvent : PubSubEvent<AfterContactSavedEventArgs>
    {

    }

    public class AfterContactSavedEventArgs
    {
        public int Id { get; set; }
        public string DisplayMember { get; set; }
    }
}

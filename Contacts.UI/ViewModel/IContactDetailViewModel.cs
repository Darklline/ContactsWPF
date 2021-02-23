using System.Threading.Tasks;

namespace Contacts.UI.ViewModel
{
  public interface IContactDetailViewModel
  {
    Task LoadAsync(int contactId);
  }
}
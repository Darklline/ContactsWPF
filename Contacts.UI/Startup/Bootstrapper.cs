using Autofac;
using Contacts.UI.Data;
using Contacts.UI.ViewModel;

namespace Contacts.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<ContactDataService>().As<IContactDataService>();

            return builder.Build();
        }
    }
}

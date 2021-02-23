using Autofac;
using Contacts.DataAccess;
using Contacts.UI.Data;
using Contacts.UI.ViewModel;
using FriendOrganizer.UI.ViewModel;
using Prism.Events;

namespace Contacts.UI.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<ContactDbContext>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<ContactDetailViewModel>().As<IContactDetailViewModel>();
            builder.RegisterType<LookUpDataService>().AsImplementedInterfaces();
            builder.RegisterType<ContactDataService>().As<IContactDataService>();

            return builder.Build();
        }
    }
}

using Autofac;
using Contacts.DataAccess;
using Contacts.UI.Data.Lookups;
using Contacts.UI.Data.Repositories;
using Contacts.UI.View.Services;
using Contacts.UI.ViewModel;
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
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<ContactDetailViewModel>().As<IContactDetailViewModel>();
            builder.RegisterType<LookUpDataService>().AsImplementedInterfaces();
            builder.RegisterType<ContactRepository>().As<IContactRepository>();

            return builder.Build();
        }
    }
}

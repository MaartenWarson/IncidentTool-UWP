using Autofac;
using IncidentTool.Interfaces.Locator;
using IncidentTool.Interfaces.Navigation;
using IncidentTool.Interfaces.Repositories;
using IncidentTool.Interfaces.Services.Data;
using IncidentTool.Interfaces.Services.General;
using IncidentTool.Repositories;
using IncidentTool.Services.Data;
using IncidentTool.Services.General;
using IncidentTool.Services.Locator;
using IncidentTool.Services.Navigation;
using IncidentTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentTool.Container
{
    public class AppContainer : IDependencyResolver
    {
        private IContainer _container; // Autofac container
        private static AppContainer _instance;
        public static AppContainer Instance => _instance ?? (_instance = new AppContainer()); // Instantie maken van de container

        private AppContainer()
        {
            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // ViewModels
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<HomeViewModel>();
            builder.RegisterType<CreateDeviceViewModel>();
            builder.RegisterType<UnsolvedIncidentsViewModel>();
            builder.RegisterType<SolvedIncidentsViewModel>();
            builder.RegisterType<CreateQRViewModel>();
            builder.RegisterType<CreateDeviceTypeViewModel>();
            builder.RegisterType<CreateIncidentViewModel>();

            // Services
            builder.RegisterType<QRService>().As<IQRService>();
            builder.Register(c => Instance).As<IDependencyResolver>();
            builder.RegisterType<DeviceDataService>().As<IDeviceDataService>();
            builder.RegisterType<DeviceTypeDataService>().As<IDeviceTypeDataService>();
            builder.RegisterType<IncidentDataService>().As<IIncidentDataService>();
            builder.RegisterType<OccurredIncidentDataService>().As<IOccurredIncidentDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();
            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>();
            builder.RegisterType<NavigationService>().As<INavigationService>();

            // Repository
            builder.RegisterType<GenericRepository>().As<IGenericRepository>();

            // Container builden
            _container = builder.Build();
        }

        public object Resolve(Type typeName)
        {
            return _container.Resolve(typeName);
        }

        // Deze methode wordt opgeroepen wanneer we hard gecodeerd een service gaan resolven (en niet via de constructor)
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}

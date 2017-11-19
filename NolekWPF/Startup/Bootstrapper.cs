using Autofac;
using NolekWPF.DataServices;
using NolekWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.DataAccess;
using NolekWPF.DataServices.Repositories;
using Prism.Events;

namespace NolekWPF.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            //views
            builder.RegisterType<MainWindow>().AsSelf();

            //db context
            builder.RegisterType<wiki_nolek_dk_dbEntities>().AsSelf(); //create new context when it is needed

            //data services
            builder.RegisterType<EquipmentDataService>().As<IEquipmentDataService>();
            builder.RegisterType<EquipmentLookupDataService>().As<IEquipmentLookupDataService>();

            //repositories
            builder.RegisterType<EquipmentRepository>().As<IEquipmentRepository>();

            //view models
            builder.RegisterType<EquipmentListViewModel>().As<IEquipmentListViewModel>();
            builder.RegisterType<EquipmentCreateViewModel>().As<IEquipmentCreateViewModel>();
            builder.RegisterType<EquipmentDetailViewModel>().As<IEquipmentDetailViewModel>();
            builder.RegisterType<MainViewModel>().AsSelf();

            //register event aggregators
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            return builder.Build();
        }
    }
}

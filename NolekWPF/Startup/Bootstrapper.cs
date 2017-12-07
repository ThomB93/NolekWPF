﻿using Autofac;
using NolekWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NolekWPF.DataAccess;
using Prism.Events;
using NolekWPF.Data.DataServices;
using NolekWPF.Data.Repositories;
using NolekWPF.Equipment.ViewModels;
using NolekWPF.ViewModels.Component;
using NolekWPF.ViewModels.Login;
using NolekWPF.Pages.Login;

namespace NolekWPF.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            //views
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<LoginView>().AsSelf();

            //db context
            builder.RegisterType<wiki_nolek_dk_dbEntities>().AsSelf(); //create new context when it is needed

            //data services
            builder.RegisterType<EquipmentDataService>().As<IEquipmentDataService>();
            builder.RegisterType<EquipmentLookupDataService>().As<IEquipmentLookupDataService>();
            builder.RegisterType<ErrorDataService>().As<IErrorDataService>();
            builder.RegisterType<ComponentDataService>().As<IComponentDataService>();
            builder.RegisterType<UserDataService>().As<IUserDataService>();

            //repositories
            builder.RegisterType<EquipmentRepository>().As<IEquipmentRepository>();
            builder.RegisterType<ComponentRepository>().As<IComponentRepository>();
            builder.RegisterType<ErrorRepository>().As<IErrorRepository>();

            //view models
            builder.RegisterType<EquipmentListViewModel>().As<IEquipmentListViewModel>();
            builder.RegisterType<EquipmentCreateViewModel>().As<IEquipmentCreateViewModel>();
            builder.RegisterType<EquipmentDetailViewModel>().As<IEquipmentDetailViewModel>();

            builder.RegisterType<ComponentListViewModel>().As<IComponentListViewModel>();
            builder.RegisterType<ComponentCreateViewModel>().As<IComponentCreateViewModel>();
            builder.RegisterType<ComponentDetailViewModel>().As<IComponentDetailViewModel>();

            builder.RegisterType<LoginViewModel>().As<ILoginViewModel>();

            builder.RegisterType<MainViewModel>().AsSelf();
            

            //register event aggregators
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            return builder.Build();
        }
    }
}

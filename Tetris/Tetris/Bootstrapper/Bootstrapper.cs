using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using Tetris.ViewModels;
using Caliburn.Metro;
using Caliburn.Metro.Autofac;
using Tetris.Views;

namespace Tetris.Bootstrapper
{

    //public class Bootstrapper : CaliburnMetroAutofacBootstrapper<Views.MainWindowView>
    //{
    //    protected override void ConfigureContainer(ContainerBuilder builder)
    //    {
    //        builder.RegisterType<AppWindowManager>().As<IWindowManager>().SingleInstance();
    //        var assembly = typeof(MainWindowView).Assembly;
    //        builder.RegisterAssemblyTypes(assembly)
    //            .Where(item => item.Name.EndsWith("ViewModel") && item.IsAbstract == false)
    //            .AsSelf()
    //            .SingleInstance();
    //    }
    //}

    public class Bootstrapper : Caliburn.Micro.BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainWindowViewModel>();
        }
    }

    
}

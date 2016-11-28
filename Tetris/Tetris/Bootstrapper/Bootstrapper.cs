﻿using System;
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
    /// <summary>
    /// Basic bootstrapper for proper Caliburn.Micro working
    /// </summary>
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

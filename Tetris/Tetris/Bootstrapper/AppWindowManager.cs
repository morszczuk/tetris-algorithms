using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Metro.Core;
using Caliburn.Micro;
using MahApps.Metro.Controls;


namespace Tetris.Bootstrapper
{
    /// <summary>
    /// Caliburn.Micro AppWindowManager 
    /// </summary>
    [Export(typeof(IWindowManager))]
    public class AppWindowManager : MetroWindowManager
    {
        public override MetroWindow CreateCustomWindow(object view, bool windowIsView)
        {
            if (windowIsView)
            {
                return view as Views.MainWindowView;
            }

            return new Views.MainWindowView
            {
                Content = view
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Tetris.Helpers;

namespace Tetris.ViewModels
{
    [Export(typeof(MainWindowViewModel))]

    public class MainWindowViewModel : Conductor<object>, IHaveDisplayName
    {
        private readonly IWindowManager _windowManager;


        private ShellViewModel _shellViewModel;
        private BrowseBricksViewModel _browseBricksViewModel;



        public MainWindowViewModel()
        {
            _windowManager = new WindowManager();


            ActivatgeShellView();
        }

        public override string DisplayName { get; set; } = "Tetris";

        public void ActivateLibrary()
        {
            _browseBricksViewModel = new BrowseBricksViewModel(_windowManager, _shellViewModel.BrickTypes, this);
            ActivateItem(_browseBricksViewModel);
        }

        public void ActivatgeShellView()
        {
            if (_shellViewModel == null) _shellViewModel = new ShellViewModel(_windowManager, this);
            ActivateItem(_shellViewModel);
        }

        public void ActivateRunningAlgorithmView(bool isStep)
        {

            ActivateItem(new RunningAlgorithmViewModel(_windowManager, _shellViewModel.BrickTypes, this, isStep,_shellViewModel.WellNo,_shellViewModel.WellWidth));

        }

    }
}

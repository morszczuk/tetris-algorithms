using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Tetris.Models;

namespace Tetris.ViewModels
{
    public class BrowseBricksViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private BindableCollection<BrickType> _brickTypes;
        private string _displayName = "Kolekcja klocków";
        private MainWindowViewModel _mainWindowViewModel;

        public BrowseBricksViewModel(IWindowManager windowManager, List<BrickType> brickTypes, MainWindowViewModel mainWindowViewModel)
        {
            _windowManager = windowManager;
            _brickTypes = new BindableCollection<BrickType>(brickTypes);
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public BindableCollection<BrickType> BrickTypes
        {
            get { return _brickTypes; }
            set
            {
                _brickTypes = value;
                NotifyOfPropertyChange(()=>BrickTypes);
            }
        }

        public void BackButton()
        {
            _mainWindowViewModel.ActivatgeShellView();
        }
    }
}

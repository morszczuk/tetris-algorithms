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
        private BindableCollection<Brick> _bricks;
        private string _displayName = "Kolekcja klocków";
        private MainWindowViewModel _mainWindowViewModel;

        public BrowseBricksViewModel(IWindowManager windowManager,List<Brick> bricks, MainWindowViewModel mainWindowViewModel)
        {
            _windowManager = windowManager;
            _bricks = new BindableCollection<Brick>(bricks);
            _mainWindowViewModel = mainWindowViewModel;
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }

        public BindableCollection<Brick> Bricks
        {
            get { return _bricks; }
            set
            {
                _bricks = value;
                NotifyOfPropertyChange(()=>Bricks);
            }
        }

        public void BackButton()
        {
            _mainWindowViewModel.ActivatgeShellView();
        }
    }
}

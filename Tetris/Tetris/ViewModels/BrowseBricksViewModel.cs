using System.Collections.Generic;
using Caliburn.Micro;
using Tetris.Models;

namespace Tetris.ViewModels
{
    public class BrowseBricksViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private BindableCollection<BrickType> _brickTypes;
        private readonly MainWindowViewModel _mainWindowViewModel;

        public BrowseBricksViewModel(IWindowManager windowManager, IEnumerable<BrickType> brickTypes, MainWindowViewModel mainWindowViewModel)
        {
            _windowManager = windowManager;
            _brickTypes = new BindableCollection<BrickType>(brickTypes);
            _mainWindowViewModel = mainWindowViewModel;
        }

        public override string DisplayName { get; set; } = "Kolekcja klocków";

        public BindableCollection<BrickType> BrickTypes
        {
            get { return _brickTypes; }
            set
            {
                _brickTypes = value;
                NotifyOfPropertyChange(()=> BrickTypes);
            }
        }

        public void BackButton()
        {
            _mainWindowViewModel.ActivatgeShellView();
        }
    }
}

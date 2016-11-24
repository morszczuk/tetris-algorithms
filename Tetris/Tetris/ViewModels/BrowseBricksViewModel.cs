using System.Collections.Generic;
using Caliburn.Micro;
using Tetris.Models;

namespace Tetris.ViewModels
{
    public class BrowseBricksViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private readonly MainWindowViewModel _mainWindowViewModel;

        private BindableCollection<BrickType> _brickTypes;
        private int _everyCardinality;


        public BrowseBricksViewModel(IWindowManager windowManager, IEnumerable<BrickType> brickTypes, MainWindowViewModel mainWindowViewModel)
        {
            _windowManager = windowManager;
            _brickTypes = new BindableCollection<BrickType>(brickTypes);
            _mainWindowViewModel = mainWindowViewModel;
            var enumerator = brickTypes.GetEnumerator();
            if (enumerator.MoveNext())
                EveryCardinality = enumerator.Current.DefaultCount;
        }

        public int EveryCardinality
        {
            get { return _everyCardinality; }
            set
            {
                _everyCardinality = value;
                NotifyOfPropertyChange(() => EveryCardinality);
            }
        }

        public override string DisplayName { get; set; } = "Kolekcja klocków";

        public BindableCollection<BrickType> BrickTypes
        {
            get { return _brickTypes; }
            set
            {
                _brickTypes = value;
                NotifyOfPropertyChange(() => BrickTypes);
            }
        }

        public void BackButton()
        {
            _mainWindowViewModel.ActivatgeShellView();
        }

        public void CardinalityOnEveryBrickOnClick()
        {
            foreach (var brick in BrickTypes)
            {
                brick.DefaultCount = EveryCardinality;
            }
            BrickTypes = new BindableCollection<BrickType>(BrickTypes);
        }
    }
}

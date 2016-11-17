using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Tetris.AlgorithmLogic;
using Tetris.Enums;
using Tetris.Models;

namespace Tetris.ViewModels
{

    public class RunningAlgorithmViewModel : Conductor<object>
    {
        private readonly IWindowManager _windowManager;
        private string _displayName = "Uruchomiony algorytm";
        private MainWindowViewModel _mainWindowViewModel;

        private AlgorithmInput _algorithmParameters;



        public RunningAlgorithmViewModel(IWindowManager windowManager, List<Brick> bricks, MainWindowViewModel mainWindowViewModel,bool isStep,int wellNo,int wellWidth)
        {
            _windowManager = windowManager;
            _mainWindowViewModel = mainWindowViewModel;

            _algorithmParameters=new AlgorithmInput(new BricksShelf(bricks) ,wellNo,wellWidth,isStep?AlgorithmsEnum.Step : AlgorithmsEnum.Continuous);

        }

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }


        //public void BackButton()
        //{
        //    _mainWindowViewModel.ActivatgeShellView();
        //}



    }
}

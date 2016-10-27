using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Tetris.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        private bool _isAlgorithmActive = false;
        private bool _areComputationsRunning = false;
        private int _wellNo=2;
        private int _wellWidth = 10;
        private bool _isStep = false;

        public bool IsStep
        {
            get
            {
                return _isStep;
            }
            set
            {
                _isStep = value;
                OnPropertyChanged("IsStep");
            }
        }
    

        public bool IsAlgorithmActive
        {
            get
            { 
                return _isAlgorithmActive;
            }
        }

        public bool IsAlgorithmLaunchWindowShowed
        {
            get
            {
                return !_isAlgorithmActive;
            }
        }

        public bool IsRunningAlgorithmWindowShowed
        {
            get
            {
                return _isAlgorithmActive;
            }
        }

        public bool AreComputationsRunning
        {
            get
            {
                return _areComputationsRunning;
            }
        }

        public bool AreComputationsPaused
        {
            get
            {
                return !_areComputationsRunning;
            }
            set
            {
                _areComputationsRunning = !value;
                OnPropertyChanged("AreComputationsRunning");
                OnPropertyChanged("AreComputationsPaused");
                OnPropertyChanged("ComputationsButtonComunicate");
            }
        }

        
         public int WellWidth
        {
            get
            {
                return _wellWidth;
            }
            set
            {
                _wellWidth = value;
                OnPropertyChanged("WellWidth");
            }
        }

        public int WellNo
        {
            get
            {
                return _wellNo;
            }
            set
            {
                _wellNo = value;
                OnPropertyChanged("WellNo");
            }
        }

        public string ComputationsButtonComunicate
        {
            get
            {
                return _areComputationsRunning ? "TimerPause" : "TimerPlay";
            }
        }

        public ICommand StartComputationsCommand
        {
            
            get {
                return new RelayCommand((x) => this.StartComputations(x));
            }
        }

        public ICommand StartComputationsCommand2
        {
            get { return new RelayCommand((x) => this.StartComputations2(x)); }
        }

        public void StartComputations(object parameter)
        {
            IsStep = true;

            _isAlgorithmActive = true;
            _areComputationsRunning = true;

            OnPropertyChanged("IsAlgorithmLaunchWindowShowed");
            OnPropertyChanged("IsRunningAlgorithmWindowShowed");
            OnPropertyChanged("AreComputationsRunning");
            OnPropertyChanged("AreComputationsPaused");
            OnPropertyChanged("ComputationsButtonComunicate");
            OnPropertyChanged("IsAlgorithmActive");
        }

        public void StartComputations2(object parameter)
        {
            
            IsStep = false;
            _isAlgorithmActive = true;
            _areComputationsRunning = true;



            OnPropertyChanged("IsAlgorithmLaunchWindowShowed");
            OnPropertyChanged("IsRunningAlgorithmWindowShowed");
            OnPropertyChanged("AreComputationsRunning");
            OnPropertyChanged("AreComputationsPaused");
            OnPropertyChanged("ComputationsButtonComunicate");
            OnPropertyChanged("IsAlgorithmActive");
        }

        public ICommand ChangeComputationsStatusCommand
        {
            get { return new RelayCommand((x) => this.ChangeComputationsStatus(x)); }
        }
        
        void ChangeComputationsStatus(object parameter)
        {
            _areComputationsRunning = !_areComputationsRunning;
            OnPropertyChanged("AreComputationsRunning");
            OnPropertyChanged("AreComputationsPaused");
            OnPropertyChanged("ComputationsButtonComunicate");
        }

        public ICommand EndComputationsCommand
        {
            get { return new RelayCommand((x) => this.EndComputations(x)); }
        }

        void EndComputations(object parameter)
        {
            _isAlgorithmActive = false;
            OnPropertyChanged("IsAlgorithmActive");
            OnPropertyChanged("IsAlgorithmLaunchWindowShowed");
            OnPropertyChanged("IsRunningAlgorithmWindowShowed");
        }
        
    }
}

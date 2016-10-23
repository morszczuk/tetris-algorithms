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

        public string ComputationsButtonComunicate
        {
            get
            {
                return _areComputationsRunning ? "STOP" : "KONTYNUUJ";
            }
        }

        public ICommand StartComputationsCommand
        {
            get { return new RelayCommand((x) => this.StartComputations(x)); }
        }

        public void StartComputations(object parameter)
        {
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

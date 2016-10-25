using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Tetris.Model.Models;
using Tetris.ViewModel.Helpers;

namespace Tetris.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public bool IsAlgorithmActive => IsRunningAlgorithmWindowShowed;

        public bool IsAlgorithmLaunchWindowShowed => !IsRunningAlgorithmWindowShowed;

        public bool IsRunningAlgorithmWindowShowed { get; private set; } = false;

        public bool AreComputationsRunning { get; private set; } = false;

        public bool AreComputationsPaused
        {
            get
            {
                return !AreComputationsRunning;
            }
            set
            {
                AreComputationsRunning = !value;
                OnPropertyChanged("AreComputationsRunning");
                OnPropertyChanged("AreComputationsPaused");
                OnPropertyChanged("ComputationsButtonComunicate");
            }
        }

        public string ComputationsButtonComunicate => AreComputationsRunning ? "STOP" : "KONTYNUUJ";

        public Well Well { get; private set; }

        public int WellWidth => Well?.Width ?? 0;

        public List<Brick> Bricks { get; private set; }

        public bool IsLibraryShowed => Bricks != null && Bricks.Count > 0;

    #region Commands

        public ICommand StartComputationsCommand => new RelayCommand(this.StartComputations);
        
        private void StartComputations(object parameter)
        {
            IsRunningAlgorithmWindowShowed = true;
            AreComputationsRunning = true;

            OnPropertyChanged("IsAlgorithmLaunchWindowShowed");
            OnPropertyChanged("IsRunningAlgorithmWindowShowed");
            OnPropertyChanged("AreComputationsRunning");
            OnPropertyChanged("AreComputationsPaused");
            OnPropertyChanged("ComputationsButtonComunicate");
            OnPropertyChanged("IsAlgorithmActive");
        }

        public ICommand ChangeComputationsStatusCommand => new RelayCommand(this.ChangeComputationsStatus);

        private void ChangeComputationsStatus(object parameter)
        {
            AreComputationsRunning = !AreComputationsRunning;
            OnPropertyChanged("AreComputationsRunning");
            OnPropertyChanged("AreComputationsPaused");
            OnPropertyChanged("ComputationsButtonComunicate");
        }

        public ICommand EndComputationsCommand => new RelayCommand(this.EndComputations);

        private void EndComputations(object parameter)
        {
            IsRunningAlgorithmWindowShowed = false;
            OnPropertyChanged("IsAlgorithmActive");
            OnPropertyChanged("IsAlgorithmLaunchWindowShowed");
            OnPropertyChanged("IsRunningAlgorithmWindowShowed");
        }

        public ICommand LoadBricksCommand => new RelayCommand(this.LoadBricks);

        private void LoadBricks(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var loader = new BricksLoader(new StreamReader(openFileDialog.FileName));
                var result = loader.ReadFile();
                Bricks = result.Bricks;
                Well = new Well(result.WellWidth);
                OnPropertyChanged("Bricks");
                OnPropertyChanged("Well");
                OnPropertyChanged("WellWidth");
                OnPropertyChanged("IsLibraryShowed");
            }
        }

        

    #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.AlgorithmLogic.Evaluators;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.AlgorithmLogic.Strategies;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    public class AlgorithmExecutor
    {
        public AlgorithmInput Settings { get; }
        public List<WellState> ActiveStates { get; private set; }

        private IBrickPositioner _positioner;
        private IWellStateEvaluator _evaluator;
        private IWellStateSelectionStrategy _selectionStrategy;

        public AlgorithmExecutor(AlgorithmInput settings)
        {
            Settings = settings;
            Init();
        }

        public void Init()
        {
            var well = new Well(Settings.WellWidth);
            var initialState = new WellState(well, Settings.BricksShelf);

            ActiveStates = new List<WellState>();
            for (var i = 0; i < Settings.WellNo; i++)
                ActiveStates.Add(initialState);

            _positioner = new BasicBottomLeftPositioner();
            _evaluator = new PointEvaluator();
           // _evaluator = new HeightEvaluator();
            _selectionStrategy = new TopKStates(Settings.WellNo, _evaluator);
        }

        public void Run()
        {
            while (!IsFinished())
            {
                MakeStep();
            }
        }

        public void MakeStep()
        {
            var newStates = new List<WellState>();
            foreach (var state in ActiveStates)
            {
                foreach (var brick in state.BricksShelf.AvailableBricks())
                {
                    foreach (RotateEnum rotation in Enum.GetValues(typeof(RotateEnum)))
                    {
                        var tmpState = new WellState(state);
                        tmpState.BricksShelf.Bricks[brick]--;
                        newStates.AddRange(_positioner.PlaceBrick(tmpState, brick.Rotate(rotation)));
                    }
                }
            }
            ActiveStates = _selectionStrategy.Select(newStates);
        }

        public bool IsFinished()
        {
            return ActiveStates.All(s => !s.BricksShelf.AvailableBricks().Any());
        }

    }
}

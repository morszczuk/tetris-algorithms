using System.Collections.Generic;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    public class StatesGenerator
    {

        private readonly IBrickPositioner _positioner;

        public StatesGenerator(IBrickPositioner positioner)
        {
            _positioner = positioner;
        }

        public List<WellState> Generate(WellState wellState) 
        {
            var newStates = new List<WellState>();
            foreach (var brickType in wellState.BricksShelf.AvailableBrickTypes())
            {
                foreach (var rotation in brickType.AvailableRotations)
                {
                    var tmpState = new WellState(wellState);
                    tmpState.BricksShelf.Bricks[brickType]--;
                    newStates.AddRange(_positioner.PlaceBrick(tmpState, brickType.Brick(rotation)));
                }
            }
            return newStates;
        }

    }
}

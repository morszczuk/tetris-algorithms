using System.Linq;
using System.Collections.Generic;
using Tetris.AlgorithmLogic.Positioners;
using Tetris.Models;

namespace Tetris.AlgorithmLogic
{
    /// <summary>
    /// Generates new wellstates using given positioner
    /// </summary>
    public class StatesGenerator
    {

        private readonly IBrickPositioner _positioner;

        public StatesGenerator(IBrickPositioner positioner)
        {
            _positioner = positioner;
        }
        /// <summary>
        /// Paraller method for generating new wellstate
        /// </summary>
        /// <param name="wellState">Source well state</param>
        /// <returns>List of new generated well states after positioning each of avaliable bricks with each possible rotation.</returns>
        public List<WellState> Generate(WellState wellState)
        {
            return wellState.BricksShelf
                            .AvailableBrickTypes
                            .AsParallel()
                            .SelectMany<BrickType, Brick>(BrickRotations)
                            .SelectMany<Brick, WellState>(brick => StatesWithBrick(wellState, brick))
                            .ToList();
        }

        private IEnumerable<Brick> BrickRotations(BrickType brickType)
        {
            return brickType.AvailableRotations.Select(rotation => brickType.Brick(rotation));
        }

        private IEnumerable<WellState> StatesWithBrick(WellState wellState, Brick brick)
        {
            var tmpState = new WellState(wellState);
            tmpState.BricksShelf.Bricks[brick.BrickType]--;
            return _positioner.PlaceBrick(tmpState, brick);
        }

    }
}

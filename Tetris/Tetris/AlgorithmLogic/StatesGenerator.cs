using System.Linq;
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

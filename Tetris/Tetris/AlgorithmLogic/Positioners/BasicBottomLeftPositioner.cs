using System;
using System.Collections.Generic;
using Tetris.Models;

namespace Tetris.AlgorithmLogic.Positioners
{
    public class BasicBottomLeftPositioner : IBrickPositioner
    {
        public IEnumerable<WellState> PlaceBrick(WellState wellState, BrickType brick)
        {
            var generatedStates = new List<WellState>(4);
            foreach(RotateEnum rotation in brick.AvailableRotations)
                generatedStates.Add(PlaceBrick(wellState, brick, rotation));
            return generatedStates;
        }

        private WellState PlaceBrick(WellState wellState, BrickType brickType, RotateEnum rotation)
        {
            for (var y = wellState.FullRows; y <= wellState.Fill.Count; y++)
            {
                for (var x = 0; x < wellState.Well.Width; x++)
                {
                    if (wellState.AddBrick(brickType, x, y, rotation))
                        return wellState;
                }
            }
            return null;
        }
    }
}

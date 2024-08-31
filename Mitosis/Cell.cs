using System.Diagnostics;
using System.Numerics;

namespace Mitosis
{
    internal class Cell
    {
        readonly Random rand = new Random();
        private float maxSpeed = 5f;
        private float minSpeed = -5f;
        
        private float cellSplitSpeed = 20f;
        private int cellSplitCounter = 20;
        private bool positiveXCellSplit;

        public Vector2 Position;
        public float Radius;

        private double xToAdd;
        private double yToAdd;

        public Cell(Vector2 pos, float rad)
        {
            Radius = rad;
            Position = pos;
        }

        public void UpdatePosition()
        {
            //Movement after split
            if(cellSplitCounter < 6)
            {
                if(positiveXCellSplit)
                {
                    xToAdd = rand.NextDouble() * (cellSplitSpeed - minSpeed) + minSpeed;
                    yToAdd = rand.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
                }
                else
                {
                    xToAdd = rand.NextDouble() * (-cellSplitSpeed - minSpeed) + minSpeed;
                    yToAdd = rand.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
                }
                cellSplitCounter++;
            }
            //Regular movement
            else
            {
                xToAdd = rand.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
                yToAdd = rand.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
            }

            Position.X += (float)xToAdd;
            Position.Y += (float)yToAdd;
        }

        public void CellSplitMovement(bool positiveX)
        {
            cellSplitCounter = 0;
            positiveXCellSplit = positiveX;
        }
    }
}
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Covid2020
{
    abstract class Character
    {
        public Vector2 position;
        public Vector2 targetPosition;

        public int moveSpeed;

        protected static double[] directionAngles =
        {
            0.50 * Math.PI, // Down
            0.75 * Math.PI, // DownLeft
            1.00 * Math.PI, // Left
            1.25 * Math.PI, // UpLeft
            1.50 * Math.PI, // Up
            1.75 * Math.PI, // UpRight
            0.00 * Math.PI, // Right
            0.25 * Math.PI  // DownRight
        };

        public enum Direction
        {
            Down,
            DownLeft,
            Left,
            UpLeft,
            Up,
            UpRight,
            Right,
            DownRight
        }

        public Character(Vector2 startPosition, int speed)
        {
            this.position = startPosition;
            this.moveSpeed = speed;
        }

        public virtual void Draw(CanvasDrawingSession drawSession)
        {

        }

        public double CalculateTargetAngle()
        {
            Vector2 offset = targetPosition - position;

            Vector2 normOffset = Vector2.Normalize(offset);

            double angle = Math.Atan2(normOffset.Y, normOffset.X);

            return angle;
        }

        protected Direction CalculateAimDirection()
        {
            double angle = this.CalculateTargetAngle();

            while (angle < 0)
                angle += Math.PI * 2;

            double bestDirectionScore = double.MaxValue;
            Direction bestDirection = Direction.Down;

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                var directionAngle = directionAngles[(int)direction];

                var score = Math.Abs(directionAngle - angle);

                if (score < bestDirectionScore)
                {
                    bestDirectionScore = score;
                    bestDirection = direction;
                }
            }

            return bestDirection;
        }
    }
}

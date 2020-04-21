using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Covid2020.Assets
{
    class Player
    {
        public List<CanvasBitmap> aimingBitmaps;
        public List<CanvasBitmap> reloadBitmaps;

        public CanvasBitmap testBitmap;

        public Vector2 position;
        public Vector2 pointPosition;
        public int moveSpeed;

        public bool moveUp;
        public bool moveDown;
        public bool moveLeft;
        public bool moveRight;

        public bool idle;
        public bool reloading;

        static double[] directionAngles =
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

        public Player(Vector2 startPos, int speed)
        {
            position = startPos;
            moveSpeed = speed;
        }

        public void Draw(CanvasDrawingSession drawSession)
        {
            Direction aimDirection = CalculateAimDirection();
            int assetIndex = (int)aimDirection;

            if (reloading)
            {

            }
            else
            {
                if (assetIndex < aimingBitmaps.Count)
                {
                    drawSession.DrawImage(aimingBitmaps[assetIndex], position);
                }
                else
                {

                }
            }
        }

        public void UpdatePosition()
        {
            if(moveUp)
            {
                position.Y -= moveSpeed;
            }
            else if(moveDown)
            {
                position.Y += moveSpeed;
            }

            if (moveRight)
            {
                position.X += moveSpeed;
            }
            else if (moveLeft)
            {
                position.X -= moveSpeed;
            }
        }

        private Direction CalculateAimDirection()
        {
            Vector2 offset = pointPosition - position;

            Vector2 normOffset = Vector2.Normalize(offset);

            double angle = Math.Atan2(normOffset.Y, normOffset.X);

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

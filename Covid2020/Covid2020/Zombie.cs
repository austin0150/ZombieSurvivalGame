using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Covid2020
{
    class Zombie
    {
        public List<CanvasBitmap> zombieBitmaps;
        public CanvasBitmap deadZombieBitmap;

        public Vector2 position;
        public Vector2 targetPosition;

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

        public Zombie(Vector2 startPos, int speed)
        {

        }
    }
}

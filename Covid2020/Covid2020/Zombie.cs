using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Covid2020
{
    class Zombie : Character
    {
        public List<CanvasBitmap> zombieBitmaps;
        public CanvasBitmap deadZombieBitmap;

        public Zombie(Vector2 startPosition, int speed)
            : base(startPosition, speed)
        {
            
        }

        public override void Draw(CanvasDrawingSession drawSession)
        {
            Direction pointDirection = CalculateAimDirection();

            int assetIndex = (int)pointDirection;

            if (assetIndex < zombieBitmaps.Count)
            {
                drawSession.DrawImage(zombieBitmaps[assetIndex], position);
            }
        }

        public override void UpdatePosition()
        {
            double angle = this.CalculateTargetAngle();

            double x = 0;
            double y = 0;

            this.CalculateMovement(ref x, ref y, angle);

            position.X = (float)x;
            position.Y = (float)y;
        }

        public void CalculateMovement(ref double X, ref double Y, double angle)
        {
            Y = this.moveSpeed * (Math.Sin(angle));
            X = this.moveSpeed * (Math.Cos(angle));;
        }
    }
}

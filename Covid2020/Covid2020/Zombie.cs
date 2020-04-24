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

            Vector2 moveVector = this.CalculateMovement(angle);

            this.position += moveVector;
        }

        public Vector2 CalculateMovement(double angle)
        {
            Vector2 returnVector = new Vector2();
            returnVector.Y = (float) (this.moveSpeed * (Math.Sin(angle)));
            returnVector.X = (float) (this.moveSpeed * (Math.Cos(angle)));

            return returnVector;
        }
    }
}

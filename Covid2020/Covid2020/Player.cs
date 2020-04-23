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

namespace Covid2020
{
    class Player : Character
    {
        public List<CanvasBitmap> aimingBitmaps;
        public List<CanvasBitmap> reloadBitmaps;

        public bool moveUp;
        public bool moveDown;
        public bool moveLeft;
        public bool moveRight;

        public bool idle;
        public bool reloading;

        public Player(Vector2 startPosition, int speed)
            : base(startPosition, speed)
        {
        }

        public override void Draw(CanvasDrawingSession drawSession) 
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
    }
}

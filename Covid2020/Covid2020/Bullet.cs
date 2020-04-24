using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Covid2020
{
    class Bullet
    {
        public Vector2 position;
        int speed = 30;
        double Direction;
        CanvasBitmap img;
        public bool Valid = true;


        public Bullet (double direction, CanvasBitmap image, Vector2 pos)
        {
            Direction = direction;
            img = image;
            position = pos;
        }

        public void Draw(CanvasDrawingSession drawSession)
        {
            double x =0;
            double y =0;
            
            CalcNewPos(ref x, ref y);

            position.X += (float)x;
            position.Y += (float)y;

            if(position.Y <= 0 || position.Y > 720)
            {
                Valid = false;
            }
            if(position.X <= 0 || position.X > 1080)
            {
                Valid = false;
            }

            drawSession.DrawImage(img, position);

        }

        public void CalcNewPos(ref double X, ref double Y)
        {
            Y = speed * (Math.Sin(Direction));
            X = speed * (Math.Cos(Direction));
        }
    }
}

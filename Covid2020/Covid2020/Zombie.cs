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


    }
}

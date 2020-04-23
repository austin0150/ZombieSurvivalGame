using Covid2020.Assets;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;

namespace Covid2020
{
    class CovidGame
    {
        public bool GameOver { get; set; }
        public Player player;
        public List<Bullet> bullets;

        public CovidGame(Vector2 SpawnPos)
        {
            player = new Player(SpawnPos, 5);
            bullets = new List<Bullet>();
        }

        public void Draw(CanvasDrawingSession drawingSession)
        {
            player.Draw(drawingSession);

            for(int i =0; i < bullets.Count; i++)
            { 
                if(!bullets[i].Valid)
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        public void Update()
        {
            player.UpdatePosition();
        }

        public void SetPlayerMoveUp(bool move)
        {
            player.moveUp = move;
        }

        public void SetPlayerMoveDown(bool move)
        {
            player.moveDown = move;
        }

        public void SetPlayerMoveLeft(bool move)
        {
            player.moveLeft = move;
        }

        public void SetPlayerMoveRight(bool move)
        {
            player.moveRight = move;
        }

        public void SetPlayerAimLocation(Vector2 position)
        {
            player.pointPosition = position;
        }

        public void SetBitmaps(List<CanvasBitmap> moveBitmaps,
                               List<CanvasBitmap> reloadBitmaps)
        {
            player.aimingBitmaps = moveBitmaps;
            player.reloadBitmaps = reloadBitmaps;
        }
    }
}

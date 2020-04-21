using Covid2020.Assets;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Covid2020
{
    class CovidGame
    {
        public bool GameOver { get; set; }
        public Player player;

        public CovidGame(Vector2 SpawnPos)
        {
            player = new Player(SpawnPos, 5);
        }

        public void Draw(CanvasDrawingSession drawingSession)
        {
            player.Draw(drawingSession);
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

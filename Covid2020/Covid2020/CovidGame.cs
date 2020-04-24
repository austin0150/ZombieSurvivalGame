using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Covid2020
{
    class CovidGame
    {
        public bool GameOver { get; set; }
        public Player player;
        public List<Bullet> bullets;
        public List<Zombie> zombies;
        public List<CanvasBitmap> zombieMovement;
        public CanvasBitmap deadZombie;
        private Stopwatch spawnStopwatch;
        private Vector2 spawnPos;
        private Stopwatch spawnInc;

        public CovidGame(Vector2 SpawnPos)
        {
            this.spawnPos = SpawnPos;
            this.spawnStopwatch = new Stopwatch();
            this.spawnStopwatch.Start();
            this.spawnInc = new Stopwatch();
            this.spawnInc.Start();
            player = new Player(SpawnPos, 5);
            bullets = new List<Bullet>();
            zombies = new List<Zombie>();
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

            for (int index = 0; index < zombies.Count; index++)
            {
                zombies[index].Draw(drawingSession);
            }

            if (spawnStopwatch.ElapsedMilliseconds < 30000)
            {
                if (spawnInc.ElapsedMilliseconds > 5000)
                {
                    Zombie zombie = new Zombie(spawnPos, 2);
                    zombie.zombieBitmaps = zombieMovement;
                    zombies.Add(zombie);
                    spawnInc.Reset();
                    spawnInc.Start();
                }
            }
            else if (spawnStopwatch.ElapsedMilliseconds < 60000)
            {
                if (spawnInc.ElapsedMilliseconds > 4000)
                {
                    Zombie zombie = new Zombie(spawnPos, 2);
                    zombie.zombieBitmaps = zombieMovement;
                    zombies.Add(zombie);
                    spawnInc.Reset();
                    spawnInc.Start();

                }
            }
            else if (spawnStopwatch.ElapsedMilliseconds < 100000)
            {
                if (spawnInc.ElapsedMilliseconds > 3000)
                {
                    Zombie zombie = new Zombie(spawnPos, 2);
                    zombie.zombieBitmaps = zombieMovement;
                    zombies.Add(zombie);
                    spawnInc.Reset();
                    spawnInc.Start();

                }
            }
            else if (spawnStopwatch.ElapsedMilliseconds < 160000)
            {
                if (spawnInc.ElapsedMilliseconds > 2000)
                {
                    Zombie zombie = new Zombie(spawnPos, 3);
                    zombie.zombieBitmaps = zombieMovement;
                    zombies.Add(zombie);
                    spawnInc.Reset();
                    spawnInc.Start();

                }
            }
            else
            {
                if (spawnInc.ElapsedMilliseconds > 1000)
                {
                    Zombie zombie = new Zombie(spawnPos, 4);
                    zombie.zombieBitmaps = zombieMovement;
                    zombies.Add(zombie);
                    spawnInc.Reset();
                    spawnInc.Start();

                }
            }

            player.UpdatePosition();

            for (int index = 0; index < zombies.Count; index++)
            {
                zombies[index].targetPosition = player.position;
                zombies[index].UpdatePosition();
            }
        }

        public void Update()
        {
 
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
            player.targetPosition = position;
        }

        public void SetBitmaps(List<CanvasBitmap> playerMoveBitmaps,
                               List<CanvasBitmap> playerReloadBitmaps,
                               List<CanvasBitmap> zombieMove,
                               CanvasBitmap       deadZombie)
        {
            this.zombieMovement = zombieMove;
            this.deadZombie = deadZombie;
            this.player.aimingBitmaps = playerMoveBitmaps;
            this.player.reloadBitmaps = playerReloadBitmaps;
        }


    }
}

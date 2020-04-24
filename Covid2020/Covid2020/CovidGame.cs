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
            this.spawnPos = new Vector2(-100, 0);
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
            if (!GameOver)
            {
                player.Draw(drawingSession);

                for (int i = 0; i < bullets.Count; i++)
                {
                    if (!bullets[i].Valid)
                    {
                        bullets.RemoveAt(i);
                    }
                }

                for (int index = 0; index < zombies.Count; index++)
                {
                    zombies[index].Draw(drawingSession);
                }


            }
        }

        public void Update()
        {
            if (!GameOver)
            {
                if (spawnStopwatch.ElapsedMilliseconds < 30000)
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
                else if (spawnStopwatch.ElapsedMilliseconds < 60000)
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
                else if (spawnStopwatch.ElapsedMilliseconds < 100000)
                {
                    if (spawnInc.ElapsedMilliseconds > 2000)
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
                    if (zombies[index].destroyed)
                    {
                        zombies.RemoveAt(index);
                    }
                    else
                    {
                        zombies[index].targetPosition = player.position;
                        zombies[index].UpdatePosition();

                        double xRangeLow = zombies[index].position.X - 40;
                        double xRangeHigh = zombies[index].position.X + 40;
                        double yRangeLow = zombies[index].position.Y - 40;
                        double yRangeHigh = zombies[index].position.Y + 40;

                        if ((player.position.X > xRangeLow && player.position.X < xRangeHigh) && (player.position.Y > yRangeLow && player.position.Y < yRangeHigh))
                        {
                            player.RegisterDamage();
                        }
                    }
                    
                }

                for (int i = 0; i < bullets.Count; i++)
                {
                    double bulletX = bullets[i].position.X + 20;
                    double bulletY = bullets[i].position.Y + 20;
                    double ZombX = 0;
                    double ZombY = 0;
                    double xRangeLow = bulletX - 40;
                    double xRangeHigh = bulletX + 40;
                    double yRangeLow = bulletY - 40;
                    double yRangeHigh = bulletY + 40;
                    foreach (Zombie zomb in zombies)
                    {
                        ZombX = zomb.position.X + 50;
                        ZombY = zomb.position.Y + 50;

                        if ((ZombX > xRangeLow && ZombX < xRangeHigh) && (ZombY > yRangeLow && ZombY < yRangeHigh))
                        {
                            zomb.RegisterDamage();
                            bullets[i].Valid = false;
                        }
                    }
                }

                if (player.destroyed)
                {
                    GameOver = true;
                }
            }
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

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
using System.Diagnostics;

namespace Covid2020
{
    class CovidGame
    {
        public bool GameOver { get; set; }

        public Player player;
        public int NumberOfKills { get; set; }
        public double NumberShotsFired { get; set; }
        public double NumberShotsRegistered { get; set; }
        public double Accuracy { get; set; }
        private Stopwatch Clock;

        public CovidGame(Vector2 SpawnPos)
        {
            player = new Player(SpawnPos, 5);
            Clock = new Stopwatch();
            Clock.Start();
        }

        public void Draw(CanvasDrawingSession drawingSession)
        {
            Vector2 ScoreText = new Vector2();
            ScoreText.X = 140;
            ScoreText.Y = 10;
            player.Draw(drawingSession);
            drawingSession.DrawText("SCORE", ScoreText, Colors.Black);

            Vector2 ScoreNumber = new Vector2();
            ScoreNumber.X = 230;
            ScoreNumber.Y = 10;
            var currentScore = GetScore().ToString();
            player.Draw(drawingSession);
            drawingSession.DrawText(currentScore, ScoreNumber, Colors.Black);

            Vector2 ClockText = new Vector2();
            ClockText.X = 740;
            ClockText.Y = 10;
            player.Draw(drawingSession);
            drawingSession.DrawText("TIME", ClockText, Colors.Black);

            Vector2 ClockTime = new Vector2();
            ClockTime.X = 800;
            ClockTime.Y = 10;
            var currentTime = Clock.Elapsed.ToString();
            player.Draw(drawingSession);
            drawingSession.DrawText(currentTime, ClockTime, Colors.Black);

            Vector2 Title = new Vector2();
            Title.X = 470;
            Title.Y = 10;
            player.Draw(drawingSession);
            drawingSession.DrawText("COVID2020", Title, Colors.Black);
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
        //Increase score
        public void IncreaseScore()
        {
            NumberOfKills = NumberOfKills + 1;
        }

        // Get score
        public int GetScore()
        {
            return NumberOfKills;
        }

        //Start time
        public void StartClock()
        {

        }

        //Get Time
        public string GetClock()
        {
            return "100.00";
        }
        //End game

        //Display running scoreboard

    }
}

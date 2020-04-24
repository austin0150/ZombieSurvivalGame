using Covid2020.Assets;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Brushes;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media.Core;
using Windows.Media.Playback;
using System.Diagnostics;

namespace Covid2020
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        private CovidGame covidGame;
        private bool PAUSED;
        private int SHOTCOUNTER;
        private CanvasBitmap background;
        private CanvasBitmap greenVirus;
        private CanvasBitmap redVirus;
        private MediaPlayer GUNSHOTMEDIA;
        private Stopwatch timer;

        public Game()
        {
            this.InitializeComponent();

            PAUSED = false;
            SHOTCOUNTER = 0;
            covidGame = new CovidGame(canvas.Size.ToVector2()/2);

            Window.Current.CoreWindow.KeyDown += Canvas_KeyDown;
            Window.Current.CoreWindow.KeyUp += Canvas_KeyUp;
            Window.Current.CoreWindow.PointerPressed += Canvas_PointerPressed;
            Window.Current.CoreWindow.PointerMoved += Canvas_PointerMoved;

            timer = new Stopwatch();
            timer.Start();

            GUNSHOTMEDIA = new MediaPlayer();
            GUNSHOTMEDIA.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/GUNSHOT.mp3", UriKind.RelativeOrAbsolute));

        }

        private void Canvas_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.W)
            {
                covidGame.SetPlayerMoveUp(true);
            }
            if (args.VirtualKey == Windows.System.VirtualKey.A)
            {
                covidGame.SetPlayerMoveLeft(true);
            }
            if (args.VirtualKey == Windows.System.VirtualKey.S)
            {
                covidGame.SetPlayerMoveDown(true);
            }
            if (args.VirtualKey == Windows.System.VirtualKey.D)
            {
                covidGame.SetPlayerMoveRight(true);
            }
            if (args.VirtualKey == Windows.System.VirtualKey.Escape)
            {
                //pause
            }
        }

        private void Canvas_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            if (args.VirtualKey == Windows.System.VirtualKey.W)
            {
                covidGame.SetPlayerMoveUp(false);
            }
            if (args.VirtualKey == Windows.System.VirtualKey.A)
            {
                covidGame.SetPlayerMoveLeft(false);

            }
            if (args.VirtualKey == Windows.System.VirtualKey.S)
            {
                covidGame.SetPlayerMoveDown(false);

            }
            if (args.VirtualKey == Windows.System.VirtualKey.D)
            {
                covidGame.SetPlayerMoveRight(false);

            }
            if (args.VirtualKey == Windows.System.VirtualKey.Escape)
            {
                canvas.Paused = true;
                PAUSED = true;
                PauseMenu_Grid.Visibility = Visibility.Visible;
            }
        }

        private void Canvas_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            if (args.CurrentPoint.Properties.IsLeftButtonPressed)
            {
                if(timer.ElapsedMilliseconds > 1000)
                {
                    SHOTCOUNTER = 5;
                    timer.Restart();
                    GUNSHOTMEDIA.Play();
                }
                
            }
        }

        private void Canvas_PointerMoved(CoreWindow sender, PointerEventArgs args)
        {
            covidGame.SetPlayerAimLocation(args.CurrentPoint.Position.ToVector2());

            
        }

        private void Canvas_Draw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            if (this.covidGame.GameOver == false)
            {
                if(!PAUSED)
                {
                    args.DrawingSession.DrawImage(background,0,50);
                    covidGame.Draw(args.DrawingSession);

                    args.DrawingSession.DrawImage(greenVirus,370,0);
                    args.DrawingSession.DrawImage(redVirus,625,0);

                    Color color = new Color();
                    color.A = 100;
                    color.R = 100;
                    color.G = 0;
                    color.B = 0;
                    
                    Vector2 point1 = covidGame.player.position;
                    point1.X += 50;
                    point1.Y += 30;
                    Vector2 point2 = covidGame.player.pointPosition;
                    //set laser sight
                    args.DrawingSession.DrawLine(point1, point2, color, 5);

                    if(SHOTCOUNTER > 0)
                    {

                        Point point = new Point();
                        point.X = 250;
                        point.Y = 250;
                        Size size = new Size(500, 500);
                        Rect rect = new Rect(point,size);
                        CanvasSolidColorBrush brush = new CanvasSolidColorBrush(args.DrawingSession,Colors.WhiteSmoke);
                        brush.Opacity = (float)0.7;
                        args.DrawingSession.DrawRectangle(rect, brush,1000);
                        SHOTCOUNTER--;
                    }
                }
            }
        }

        private void Canvas_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResources(sender).AsAsyncAction());
        }

        async Task CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender)
        {
            background = await CanvasBitmap.LoadAsync(sender, "Assets/floor.jpg");
            greenVirus = await CanvasBitmap.LoadAsync(sender, "Assets/Green_Virus.png");
            redVirus = await CanvasBitmap.LoadAsync(sender, "Assets/Red_Virus.png");

            List <CanvasBitmap> aimAssets = new List<CanvasBitmap>();
            foreach (string aimAsset in GameAssets.PlayerAiming)
            {
                aimAssets.Add(await CanvasBitmap.LoadAsync(sender, aimAsset));
            }

            // need to add the bitmaps for reloading
            covidGame.SetBitmaps(aimAssets, new List<CanvasBitmap>());
        }

        private void Canvas_Update(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            if(!PAUSED)
            {
                covidGame.Update();
            }
            
        }

        private void PauseMenuResume_Button_Click(object sender, RoutedEventArgs e)
        {
            canvas.Paused = false;
            PAUSED = false;
            PauseMenu_Grid.Visibility = Visibility.Collapsed;
        }

        private void PauseMenuExit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void PauseMenuReset_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Game));
        }
    }
}

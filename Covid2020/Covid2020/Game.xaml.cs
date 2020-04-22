using Covid2020.Assets;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Covid2020
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        private CovidGame covidGame;
        private bool PAUSED;

        public Game()
        {
            this.InitializeComponent();

            PAUSED = false;
            covidGame = new CovidGame(canvas.Size.ToVector2()/2);

            Window.Current.CoreWindow.KeyDown += Canvas_KeyDown;
            Window.Current.CoreWindow.KeyUp += Canvas_KeyUp;
            Window.Current.CoreWindow.PointerPressed += Canvas_PointerPressed;
            Window.Current.CoreWindow.PointerMoved += Canvas_PointerMoved;
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
                PAUSED = true;
                PauseMenu_Grid.Visibility = Visibility.Visible;
            }
        }

        private void Canvas_PointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            if (args.CurrentPoint.Properties.IsLeftButtonPressed)
            {
                // shoot
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
                    covidGame.Draw(args.DrawingSession);
                }
                
            }
        }

        private void Canvas_CreateResources(CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
        {
            args.TrackAsyncAction(CreateResources(sender).AsAsyncAction());
        }

        async Task CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender)
        {
            List<CanvasBitmap> aimAssets = new List<CanvasBitmap>();
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

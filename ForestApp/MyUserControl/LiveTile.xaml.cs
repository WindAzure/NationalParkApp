using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyUserControl
{
    public sealed partial class LiveTile : UserControl ,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            var eventHandler=PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public delegate void LiveTileEvent(object sender, object e);
        public event LiveTileEvent AnimationComplete = null;

        private DispatcherTimer Timmer = new DispatcherTimer();

        public LiveTile()
        {
            this.InitializeComponent();
            this.DataContext=this;
            Timmer.Interval = TimeSpan.FromSeconds(3);
            Timmer.Tick += Timmer_Tick;
            Timmer.Start();
        }

        void Timmer_Tick(object sender, object e)
        {
            MoveBoard();
        }

        private String _img1Path = null;
        public String Img1Path
        {
            set
            {
                SetProperty(ref this._img1Path, value, "Img1Path");
            }
            get
            {
                return _img1Path;
            }
        }

        private String _img2Path = null;
        public String Img2Path
        {
            set
            {
                SetProperty(ref this._img2Path, value, "Img2Path");
            }
            get
            {
                return _img2Path;
            }
        }

        private String _img3Path = null;
        public String Img3Path
        {
            set
            {
                SetProperty(ref this._img3Path, value, "Img3Path");
            }
            get
            {
                return _img3Path;
            }
        }

        private bool firstRun = false;

        private void ReSetBoard()
        {
            String temp = Img1Path;
            Img1Path = Img2Path;
            Img2Path = Img3Path;
            Img3Path = temp;
        }

        private void MoveBoard()
        {
            if (firstRun)
            {
                ReSetBoard();
            }

            DoubleAnimation ani1 = new DoubleAnimation();
            ani1.From = 0;
            ani1.To = -480;
            ani1.Duration = new Duration(TimeSpan.FromSeconds(1));
            ani1.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(ani1, img1Board);
            Storyboard.SetTargetProperty(ani1, "img1Borad.X");
            
            DoubleAnimation ani2 = new DoubleAnimation();
            ani2.From = 0;
            ani2.To = -480;
            ani2.Duration = new Duration(TimeSpan.FromSeconds(1));
            ani2.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(ani2, img2Board);
            Storyboard.SetTargetProperty(ani2, "img2Board.X");
            
            Storyboard board = new Storyboard();
            board.Children.Add(ani1);
            board.Children.Add(ani2);
            board.Begin();
            firstRun = true;
            board.Completed += board_Completed;
        }

        void board_Completed(object sender, object e)
        {
            if (AnimationComplete != null)
            {
                AnimationComplete(sender, e);
            }
        }

    }
}

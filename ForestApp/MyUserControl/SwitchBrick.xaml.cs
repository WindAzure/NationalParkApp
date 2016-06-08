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
    public interface ItemMember
    {
        ObservableCollection<String> ImgPath { set; get; }
        String Title { set; get; }
        String Info { set; get; }
    }

    public partial class SwitchBrick : UserControl,INotifyPropertyChanged
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
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<Object> _collection = new ObservableCollection<Object>();
        public ObservableCollection<Object> Collection
        {
            set
            {
                SetProperty(ref this._collection, value, "Collection");
                BindingData();
            }
            get
            {
                return _collection;
            }
        }
        
        private void BindingData()
        {
            Random rnd = new Random();
            int index1 = rnd.Next(_collection.Count());

            if (_collection[index1] is ItemMember)
            {
                ItemMember item1 = _collection[index1] as ItemMember;
                Img1Path = item1.ImgPath[0];
                Title1 = item1.Title;
                Info1 = item1.Info;

                int index2 = rnd.Next(_collection.Count());
                while (index1 == index2)
                {
                    index2 = rnd.Next(_collection.Count());
                }


                ItemMember item2 = _collection[index2] as ItemMember;
                Img2Path = item2.ImgPath[0];
                Title2 = item2.Title;
                Info2 = item2.Info;
            }
            
        }

        private bool firstRun = false;

        private String _title1 = null;
        public String Title1 
        {
            set
            {
                SetProperty(ref this._title1, value, "Title1");
            }
            get 
            {
                return _title1;
            }
        }

        private String _info1 = null;
        public String Info1
        {
            set
            {
                SetProperty(ref this._info1, value, "Info1");
            }
            get
            {
                return _info1;
            }
        }

        private String _title2 = null;
        public String Title2
        {
            set
            {
                SetProperty(ref this._title2, value, "Title2");
            }
            get
            {
                return _title2;
            }
        }

        private String _info2 = null;
        public String Info2
        {
            set
            {
                SetProperty(ref this._info2, value, "Info2");
            }
            get
            {
                return _info2;
            }
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

        private bool _isEnable = false;
        public bool IsEnable 
        {
            set
            {
                SetProperty(ref this._isEnable, value, "IsEnable");
            }
            get
            {
                return _isEnable;
            }
        }

        private int SwitchTime = 0;

        public SwitchBrick()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
        
        private void SwitchInfoUp()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = 0;
            animation.To = -145;
            animation.Duration = new Duration(TimeSpan.FromSeconds(2.0));
            animation.EasingFunction=new QuadraticEase(){EasingMode=EasingMode.EaseOut};
            Storyboard.SetTarget(animation, forestBoard);
            Storyboard.SetTargetProperty(animation, "forestBoard.Y");
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin();
            storyboard.Completed += SwitchInfoUp_Completed;
        }

        private void SwitchInfoUp_Completed(object sender, object e)
        {
            SwitchInfoDown();
        }

        private void SwitchInfoDown()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = -145;
            animation.To = 0;
            animation.Duration = new Duration(TimeSpan.FromSeconds(2.0));
            animation.EasingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(animation,forestBoard);
            Storyboard.SetTargetProperty(animation, "forestBoard.Y");
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Completed+=SwitchInfoDown_Complete;
            storyboard.Begin();
        }

        private void SwitchInfoDown_Complete(object sender,object e)
        {
            SwitchTime++;
            if (SwitchTime > 3)
            {
                SwitchTime = 0;
                MoveImgBoard();
            }
            else
            {
                SwitchInfoUp(); 
            }
        }

        private void ReSetImgBoard()
        {
            Img1Path = Img2Path;
            Info1 = Info2;
            Title1 = Title2;

            Random rnd = new Random();
            ItemMember item;
            do
            {
                int index = rnd.Next(Collection.Count());
                item=Collection[index] as ItemMember;
            }while(item.ImgPath[0]==Img2Path);
            Img2Path = item.ImgPath[0];
            Info2 = item.Info;
            Title2 = item.Title;
        }

        private void MoveImgBoard()
        {
            if (firstRun)
            {
                ReSetImgBoard();
            }

            DoubleAnimation ani1 = new DoubleAnimation();
            ani1.From = 0;
            ani1.To = -500;
            ani1.Duration = new Duration(TimeSpan.FromSeconds(3));
            ani1.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(ani1, img1Board);
            Storyboard.SetTargetProperty(ani1, "img1Borad.X");

            DoubleAnimation ani2 = new DoubleAnimation();
            ani2.From = 0;
            ani2.To = -500;
            ani2.Duration = new Duration(TimeSpan.FromSeconds(3));
            ani2.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(ani2, img2Board);
            Storyboard.SetTargetProperty(ani2, "img2Board.X");

            DoubleAnimation ani3 = new DoubleAnimation();
            ani3.From = 0;
            ani3.To = -500;
            ani3.Duration = new Duration(TimeSpan.FromSeconds(3));
            ani3.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(ani3, TitleBoard1);
            Storyboard.SetTargetProperty(ani3, "TitleBoard1.X");

            DoubleAnimation ani4 = new DoubleAnimation();
            ani4.From = 0;
            ani4.To = -500;
            ani4.Duration = new Duration(TimeSpan.FromSeconds(3));
            ani4.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(ani4, TitleBoard2);
            Storyboard.SetTargetProperty(ani4, "TitleBoard2.X");

            DoubleAnimation ani5 = new DoubleAnimation();
            ani5.From = 0;
            ani5.To = -500;
            ani5.Duration = new Duration(TimeSpan.FromSeconds(3));
            ani5.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(ani5, InfoBoard1);
            Storyboard.SetTargetProperty(ani5, "InfoBoard1.X");

            DoubleAnimation ani6 = new DoubleAnimation();
            ani6.From = 0;
            ani6.To = -500;
            ani6.Duration = new Duration(TimeSpan.FromSeconds(3));
            ani6.EasingFunction = new CircleEase() { EasingMode = EasingMode.EaseOut };
            Storyboard.SetTarget(ani6, InfoBoard2);
            Storyboard.SetTargetProperty(ani6, "InfoBoard2.X");

            Storyboard board = new Storyboard();
            board.Children.Add(ani1);
            board.Children.Add(ani2);
            board.Children.Add(ani3);
            board.Children.Add(ani4);
            board.Children.Add(ani5);
            board.Children.Add(ani6);
            board.Completed+=MoveImgBoard_Complete;
            board.Begin();
            firstRun = true;
        }

        private void MoveImgBoard_Complete(object sender, object e)
        {
            SwitchInfoUp();
        }

        public void Start()
        {
            SwitchInfoUp();
        }
    }
}

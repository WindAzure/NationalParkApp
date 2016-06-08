using ForestApp.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ForestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ParkSource _source = null;
        public ParkSource Model
        {
            get
            {
                if (_source == null)
                {
                    _source = new ParkSource();
                }
                return _source;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = Model;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            await Model.LoadDataSource();
            Brick.Collection = Model.Parks;

            await Task.Delay(TimeSpan.FromSeconds(2));
            Brick.Start();
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace LikeARock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImagePage : Page
    {
        private SolAndImage mCurrentContext = new SolAndImage();
        private int mCurrentIndex;

        public ImagePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var lCurrentContext = e.Parameter;
            mCurrentContext = (SolAndImage)lCurrentContext;
            BindObjects();
            mCurrentIndex = mCurrentContext.Images.IndexOf(mCurrentContext.SelectedImage);
        }

        private void BindObjects()
        {
            uSelectedImageDisplay.DataContext = mCurrentContext.SelectedImage;
            uSolTxt.Text = mCurrentContext.Sol.ToString();
            uTeamTxt.Text = mCurrentContext.SelectedImage.Contributor;
            uCameraTypeTxt.Text = mCurrentContext.SelectedImage.CameraModelType;
            uInstmntTxt.Text = mCurrentContext.SelectedImage.Instrument;
            uUtcTxt.Text = mCurrentContext.SelectedImage.Utc;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), mCurrentContext);
        }

        private void btnPrevImage_Click(object sender, RoutedEventArgs e)
        {
            if (mCurrentIndex != 0)
            {
                mCurrentIndex--;
                mCurrentContext.SelectedImage = mCurrentContext.Images[mCurrentIndex];
                BindObjects();
            }
        }

        private void btnNextImage_Click(object sender, RoutedEventArgs e)
        {
            //mCurrentIndex < mCurrentContext.Images.Count - 1
        }
    }
}

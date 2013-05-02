using LikeARock.DataModels;
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
        private MarsImage mSelectedImage = new MarsImage();

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
            var lSelectedImage = e.Parameter;
            mSelectedImage = (MarsImage)lSelectedImage;
            SelectedImageDisplay.DataContext = lSelectedImage;
            uSolTxt.Text = mSelectedImage.Sol.ToString();
            uTeamTxt.Text = mSelectedImage.Contributor;
            uCameraTypeTxt.Text = mSelectedImage.CameraModelType;
            uInstmntTxt.Text = mSelectedImage.Instrument;
            uUtcTxt.Text = mSelectedImage.Utc;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrevImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNextImage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

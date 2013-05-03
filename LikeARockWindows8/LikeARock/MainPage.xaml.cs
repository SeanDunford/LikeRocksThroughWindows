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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Windows.Data.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using System.Runtime.Serialization.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace LikeARock
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static string mNASAURL = "http://mars.jpl.nasa.gov/msl-raw-images/image/image_manifest.json";
        private Manifest mManifest;
        private SolImages mCurrentSol;
        private SolAndImage mCurrentContext;

        public MainPage()
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
            if (lCurrentContext != "")
            {
                mCurrentContext = (SolAndImage)lCurrentContext;
            }
            GetJSON(mNASAURL);

        }
        /*I only want to serialize the SolImages Object when a specific Sol Day is requested to be viewed. 
         * 
         */ 
        private async void GetJSON(string aNASAURL)
        {
            var client = new HttpClient();
            var lResponse = await client.GetStringAsync(aNASAURL);
            var lManifest = JObject.Parse(lResponse);
            mManifest = lManifest.ToObject<Manifest>();
            var lSols = lManifest["sols"] as JArray;
            SolDaySelect.ItemsSource = mManifest.Sols;
            SolDaySelect.DisplayMemberPath = "Sol";
            if (mCurrentContext != null)
            {
                SolDay lSolDay = new SolDay();
                foreach (SolDay lSol in mManifest.Sols)
                {
                    if (lSol.Sol == mCurrentContext.Sol)
                    {
                        lSolDay = lSol;
                    }

                }
                if (lSolDay.Catalog_url != "")
                {
                    string lCatalogUrl = lSolDay.Catalog_url;
                    BindSolDay(lCatalogUrl);
                }
            }
            else
            {
                int lIndex = mManifest.Sols.Count;
                lIndex--;
                string lCatalogUrl = mManifest.Sols[lIndex].Catalog_url;
                BindSolDay(lCatalogUrl);
            }
            
        }

        private async void BindSolDay(string aCatalogURL)
        {
            var client = new HttpClient();
            var lResponse = await client.GetStringAsync(aCatalogURL);
            var lSolDay = JObject.Parse(lResponse);
            mCurrentSol = lSolDay.ToObject<SolImages>();
            SolGridView.DataContext = mCurrentSol.Images;
            uSolTxt.Text = mCurrentSol.Sol.ToString();
        }

        private Manifest Deserialize(Stream aResponse)
        {
            return new Manifest();
        }

        private void SolDaySelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int lSelectedSol = (int)SolDaySelect.SelectedIndex;
            string lCatalogUrl = mManifest.Sols[lSelectedSol].Catalog_url;
            BindSolDay(lCatalogUrl);
        }

        private void SolGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var lSelectedImage = SolGridView.SelectedItem;
            SolAndImage lCurrentContext = new SolAndImage();
            lCurrentContext.SelectedImage = (MarsImage)lSelectedImage;
            lCurrentContext.Images = mCurrentSol.Images;
            lCurrentContext.Sol = mCurrentSol.Sol;
            this.Frame.Navigate(typeof(ImagePage), lCurrentContext);
        }

        private void btnLatestSol_Click(object sender, RoutedEventArgs e)
        {
            int lCount = mManifest.Sols.Count;
            BindSolDay(mManifest.Sols[lCount-1].Catalog_url);
        }
    }


}

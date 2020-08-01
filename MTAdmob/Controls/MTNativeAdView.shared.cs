//using System;
//using Xamarin.Forms;

//namespace MarcTron.Plugin.Controls
//{
//    // ReSharper disable once InconsistentNaming
//    public class MTNativeAdView : View
//    {
//        public event EventHandler AdsClicked;
//        public event EventHandler AdsClosed;
//        public event EventHandler AdsImpression;
//        public event EventHandler AdsOpened;
//        public event EventHandler AdsFailedToLoad;
//        public event EventHandler AdsLeftApplication;
//        public event EventHandler AdsLoaded;

//        public static readonly BindableProperty AdsIdProperty = BindableProperty.Create("AdsId", typeof(string), typeof(MTAdView));

//        public string AdsId
//        {
//            get => (string)GetValue(AdsIdProperty);
//            set => SetValue(AdsIdProperty, value);
//        }

//        public static readonly BindableProperty PersonalizedAdsProperty = BindableProperty.Create("PersonalizedAds", typeof(bool), typeof(MTAdView));

//        public bool? PersonalizedAds
//        {
//            get => (bool?)GetValue(PersonalizedAdsProperty);
//            set => SetValue(PersonalizedAdsProperty, value);
//        }

//        internal void AdClicked(object sender, EventArgs e)
//        {
//            AdsClicked?.Invoke(sender,e);
//        }

//        internal void AdClosed(object sender, EventArgs e)
//        {
//            AdsClosed?.Invoke(sender, e);
//        }

//        internal void AdImpression(object sender, EventArgs e)
//        {
//            AdsImpression?.Invoke(sender, e);
//        }

//        internal void AdOpened(object sender, EventArgs e)
//        {
//            AdsOpened?.Invoke(sender, e);
//        }

//        internal void AdFailedToLoad(object sender, EventArgs e)
//        {
//            AdsFailedToLoad?.Invoke(sender, e);
//        }

//        internal void AdLeftApplication(object sender, EventArgs e)
//        {
//            AdsLeftApplication?.Invoke(sender, e);
//        }

//        internal void AdLoaded(object sender, EventArgs e)
//        {
//            AdsLoaded?.Invoke(sender, e);
//        }
//    }
//}

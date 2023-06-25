using System;
using Android.Content;
using Android.Gms.Ads;
using Android.Widget;
using MarcTron.Plugin.Controls;
using MarcTron.Plugin.Listeners;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MarcTron.Plugin.Renderers;
using MarcTron.Plugin.Extra;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(MTAdView), typeof(AdViewRenderer))]

namespace MarcTron.Plugin.Renderers
{
    public class AdViewRenderer : ViewRenderer<MTAdView, AdView>
    {
        string _adUnitId = string.Empty;
        AdView _adView;
        private MTAdView currentAdView;
        private MyAdBannerListener listener;
        public AdViewRenderer(Context context) : base(context)
        {
        }

        private void CreateNativeControl(MTAdView myMtAdView, string adsId, BannerSize adSize)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_adView != null)
                return;

            currentAdView = myMtAdView;

            _adUnitId = !string.IsNullOrEmpty(adsId) ? adsId : CrossMTAdmob.Current.AdsId;

            if (string.IsNullOrEmpty(_adUnitId))
            {
                Console.WriteLine("You must set the adsID before using it");
            }

            CreateAdView();
        }

        private void CreateAdView()
        {
            if (listener != null)
            {
                listener.AdClicked -= currentAdView.AdClicked;
                listener.AdClosed -= currentAdView.AdClosed;
                listener.AdImpression -= currentAdView.AdImpression;
                listener.AdOpened -= currentAdView.AdOpened;
                listener.AdFailedToLoad -= currentAdView.AdFailedToLoad;
                listener.AdLoaded -= currentAdView.AdLoaded;
                listener.AdLoaded -= Listener_AdLoaded;
                listener.AdSwiped -= currentAdView.AdSwiped;
                listener = null;
            }

            listener = new MyAdBannerListener();

            listener.AdClicked += currentAdView.AdClicked;
            listener.AdClosed += currentAdView.AdClosed;
            listener.AdImpression += currentAdView.AdImpression;
            listener.AdOpened += currentAdView.AdOpened;
            listener.AdFailedToLoad += Listener_AdFailedToLoad;
            listener.AdFailedToLoad += currentAdView.AdFailedToLoad;
            listener.AdLoaded += Listener_AdLoaded;
            listener.AdLoaded += currentAdView.AdLoaded;
            listener.AdSwiped += currentAdView.AdSwiped;

           _adView = new AdView(Context)
            {
                AdSize = GetAdSize(currentAdView.AdSize),
                AdUnitId = _adUnitId,
                AdListener = listener,
                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
            };
        }

        private void Listener_AdFailedToLoad(object sender, MTEventArgs e)
        {
            try
            {
                if (currentAdView.AutoSize)
                {
                    var size = _adView.AdSize;
                    currentAdView.HeightRequest = 0;
                }
            }
            catch (Exception ecc)
            {
                Console.WriteLine($"Error in Ad failed to load: {ecc.Message}");
            }
        }

        private void Listener_AdLoaded(object sender, EventArgs e)
        {
            try
            {
                if (currentAdView.AutoSize)
                {
                    var size = _adView.AdSize;
                    currentAdView.HeightRequest = size.Height;
                }
            }
            catch (Exception ecc)
            {
                Console.WriteLine($"Error in Ad loaded: {ecc.Message}");
            }
        }
        
        protected override void OnElementChanged(ElementChangedEventArgs<MTAdView> e)
        {
            base.OnElementChanged(e);
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (Control == null)
            {
                CreateNativeControl(e.NewElement, e.NewElement.AdsId, e.NewElement.AdSize);
                SetNativeControl(_adView);
                LoadAds();
            }
        }

        private void LoadAds()
        {
            var requestBuilder = MTAdmobImplementation.GetRequest();
            _adView.LoadAd(requestBuilder.Build());
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if ((e.PropertyName.Equals("Width") || e.PropertyName.Equals("AdSize")) && _adView != null)
            {
                CreateAdView();
                SetNativeControl(_adView);
                LoadAds();
            }
        }

        private AdSize GetAdSize(BannerSize size)
        {
            switch (size)
            {
                case BannerSize.Banner:
                    return AdSize.Banner;
                case BannerSize.LargeBanner:
                    return AdSize.LargeBanner;
                case BannerSize.MediumRectangle:
                    return AdSize.MediumRectangle;
                case BannerSize.FullBanner:
                    return AdSize.FullBanner;
                case BannerSize.Leaderboard:
                    return AdSize.Leaderboard;
                case BannerSize.AnchoredAdaptive:
                    return GetAdaptiveAdSize(true);
                case BannerSize.InlineAdaptive:
                    return GetAdaptiveAdSize(false);
                case BannerSize.Smart:
                    return AdSize.SmartBanner;
                default:
                    return GetAdaptiveAdSize(true);
            }
        }

        private AdSize GetAdaptiveAdSize(bool isAnchored)
        {
            //var outMetrics = new DisplayMetrics();
            //var display = Context?.GetSystemService("window").JavaCast<IWindowManager>();

            //display?.DefaultDisplay?.GetMetrics(outMetrics);

            //float widthPixels = outMetrics.WidthPixels;
            //float density = outMetrics.Density;

            //int adWidth = (int)(widthPixels / density);

            //var w = currentAdView.Width;
            int adWidth = (int)currentAdView.Width;

            if (isAnchored)
                return AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSize(Context, adWidth);
            else
                return AdSize.GetCurrentOrientationInlineAdaptiveBannerAdSize(Context, adWidth);
        }
    }
}
using System;
using Android.Content;
using Android.Gms.Ads;
using Android.Widget;
using MarcTron.Plugin.Controls;
using MarcTron.Plugin.Listeners;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MarcTron.Plugin.Renderers;
using Android.Util;
using Android.Runtime;
using Android.Views;

[assembly: ExportRenderer(typeof(MTAdView), typeof(AdViewRenderer))]

namespace MarcTron.Plugin.Renderers
{
    public class AdViewRenderer : ViewRenderer<MTAdView, AdView>
    {
        string _adUnitId = string.Empty;
        AdView _adView;

        public AdViewRenderer(Context context) : base(context)
        {
        }

        private void CreateNativeControl(MTAdView myMtAdView, string adsId)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_adView != null)
                return;

            _adUnitId = !string.IsNullOrEmpty(adsId) ? adsId : CrossMTAdmob.Current.AdsId;

            if (string.IsNullOrEmpty(_adUnitId))
            {
                Console.WriteLine("You must set the adsID before using it");
            }

            var listener = new MyAdBannerListener();

            listener.AdClicked += myMtAdView.AdClicked;
            listener.AdClosed += myMtAdView.AdClosed;
            listener.AdImpression += myMtAdView.AdImpression;
            listener.AdOpened += myMtAdView.AdOpened;
            listener.AdFailedToLoad += myMtAdView.AdFailedToLoad;
            listener.AdLoaded += myMtAdView.AdLoaded;
            listener.AdLeftApplication += myMtAdView.AdLeftApplication;

            _adView = new AdView(Context)
            {
                AdSize = GetAdSize(),
                AdUnitId = _adUnitId,
                AdListener = listener,
                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
            };

            var requestBuilder = MTAdmobImplementation.GetRequest();
            _adView.LoadAd(requestBuilder.Build());
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MTAdView> e)
        {
            base.OnElementChanged(e);
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (Control == null)
            {
                CreateNativeControl(e.NewElement, e.NewElement.AdsId);
                SetNativeControl(_adView);
            }
        }
        
        private AdSize GetAdSize()
        {
            var outMetrics = new DisplayMetrics();
            var display = Context?.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            display?.DefaultDisplay?.GetMetrics(outMetrics);

            float widthPixels = outMetrics.WidthPixels;
            float density = outMetrics.Density;

            int adWidth = (int)(widthPixels / density);

            return AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSize(Context, adWidth);
        }
    }
}
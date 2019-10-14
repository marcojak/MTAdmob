using System;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Widget;
using Google.Ads.Mediation.Admob;
using MarcTron.Plugin.Controls;
using MarcTron.Plugin.Listeners;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MarcTron.Plugin.Renderers;

[assembly: ExportRenderer(typeof(MTAdView), typeof(AdViewRenderer))]

namespace MarcTron.Plugin.Renderers
{
    public class AdViewRenderer : ViewRenderer<MTAdView, AdView>
    {
        string _adUnitId = string.Empty;
        readonly AdSize _adSize = AdSize.SmartBanner;
        AdView _adView;

        public AdViewRenderer(Context context) : base(context)
        {
        }

        private void CreateNativeControl(MTAdView myMtAdView, string adsId, bool? personalizedAds)
        {
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

            _adView = new AdView(Context)
            {
                AdSize = _adSize,
                AdUnitId = _adUnitId,
                AdListener = listener,
                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
            };

            var requestBuilder = new AdRequest.Builder();
            if (CrossMTAdmob.Current.TestDevices != null)
            {
                foreach (var testDevice in CrossMTAdmob.Current.TestDevices)
                {
                    requestBuilder.AddTestDevice(testDevice);
                }
            }

            if ((personalizedAds.HasValue && personalizedAds.Value) || CrossMTAdmob.Current.UserPersonalizedAds)
            {
                _adView.LoadAd(requestBuilder.Build());
            }
            else
            {
                Bundle bundleExtra = new Bundle();
                bundleExtra.PutString("npa", "1");

                _adView.LoadAd(requestBuilder
                    .AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), bundleExtra)
                    .Build());
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MTAdView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeControl(e.NewElement, e.NewElement.AdsId, e.NewElement.PersonalizedAds);
                SetNativeControl(_adView);
            }
        }
    }
}
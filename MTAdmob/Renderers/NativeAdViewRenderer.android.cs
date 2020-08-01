//using System;
//using Android.Content;
//using Android.Gms.Ads;
//using Android.Gms.Ads.Formats;
//using Android.OS;
//using Android.Widget;
//using Google.Ads.Mediation.Admob;
//using MarcTron.Plugin.Controls;
//using MarcTron.Plugin.Listeners;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
//using MarcTron.Plugin.Renderers;

//[assembly: ExportRenderer(typeof(MTNativeAdView), typeof(AdViewRenderer))]

//namespace MarcTron.Plugin.Renderers
//{
//    public class NativeAdViewRenderer : ViewRenderer<MTNativeAdView, AdView>
//    {
//        string _adUnitId = string.Empty;
//        readonly AdSize _adSize = AdSize.SmartBanner;
//        AdView _adView;

//        public NativeAdViewRenderer(Context context) : base(context)
//        {
//        }

//        private void CreateNativeControl(MTNativeAdView myMtAdView, string adsId, bool? personalizedAds)
//        {
//            if (_adView != null)
//                return;

//            _adUnitId = !string.IsNullOrEmpty(adsId) ? adsId : CrossMTAdmob.Current.AdsId;

//            if (string.IsNullOrEmpty(_adUnitId))
//            {
//                Console.WriteLine("You must set the adsID before using it");
//            }

//            var listener = new MyAdBannerListener();

//            listener.AdClicked += myMtAdView.AdClicked;
//            listener.AdClosed += myMtAdView.AdClosed;
//            listener.AdImpression += myMtAdView.AdImpression;
//            listener.AdOpened += myMtAdView.AdOpened;
//            listener.AdFailedToLoad += myMtAdView.AdFailedToLoad;
//            listener.AdLoaded += myMtAdView.AdLoaded;
//            listener.AdLeftApplication += myMtAdView.AdLeftApplication;

//            var adLoader = new AdLoader.Builder(Context, _adUnitId).ForContentAd().WithAdListener(listener).Build();


//            _adView = new AdView(Context)
//            {
//                AdSize = _adSize,
//                AdUnitId = _adUnitId,
//                AdListener = listener,
//                LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent)
//            };

//            var requestBuilder = new AdRequest.Builder();
//            if (CrossMTAdmob.Current.TestDevices != null)
//            {
//                foreach (var testDevice in CrossMTAdmob.Current.TestDevices)
//                {
//                    requestBuilder.AddTestDevice(testDevice);
//                }
//            }

//            if ((personalizedAds.HasValue && personalizedAds.Value) || CrossMTAdmob.Current.UserPersonalizedAds)
//            {
//                adLoader.LoadAd(requestBuilder.Build());
//            }
//            else
//            {
//                Bundle bundleExtra = new Bundle();
//                bundleExtra.PutString("npa", "1");

//                adLoader.LoadAd(requestBuilder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), bundleExtra).Build());
//            }
//        }

//        protected override void OnElementChanged(ElementChangedEventArgs<MTNativeAdView> e)
//        {
//            base.OnElementChanged(e);
//            if (Control == null)
//            {
//                CreateNativeControl(e.NewElement, e.NewElement.AdsId, e.NewElement.PersonalizedAds);
//                SetNativeControl(_adView);
//            }
//        }
//    }
//}
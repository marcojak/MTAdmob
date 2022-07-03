using System;
using CoreGraphics;
using Google.MobileAds;
using MarcTron.Plugin.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MarcTron.Plugin.Renderers;

[assembly: ExportRenderer(typeof(MTAdView), typeof(AdViewRenderer))]

namespace MarcTron.Plugin.Renderers
{
    public class AdViewRenderer : ViewRenderer<MTAdView, BannerView>
    {
        string _adUnitId = string.Empty;
        BannerView _adView;

        private void CreateNativeControl(UIViewController controller, MTAdView myMtAdView, string adsId, /*bool? personalizedAds,*/ bool needToRefreshAdView)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_adView != null && !needToRefreshAdView)
                return;

            _adUnitId = !string.IsNullOrEmpty(adsId) ? adsId : CrossMTAdmob.Current.AdsId;

            if (string.IsNullOrEmpty(_adUnitId))
            {
                Console.WriteLine("You must set the adsID before using it");
            }

            _adView = new BannerView(AdSizeCons.SmartBannerPortrait,
                new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - AdSizeCons.Banner.Size.Height))
            {
                AdUnitId = _adUnitId,
                RootViewController = controller
            };

            _adView.AdReceived += myMtAdView.AdImpression;
            //_adView.WillLeaveApplication += myMtAdView.AdClicked;
            _adView.WillPresentScreen += myMtAdView.AdClicked;

            var request = MTAdmobImplementation.GetRequest();

            //bool addExtra = false;
            //var dict = new Dictionary<string,string>();
            //if ((!personalizedAds.HasValue || !personalizedAds.Value) || !CrossMTAdmob.Current.UserPersonalizedAds)
            //{
            //    dict.Add(new NSString("npa"), new NSString("1"));
            //    addExtra = true;
            //}

            //if (CrossMTAdmob.Current.UseRestrictedDataProcessing)
            //{
            //    dict.Add(new NSString("rdp"), new NSString("1"));
            //    addExtra = true;
            //}

            //var request = GetRequest();
            //if (addExtra)
            //{
            //    var extras = new Extras
            //    {
            //        AdditionalParameters = NSDictionary.FromObjectsAndKeys(dict.Values.ToArray(), dict.Keys.ToArray())
            //    };
            //    request.RegisterAdNetworkExtras(extras);
            //}

            _adView.LoadRequest(request);
        }

        Request GetRequest()
        {
            var request = Request.GetDefaultRequest();
            return request;
        }

        UIViewController GetVisibleViewController()
        {
            var rootController = UIApplication.SharedApplication.Delegate?.GetWindow()?.RootViewController;

            if (rootController == null)
                return null;

            if (rootController.PresentedViewController == null)
                return rootController;

            if (rootController.PresentedViewController is UINavigationController controller)
            {
                return controller.VisibleViewController;
            }

            if (rootController.PresentedViewController is UITabBarController barController)
            {
                return barController.SelectedViewController;
            }

            return rootController.PresentedViewController;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<MTAdView> e)
        {
            base.OnElementChanged(e);

            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_adView != null)
                return;

            if (Control == null)
            {
                UIViewController controller = GetVisibleViewController();
                if (controller != null)
                {
                    if (e.NewElement != null)
                        CreateNativeControl(controller, e.NewElement, e.NewElement.AdsId, false);
                    else if (e.OldElement != null)
                        CreateNativeControl(controller, e.OldElement, e.OldElement.AdsId, true);
                    else
                        return;
                    SetNativeControl(_adView);
                }
            }
        }
    }
}
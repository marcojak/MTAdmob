using System;
using CoreGraphics;
using Foundation;
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

        private void CreateNativeControl(UIViewController controller, MTAdView myMtAdView, string adsId, bool? personalizedAds, bool needToRefreshAdView)
        {
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

            if ((personalizedAds.HasValue && personalizedAds.Value) || CrossMTAdmob.Current.UserPersonalizedAds)
            {
                _adView.LoadRequest(GetRequest());
            }
            else
            {
                var request = GetRequest();
                var extras = new Extras {AdditionalParameters = NSDictionary.FromObjectAndKey(new NSString("1"), new NSString("npa"))};
                request.RegisterAdNetworkExtras(extras);
                _adView.LoadRequest(request);
            }
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

            if (_adView != null)
                return;

            if (Control == null)
            {
                UIViewController controller = GetVisibleViewController();
                if (controller != null)
                {
                    if (e.NewElement != null)
                        CreateNativeControl(controller, e.NewElement, e.NewElement.AdsId, e.NewElement.PersonalizedAds, false);
                    else if (e.OldElement != null)
                        CreateNativeControl(controller, e.OldElement, e.OldElement.AdsId, e.OldElement.PersonalizedAds, true);
                    else
                        return;
                    SetNativeControl(_adView);
                }
            }
        }
    }
}
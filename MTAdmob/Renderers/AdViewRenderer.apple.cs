using System;
using CoreGraphics;
using Google.MobileAds;
using MarcTron.Plugin.Controls;
using MarcTron.Plugin.Extra;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using MarcTron.Plugin.Renderers;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(MTAdView), typeof(AdViewRenderer))]

namespace MarcTron.Plugin.Renderers
{
    public class AdViewRenderer : ViewRenderer<MTAdView, BannerView>
    {
        string _adUnitId = string.Empty;
        BannerView _adView;
        private MTAdView currentAdView;

        private void CreateNativeControl(UIViewController controller, MTAdView myMtAdView, string adsId, BannerSize adSize,
            bool needToRefreshAdView)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_adView != null && !needToRefreshAdView)
                return;

            currentAdView = myMtAdView;

            _adUnitId = !string.IsNullOrEmpty(adsId) ? adsId : CrossMTAdmob.Current.AdsId;

            if (string.IsNullOrEmpty(_adUnitId))
            {
                Console.WriteLine("You must set the adsID before using it");
            }

            CreateAdView(controller);
        }

        private void CreateAdView(UIViewController controller)
        {
            if (_adView!=null)
            {
                _adView.AdReceived -= currentAdView.AdLoaded;
                _adView.AdReceived -= _adView_AdReceived;
                _adView.ReceiveAdFailed -= (s, e) => currentAdView.AdFailedToLoad(s, new MTEventArgs { ErrorCode = (int)e.Error.Code, ErrorMessage = e.Error.LocalizedFailureReason, ErrorDomain = e.Error.Domain });
                _adView.ReceiveAdFailed -= _adView_ReceiveAdFailed;
                _adView.WillPresentScreen -= currentAdView.AdClicked;

                _adView.ClickRecorded -= currentAdView.AdClicked;
                _adView.ImpressionRecorded -= currentAdView.AdImpression;

                _adView.RemoveFromSuperview();
                _adView = null;
            }

            _adView = new BannerView(GetAdSize(currentAdView.AdSize))
            {
                AdUnitId = _adUnitId,
                RootViewController = controller,
                TranslatesAutoresizingMaskIntoConstraints= false,
            };
            
            _adView.AdReceived += currentAdView.AdLoaded;
            _adView.AdReceived += _adView_AdReceived;
            _adView.ReceiveAdFailed += (s, e) => currentAdView.AdFailedToLoad(s, new MTEventArgs { ErrorCode = (int)e.Error.Code, ErrorMessage = e.Error.LocalizedFailureReason, ErrorDomain = e.Error.Domain });
            _adView.ReceiveAdFailed += _adView_ReceiveAdFailed;
            _adView.WillPresentScreen += currentAdView.AdClicked;

            _adView.ClickRecorded += currentAdView.AdClicked;
            _adView.ImpressionRecorded += currentAdView.AdImpression;          
        }

        private void _adView_ReceiveAdFailed(object sender, BannerViewErrorEventArgs e)
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

        private void _adView_AdReceived(object sender, EventArgs e)
        {
            try
            {
                if (currentAdView.AutoSize)
                {
                    var size = _adView.AdSize;
                    currentAdView.HeightRequest = size.Size.Height;
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
                UIViewController controller = GetVisibleViewController();
                if (controller != null)
                {
                    if (e.NewElement != null)
                        CreateNativeControl(controller, e.NewElement, e.NewElement.AdsId, e.NewElement.AdSize, false);
                    else if (e.OldElement != null)
                        CreateNativeControl(controller, e.OldElement, e.OldElement.AdsId, e.OldElement.AdSize, true);
                    else
                        return;
                    SetNativeControl(_adView);
                    LoadAds();
                }
            }
        }

        private void LoadAds()
        {
            var request = MTAdmobImplementation.GetRequest();
            _adView.LoadRequest(request);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if ((e.PropertyName.Equals("Width") || e.PropertyName.Equals("AdSize")) && _adView != null)
            {
                UIViewController controller = GetVisibleViewController();
                if (controller != null)
                {
                    CreateAdView(controller);
                    SetNativeControl(_adView);
                    LoadAds();
                }
            }
        }

        private AdSize GetAdSize(BannerSize size)
        {
            switch (size)
            {
                case BannerSize.Banner:
                    return AdSizeCons.Banner;
                case BannerSize.LargeBanner:
                    return AdSizeCons.LargeBanner;
                case BannerSize.MediumRectangle:
                    return AdSizeCons.MediumRectangle;
                case BannerSize.FullBanner:
                    return AdSizeCons.FullBanner;
                case BannerSize.Leaderboard:
                    return AdSizeCons.Leaderboard;
                case BannerSize.AnchoredAdaptive:
                    return GetAdaptiveAdSize(true);
                case BannerSize.InlineAdaptive:
                    return GetAdaptiveAdSize(false);
                case BannerSize.Smart:
                    return AdSizeCons.SmartBannerPortrait;
                default:
                    return GetAdaptiveAdSize(true);
            }
        }

        private AdSize GetAdaptiveAdSize(bool isAnchored)
        {
            UIViewController controller = GetVisibleViewController();
            CGRect frame = controller.View.Frame;

            frame =  SafeAreaInsets.InsetRect(controller.View.Frame);
            int adWidth = (int)currentAdView.Width;

            if (isAnchored)
                return AdSizeCons.GetCurrentOrientationAnchoredAdaptiveBannerAdSize(adWidth);
            else
                return AdSizeCons.GetCurrentOrientationInlineAdaptiveBannerAdSizeh(adWidth);
        }

        private UIViewController GetVisibleViewController()
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
    }
}
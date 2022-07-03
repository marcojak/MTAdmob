using Google.MobileAds;
using System;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace MarcTron.Plugin.Services
{
    class InterstitialService : FullScreenContentDelegate
    {
        InterstitialAd _adInterstitial;
        private MTAdmobImplementation mTAdmobImplementation;

        public InterstitialService(MTAdmobImplementation mTAdmobImplementation)
        {
            this.mTAdmobImplementation = mTAdmobImplementation;
        }

        private async Task CreateInterstitialAd(string adUnit)
        {
            var request = MTAdmobImplementation.GetRequest();
            _adInterstitial = await InterstitialAd.LoadAsync(adUnit, request);
            if (_adInterstitial != null)
            {
                _adInterstitial.Delegate = this;
            }
        }

        public void LoadInterstitial(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            CreateInterstitialAd(adUnit);

            //var request = MTAdmobImplementation.GetRequest();
            //_adInterstitial.LoadRequest(request);
        }

        public void ShowInterstitial()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;
            if (_adInterstitial != null)
            {
                var canPresent = _adInterstitial.CanPresent(GetViewController(), out var error);
                if (canPresent)
                {
                    var window = UIApplication.SharedApplication.KeyWindow;
                    var vc = window.RootViewController;
                    while (vc.PresentedViewController != null)
                    {
                        vc = vc.PresentedViewController;
                    }

                    _adInterstitial.Present(vc);
                }
            }
        }

        private UIViewController GetViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }

            return vc;
        }

        internal bool IsLoaded()
        {
            return _adInterstitial != null && _adInterstitial.CanPresent(GetViewController(), out var error);
        }

        private void _adInterstitial_WillDismissScreen(object sender, EventArgs e)
        {
            mTAdmobImplementation.MOnInterstitialClosed();
        }

        private void _adInterstitial_WillPresentScreen(object sender, EventArgs e)
        {
            mTAdmobImplementation.MOnInterstitialOpened();
        }

        private void _adInterstitial_AdReceived(object sender, EventArgs e)
        {
            mTAdmobImplementation.MOnInterstitialLoaded();
        }

        public override void DidPresentFullScreenContent(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidPresentFullScreenContent");
            mTAdmobImplementation.MOnInterstitialOpened();
        }

        public override void DidFailToPresentFullScreenContent(FullScreenPresentingAd ad, NSError error)
        {
            Console.WriteLine("DidFailToPresentFullScreenContent");
        }

        public override void DidDismissFullScreenContent(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidDismissFullScreenContent");
            mTAdmobImplementation.MOnInterstitialClosed();
        }
    }
}

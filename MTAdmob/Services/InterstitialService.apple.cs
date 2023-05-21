using Google.MobileAds;
using System;
using Foundation;
using UIKit;

namespace MarcTron.Plugin.Services
{
    class InterstitialService : FullScreenContentDelegate
    {
        private InterstitialAd _adInterstitial;
        private readonly MTAdmobImplementation _admobImplementation;

        public InterstitialService(MTAdmobImplementation admobImplementation)
        {
            _admobImplementation = admobImplementation;
        }
        public void LoadInterstitial(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            CreateInterstitialAd(adUnit);
        }

        private void CreateInterstitialAd(string adUnit)
        {
            var request = MTAdmobImplementation.GetRequest();
            InterstitialAd.Load(adUnit, request, OnInterstitialLoaded);
        }

        private void OnInterstitialLoaded(InterstitialAd interstitialAd, NSError error)
        {
            _adInterstitial = interstitialAd;
            if (error == null)
            {
                _admobImplementation.MOnInterstitialLoaded();
                _adInterstitial.Delegate = this;
            }
            else
            {
                _admobImplementation.MOnInterstitialFailedToLoad(error);
            }
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

        public override void DidPresentFullScreenContent(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidPresentFullScreenContent");
            _admobImplementation.MOnInterstitialOpened();
        }

        public override void DidFailToPresentFullScreenContent(FullScreenPresentingAd ad, NSError error)
        {
            Console.WriteLine("DidFailToPresentFullScreenContent");
            _admobImplementation.MOnInterstitialFailedToShow(error);
        }

        public override void DidDismissFullScreenContent(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidDismissFullScreenContent");
            _admobImplementation.MOnInterstitialClosed();
        }

        public override void DidRecordImpression(FullScreenPresentingAd ad)
        {
            _admobImplementation.MOnInterstitialImpression();
        }

        public override void DidRecordClick(FullScreenPresentingAd ad)
        {
            _admobImplementation.MOnInterstitialClicked();
        }
    }
}

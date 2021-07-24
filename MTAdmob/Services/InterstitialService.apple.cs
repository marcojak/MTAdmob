using Google.MobileAds;
using System;
using UIKit;

namespace MarcTron.Plugin.Services
{
    class InterstitialService
    {
        Interstitial _adInterstitial;
        private MTAdmobImplementation mTAdmobImplementation;

        public InterstitialService(MTAdmobImplementation mTAdmobImplementation)
        {
            this.mTAdmobImplementation = mTAdmobImplementation;
        }

        private void CreateInterstitialAd(string adUnit)
        {
            try
            {
                if (_adInterstitial != null)
                {
                    _adInterstitial.AdReceived -= _adInterstitial_AdReceived;
                    _adInterstitial.WillPresentScreen -= _adInterstitial_WillPresentScreen;
                    _adInterstitial.WillDismissScreen -= _adInterstitial_WillDismissScreen;
                    _adInterstitial = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _adInterstitial = new Interstitial(adUnit);

            _adInterstitial.AdReceived += _adInterstitial_AdReceived;
            _adInterstitial.WillPresentScreen += _adInterstitial_WillPresentScreen;
            _adInterstitial.WillDismissScreen += _adInterstitial_WillDismissScreen;
        }

        public void LoadInterstitial(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            CreateInterstitialAd(adUnit);

            var request = MTAdmobImplementation.GetRequest();
            _adInterstitial.LoadRequest(request);
        }

        public void ShowInterstitial()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_adInterstitial != null && _adInterstitial.IsReady)
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

        internal bool IsLoaded()
        {
            return _adInterstitial != null && _adInterstitial.IsReady;
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
    }
}

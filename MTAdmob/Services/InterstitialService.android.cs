using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using System;
using Xamarin.Forms.Platform.Android;

namespace MarcTron.Plugin.Services
{
    public class InterstitialService : InterstitialAdLoadCallback
    {
        private Android.Gms.Ads.Interstitial.InterstitialAd _interstitialAd;

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
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            var requestBuilder = MTAdmobImplementation.GetRequest();
            InterstitialAd.Load(Android.App.Application.Context, adUnit, requestBuilder.Build(), this);
        }

        public bool IsLoaded()
        {
            return _interstitialAd != null;
        }

        public void ShowInterstitial()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_interstitialAd != null)
            {
                _interstitialAd.Show(Android.App.Application.Context.GetActivity());
                _interstitialAd = null;
            }
            else
            {
                Console.WriteLine("Interstitial not loaded");
            }
        }

        public override void OnInterstitialAdLoaded(Android.Gms.Ads.Interstitial.InterstitialAd interstitialAd)
        {
            base.OnInterstitialAdLoaded(interstitialAd);
            _interstitialAd = interstitialAd;
            _interstitialAd.FullScreenContentCallback = new MyFullScreenContentCallback(_admobImplementation, true);
            _admobImplementation.MOnInterstitialLoaded();
        }
      
        public override void OnAdFailedToLoad(LoadAdError error)
        {
            base.OnAdFailedToLoad(error);
            _admobImplementation.MOnInterstitialFailedToLoad(error);
            _interstitialAd = null;
        }
    }
}

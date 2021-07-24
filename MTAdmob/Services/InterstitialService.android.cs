using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using System;
using Xamarin.Forms.Platform.Android;

namespace MarcTron.Plugin.Services
{
    public class InterstitialService : InterstitialAdLoadCallback
    {
        private Android.Gms.Ads.Interstitial.InterstitialAd mInterstitialAd;

        public MTAdmobImplementation mTAdmobImplementation { get; }

        public InterstitialService(MTAdmobImplementation mTAdmobImplementation)
        {
            this.mTAdmobImplementation = mTAdmobImplementation;
        }

        private void CreateInterstitialAd(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            var context = Android.App.Application.Context;
            var requestBuilder = MTAdmobImplementation.GetRequest(mTAdmobImplementation);
            InterstitialAd.Load(context, adUnit, requestBuilder.Build(), this);
        }

        public void LoadInterstitial(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            CreateInterstitialAd(adUnit);
        }

        public bool IsLoaded()
        {
            return mInterstitialAd != null;
        }

        public void ShowInterstitial()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (mInterstitialAd != null)
            {
                mInterstitialAd.Show(Android.App.Application.Context.GetActivity());
            }
            else
            {
                Console.WriteLine("Interstitial not loaded");
            }
        }

        public override void OnInterstitialAdLoaded(Android.Gms.Ads.Interstitial.InterstitialAd interstitialAd)
        {
            base.OnInterstitialAdLoaded(interstitialAd);
            mInterstitialAd = interstitialAd;
            mInterstitialAd.FullScreenContentCallback = new MyFullScreenContentCallback(mTAdmobImplementation, true);
            mTAdmobImplementation.MOnInterstitialLoaded();
        }

      
        public override void OnAdFailedToLoad(LoadAdError p0)
        {
            base.OnAdFailedToLoad(p0);
            mInterstitialAd = null;
        }
    }
}

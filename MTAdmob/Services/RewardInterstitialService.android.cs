using Android.Gms.Ads;
using System;
using Xamarin.Forms.Platform.Android;
using RewardedInterstitialAd = Android.Gms.Ads.Hack.RewardedInterstitialAd;
using RewardedInterstitialAdLoadCallback = Android.Gms.Ads.Hack.RewardedInterstitialAdLoadCallback;

namespace MarcTron.Plugin.Services
{
    class RewardInterstitialService : RewardedInterstitialAdLoadCallback, IOnUserEarnedRewardListener
    {
        private Android.Gms.Ads.RewardedInterstitial.RewardedInterstitialAd _rewardedInterstitialAd;
        private readonly MTAdmobImplementation _admobImplementation;

        public RewardInterstitialService(MTAdmobImplementation mTAdmobImplementation)
        {
            _admobImplementation = mTAdmobImplementation;
        }

        public void LoadRewardInterstitial(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled || _rewardedInterstitialAd != null)
                return;

            CreateRewardInterstitialAd(adUnit);
        }

        private void CreateRewardInterstitialAd(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            var context = Android.App.Application.Context;
            var requestBuilder = MTAdmobImplementation.GetRequest();
            RewardedInterstitialAd.Load(context, adUnit, requestBuilder.Build(), this);
        }


        public bool IsLoaded()
        {
            return _rewardedInterstitialAd != null;
        }

        public void ShowRewardInterstitial()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_rewardedInterstitialAd != null)
            {
                _rewardedInterstitialAd.Show(Android.App.Application.Context.GetActivity(), this);
                _rewardedInterstitialAd = null;
            }
            else
            {
                Console.WriteLine("Reward not loaded");
            }
        }

        public override void OnAdFailedToLoad(LoadAdError error)
        {
            base.OnAdFailedToLoad(error);
            _admobImplementation.MOnRewardFailedToLoad(error);
            _rewardedInterstitialAd = null;
        }

        public override void OnRewardedInterstitialAdLoaded(Android.Gms.Ads.RewardedInterstitial.RewardedInterstitialAd rewardedAd)
        {
            base.OnRewardedInterstitialAdLoaded(rewardedAd);
            _rewardedInterstitialAd = rewardedAd;
            _rewardedInterstitialAd.FullScreenContentCallback = new MyFullScreenContentCallback(_admobImplementation, false);
            _admobImplementation.MOnRewardLoaded();
        }

        public void OnUserEarnedReward(Android.Gms.Ads.Rewarded.IRewardItem reward)
        {
            _admobImplementation.MOnUserEarnedReward(reward);
        }
    }
}

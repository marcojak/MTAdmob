using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using System;
using Xamarin.Forms.Platform.Android;

namespace MarcTron.Plugin.Services
{
    class RewardService : RewardedAdLoadCallback, IOnUserEarnedRewardListener
    {
        private Android.Gms.Ads.Rewarded.RewardedAd _mRewardedAd;
        private readonly MTAdmobImplementation _admobImplementation;

        public RewardService(MTAdmobImplementation admobImplementation)
        {
            _admobImplementation = admobImplementation;
        }

        private void CreateRewardAd(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            var context = Android.App.Application.Context;
            var requestBuilder = MTAdmobImplementation.GetRequest();
            RewardedAd.Load(context, adUnit, requestBuilder.Build(), this);
        }

        public void LoadReward(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            CreateRewardAd(adUnit);
        }

        public bool IsLoaded()
        {
            return _mRewardedAd != null;
        }

        public void ShowReward()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_mRewardedAd != null)
            {
                _mRewardedAd.Show(Android.App.Application.Context.GetActivity(), this);
                _mRewardedAd = null;
            }
            else
            {
                Console.WriteLine("Interstitial not loaded");
            }
        }

        public override void OnAdFailedToLoad(LoadAdError error)
        {
            base.OnAdFailedToLoad(error);
            _admobImplementation.MOnRewardFailedToLoad(error);
            _mRewardedAd = null;
        }

        public override void OnRewardedAdLoaded(Android.Gms.Ads.Rewarded.RewardedAd rewardedAd)
        {
            base.OnRewardedAdLoaded(rewardedAd);
            _mRewardedAd = rewardedAd;
            _mRewardedAd.FullScreenContentCallback = new MyFullScreenContentCallback(_admobImplementation, false);
            _admobImplementation.MOnRewardLoaded();
        }

        public void OnUserEarnedReward(Android.Gms.Ads.Rewarded.IRewardItem p0)
        {
            _admobImplementation.MOnUserEarnedReward(p0);
        }
    }
}

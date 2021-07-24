using Android.Gms.Ads;
using Android.Gms.Ads.Hack;
using System;
using Xamarin.Forms.Platform.Android;

namespace MarcTron.Plugin.Services
{
    class RewardService : RewardedAdLoadCallback, IOnUserEarnedRewardListener
    {
        private Android.Gms.Ads.Rewarded.RewardedAd mRewardedAd;
        public MTAdmobImplementation mTAdmobImplementation { get; }

        public RewardService(MTAdmobImplementation mTAdmobImplementation)
        {
            this.mTAdmobImplementation = mTAdmobImplementation;
        }

        private void CreateRewardAd(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            var context = Android.App.Application.Context;
            var requestBuilder = MTAdmobImplementation.GetRequest(mTAdmobImplementation);
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
            return mRewardedAd != null;
        }

        public void ShowReward()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (mRewardedAd != null)
            {
                mRewardedAd.Show(Android.App.Application.Context.GetActivity(), this);
            }
            else
            {
                Console.WriteLine("Interstitial not loaded");
            }
        }

        public override void OnAdFailedToLoad(LoadAdError p0)
        {
            base.OnAdFailedToLoad(p0);
            mRewardedAd = null;
        }

        public override void OnRewardedAdLoaded(Android.Gms.Ads.Rewarded.RewardedAd rewardedAd)
        {
            base.OnRewardedAdLoaded(rewardedAd);
            mRewardedAd = rewardedAd;
            mRewardedAd.FullScreenContentCallback = new MyFullScreenContentCallback(mTAdmobImplementation, false);
            mTAdmobImplementation.MOnRewardLoaded();
        }

        public void OnUserEarnedReward(Android.Gms.Ads.Rewarded.IRewardItem p0)
        {
            mTAdmobImplementation.MOnUserEarnedReward(p0);
        }
    }

}

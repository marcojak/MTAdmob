using Foundation;
using Google.MobileAds;
using MarcTron.Plugin.CustomEventArgs;
using System;
using UIKit;

namespace MarcTron.Plugin.Services
{
    class RewardService : RewardBasedVideoAdDelegate
    {
        private MTAdmobImplementation mTAdmobImplementation;

        public RewardService(MTAdmobImplementation mTAdmobImplementation)
        {
            this.mTAdmobImplementation = mTAdmobImplementation;
            RewardBasedVideoAd.SharedInstance.Delegate = this;
        }

        public bool IsRewardedVideoLoaded()
        {
            return RewardBasedVideoAd.SharedInstance.IsReady;
        }

        public void LoadRewardedVideo(string adUnit, MTRewardedAdOptions options = null)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            //old method
            if (RewardBasedVideoAd.SharedInstance.IsReady)
            {
                mTAdmobImplementation.MOnRewardedVideoAdLoaded();
                return;
            }

            RewardBasedVideoAd.SharedInstance.CustomRewardString = options?.CustomData;

            var request = MTAdmobImplementation.GetRequest();
            RewardBasedVideoAd.SharedInstance.LoadRequest(request, adUnit);

            //new method
            //if (_rewardedAd==null)
            //    _rewardedAd = new RewardedAd();
            //_rewardedAd.LoadRequest(request, completion);
        }

        public void ShowRewardedVideo()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            //old method
            if (RewardBasedVideoAd.SharedInstance.IsReady)
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                RewardBasedVideoAd.SharedInstance.Present(vc);
            }

            //new method
            //if (_rewardedAd.IsReady)
            //{
            //    var window = UIApplication.SharedApplication.KeyWindow;
            //    var vc = window.RootViewController;
            //    while (vc.PresentedViewController != null)
            //    {
            //        vc = vc.PresentedViewController;
            //    }
            //    _rewardedAd.Present(vc, this);
            //}
        }


        //old method
        public override void DidRewardUser(RewardBasedVideoAd rewardBasedVideoAd, AdReward reward)
        {
            mTAdmobImplementation.MOnRewarded(new MTEventArgs() { RewardAmount = (int)reward.Amount, RewardType = reward.Type });
        }


        public override void DidClose(RewardBasedVideoAd rewardBasedVideoAd)
        {
            mTAdmobImplementation.MOnRewardedVideoAdClosed();
        }

        public override void DidCompletePlaying(RewardBasedVideoAd rewardBasedVideoAd)
        {
            mTAdmobImplementation.MOnRewardedVideoAdCompleted();
        }

        public override void DidFailToLoad(RewardBasedVideoAd rewardBasedVideoAd, NSError error)
        {
            mTAdmobImplementation.MOnRewardedVideoAdFailedToLoad(new MTEventArgs() { ErrorCode = (int)error.Code });
        }

        public override void DidOpen(RewardBasedVideoAd rewardBasedVideoAd)
        {
            mTAdmobImplementation.MOnRewardedVideoAdOpened();
        }

        public override void DidReceiveAd(RewardBasedVideoAd rewardBasedVideoAd)
        {
            mTAdmobImplementation.MOnRewardedVideoAdLoaded();
        }

        public override void DidStartPlaying(RewardBasedVideoAd rewardBasedVideoAd)
        {
            mTAdmobImplementation.MOnRewardedVideoStarted();
        }

        public override void WillLeaveApplication(RewardBasedVideoAd rewardBasedVideoAd)
        {
            mTAdmobImplementation.MOnRewardedVideoAdLeftApplication();
        }
    }
}

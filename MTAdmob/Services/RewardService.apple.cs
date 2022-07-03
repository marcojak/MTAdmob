using Foundation;
using Google.MobileAds;
using MarcTron.Plugin.CustomEventArgs;
using System;
using System.Threading.Tasks;
using UIKit;

namespace MarcTron.Plugin.Services
{
    class RewardService : FullScreenContentDelegate
    {
        RewardedAd _adRewarded;
        private MTAdmobImplementation mTAdmobImplementation;

        public RewardService(MTAdmobImplementation mTAdmobImplementation)
        {
            this.mTAdmobImplementation = mTAdmobImplementation;
        }

        private async Task CreateRewardedAd(string adUnit)
        {
            var request = MTAdmobImplementation.GetRequest();
            _adRewarded = await RewardedAd.LoadAsync(adUnit, request);
            if (_adRewarded != null)
            {
                _adRewarded.Delegate = this;
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

        public bool IsRewardedVideoLoaded()
        {
            return _adRewarded != null && _adRewarded.CanPresent(GetViewController(), out var error);
        }

        public void LoadRewardedVideo(string adUnit, MTRewardedAdOptions options = null)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            CreateRewardedAd(adUnit);

            //Hot to pass this???
            //RewardBasedVideoAd.SharedInstance.CustomRewardString = options?.CustomData;

        }

        public void ShowRewardedVideo()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;
            if (_adRewarded != null)
            {
                var canPresent = _adRewarded.CanPresent(GetViewController(), out var error);
                if (canPresent)
                {
                    var window = UIApplication.SharedApplication.KeyWindow;
                    var vc = window.RootViewController;
                    while (vc.PresentedViewController != null)
                    {
                        vc = vc.PresentedViewController;
                    }

                    _adRewarded.Present(vc, DidEarnReward);
                }
            }
        }

        private void DidEarnReward()
        {
            var r = _adRewarded.AdReward;
        }


        ////old method
        //public override void DidRewardUser(RewardBasedVideoAd rewardBasedVideoAd, AdReward reward)
        //{
        //    mTAdmobImplementation.MOnRewarded(new MTEventArgs() { RewardAmount = (int)reward.Amount, RewardType = reward.Type });
        //}


        //public override void DidClose(RewardBasedVideoAd rewardBasedVideoAd)
        //{
        //    mTAdmobImplementation.MOnRewardedVideoAdClosed();
        //}

        //public override void DidCompletePlaying(RewardBasedVideoAd rewardBasedVideoAd)
        //{
        //    mTAdmobImplementation.MOnRewardedVideoAdCompleted();
        //}

        //public override void DidFailToLoad(RewardBasedVideoAd rewardBasedVideoAd, NSError error)
        //{
        //    mTAdmobImplementation.MOnRewardedVideoAdFailedToLoad(new MTEventArgs() { ErrorCode = (int)error.Code });
        //}

        //public override void DidOpen(RewardBasedVideoAd rewardBasedVideoAd)
        //{
        //    mTAdmobImplementation.MOnRewardedVideoAdOpened();
        //}

        //public override void DidReceiveAd(RewardBasedVideoAd rewardBasedVideoAd)
        //{
        //    mTAdmobImplementation.MOnRewardedVideoAdLoaded();
        //}

        //public override void DidStartPlaying(RewardBasedVideoAd rewardBasedVideoAd)
        //{
        //    mTAdmobImplementation.MOnRewardedVideoStarted();
        //}

        //public override void WillLeaveApplication(RewardBasedVideoAd rewardBasedVideoAd)
        //{
        //    mTAdmobImplementation.MOnRewardedVideoAdLeftApplication();
        //}

        public override void DidPresentFullScreenContent(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidPresentFullScreenContent");
            mTAdmobImplementation.MOnRewardedVideoAdOpened();
        }

        public override void DidFailToPresentFullScreenContent(FullScreenPresentingAd ad, NSError error)
        {
            Console.WriteLine("DidFailToPresentFullScreenContent");
            mTAdmobImplementation.MOnRewardedVideoAdFailedToLoad(new MTEventArgs() { ErrorCode = (int)error.Code, ErrorDomain = error.Domain, ErrorMessage = error.LocalizedDescription });
        }

        public override void DidDismissFullScreenContent(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidDismissFullScreenContent");
            mTAdmobImplementation.MOnRewardedVideoAdClosed();
        }
    }
}

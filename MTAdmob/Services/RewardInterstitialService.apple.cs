using Foundation;
using Google.MobileAds;
using MarcTron.Plugin.Extra;
using System;
using UIKit;

namespace MarcTron.Plugin.Services
{
    class RewardInterstitialService : FullScreenContentDelegate, IRewardedAdDelegate
    {
        private RewardedInterstitialAd _rewardedInterstitialAd;
        private readonly MTAdmobImplementation _admobImplementation;

        public RewardInterstitialService(MTAdmobImplementation admobImplementation)
        {
            _admobImplementation = admobImplementation;
        }

        public void LoadRewardedInterstitial(string adUnit, MTRewardedAdOptions options = null)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            CreateRewardedAd(adUnit);
        }

        private void CreateRewardedAd(string adUnit)
        {
            var request = MTAdmobImplementation.GetRequest();
            RewardedInterstitialAd.Load(adUnit, request, RewardedInterstitialLoaded);
        }

        private void RewardedInterstitialLoaded(RewardedInterstitialAd rewardedInterstitialAd, NSError error)
        {
            _rewardedInterstitialAd = rewardedInterstitialAd;
            if (error == null)
            {
                _admobImplementation.MOnRewardLoaded();
            }
            else
            {
                _admobImplementation.MOnRewardFailedToLoad(error);
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

        public bool IsRewardedInterstitialLoaded()
        {
            return _rewardedInterstitialAd != null && _rewardedInterstitialAd.CanPresent(GetViewController(), out var error);
        }

        public void ShowRewardedInterstitial()
        {
            try
            {
                if (!CrossMTAdmob.Current.IsEnabled)
                    return;
                if (_rewardedInterstitialAd != null)
                {
                    var canPresent = _rewardedInterstitialAd.CanPresent(GetViewController(), out var error);
                    if (canPresent)
                    {
                        var window = UIApplication.SharedApplication.KeyWindow;
                        var vc = window.RootViewController;
                        while (vc.PresentedViewController != null)
                        {
                            vc = vc.PresentedViewController;
                        }

                        _rewardedInterstitialAd.Present(vc, this);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Rewarded interstitial not fully implemented yet!");
            }
        }

        public void UserDidEarnReward(RewardedAd rewardedAd, AdReward reward)
        {
            if (_rewardedInterstitialAd.Reward != null)
            {
                _admobImplementation.MOnUserEarnedReward(new MTEventArgs() { RewardAmount = (int)_rewardedInterstitialAd.Reward.Amount, RewardType = _rewardedInterstitialAd.Reward.Type });
            }
        }

        public override void DidPresentFullScreenContent(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidPresentFullScreenContent");
            _admobImplementation.MOnRewardOpened();
        }

        public override void DidFailToPresentFullScreenContent(FullScreenPresentingAd ad, NSError error)
        {
            Console.WriteLine("DidFailToPresentFullScreenContent");
            _admobImplementation.MOnRewardFailedToShow(error);
        }

        public override void DidDismissFullScreenContent(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidDismissFullScreenContent");
            _admobImplementation.MOnRewardClosed();
        }

        public override void DidRecordImpression(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidRecordImpression");
            _admobImplementation.MOnRewardImpression();
        }

        public override void DidRecordClick(FullScreenPresentingAd ad)
        {
            Console.WriteLine("DidRecordClick");
            _admobImplementation.MOnRewardedClicked();
        }
    }
}

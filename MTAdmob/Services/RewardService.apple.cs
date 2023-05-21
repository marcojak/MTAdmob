﻿using Foundation;
using Google.MobileAds;
using MarcTron.Plugin.Extra;
using System;
using UIKit;

namespace MarcTron.Plugin.Services
{
    class RewardService : FullScreenContentDelegate
    {
        private RewardedAd _adRewarded;
        private readonly MTAdmobImplementation _admobImplementation;

        public RewardService(MTAdmobImplementation admobImplementation)
        {
            _admobImplementation = admobImplementation;
        }

        public void LoadRewarded(string adUnit, MTRewardedAdOptions options = null)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            CreateRewardedAd(adUnit);
        }

        private void CreateRewardedAd(string adUnit)
        {
            var request = MTAdmobImplementation.GetRequest();
            RewardedAd.Load(adUnit, request, RewardedLoaded);
        }

        private void RewardedLoaded(RewardedAd rewardedAd, NSError error)
        {
            _adRewarded = rewardedAd;
            if (error == null)
            {
                _admobImplementation.MOnRewardLoaded();
                _adRewarded.Delegate = this;
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

        public bool IsRewardedLoaded()
        {
            return _adRewarded != null && _adRewarded.CanPresent(GetViewController(), out var error);
        }

        public void ShowRewarded()
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
            if (_adRewarded.AdReward != null)
            {
                _admobImplementation.MOnUserEarnedReward(new MTEventArgs() { RewardAmount = (int)_adRewarded.AdReward.Amount, RewardType = _adRewarded.AdReward.Type });
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

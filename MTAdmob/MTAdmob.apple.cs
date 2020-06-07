using System;
using System.Collections.Generic;
using Foundation;
using Google.MobileAds;
using MarcTron.Plugin.CustomEventArgs;
using MarcTron.Plugin.Interfaces;
using UIKit;

namespace MarcTron.Plugin
{
    /// <summary>
    /// Interface for MTAdmob
    /// </summary>
    public class MTAdmobImplementation : RewardBasedVideoAdDelegate, IMTAdmob/*, IRewardedAdDelegate*/ //New rewarded delegate
    {
        public string AdsId { get; set; }
        public bool UserPersonalizedAds { get; set; }
        public List<string> TestDevices { get; set; }

        Interstitial _adInterstitial;

        //New reward object
        //RewardedAd _rewardedAd;

        public event EventHandler<MTEventArgs> OnRewarded;
        public event EventHandler OnRewardedVideoAdClosed;
        public event EventHandler<MTEventArgs> OnRewardedVideoAdFailedToLoad;
        public event EventHandler OnRewardedVideoAdLeftApplication;
        public event EventHandler OnRewardedVideoAdLoaded;
        public event EventHandler OnRewardedVideoAdOpened;
        public event EventHandler OnRewardedVideoStarted;
        public event EventHandler OnRewardedVideoAdCompleted;
        public event EventHandler OnInterstitialLoaded;
        public event EventHandler OnInterstitialOpened;
        public event EventHandler OnInterstitialClosed;

        public MTAdmobImplementation()
        {
            RewardBasedVideoAd.SharedInstance.Delegate = this;
            //New reward object
            //_rewardedAd = new RewardedAd();
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
            CreateInterstitialAd(adUnit);

            var request = Request.GetDefaultRequest();
            if (CrossMTAdmob.Current.TestDevices != null)
                request.TestDevices = CrossMTAdmob.Current.TestDevices.ToArray();
            _adInterstitial.LoadRequest(request);
        }

        public void ShowInterstitial()
        {
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

        public bool IsInterstitialLoaded()
        {
            return _adInterstitial != null && _adInterstitial.IsReady;
        }

        private void _adInterstitial_WillDismissScreen(object sender, EventArgs e)
        {
            OnInterstitialClosed?.Invoke(sender, e);
        }

        private void _adInterstitial_WillPresentScreen(object sender, EventArgs e)
        {
            OnInterstitialOpened?.Invoke(sender, e);
        }

        private void _adInterstitial_AdReceived(object sender, EventArgs e)
        {
            OnInterstitialLoaded?.Invoke(sender, e);
        }

        public bool IsRewardedVideoLoaded()
        {
            return RewardBasedVideoAd.SharedInstance.IsReady;
        }

        public void LoadRewardedVideo(string adUnit, MTRewardedAdOptions options = null)
        {
            //old method
            if (RewardBasedVideoAd.SharedInstance.IsReady)
            {
                OnRewardedVideoAdLoaded?.Invoke(null, null);
                return;
            }

            RewardBasedVideoAd.SharedInstance.CustomRewardString = options?.CustomData;

            var request = Request.GetDefaultRequest();
            if (CrossMTAdmob.Current.TestDevices != null)
                request.TestDevices = CrossMTAdmob.Current.TestDevices.ToArray();
            RewardBasedVideoAd.SharedInstance.LoadRequest(request, adUnit);

            //new method
            //if (_rewardedAd==null)
            //    _rewardedAd = new RewardedAd();
            //_rewardedAd.LoadRequest(request, completion);
        }

        //private void completion(RequestError error)
        //{
        //}

        public void ShowRewardedVideo()
        {
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
            OnRewarded?.Invoke(rewardBasedVideoAd, new MTEventArgs() { RewardAmount = (int)reward.Amount, RewardType = reward.Type });
        }

        //new method
        //public void UserDidEarnReward(RewardedAd rewardedAd, AdReward reward)
        //{
        //    OnRewarded?.Invoke(rewardedAd, new MTEventArgs() { RewardAmount = (int)reward.Amount, RewardType = reward.Type });
        //}

        public override void DidClose(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedVideoAdClosed?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void DidCompletePlaying(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedVideoAdCompleted?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void DidFailToLoad(RewardBasedVideoAd rewardBasedVideoAd, NSError error)
        {
            OnRewardedVideoAdFailedToLoad?.Invoke(rewardBasedVideoAd, new MTEventArgs() { ErrorCode = (int)error.Code });
        }

        public override void DidOpen(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedVideoAdOpened?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void DidReceiveAd(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedVideoAdLoaded?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void DidStartPlaying(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedVideoStarted?.Invoke(rewardBasedVideoAd, new EventArgs());
        }

        public override void WillLeaveApplication(RewardBasedVideoAd rewardBasedVideoAd)
        {
            OnRewardedVideoAdLeftApplication?.Invoke(rewardBasedVideoAd, new EventArgs());
        }
    }
}
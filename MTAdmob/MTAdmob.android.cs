using System;
using System.Collections.Generic;
using Android.App;
using Android.Gms.Ads;
using Android.Gms.Ads.Reward;
using Android.OS;
using Google.Ads.Mediation.Admob;
using MarcTron.Plugin.CustomEventArgs;
using MarcTron.Plugin.Interfaces;
using MarcTron.Plugin.Listeners;
// ReSharper disable InconsistentNaming

namespace MarcTron.Plugin
{
    /// <summary>
    /// Interface for MTAdmob
    /// </summary>
    public class MTAdmobImplementation : IMTAdmob
    {
        public bool IsEnabled { get; set; } = true;
        public string AdsId { get; set; }
        public bool UserPersonalizedAds { get; set; }
        public bool UseRestrictedDataProcessing { get; set; } = false;
        public bool ComplyWithFamilyPolicies { get; set; } = false;
        public List<string> TestDevices { get; set; }

        InterstitialAd _ad;

        IRewardedVideoAd _rewardedAds;

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

        private void CreateInterstitialAd(string adUnit)
        {
            var context = Application.Context;
            _ad = new InterstitialAd(context) {AdUnitId = adUnit};
            var interstitialAdListener = new InterstitialAdListener();

            interstitialAdListener.AdLoaded += InterstitialAdListener_AdLoaded;
            interstitialAdListener.AdOpened += InterstitialAdListener_AdOpened;
            interstitialAdListener.AdClosed += InterstitialAdListener_AdClosed;

            _ad.AdListener = interstitialAdListener;
        }

        public bool IsInterstitialLoaded()
        {
            return _ad != null && _ad.IsLoaded;
        }

        public static AdRequest.Builder GetRequest()
        {
            bool addBundle = false;
            Bundle bundleExtra = new Bundle();
            var requestBuilder = new AdRequest.Builder();

            if (CrossMTAdmob.Current.TestDevices != null)
            {
                foreach (var testDevice in CrossMTAdmob.Current.TestDevices)
                {
                    requestBuilder.AddTestDevice(testDevice);
                }
            }
            
            if (!CrossMTAdmob.Current.UserPersonalizedAds)
            {
                bundleExtra.PutString("npa", "1");
                addBundle = true;
            }

            if (CrossMTAdmob.Current.UseRestrictedDataProcessing)
            {
                bundleExtra.PutString("rdp", "1");
                addBundle = true;
            }

            if (CrossMTAdmob.Current.ComplyWithFamilyPolicies)
            {
                requestBuilder.TagForChildDirectedTreatment(CrossMTAdmob.Current.ComplyWithFamilyPolicies);
                bundleExtra.PutString("max_ad_content_rating", "G");
                addBundle = true;
            }

            if (addBundle)
                requestBuilder = requestBuilder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), bundleExtra);

            return requestBuilder;
        }

        public void LoadInterstitial(string adUnit)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_ad == null || _ad?.AdUnitId != adUnit)
                CreateInterstitialAd(adUnit);

            if (!_ad.IsLoaded && !_ad.IsLoading)
            {
                var requestBuilder = GetRequest();
                _ad.LoadAd(requestBuilder.Build());
            }
            else
            {
                Console.WriteLine("Interstitial already loaded");
            }
        }

        public void ShowInterstitial()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_ad != null && _ad.IsLoaded)
            {
                _ad.Show();
            }
            else
            {
                Console.WriteLine("Interstitial not loaded");
            }
        }

        private void InterstitialAdListener_AdClosed(object sender, EventArgs e)
        {
            OnInterstitialClosed?.Invoke(sender, e);
        }

        private void InterstitialAdListener_AdOpened(object sender, EventArgs e)
        {
            OnInterstitialOpened?.Invoke(sender, e);
        }

        private void InterstitialAdListener_AdLoaded(object sender, EventArgs e)
        {
            OnInterstitialLoaded?.Invoke(sender, e);
        }

        private void CreateRewardedVideo()
        {
            var context = Application.Context;
            _rewardedAds = MobileAds.GetRewardedVideoAdInstance(context);

            var rewardListener = new MyRewardedVideoAdListener();
            _rewardedAds.RewardedVideoAdListener = rewardListener;

            rewardListener.OnRewardedEvent += RewardListener_OnRewardedEvent;
            rewardListener.OnRewardedVideoAdClosedEvent += RewardListener_OnRewardedVideoAdClosedEvent;
            rewardListener.OnRewardedVideoAdFailedToLoadEvent += RewardListener_OnRewardedVideoAdFailedToLoadEvent;
            rewardListener.OnRewardedVideoAdLeftApplicationEvent += RewardListener_OnRewardedVideoAdLeftApplicationEvent;
            rewardListener.OnRewardedVideoAdLoadedEvent += RewardListener_OnRewardedVideoAdLoadedEvent;
            rewardListener.OnRewardedVideoAdOpenedEvent += RewardListener_OnRewardedVideoAdOpenedEvent;
            rewardListener.OnRewardedVideoStartedEvent += RewardListener_OnRewardedVideoStartedEvent;
            rewardListener.OnRewardedVideoCompletedEvent += RewardListener_OnRewardedVideoCompletedEvent;
        }

        public bool IsRewardedVideoLoaded()
        {
            return _rewardedAds != null && _rewardedAds.IsLoaded;
        }

        public void LoadRewardedVideo(string adUnit, MTRewardedAdOptions options = null)
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_rewardedAds == null)
            {
                CreateRewardedVideo();
            }

            if (!_rewardedAds.IsLoaded)
            {
                var requestBuilder = GetRequest();

#if !MONOANDROID81
                if (options != null)
                {
                    _rewardedAds.UserId = options.UserId;
                    _rewardedAds.CustomData = options.CustomData;
                }
#endif
                _rewardedAds.LoadAd(adUnit, requestBuilder.Build());
            }
            else
            {
                Console.WriteLine("Rewarded Video already loaded");
            }
        }

        public void ShowRewardedVideo()
        {
            if (!CrossMTAdmob.Current.IsEnabled)
                return;

            if (_rewardedAds != null && _rewardedAds.IsLoaded)
            {
                _rewardedAds.Show();
            }
            else
            {
                Console.WriteLine("Rewarded Video not loaded");
            }
        }

        private void RewardListener_OnRewardedVideoAdLoadedEvent(object sender, EventArgs e)
        {
            OnRewardedVideoAdLoaded?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedVideoStartedEvent(object sender, EventArgs e)
        {
            OnRewardedVideoStarted?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedVideoAdLeftApplicationEvent(object sender, EventArgs e)
        {
            OnRewardedVideoAdLeftApplication?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedVideoAdFailedToLoadEvent(object sender, MTEventArgs e)
        {
            OnRewardedVideoAdFailedToLoad?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedVideoAdOpenedEvent(object sender, EventArgs e)
        {
            OnRewardedVideoAdOpened?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedVideoAdClosedEvent(object sender, EventArgs e)
        {
            OnRewardedVideoAdClosed?.Invoke(sender, e);
        }

        private void RewardListener_OnRewardedEvent(object sender, MTEventArgs e)
        {
            OnRewarded?.Invoke(null, e);
        }

        private void RewardListener_OnRewardedVideoCompletedEvent(object sender, EventArgs e)
        {
            OnRewardedVideoAdCompleted?.Invoke(sender, e);
        }
    }
}

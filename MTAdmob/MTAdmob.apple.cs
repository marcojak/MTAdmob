using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Google.MobileAds;
using MarcTron.Plugin.CustomEventArgs;
using MarcTron.Plugin.Interfaces;
using MarcTron.Plugin.Services;

namespace MarcTron.Plugin
{
    /// <summary>
    /// Interface for MTAdmob
    /// </summary>
    public class MTAdmobImplementation : IMTAdmob/*, IRewardedAdDelegate*/ //New rewarded delegate
    {
        public bool IsEnabled { get; set; } = true;
        public string AdsId { get; set; }
        public bool UserPersonalizedAds { get; set; }
        public List<string> TestDevices { get; set; }
        public bool UseRestrictedDataProcessing { get; set; } = false;
        public bool ComplyWithFamilyPolicies { get; set; } = false;
        /// <summary>
        /// Not implemented yet on iOS
        /// </summary>
        public MTTagForChildDirectedTreatment TagForChildDirectedTreatment { get ; set ; }
        /// <summary>
        /// Not implemented yet on iOS
        /// </summary>
        public MTTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get ; set ; }
        /// <summary>
        /// Not implemented yet on iOS
        /// </summary>
        public string MaxAdContentRating { get ; set ; }

        InterstitialService interstitialService;
        RewardService rewardService;

        Interstitial _adInterstitial;

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

        public virtual void MOnInterstitialLoaded() => OnInterstitialLoaded?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialOpened() => OnInterstitialOpened?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialClosed() => OnInterstitialClosed?.Invoke(this, EventArgs.Empty);


        public virtual void MOnRewardedVideoAdLoaded() => OnRewardedVideoAdLoaded?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewarded(MTEventArgs args) => OnRewarded?.Invoke(this, args);
        public virtual void MOnRewardedVideoAdClosed() => OnRewardedVideoAdClosed?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardedVideoAdCompleted() => OnRewardedVideoAdCompleted?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardedVideoAdFailedToLoad(MTEventArgs args) => OnRewardedVideoAdFailedToLoad?.Invoke(this, args);
        public virtual void MOnRewardedVideoAdOpened() => OnRewardedVideoAdOpened?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardedVideoStarted() => OnRewardedVideoStarted?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardedVideoAdLeftApplication() => OnRewardedVideoAdLeftApplication?.Invoke(this, EventArgs.Empty);

        public MTAdmobImplementation()
        {
            interstitialService = new InterstitialService(this);
            rewardService = new RewardService(this);           
        }

        public static Request GetRequest()
        {
            var request = Request.GetDefaultRequest();
            

            bool addExtra = false;
            var dict = new Dictionary<string, string>();

            MobileAds.SharedInstance.RequestConfiguration.TagForChildDirectedTreatment(CrossMTAdmob.Current.ComplyWithFamilyPolicies);
            if (CrossMTAdmob.Current.TestDevices != null)
                request.TestDevices = CrossMTAdmob.Current.TestDevices.ToArray();
         
            if (!CrossMTAdmob.Current.UserPersonalizedAds)
            {
                dict.Add(new NSString("npa"), new NSString("1"));
                addExtra = true;
            }

            if (CrossMTAdmob.Current.UseRestrictedDataProcessing)
            {
                dict.Add(new NSString("rdp"), new NSString("1"));
                addExtra = true;
            }

            if (CrossMTAdmob.Current.ComplyWithFamilyPolicies)
            {
                request.Tag(CrossMTAdmob.Current.ComplyWithFamilyPolicies);
                dict.Add(new NSString("max_ad_content_rating"), new NSString("G"));
                addExtra = true;
            }

            if (addExtra)
            {
                var extras = new Extras
                {
                    AdditionalParameters = NSDictionary.FromObjectsAndKeys(dict.Values.ToArray(), dict.Keys.ToArray())
                };
                request.RegisterAdNetworkExtras(extras);
            }

            return request;
        }

        public bool IsInterstitialLoaded()
        {
            return interstitialService.IsLoaded();
        }

        public void LoadInterstitial(string adUnit)
        {
            interstitialService.LoadInterstitial(adUnit);
        }

        public void ShowInterstitial()
        {
            interstitialService.ShowInterstitial();
        }

        public bool IsRewardedVideoLoaded()
        {
            return rewardService.IsRewardedVideoLoaded();
        }

        public void LoadRewardedVideo(string adUnit, MTRewardedAdOptions options = null)
        {
            rewardService.LoadRewardedVideo(adUnit, options);
        }

        public void ShowRewardedVideo()
        {
            rewardService.ShowRewardedVideo();
        }
    }
}
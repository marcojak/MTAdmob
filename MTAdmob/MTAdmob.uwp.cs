using MarcTron.Plugin.CustomEventArgs;
using MarcTron.Plugin.Interfaces;
using System;
using System.Collections.Generic;

namespace MarcTron.Plugin
{
    /// <summary>
    /// Interface for MTAdmob
    /// This has not been implemented yet but it could be in a next version or you can implement and send it with a pull request :)
    /// </summary>
    public class MTAdmobImplementation : IMTAdmob
    {
        public bool IsEnabled { get; set; } = true;
        public string AdsId { get; set; }
        public bool UserPersonalizedAds { get; set; }
        public List<string> TestDevices { get; set; }
        public bool UseRestrictedDataProcessing { get; set; }
        public bool ComplyWithFamilyPolicies { get; set; }
        public MTTagForChildDirectedTreatment TagForChildDirectedTreatment { get ; set ; }
        public MTTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; }
        public MTMaxAdContentRating MaxAdContentRating { get; set; }

        public event EventHandler<MTEventArgs> OnRewarded;
        public event EventHandler OnRewardedVideoAdClosed;
        public event EventHandler<MTEventArgs> OnRewardedVideoAdFailedToLoad;
        public event EventHandler OnRewardedVideoAdLeftApplication;
        public event EventHandler OnRewardedVideoAdLoaded;
        public event EventHandler OnRewardedVideoAdOpened;
        public event EventHandler OnRewardedVideoStarted;

        public event EventHandler OnInterstitialLoaded;
        public event EventHandler OnInterstitialOpened;
        public event EventHandler OnInterstitialClosed;
        public event EventHandler OnRewardedVideoAdCompleted;

        public string GetAdContentRatingString()
        {
            throw new NotImplementedException();
        }

        public bool IsInterstitialLoaded()
        {
            return false;
        }

        public bool IsRewardedVideoLoaded()
        {
            return false;
        }

        public void LoadInterstitial(string adUnit)
        {
        }

        public void LoadRewardedVideo(string adUnit, MTRewardedAdOptions options = null)
        {
        }

        public void ShowInterstitial(string adUnit)
        {
        }

        public void ShowInterstitial()
        {
        }

        public void ShowRewardedVideo(string adUnit)
        {

        }

        public void ShowRewardedVideo()
        {
        }
    }
}

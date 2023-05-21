using System;
using System.Collections.Generic;
using MarcTron.Plugin.Extra;

namespace MarcTron.Plugin.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface IMTAdmob
    {
        /// <summary>
        /// Gets if the plugin is enabled.
        /// </summary>
        bool IsEnabled { get; set; }

        string AdsId { get; set; }
        bool UserPersonalizedAds { get; set; }

        bool UseRestrictedDataProcessing { get; set; }
        bool ComplyWithFamilyPolicies { get; set; }

        MTTagForChildDirectedTreatment TagForChildDirectedTreatment { get; set; }
        MTTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; }
        MTMaxAdContentRating MaxAdContentRating { get; set; }    

        List<string> TestDevices { get; set; }

        bool IsInterstitialLoaded();
        void LoadInterstitial(string adUnit);
        void ShowInterstitial();

        bool IsRewardedLoaded();
        public void LoadRewarded(string adUnit, MTRewardedAdOptions options = null);
        void ShowRewarded();

        bool IsRewardedInterstitialLoaded();
        public void LoadRewardedInterstitial(string adUnit, MTRewardedAdOptions options = null);
        void ShowRewardedInterstitial();

        string GetAdContentRatingString();

        void SetAppMuted(bool muted);
        void SetAppVolume(float volume);

        event EventHandler OnInterstitialLoaded;
        event EventHandler<MTEventArgs> OnInterstitialFailedToLoad;
        event EventHandler OnInterstitialOpened;
        event EventHandler OnInterstitialClosed;
        event EventHandler<MTEventArgs> OnInterstitialFailedToShow;
        event EventHandler OnInterstitialImpression;
        //Only iOS
        event EventHandler OnInterstitialClicked;

        event EventHandler OnRewardedLoaded;
        event EventHandler<MTEventArgs> OnRewardedFailedToLoad;
        event EventHandler OnRewardedOpened;
        event EventHandler OnRewardedClosed;
        event EventHandler<MTEventArgs> OnRewardedFailedToShow;
        event EventHandler OnRewardedImpression;
        //Only iOS
        event EventHandler OnRewardedClicked;

        event EventHandler<MTEventArgs> OnUserEarnedReward;


        //event EventHandler<MTEventArgs> OnRewarded;
        //event EventHandler OnRewardedVideoAdClosed;
        //event EventHandler<MTEventArgs> OnRewardedVideoAdFailedToLoad;
        //event EventHandler OnRewardedVideoAdLeftApplication;
        //event EventHandler OnRewardedVideoAdLoaded;
        //event EventHandler OnRewardedVideoAdOpened;
        //event EventHandler OnRewardedVideoStarted;
        //event EventHandler OnRewardedVideoAdCompleted;
    }
}

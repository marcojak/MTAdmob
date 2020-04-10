using System;
using System.Collections.Generic;
using MarcTron.Plugin.CustomEventArgs;

namespace MarcTron.Plugin.Interfaces
{
    // ReSharper disable once InconsistentNaming
    public interface IMTAdmob
    {
        string AdsId { get; set; }
        bool UserPersonalizedAds { get; set; }

        List<string> TestDevices { get; set; }

        bool IsInterstitialLoaded();
        void LoadInterstitial(string adUnit);
        void ShowInterstitial();

        bool IsRewardedVideoLoaded();

#if !MONOANDROID81
        public void LoadRewardedVideo(string adUnit, MTRewardedAdOptions options);
#else
        void LoadRewardedVideo(string adUnit);
#endif
        void ShowRewardedVideo();


        event EventHandler OnInterstitialLoaded;
        event EventHandler OnInterstitialOpened;      
        event EventHandler OnInterstitialClosed;

        event EventHandler<MTEventArgs> OnRewarded;
        event EventHandler OnRewardedVideoAdClosed;
        event EventHandler<MTEventArgs> OnRewardedVideoAdFailedToLoad;
        event EventHandler OnRewardedVideoAdLeftApplication;
        event EventHandler OnRewardedVideoAdLoaded;
        event EventHandler OnRewardedVideoAdOpened;
        event EventHandler OnRewardedVideoStarted;
        event EventHandler OnRewardedVideoAdCompleted;
    }
}

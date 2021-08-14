using System;
using System.Collections.Generic;
using Android.Gms.Ads;
using Android.Gms.Ads.Interstitial;
using Android.Gms.Ads.Rewarded;
using Android.OS;
using Google.Ads.Mediation.Admob;
using MarcTron.Plugin.CustomEventArgs;
using MarcTron.Plugin.Interfaces;
using MarcTron.Plugin.Services;
//using static Android.Gms.Ads.RequestConfiguration;
// ReSharper disable InconsistentNaming

namespace MarcTron.Plugin
{
    /// <summary>
    /// Interface for MTAdmob
    /// </summary>
    public class MTAdmobImplementation : InterstitialAdLoadCallback, IMTAdmob
    {
        public bool IsEnabled { get; set; } = true;
        public string AdsId { get; set; }
        public bool UserPersonalizedAds { get; set; }
        public bool UseRestrictedDataProcessing { get; set; } = false;      
        public List<string> TestDevices { get; set; }
        public MTTagForChildDirectedTreatment TagForChildDirectedTreatment { get; set; } = MTTagForChildDirectedTreatment.TagForChildDirectedTreatmentUnspecified;
        public MTTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; } = MTTagForUnderAgeOfConsent.TagForUnderAgeOfConsentUnspecified;
        public MTMaxAdContentRating MaxAdContentRating { get; set; } = MTMaxAdContentRating.MaxAdContentRatingG;
        public bool ComplyWithFamilyPolicies { get; set ; }

        InterstitialService interstitialService;
        RewardService rewardService;

        public event EventHandler<MTEventArgs> OnRewarded;
        public event EventHandler<MTEventArgs> OnUserEarnedReward;
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
        public event EventHandler<MTEventArgs> OnInterstitialFailedToShow;
        public event EventHandler OnInterstitialImpression;

        public event EventHandler OnRewardLoaded;
        public event EventHandler OnRewardOpened;
        public event EventHandler OnRewardClosed;
        public event EventHandler<MTEventArgs> OnRewardFailedToShow;
        public event EventHandler OnRewardImpression;

        public virtual void MOnInterstitialLoaded() => OnInterstitialLoaded?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialOpened() => OnInterstitialOpened?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialClosed() => OnInterstitialClosed?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialFailedToShow(AdError p0) => OnInterstitialFailedToShow?.Invoke(this, new MTEventArgs() { ErrorCode = p0.Code, ErrorMessage = p0.Message, ErrorDomain = p0.Domain});
        public virtual void MOnInterstitialImpression() => OnInterstitialImpression?.Invoke(this, EventArgs.Empty);


        public virtual void MOnRewardLoaded() => OnRewardLoaded?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardOpened() => OnRewardOpened?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardClosed() => OnRewardClosed?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardFailedToShow(AdError p0) => OnRewardFailedToShow?.Invoke(this, new MTEventArgs() { ErrorCode = p0.Code, ErrorMessage = p0.Message, ErrorDomain = p0.Domain });
        public virtual void MOnUserEarnedReward(IRewardItem p0) => OnUserEarnedReward?.Invoke(this, new MTEventArgs() { RewardType=p0.Type, RewardAmount=p0.Amount });
        public virtual void MOnRewardImpression() => OnRewardImpression?.Invoke(this, EventArgs.Empty);

        public MTAdmobImplementation()
        {
            interstitialService = new InterstitialService(this);
            rewardService = new RewardService(this);
        }

        public static AdRequest.Builder GetRequest(/*MTAdmobImplementation mTAdmobImplementation*/)
        {
            bool addBundle = false;
            Bundle bundleExtra = new Bundle();
            var requestBuilder = new AdRequest.Builder();
            var configuration = new RequestConfiguration.Builder();

            if (CrossMTAdmob.Current.TestDevices != null)
            {
                configuration = configuration.SetTestDeviceIds(CrossMTAdmob.Current.TestDevices);                
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

            configuration.SetTagForChildDirectedTreatment((int)CrossMTAdmob.Current.TagForChildDirectedTreatment);
            configuration.SetTagForUnderAgeOfConsent((int)CrossMTAdmob.Current.TagForUnderAgeOfConsent);
            configuration.SetMaxAdContentRating(CrossMTAdmob.Current.GetAdContentRatingString());
            MobileAds.RequestConfiguration = configuration.Build();

            if (addBundle)
                requestBuilder = requestBuilder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(AdMobAdapter)), bundleExtra);

            return requestBuilder;
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
            return rewardService.IsLoaded();
        }

        public void LoadRewardedVideo(string adUnit, MTRewardedAdOptions options = null)
        {
            rewardService.LoadReward(adUnit);
        }

        public void ShowRewardedVideo()
        {
            rewardService.ShowReward();
        }

        public string GetAdContentRatingString()
        {
            switch (MaxAdContentRating)
            {
                case MTMaxAdContentRating.MaxAdContentRatingG:
                    return "MAX_AD_CONTENT_RATING_G";
                case MTMaxAdContentRating.MaxAdContentRatingPg:
                    return "MAX_AD_CONTENT_RATING_PG";
                case MTMaxAdContentRating.MaxAdContentRatingT:
                    return "MAX_AD_CONTENT_RATING_T";
                case MTMaxAdContentRating.MaxAdContentRatingMa:
                    return "MAX_AD_CONTENT_RATING_MA";
                default: return "MAX_AD_CONTENT_RATING_G";
            }
        }
    }
}

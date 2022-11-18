using System;
using Android.Gms.Ads;
using Android.Gms.Ads.Interstitial;
using Android.Gms.Ads.Rewarded;
using Android.OS;
using Google.Ads.Mediation.Admob;
using MarcTron.Plugin.Extra;
using MarcTron.Plugin.Interfaces;
using MarcTron.Plugin.Services;
//using static Android.Gms.Ads.RequestConfiguration;
// ReSharper disable InconsistentNaming

namespace MarcTron.Plugin
{
    /// <summary>
    /// Interface for MTAdmob
    /// </summary>
    public partial class MTAdmobImplementation : InterstitialAdLoadCallback, IMTAdmob
    {
        readonly InterstitialService interstitialService;
        readonly RewardService rewardService;
        readonly RewardInterstitialService rewardInterstitialService;

        public virtual void MOnInterstitialLoaded() => OnInterstitialLoaded?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialFailedToLoad(LoadAdError error) => OnInterstitialFailedToLoad?.Invoke(this, new MTEventArgs() { ErrorCode = error.Code, ErrorMessage = error.Message, ErrorDomain = error.Domain });
        public virtual void MOnInterstitialImpression() => OnInterstitialImpression?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialOpened() => OnInterstitialOpened?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialFailedToShow(AdError error) => OnInterstitialFailedToShow?.Invoke(this, new MTEventArgs() { ErrorCode = error.Code, ErrorMessage = error.Message, ErrorDomain = error.Domain });
        public virtual void MOnInterstitialClosed() => OnInterstitialClosed?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialClicked() => OnInterstitialClicked?.Invoke(this, EventArgs.Empty);


        public virtual void MOnRewardLoaded() => OnRewardedLoaded?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardFailedToLoad(LoadAdError error) => OnRewardedFailedToLoad?.Invoke(this, new MTEventArgs() { ErrorCode = error.Code, ErrorMessage = error.Message, ErrorDomain = error.Domain });
        public virtual void MOnRewardImpression() => OnRewardedImpression?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardOpened() => OnRewardedOpened?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardFailedToShow(AdError p0) => OnRewardedFailedToShow?.Invoke(this, new MTEventArgs() { ErrorCode = p0.Code, ErrorMessage = p0.Message, ErrorDomain = p0.Domain });
        public virtual void MOnRewardClosed() => OnRewardedClosed?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardedClicked() => OnRewardedClicked?.Invoke(this, EventArgs.Empty);

        public virtual void MOnUserEarnedReward(IRewardItem p0) => OnUserEarnedReward?.Invoke(this, new MTEventArgs() { RewardType = p0.Type, RewardAmount = p0.Amount });


        public MTAdmobImplementation()
        {
            interstitialService = new InterstitialService(this);
            rewardService = new RewardService(this);
            rewardInterstitialService = new RewardInterstitialService(this);
        }

        public static AdRequest.Builder GetRequest()
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

            MobileAds.RequestConfiguration = configuration.SetTagForChildDirectedTreatment((int)CrossMTAdmob.Current.TagForChildDirectedTreatment).
            SetTagForUnderAgeOfConsent((int)CrossMTAdmob.Current.TagForUnderAgeOfConsent).
            SetMaxAdContentRating(CrossMTAdmob.Current.GetAdContentRatingString()).Build();

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

        public bool IsRewardedLoaded()
        {
            return rewardService.IsLoaded();
        }

        public void LoadRewarded(string adUnit, MTRewardedAdOptions options = null)
        {
            rewardService.LoadReward(adUnit);
        }

        public void ShowRewarded()
        {
            rewardService.ShowReward();
        }

        public void SetAppMuted(bool muted)
        {
            MobileAds.SetAppMuted(muted);
        }

        public void SetAppVolume(float volume)
        {
            MobileAds.SetAppVolume(volume);
        }

        public bool IsRewardedInterstitialLoaded()
        {
            return rewardInterstitialService.IsLoaded();
        }

        public void LoadRewardedInterstitial(string adUnit, MTRewardedAdOptions options = null)
        {
            rewardInterstitialService.LoadRewardInterstitial(adUnit);
        }

        public void ShowRewardedInterstitial()
        {
            rewardInterstitialService.ShowRewardInterstitial();
        }

        public string GetAdContentRatingString()
        {
            switch (MaxAdContentRating)
            {
                case MTMaxAdContentRating.MaxAdContentRatingG:
                    return RequestConfiguration.MaxAdContentRatingG;
                case MTMaxAdContentRating.MaxAdContentRatingPg:
                    return RequestConfiguration.MaxAdContentRatingPg;
                case MTMaxAdContentRating.MaxAdContentRatingT:
                    return RequestConfiguration.MaxAdContentRatingT;
                case MTMaxAdContentRating.MaxAdContentRatingMa:
                    return RequestConfiguration.MaxAdContentRatingMa;
                default: return RequestConfiguration.MaxAdContentRatingUnspecified;
            }
        }
    }
}

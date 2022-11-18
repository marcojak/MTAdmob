using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using Google.MobileAds;
using MarcTron.Plugin.Extra;
using MarcTron.Plugin.Interfaces;
using MarcTron.Plugin.Services;

namespace MarcTron.Plugin
{
    /// <summary>
    /// Interface for MTAdmob
    /// </summary>
    public partial class MTAdmobImplementation : IMTAdmob
    {
        readonly InterstitialService _interstitialService;
        readonly RewardService _rewardService;
        readonly RewardInterstitialService _rewardInterstitialService;

        public virtual void MOnInterstitialLoaded() => OnInterstitialLoaded?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialOpened() => OnInterstitialOpened?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialClosed() => OnInterstitialClosed?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialFailedToLoad(NSError error) => OnInterstitialFailedToLoad?.Invoke(this, new MTEventArgs() { ErrorCode = (int?)error.Code, ErrorMessage = error.Description, ErrorDomain = error.Domain });
        public virtual void MOnInterstitialFailedToShow(NSError error) => OnInterstitialFailedToShow?.Invoke(this, new MTEventArgs() { ErrorCode = (int?)error.Code, ErrorMessage = error.Description, ErrorDomain = error.Domain });
        public virtual void MOnInterstitialImpression() => OnInterstitialImpression?.Invoke(this, EventArgs.Empty);
        public virtual void MOnInterstitialClicked() => OnInterstitialClicked?.Invoke(this, EventArgs.Empty);

        public virtual void MOnRewardLoaded() => OnRewardedLoaded?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardOpened() => OnRewardedOpened?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardClosed() => OnRewardedClosed?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardFailedToLoad(NSError error) => OnRewardedFailedToLoad?.Invoke(this, new MTEventArgs() { ErrorCode = (int)error.Code, ErrorMessage = error.Description, ErrorDomain = error.Domain });
        public virtual void MOnRewardFailedToShow(NSError error) => OnRewardedFailedToShow?.Invoke(this, new MTEventArgs() { ErrorCode = (int)error.Code, ErrorMessage = error.Description, ErrorDomain = error.Domain });
        public virtual void MOnRewardImpression() => OnRewardedImpression?.Invoke(this, EventArgs.Empty);
        public virtual void MOnRewardedClicked() => OnRewardedClicked?.Invoke(this, EventArgs.Empty);

        public virtual void MOnUserEarnedReward(MTEventArgs reward) => OnUserEarnedReward?.Invoke(this, reward);

        public MTAdmobImplementation()
        {
            _interstitialService = new InterstitialService(this);
            _rewardService = new RewardService(this);
            _rewardInterstitialService = new RewardInterstitialService(this);
        }

        public static Request GetRequest()
        {
            var request = Request.GetDefaultRequest();

            bool addExtra = false;
            var dict = new Dictionary<string, string>();

            MobileAds.SharedInstance.RequestConfiguration.TagForChildDirectedTreatment(CrossMTAdmob.Current.TagForChildDirectedTreatment == MTTagForChildDirectedTreatment.TagForChildDirectedTreatmentTrue);
            MobileAds.SharedInstance.RequestConfiguration.TagForUnderAgeOfConsent(CrossMTAdmob.Current.TagForUnderAgeOfConsent == MTTagForUnderAgeOfConsent.TagForUnderAgeOfConsentTrue);
            MobileAds.SharedInstance.RequestConfiguration.MaxAdContentRating = CrossMTAdmob.Current.GetAdContentRatingString();
            if (CrossMTAdmob.Current.TestDevices != null)
                MobileAds.SharedInstance.RequestConfiguration.TestDeviceIdentifiers = CrossMTAdmob.Current.TestDevices.ToArray();

            if (!CrossMTAdmob.Current.UserPersonalizedAds)
            {
                dict.Add(new NSString("npa"), new NSString("1"));
                addExtra = true;
            }

            if (CrossMTAdmob.Current.UseRestrictedDataProcessing)
            {
                dict.Add(new NSString("rdp"), new NSString("1"));
                addExtra = true; 
                NSUserDefaults.StandardUserDefaults.SetBool(true, "gad_rdp");
            }

            if (CrossMTAdmob.Current.ComplyWithFamilyPolicies)
            {
                CrossMTAdmob.Current.MaxAdContentRating = MTMaxAdContentRating.MaxAdContentRatingG;
                MobileAds.SharedInstance.RequestConfiguration.MaxAdContentRating = CrossMTAdmob.Current.GetAdContentRatingString();
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
            return _interstitialService.IsLoaded();
        }

        public void LoadInterstitial(string adUnit)
        {
            _interstitialService.LoadInterstitial(adUnit);
        }

        public void ShowInterstitial()
        {
            _interstitialService.ShowInterstitial();
        }

        public bool IsRewardedLoaded()
        {
            return _rewardService.IsRewardedLoaded();
        }

        public void LoadRewarded(string adUnit, MTRewardedAdOptions options = null)
        {
            _rewardService.LoadRewarded(adUnit, options);
        }

        public void ShowRewarded()
        {
            _rewardService.ShowRewarded();
        }

        public bool IsRewardedInterstitialLoaded()
        {
            return _rewardInterstitialService.IsRewardedInterstitialLoaded();
        }

        public void LoadRewardedInterstitial(string adUnit, MTRewardedAdOptions options = null)
        {
            _rewardInterstitialService.LoadRewardedInterstitial(adUnit);
        }

        public void ShowRewardedInterstitial()
        {
            _rewardInterstitialService.ShowRewardedInterstitial();
        }

        public string GetAdContentRatingString()
        {
            switch (MaxAdContentRating)
            {
                case MTMaxAdContentRating.MaxAdContentRatingG:
                    return "GADMaxAdContentRatingGeneral";
                case MTMaxAdContentRating.MaxAdContentRatingPg:
                    return "GADMaxAdContentRatingParentalGuidance";
                case MTMaxAdContentRating.MaxAdContentRatingT:
                    return "GADMaxAdContentRatingTeen";
                case MTMaxAdContentRating.MaxAdContentRatingMa:
                    return "GADMaxAdContentRatingMatureAudience";
                default: return "GADMaxAdContentRatingGeneral";
            }
        }

        public void SetAppMuted(bool muted)
        {
            MobileAds.SharedInstance.ApplicationMuted = muted;
        }

        public void SetAppVolume(float volume)
        {
            MobileAds.SharedInstance.ApplicationVolume = volume;
        }
    }
}
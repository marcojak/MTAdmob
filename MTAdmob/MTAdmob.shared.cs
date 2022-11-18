using System;
using System.Collections.Generic;
using MarcTron.Plugin.Extra;

namespace MarcTron.Plugin
{
    public partial class MTAdmobImplementation
    {
        public bool IsEnabled { get; set; } = true;
        public string AdsId { get; set; }
        public bool UserPersonalizedAds { get; set; }
        public List<string> TestDevices { get; set; }
        public bool UseRestrictedDataProcessing { get; set; } = false;
        public bool ComplyWithFamilyPolicies { get; set; }
        public MTTagForChildDirectedTreatment TagForChildDirectedTreatment { get; set; } = MTTagForChildDirectedTreatment.TagForChildDirectedTreatmentUnspecified;
        public MTTagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; } = MTTagForUnderAgeOfConsent.TagForUnderAgeOfConsentUnspecified;
        public MTMaxAdContentRating MaxAdContentRating { get; set; } = MTMaxAdContentRating.MaxAdContentRatingG;

        public event EventHandler OnInterstitialLoaded;
        public event EventHandler OnInterstitialOpened;
        public event EventHandler OnInterstitialClosed;
        public event EventHandler<MTEventArgs> OnInterstitialFailedToLoad;
        public event EventHandler<MTEventArgs> OnInterstitialFailedToShow;
        public event EventHandler OnInterstitialImpression;
        public event EventHandler OnInterstitialClicked;

        public event EventHandler OnRewardedLoaded;
        public event EventHandler<MTEventArgs> OnRewardedFailedToLoad;
        public event EventHandler OnRewardedOpened;
        public event EventHandler OnRewardedClosed;
        public event EventHandler<MTEventArgs> OnRewardedFailedToShow;
        public event EventHandler OnRewardedImpression;
        public event EventHandler OnRewardedClicked;

        public event EventHandler<MTEventArgs> OnUserEarnedReward;
    }
}

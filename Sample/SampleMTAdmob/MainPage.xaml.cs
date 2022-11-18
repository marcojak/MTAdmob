using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using MarcTron.Plugin;
using MarcTron.Plugin.Controls;
using Xamarin.Forms;
using MarcTron.Plugin.Extra;

namespace SampleMTAdmob
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private bool _shouldSetEvents = true;
        public MainPage()
        {
            InitializeComponent();
            CrossMTAdmob.Current.TestDevices = new List<string>() { "D70290758AB9D6060C3B14AA41DAB53A", "0EB3E09F26C31286957EF0BF27B58A4E" };


            CrossMTAdmob.Current.TagForChildDirectedTreatment = MTTagForChildDirectedTreatment.TagForChildDirectedTreatmentUnspecified;
            CrossMTAdmob.Current.TagForUnderAgeOfConsent = MTTagForUnderAgeOfConsent.TagForUnderAgeOfConsentUnspecified;
            CrossMTAdmob.Current.MaxAdContentRating = MTMaxAdContentRating.MaxAdContentRatingG;

            //If you want to load a banner programmatically:
            //MTAdView myAds = new MTAdView();
            //myAds.AdsId = "xxx";
            //myAds.PersonalizedAds = true;
            //myAds.AdsClicked += MyAds_AdsClicked;
            //myAds.AdsClosed += MyAds_AdVClosed;
            //myAds.AdsImpression += MyAds_AdVImpression;
            //myAds.AdsOpened += MyAds_AdVOpened;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetEvents();
        }

        void SetEvents()
        {
            if (_shouldSetEvents)
            {
                _shouldSetEvents = false;
                //CrossMTAdmob.Current.OnRewardedVideoStarted += Current_OnRewardedVideoStarted;
                CrossMTAdmob.Current.OnUserEarnedReward += Current_OnRewarded;
                CrossMTAdmob.Current.OnRewardedClosed += Current_OnRewardedVideoAdClosed;
                CrossMTAdmob.Current.OnRewardedFailedToLoad += Current_OnRewardedVideoAdFailedToLoad;
                //CrossMTAdmob.Current.OnRewardedLeftApplication += Current_OnRewardedVideoAdLeftApplication;
                CrossMTAdmob.Current.OnRewardedLoaded += Current_OnRewardedVideoAdLoaded;
                CrossMTAdmob.Current.OnRewardedOpened += Current_OnRewardedVideoAdOpened;
                //CrossMTAdmob.Current.OnRewardedCompleted += Current_OnRewardedVideoAdCompleted;

                CrossMTAdmob.Current.OnInterstitialLoaded += Current_OnInterstitialLoaded;
                CrossMTAdmob.Current.OnInterstitialOpened += Current_OnInterstitialOpened;
                CrossMTAdmob.Current.OnInterstitialClosed += Current_OnInterstitialClosed;
            }
        }

        private void Current_OnInterstitialClosed(object sender, EventArgs e)
        {
            Debug.WriteLine("OnInterstitialClosed");
        }

        private void Current_OnInterstitialOpened(object sender, EventArgs e)
        {
            Debug.WriteLine("OnInterstitialOpened");
        }

        private void Current_OnInterstitialLoaded(object sender, EventArgs e)
        {
            Debug.WriteLine("OnInterstitialLoaded");
        }

        private void Current_OnRewardedVideoAdOpened(object sender, EventArgs e)
        {
            Debug.WriteLine("OnRewardedVideoAdOpened");
        }

        private void Current_OnRewardedVideoAdLoaded(object sender, EventArgs e)
        {
            Debug.WriteLine("OnRewardedVideoAdLoaded");
        }

        private void Current_OnRewardedVideoAdLeftApplication(object sender, EventArgs e)
        {
            Debug.WriteLine("OnRewardedVideoAdLeftApplication");
        }

        private void Current_OnRewardedVideoAdFailedToLoad(object sender, MTEventArgs e)
        {
            Debug.WriteLine("OnRewardedVideoAdFailedToLoad");
        }

        private void Current_OnRewardedVideoAdClosed(object sender, EventArgs e)
        {
            Debug.WriteLine("OnRewardedVideoAdClosed");
        }

        private void Current_OnRewarded(object sender, MTEventArgs e)
        {
            Debug.WriteLine($"OnRewarded: {e.RewardType} - {e.RewardAmount}");
        }

        private void Current_OnRewardedVideoStarted(object sender, EventArgs e)
        {
            Debug.WriteLine("OnRewardedVideoStarted");
        }

        private void Current_OnRewardedVideoAdCompleted(object sender, EventArgs e)
        {
            Debug.WriteLine("OnRewardedVideoAdCompleted");
        }

        private void MyAds_AdVOpened(object sender, EventArgs e)
        {
            Console.WriteLine("MyAds_AdVOpened");
        }

        private void MyAds_AdVImpression(object sender, EventArgs e)
        {
            Console.WriteLine("MyAds_AdVImpression");
        }

        private void MyAds_AdVClosed(object sender, EventArgs e)
        {
            Console.WriteLine("MyAds_AdVClosed");
        }

        private void MyAds_AdsClicked(object sender, EventArgs e)
        {
            Console.WriteLine("MyAds_AdsClicked");
        }

        private void MyAds_AdFailedToLoad(object sender, MTEventArgs e)
        {
            Debug.WriteLine($"MyAds_AdFailedToLoad: {e.ErrorCode} - {e.ErrorMessage}");
        }

        private void MyAds_AdLoaded(object sender, EventArgs e)
        {
            Console.WriteLine("MyAds_AdLoaded");
        }

        private void LoadReward_OnClicked(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.LoadRewarded("ca-app-pub-3940256099942544/5224354917");
        }

        private void ShowReward_OnClicked(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.ShowRewarded();
        }

        private void IsRewardLoaded_OnClicked(object sender, EventArgs e)
        {
            myLabel.Text = CrossMTAdmob.Current.IsRewardedLoaded().ToString();
        }

        private void LoadRewardInterstitial_OnClicked(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.LoadRewardedInterstitial("ca-app-pub-3940256099942544/5354046379");
        }

        private void ShowRewardInterstitial_OnClicked(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.ShowRewardedInterstitial();
        }

        private void IsRewardInterstitialLoaded_OnClicked(object sender, EventArgs e)
        {
            myLabel.Text = CrossMTAdmob.Current.IsRewardedInterstitialLoaded().ToString();
        }

        private void LoadInterstitial_OnClicked(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.LoadInterstitial("ca-app-pub-3940256099942544/1033173712");
        }

        private void ShowInterstitial_OnClicked(object sender, EventArgs e)
        {
            CrossMTAdmob.Current.ShowInterstitial();
        }

        private void IsLoadedInterstitial_OnClicked(object sender, EventArgs e)
        {
            myLabel.Text = CrossMTAdmob.Current.IsInterstitialLoaded().ToString();
        }

        private void NextPage(object sender, EventArgs e)
        {
            DisableEvents();
            Navigation.PushAsync(new SecondPage());
        }

        private void DisableEvents()
        {
            _shouldSetEvents = true;
            //CrossMTAdmob.Current.OnRewardedVideoStarted -= Current_OnRewardedVideoStarted;
            CrossMTAdmob.Current.OnUserEarnedReward -= Current_OnRewarded;
            CrossMTAdmob.Current.OnRewardedClosed -= Current_OnRewardedVideoAdClosed;
            CrossMTAdmob.Current.OnRewardedFailedToLoad -= Current_OnRewardedVideoAdFailedToLoad;
            //CrossMTAdmob.Current.OnRewardedLeftApplication -= Current_OnRewardedVideoAdLeftApplication;
            CrossMTAdmob.Current.OnRewardedLoaded -= Current_OnRewardedVideoAdLoaded;
            CrossMTAdmob.Current.OnRewardedOpened -= Current_OnRewardedVideoAdOpened;
            //CrossMTAdmob.Current.OnRewardedCompleted -= Current_OnRewardedVideoAdCompleted;

            CrossMTAdmob.Current.OnInterstitialLoaded -= Current_OnInterstitialLoaded;
            CrossMTAdmob.Current.OnInterstitialOpened -= Current_OnInterstitialOpened;
            CrossMTAdmob.Current.OnInterstitialClosed -= Current_OnInterstitialClosed;
        }
    }
}

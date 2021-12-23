MtAdmob Readme

With this Plugin you can add a Google Admob Ads inside your Xamarin Android and iOS Projects with a single line!!!
This plugin supports: Banners, Interstitial and Rewarded Videos

### Please, support me
If possible, please, support my work with few coffees or even better with a Membership!
You can do it here: [Buy Me A Coffee](https://www.buymeacoffee.com/xamarinexpert)
Your help allows me to continue to spend time on this project and continue to maintain and update it with new features and to be ready for the new Google SDK 20: [Google SDK 20 Migration](https://developers.google.com/admob/android/migration).

### IMPORTANT
* Remember to edit your AppManifest otherwise it will not work on Android
* On iOS you MUST now change the Ads init. In your iOS project Replace MobileAds.Configure with MobileAds.SharedInstance.Start(CompletionHandler);
  where CompletionHandler is something like: private void CompletionHandler(InitializationStatus status){}
* Edit your info.plist adding these Keys:
  <key>GADApplicationIdentifier</key>
  <string>ca-app-pub-3940256099942544~1458002511</string> <- This is a test key, replace it with your APPID
  <key>GADIsAdManagerApp</key>
  <true/>
* If you don't do this, your iOS app will crash

* I'm slowly starting to move toward AndroidX, so it's possible that you need to install more packages in your Android project, in case of a build error,
  Visual Studio will tell you which packages you need to install.

Release Notes
Version 1.6.9
Updated  Xamarin.GooglePlayServices.Ads.Lite to 120.3.0.3
Removed MyRewardedVideoAdListener
Enabled OnRewardedVideoAdLoaded On Android
Enabled OnRewardedVideoAdFailedToLoad On Android
Enabled OnRewardedVideoAdClosed On Android

Version 1.6.8
Replaced AdSize.SmartBanner as deprecated (thanks to roswer13)
Improved AdFailedToLoad (thanks to roswer13)

Version 1.6.7
Fixed OnRewardedVideoAdCompleted on Android
Updated Xamarin.GooglePlayServices.Ads.Lite to 120.3.0.1

Version 1.6.6
Fixed OnRewardedVideoAdLoaded on Android

Version 1.6.5
Fixed missing events for rewarded video on Android (Regression)

Version 1.6.4
Fixed issue with ADs request on Android
Replaced string consts for content rating filter

Version 1.6.3
Fixed missing events for rewarded video on Android

Version 1.6.2
Updated GooglePlayServices.Ads.Lite to 120.2.0
Updated Xamarin.AndroidX.Annotation to 1.2.0
Removed Personalized property from MTAdView

Version 1.6.1
Updated GooglePlayServices.Ads.Lite to 119.6.0
Updated all dependencies
Removed support for MonoAndroid81

Version 1.6.0
Updated Xamarin.Google.iOS.MobileAds to 7.66 (Required for iOS 14+)
Updated GooglePlayServices.Ads.Lite to 119.1.0

Version 1.5.8
 Added support for Comply with Google Play’s Families Policy (Thanks to Abdul Rahman)

Version 1.5.7
 Added IsEnabled to disable the ads (banners, interstitials, rewarded videos) (Default = true)
 Added UseRestrictedDataProcessing for CCPA compliance (Default = false)
 Starting to move to AndroidX, possibly you'll need new packages in your Android project but VS will tell you

Version 1.5.6
 Fixed possible issue with some MONODROID versions for LoadRewardedVideo

Version 1.5.5
 Updated MobileAds to 7.57.0 to avoid Apple rejection because of the reference to UIWebView

Version 1.5.0
 Updated packages
 Improved code
 Add custom data to RewardedAd (thanks to Lordinaire)

Version 1.4.4
 Removed deprecated code on iOS
 Fixed issues on iOS (Please update from version 1.4.3)

Version 1.4.3
 Added Consent option to iOS
 Updated Xamarin.Google.iOS.MobileAds to 7.47.0

Version 1.4.2
 Updated GooglePlayServicesAds.Lite 71.1720.1

Version 1.4.1
 Fixed issue on iOS where sometimes events weren't called on RewardedVideos

Version 1.4
 Fixed issue with multiple events called on RewardedVideos
 Improved code for Interstitials.

Version 1.3
 Fixed crash on iOS with multiple Banner
 Improved code
 To avoid compatibilty now the Banner control has been renamed from AdView to MTAdView

Version 1.2
 With this version Loading and Showing Interstitials and rewarded videos are 2 separate events. Now the methods are:
  void LoadInterstitial(string adUnit);
  void ShowInterstitial();
  bool IsInterstitialLoaded();
  void LoadRewardedVideo(string adUnit);
  void ShowRewardedVideo();
  bool IsRewardedVideoLoaded();

  I've removed the two methods:
  void ShowInterstitial(string adUnit);
  void ShowRewardedVideo(string adUnit);

  Now you have to load the Interstitial or Rewarded Video and when you want you can show it. You can see if an interstitial or rewared video are loaded using the new methods
  bool IsInterstitialLoaded();
  bool IsRewardedVideoLoaded();

  Thanks to these changes you have now more control over the Ads.

Version 1.1
 Added support for Rewarded Videos
 Added events for Rewarded Videos
 Added events for Interstitials

Version 1.0
Banner and Insterstitial for Android
Banner and Insterstitial for iOS

BANNER

To add a Banner on a page you have two options:

XAML

<controls:MTAdView x:Name="myAds"></controls:MTAdView>

remember to add this line in your XAML:

xmlns:controls="clr-namespace:MarcTron.Plugin.Controls;assembly=Plugin.MtAdmob"


CODE

MTAdView ads = new MTAdView();


PROPERTIES

For each AdView if you want, you can set these properties:
AdsId: To add the id of your ads

For GDPR it's better to rely on a custom consent instead or using the non personalized ads as I cannot guarantee it works. So it's better if you create a custom consent

GLOBAL PROPERTIES

AdsId: To add the id of your ads

PersonalizedAds: You can set it to False if you want to use generic ads (for GDPR...)(It works only for Android Banners, for the others, you must ask for consent)
For GDPR it's better to rely on a custom consent instead or using the non personalized ads as I cannot guarantee it works. So it's better if you create a custom consent

TestDevices: You can add here the ID of your test devices

You can use Global Properties in this way:
CrossMTAdmob.Current.UserPersonalizedAds = true;


INTERSTITIAL

You can show an interstitial with a single line of code:

CrossMTAdmob.Current.ShowInterstitial();

To Load an interstitial you can use this line:

CrossMTAdmob.Current.LoadInterstitial("xx-xxx-xxx-xxxxxxxxxxxxxxxxx/xxxxxxxxxx");

REWARDED VIDEO

You can show a Rewarded video with a single line of code:

CrossMTAdmob.Current.ShowRewardedVideo();

To Load a Rewarded Video you can use this line:

CrossMTAdmob.Current.LoadRewardedVideo("xx-xxx-xxx-xxxxxxxxxxxxxxxxx/xxxxxxxxxx");


EVENTS FOR BANNERS

Just in case you need, the Banner ads offer 4 events:

AdsClicked		When a user clicks on the ads
AdsClosed		When the user closes the ads
AdsImpression	Called when an impression is recorded for an ad.
AdsOpened		When the ads is opened



EVENTS FOR INTERSTITIALS

the Interstitial ads offer 3 events:

OnInterstitialLoaded        When it's loaded
OnInterstitialOpened        When it's opened      
OnInterstitialClosed        When it's closed



EVENTS FOR REWARDED VIDEOS

The Rewarded Videos offer 7 events:

OnRewarded                          When the user gets a reward
OnRewardedVideoAdClosed             When the ads is closed
OnRewardedVideoAdFailedToLoad       When the ads fails to load
OnRewardedVideoAdLeftApplication    When the users leaves the application
OnRewardedVideoAdLoaded             When the ads is loaded
OnRewardedVideoAdOpened             When the ads is opened
OnRewardedVideoStarted              When the ads starts



IMPORTANT

Remember to include the MTAdmob library with this code (usually it's added automatically):
```
using MarcTron.Plugin;
```

IMPORTANT

To test the banner during the development google uses two Banner Id, one for Android and the other for iOS. Use them then remember to replace them with your own IDs:
Android: ca-app-pub-3940256099942544/6300978111
iOS: ca-app-pub-3940256099942544/2934735716


If the Banners don't appear in your app, probably it's a size problem. To solve it, add this style you your app.xaml:

<Style TargetType="controls:MTAdView">
    <Setter Property="HeightRequest">
        <Setter.Value>
            <x:OnIdiom Phone="50" Tablet="90"></x:OnIdiom>
        </Setter.Value>
    </Setter>
</Style>

IMPORTANT FOR ANDROID:

Before loading ads, have your app initialize the Mobile Ads SDK by calling MobileAds.initialize() with your AdMob App ID. 
This needs to be done only once, ideally at app launch. For example:


protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            MobileAds.Initialize(ApplicationContext);
            Xamarin.Forms.Forms.Init(this, savedInstanceState); 
            LoadApplication(new App());
        }

Remeber to add this to your AppManifest:

<!-- Sample AdMob App ID: ca-app-pub-3940256099942544~3347511713 -->
<meta-data android:name="com.google.android.gms.ads.APPLICATION_ID"
           android:value="ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy"/>


IMPORTANT FOR IOS:

Before loading ads, have your app initialize the Mobile Ads SDK by calling MobileAds.initialize() with your AdMob App ID. 
This needs to be done only once, ideally at app launch. For example:


public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            <!-- Sample AdMob App ID: ca-app-pub-3940256099942544~1458002511 -->
            MobileAds.Configure("xx-xxx-xxx-xxxxxxxxxxxxxxxx~xxxxxxxxxx");

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

In case the plugin doesn't install automatically the nuget package 
Xamarin.Google.iOS.MobileAds
you need to add it manually.

That's it. Cannot be easier than that :)


LINKS
Available on Nuget: https://www.nuget.org/packages/MarcTron.Admob
Tutorial: https://www.xamarinexpert.it/admob-made-easy/
To report any issue: https://github.com/marcojak/MTAdmob/issues

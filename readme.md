## Stop the WAR in Ukraine!


### MtAdmob plugin for Xamarin (Android & iOS)

With this Plugin you can add a Google Admob Ads inside your Xamarin Android and iOS Projects with a single line!!!
This plugin supports: Banners, Interstitial and Rewarded Videos

## If you are looking for the newer version of my plugin for MAUI, you can visit: [MauiMTAdmob](https://github.com/marcojak/MauiMTAdmob)

### Please, support me
If possible, please, support my work with few coffees or even better with a Membership!
You can do it here: [Buy Me A Coffee](https://www.buymeacoffee.com/xamarinexpert)
Your help allows me to continue to spend time on this project and continue to maintain and update it with new features and to be ready for the new Google SDK 20: [Google SDK 20 Migration](https://developers.google.com/admob/android/migration).


### PLEASE NOTE

The previous versions of the plugin are compatible with Xamarin.Google.iOS.MobileAds up to version 7.66.0.
You need to use version >= 1.8 to use the newer versions of Xamarin.Google.iOS.MobileAds.
~~In version 8.13.0 Google changed interstitials and rewards for iOS and these changes are not supported yet.
I'm working on a new version of the plugin that will support the changes made by Google.
For now just don't update Xamarin.Google.iOS.MobileAds to version 8.13.0.~~

## Setup
* Available on Nuget: https://www.nuget.org/packages/MarcTron.Admob/
* Install in your .NetStandard project and Android/iOS projects

#### This plugin supports:
* Xamarin.Android
* Xamarin.iOS


## How to use MTAdmob

 You can find a tutorial on my blog: https://www.xamarinexpert.it/admob-made-easy/

### To add a banner in your project

To add a Banner on a page you have two options:

#### 1) XAML

```csharp
<controls:MTAdView x:Name="myAds"/>
```

remember to add this line in your XAML:
```csharp
xmlns:controls="clr-namespace:MarcTron.Plugin.Controls;assembly=Plugin.MtAdmob"
```

As you can have different Banner Ids for iOS and for Android, you could use the OnPlatform-Property like this:
```csharp
<controls:MTAdView x:Name="myAds" AdsId="{OnPlatform Android='ca-app-pub-3940256099942544/6300978111', 
                                   iOS='ca-app-pub-3940256099942544/2934735716'}"/>
```
Or set it in App.xaml (similar to HeightRequest as explained below) for all banners in your app.

#### 2) Code
```csharp
MTAdView ads = new MTAdView();
```

#### Important

To test the banner during the development google uses two Banner Id, one for Android and the other for iOS. Use them then remember to replace them with your own IDs:
```csharp
Android: ca-app-pub-3940256099942544/6300978111
iOS: ca-app-pub-3940256099942544/2934735716
```

**If the Banners don't appear in your app, probably it's a size problem. To solve it, add this style you your app.xaml:**
```csharp
<Style TargetType="controls:MTAdView">
    <Setter Property="HeightRequest">
        <Setter.Value>
            <x:OnIdiom Phone="50" Tablet="90"/>
        </Setter.Value>
    </Setter>
</Style>
```

### Properties

For each AdView if you want, you can set these properties:

AdsId: To add the id of your ads

PersonalizedAds: You can set it to False if you want to use generic ads (for GDPR...) (It works only for Android Banners, for the others, you must ask for consent)

**For GDPR it's better to rely on a custom consent instead or using the non personalized ads as I cannot guarantee it works. So it's better if you create a custom consent**

### Global Properties

AdsId: To add the id of your ads

PersonalizedAds: You can set it to False if you want to use generic ads (for GDPR...) (It works only for Android Banners, for the others, you must ask for consent)
**For GDPR it's better to rely on a custom consent instead or using the non personalized ads as I cannot guarantee it works. So it's better if you create a custom consent**

TestDevices: You can add here the ID of your test devices

You can use Global Properties in this way:
```csharp
CrossMTAdmob.Current.UserPersonalizedAds = true;
```

### Interstitial

You can show an interstitial with a single line of code:
```csharp
CrossMTAdmob.Current.ShowInterstitial();
```
To Load an interstitial you can use this line:
```csharp
CrossMTAdmob.Current.LoadInterstitial("xx-xxx-xxx-xxxxxxxxxxxxxxxxx/xxxxxxxxxx");
```

### Rewarded video

You can show a Rewarded video with a single line of code:
```csharp
CrossMTAdmob.Current.ShowRewardedVideo();
```
To Load a Rewarded Video you can use this line:
```csharp
CrossMTAdmob.Current.LoadRewardedVideo("xx-xxx-xxx-xxxxxxxxxxxxxxxxx/xxxxxxxxxx");
```

### Events for Banners

Just in case you need, the Banner ads offer 4 events:
```csharp
AdsClicked		    When a user clicks on the ads
AdsClosed		    When the user closes the ads
AdsImpression	    Called when an impression is recorded for an ad.
AdsOpened		    When the ads is opened
```

### Events for Interstitials

the Interstitial ads offer 3 events:
```csharp
OnInterstitialLoaded        When it's loaded
OnInterstitialOpened        When it's opened
OnInterstitialClosed        When it's closed
```

### Events for Rewarded Videos

The Rewarded Videos offer 7 events:
```csharp
OnRewarded                          When the user gets a reward
OnRewardedVideoAdClosed             When the ads is closed
OnRewardedVideoAdFailedToLoad       When the ads fails to load
OnRewardedVideoAdLeftApplication    When the users leaves the application
OnRewardedVideoAdLoaded             When the ads is loaded
OnRewardedVideoAdOpened             When the ads is opened
OnRewardedVideoStarted              When the ads starts
OnRewardedVideoAdCompleted          When the ads is completed
```

### Important

Remember to include the MTAdmob library with this code (usually it's added automatically):
```csharp
using MarcTron.Plugin;
```


### Important for Android

Before loading ads, have your app initialize the Mobile Ads SDK by calling MobileAds.initialize() with your AdMob App ID. 
This needs to be done only once, ideally at app launch. For example:

```csharp
protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);            
            MobileAds.Initialize(ApplicationContext);
            Xamarin.Forms.Forms.Init(this, savedInstanceState); 
            LoadApplication(new App());
        }
```
Remember to add this to your AppManifest:
```csharp
<!-- Sample AdMob App ID: ca-app-pub-3940256099942544~3347511713 -->
<meta-data android:name="com.google.android.gms.ads.APPLICATION_ID"
           android:value="ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy"/>
```
If your Ads are not displayed on your Android Emulator, make sure your Emulator has Google Play Store API installed, otherwise you'll find this message in your Debugger Console:

[GooglePlayServicesUtil] Google Play Store is missing.

If it's still not showing ads after adding Play Store, open Google Play Store on your emulator. There might be some config work to be done by Play Store, e.g. login with your google account, so just start it and make sure it's working fine on your emulator.


### IMPORTANT FOR IOS:

Before loading ads, have your app initialize the Mobile Ads SDK by calling MobileAds.SharedInstance.Start with your AdMob App ID. 
This needs to be done only once, ideally at app launch. For example:

```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            MobileAds.SharedInstance.Start(CompletionHandler);

            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }

private void CompletionHandler(InitializationStatus status){}
```

Edit your info.plist adding these Keys:
```csharp
  <key>GADApplicationIdentifier</key>
  <string>ca-app-pub-3940256099942544~1458002511</string> <- This is a test key, replace it with your APPID
  <key>GADIsAdManagerApp</key>
  <true/>
```

If you are builing your app for iOS on Windows without using your Mac, then
MobileAds.SharedInstance will be null and the ads will not work.

You have to build the app using your Mac to use Admob on iOS!

That's it. Cannot be easier than that :)

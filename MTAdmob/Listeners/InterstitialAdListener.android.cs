using System;
using Android.Gms.Ads;

namespace MarcTron.Plugin.Listeners
{
    public class InterstitialAdListener : AdListener
    {
        public event EventHandler AdLoaded;
        public event EventHandler AdOpened;
        public event EventHandler AdClosed;

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();

            AdLoaded?.Invoke(null,null);
            Console.WriteLine("OnInterstitialLoaded");
        }

        public override void OnAdOpened()
        {
            base.OnAdOpened();
            AdOpened?.Invoke(null, null);
            Console.WriteLine("OnInterstitialOpened");
        }

        public override void OnAdClosed()
        {
            base.OnAdClosed();
            AdClosed?.Invoke(null, null);
            Console.WriteLine("OnAdClosed");
        }
    }
}
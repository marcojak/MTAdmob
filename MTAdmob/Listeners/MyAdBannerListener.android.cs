using System;
using Android.Gms.Ads;

namespace MarcTron.Plugin.Listeners
{
    public class MyAdBannerListener : AdListener
    {
        public event EventHandler AdClicked;
        public event EventHandler AdClosed;
        public event EventHandler AdImpression;
        public event EventHandler AdOpened;
        public event EventHandler AdFailedToLoad;
        public event EventHandler AdLeftApplication;
        public event EventHandler AdLoaded;

        public override void OnAdClicked()
        {
            base.OnAdClicked();
            AdClicked?.Invoke(this,null);
        }

        public override void OnAdClosed()
        {
            base.OnAdClosed();
            AdClosed?.Invoke(this, null);
        }

        public override void OnAdImpression()
        {
            base.OnAdImpression();
            AdImpression?.Invoke(this, null);
        }

        public override void OnAdOpened()
        {
            base.OnAdOpened();
            AdOpened?.Invoke(this, null);
        }

        //public override void OnAdFailedToLoad(int errorCode)
        //{
        //    base.OnAdFailedToLoad(errorCode);
        //    AdFailedToLoad?.Invoke(this, null);
        //}

        //public override void OnAdLeftApplication()
        //{
        //    base.OnAdLeftApplication();
        //    AdLeftApplication?.Invoke(this, null);
        //}

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();
            AdLoaded?.Invoke(this, null);
        }
    }
}

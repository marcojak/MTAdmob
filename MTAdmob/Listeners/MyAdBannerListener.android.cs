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
    }
}

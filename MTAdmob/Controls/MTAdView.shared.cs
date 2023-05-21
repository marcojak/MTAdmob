using System;
using MarcTron.Plugin.Extra;
using Xamarin.Forms;

namespace MarcTron.Plugin.Controls
{
    // ReSharper disable once InconsistentNaming
    public class MTAdView : View
    {
        public event EventHandler AdsClicked;
        public event EventHandler AdsClosed;
        public event EventHandler AdsImpression;
        public event EventHandler AdsOpened;
        public event EventHandler<MTEventArgs> AdsFailedToLoad;
        public event EventHandler AdsLoaded;
        public event EventHandler AdsSwiped;

        public static readonly BindableProperty AdsIdProperty = BindableProperty.Create("AdsId", typeof(string), typeof(MTAdView));

        public string AdsId
        {
            get => (string)GetValue(AdsIdProperty);
            set => SetValue(AdsIdProperty, value);
        }

        public static readonly BindableProperty AdSizeProperty = BindableProperty.Create("AdSize", typeof(BannerSize), typeof(MTAdView));
        public BannerSize AdSize
        {
            get => (BannerSize)GetValue(AdSizeProperty);
            set => SetValue(AdSizeProperty, value);
        }

        public static readonly BindableProperty AutoSizeProperty = BindableProperty.Create("AutoSize", typeof(bool), typeof(MTAdView));
        public bool AutoSize
        {
            get => (bool)GetValue(AutoSizeProperty);
            set => SetValue(AutoSizeProperty, value);
        }

        internal void AdClicked(object sender, EventArgs e)
        {
            AdsClicked?.Invoke(sender,e);
        }

        internal void AdClosed(object sender, EventArgs e)
        {
            AdsClosed?.Invoke(sender, e);
        }

        internal void AdImpression(object sender, EventArgs e)
        {
            AdsImpression?.Invoke(sender, e);
        }

        internal void AdOpened(object sender, EventArgs e)
        {
            AdsOpened?.Invoke(sender, e);
        }

        internal void AdFailedToLoad(object sender, MTEventArgs e)
        {
            AdsFailedToLoad?.Invoke(sender, e);
        }

        internal void AdLoaded(object sender, EventArgs e)
        {
            AdsLoaded?.Invoke(sender, e);
        }

        internal void AdSwiped(object sender, EventArgs e)
        {
            AdsSwiped?.Invoke(sender, e);
        }
    }
}

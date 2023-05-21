using Android.Gms.Ads;

namespace MarcTron.Plugin.Services
{
    public class MyFullScreenContentCallback : FullScreenContentCallback
    {
        private readonly bool _isInterstitial;

        private readonly MTAdmobImplementation _admobImplementation;

        public MyFullScreenContentCallback(MTAdmobImplementation admobImplementation, bool isInterstitial)
        {
            _admobImplementation = admobImplementation;
            _isInterstitial = isInterstitial;
        }

        public override void OnAdDismissedFullScreenContent()
        {
            base.OnAdDismissedFullScreenContent();
            if (_isInterstitial)
                _admobImplementation.MOnInterstitialClosed();
            else
                _admobImplementation.MOnRewardClosed();
        }

        public override void OnAdFailedToShowFullScreenContent(AdError error)
        {
            base.OnAdFailedToShowFullScreenContent(error);
            if (_isInterstitial)
                _admobImplementation.MOnInterstitialFailedToShow(error);
            else
                _admobImplementation.MOnRewardFailedToShow(error);
        }

        public override void OnAdShowedFullScreenContent()
        {
            base.OnAdShowedFullScreenContent();
            if (_isInterstitial)
                _admobImplementation.MOnInterstitialOpened();
            else
            {
                _admobImplementation.MOnRewardOpened();
            }
        }

        public override void OnAdImpression()
        {
            base.OnAdImpression();
            if (_isInterstitial)
                _admobImplementation.MOnInterstitialImpression();
            else
                _admobImplementation.MOnRewardImpression();
        }
    }
}

using Android.Gms.Ads;

namespace MarcTron.Plugin.Services
{
    public class MyFullScreenContentCallback : FullScreenContentCallback
    {
        private readonly bool isInterstitial;

        public MTAdmobImplementation MTAdmobImplementation { get; }

        public MyFullScreenContentCallback(MTAdmobImplementation mTAdmobImplementation, bool isInterstitial)
        {
            MTAdmobImplementation = mTAdmobImplementation;
            this.isInterstitial = isInterstitial;
        }

        public override void OnAdDismissedFullScreenContent()
        {
            base.OnAdDismissedFullScreenContent();
            if (isInterstitial)
                MTAdmobImplementation.MOnInterstitialClosed();
            else
                MTAdmobImplementation.MOnRewardClosed();
        }

        public override void OnAdFailedToShowFullScreenContent(AdError p0)
        {
            base.OnAdFailedToShowFullScreenContent(p0);
            if (isInterstitial)
                MTAdmobImplementation.MOnInterstitialFailedToShow(p0);
            else
                MTAdmobImplementation.MOnRewardFailedToShow(p0);
        }

        public override void OnAdShowedFullScreenContent()
        {
            base.OnAdShowedFullScreenContent();
            if (isInterstitial)
                MTAdmobImplementation.MOnInterstitialOpened();
            else
            {
                MTAdmobImplementation.MOnRewardOpened();
                MTAdmobImplementation.MOnRewardedVideoAdCompleted();
            }
        }

        public override void OnAdImpression()
        {
            base.OnAdImpression();
            if (isInterstitial)
                MTAdmobImplementation.MOnInterstitialImpression();
            else
                MTAdmobImplementation.MOnRewardImpression();
        }
        
    }
}

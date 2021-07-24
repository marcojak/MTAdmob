using System;
//using Android.Gms.Ads.Reward;
using MarcTron.Plugin.CustomEventArgs;

namespace MarcTron.Plugin.Listeners
{
    public class MyRewardedVideoAdListener : Java.Lang.Object//, IRewardedVideoAdListener
    {
        public event EventHandler<MTEventArgs> OnRewardedEvent;
        public event EventHandler OnRewardedVideoAdClosedEvent;
        public event EventHandler<MTEventArgs> OnRewardedVideoAdFailedToLoadEvent;
        public event EventHandler OnRewardedVideoAdLeftApplicationEvent;
        public event EventHandler OnRewardedVideoAdLoadedEvent;
        public event EventHandler OnRewardedVideoAdOpenedEvent;
        public event EventHandler OnRewardedVideoStartedEvent;
        public event EventHandler OnRewardedVideoCompletedEvent;

        //public void OnRewarded(IRewardItem reward)
        //{
        //    OnRewardedEvent?.Invoke(null, new MTEventArgs() {RewardAmount = reward.Amount, RewardType = reward.Type});
        //}

        public void OnRewardedVideoAdClosed()
        {
            OnRewardedVideoAdClosedEvent?.Invoke(null,null);
        }

        public void OnRewardedVideoAdFailedToLoad(int errorCode)
        {
            OnRewardedVideoAdFailedToLoadEvent?.Invoke(null, new MTEventArgs() {ErrorCode = errorCode});
        }

        public void OnRewardedVideoAdLeftApplication()
        {
            OnRewardedVideoAdLeftApplicationEvent?.Invoke(null, null);
        }

        public void OnRewardedVideoAdLoaded()
        {
            OnRewardedVideoAdLoadedEvent?.Invoke(null, null);
        }

        public void OnRewardedVideoAdOpened()
        {
            OnRewardedVideoAdOpenedEvent?.Invoke(null, null);
        }

        public void OnRewardedVideoStarted()
        {
            OnRewardedVideoStartedEvent?.Invoke(null, null);
        }

        public void OnRewardedVideoCompleted()
        {
            OnRewardedVideoCompletedEvent?.Invoke(null, null);
        }
    }
}
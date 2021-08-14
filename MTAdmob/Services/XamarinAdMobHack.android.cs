using System;
using System.ComponentModel;
using System.Diagnostics;
using Android.Content;
using Android.Runtime;
using Java.Interop;

namespace Android.Gms.Ads.Hack
{
    public abstract class InterstitialAd : Java.Lang.Object
    {
        private static readonly JniPeerMembers _members = new XAPeerMembers("com/google/android/gms/ads/interstitial/InterstitialAd", typeof(InterstitialAd));

        public unsafe static void Load(Context context, string adUnit, AdRequest request, AdLoadCallback callback)
        {
            IntPtr intPtr = JNIEnv.NewString(adUnit);
            try
            {
                JniArgumentValue* ptr = stackalloc JniArgumentValue[4];
                *ptr = new JniArgumentValue(context?.Handle ?? IntPtr.Zero);
                ptr[1] = new JniArgumentValue(intPtr);
                ptr[2] = new JniArgumentValue(request?.Handle ?? IntPtr.Zero);
                ptr[3] = new JniArgumentValue(callback?.Handle ?? IntPtr.Zero);
                _members.StaticMethods.InvokeVoidMethod("load.(Landroid/content/Context;Ljava/lang/String;Lcom/google/android/gms/ads/AdRequest;Lcom/google/android/gms/ads/interstitial/InterstitialAdLoadCallback;)V", ptr);
            }
            finally
            {
                JNIEnv.DeleteLocalRef(intPtr);
                GC.KeepAlive(context);
                GC.KeepAlive(request);
                GC.KeepAlive(callback);
            }
        }
    }

    [Register("com/google/android/gms/ads/interstitial/InterstitialAdLoadCallback", DoNotGenerateAcw = true)]
    public abstract class InterstitialAdLoadCallback : AdLoadCallback
    {
        private static readonly JniPeerMembers _members = new XAPeerMembers("com/google/android/gms/ads/interstitial/InterstitialAdLoadCallback", typeof(InterstitialAdLoadCallback));

        /*internal*/
        static IntPtr class_ref => _members.JniPeerType.PeerReference.Handle;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override JniPeerMembers JniPeerMembers => _members;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override IntPtr ThresholdClass => _members.JniPeerType.PeerReference.Handle;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override Type ThresholdType => _members.ManagedPeerType;

        protected InterstitialAdLoadCallback(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        [Register(".ctor", "()V", "")]
        public unsafe InterstitialAdLoadCallback()
            : base(IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
        {
            if (!(base.Handle != IntPtr.Zero))
            {
                SetHandle(_members.InstanceMethods.StartCreateInstance("()V", GetType(), null).Handle, JniHandleOwnership.TransferLocalRef);
                _members.InstanceMethods.FinishCreateInstance("()V", this, null);
            }
        }

        private static Delegate cb_onAdLoaded;

        private static Delegate GetOnAdLoadedHandler()
        {
            if (cb_onAdLoaded is null)
            {
                cb_onAdLoaded = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, IntPtr>)n_onAdLoaded);
            }

            return cb_onAdLoaded;
        }

        private static void n_onAdLoaded(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
        {
            InterstitialAdLoadCallback? @object = Java.Lang.Object.GetObject<InterstitialAdLoadCallback>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            global::Android.Gms.Ads.Interstitial.InterstitialAd object2 = Java.Lang.Object.GetObject<global::Android.Gms.Ads.Interstitial.InterstitialAd>(native_p0, JniHandleOwnership.DoNotTransfer);
            @object!.OnInterstitialAdLoaded(object2);
        }

        [Register("onAdLoaded", "(Lcom/google/android/gms/ads/interstitial/InterstitialAd;)V", "GetOnAdLoadedHandler")]
        public virtual void OnInterstitialAdLoaded(global::Android.Gms.Ads.Interstitial.InterstitialAd interstitialAd)
        {
        }
    }

    public abstract class RewardedAd : Java.Lang.Object
    {
        private static readonly JniPeerMembers _members = new XAPeerMembers("com/google/android/gms/ads/rewarded/RewardedAd", typeof(RewardedAd));

        public unsafe static void Load(Context context, string adUnit, AdRequest request, RewardedAdLoadCallback callback)
        {
            IntPtr intPtr = JNIEnv.NewString(adUnit);
            try
            {
                JniArgumentValue* ptr = stackalloc JniArgumentValue[4];
                *ptr = new JniArgumentValue(context?.Handle ?? IntPtr.Zero);
                ptr[1] = new JniArgumentValue(intPtr);
                ptr[2] = new JniArgumentValue(request?.Handle ?? IntPtr.Zero);
                ptr[3] = new JniArgumentValue(callback?.Handle ?? IntPtr.Zero);
                _members.StaticMethods.InvokeVoidMethod("load.(Landroid/content/Context;Ljava/lang/String;Lcom/google/android/gms/ads/AdRequest;Lcom/google/android/gms/ads/rewarded/RewardedAdLoadCallback;)V", ptr);
            }
            finally
            {
                JNIEnv.DeleteLocalRef(intPtr);
                GC.KeepAlive(context);
                GC.KeepAlive(request);
                GC.KeepAlive(callback);
            }
        }
    }

    [Register("com/google/android/gms/ads/rewarded/RewardedAdLoadCallback", DoNotGenerateAcw = true)]
    public abstract class RewardedAdLoadCallback : AdLoadCallback
    {
        private static readonly JniPeerMembers _members = new XAPeerMembers("com/google/android/gms/ads/rewarded/RewardedAdLoadCallback", typeof(RewardedAdLoadCallback));

        /*internal*/
        static IntPtr class_ref => _members.JniPeerType.PeerReference.Handle;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override JniPeerMembers JniPeerMembers => _members;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override IntPtr ThresholdClass => _members.JniPeerType.PeerReference.Handle;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override Type ThresholdType => _members.ManagedPeerType;

        protected RewardedAdLoadCallback(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        [Register(".ctor", "()V", "")]
        public unsafe RewardedAdLoadCallback()
            : base(IntPtr.Zero, JniHandleOwnership.DoNotTransfer)
        {
            if (!(base.Handle != IntPtr.Zero))
            {
                SetHandle(_members.InstanceMethods.StartCreateInstance("()V", GetType(), null).Handle, JniHandleOwnership.TransferLocalRef);
                _members.InstanceMethods.FinishCreateInstance("()V", this, null);
            }
        }

        private static Delegate cb_onAdLoaded;

        private static Delegate GetOnAdLoadedHandler()
        {
            if (cb_onAdLoaded is null)
            {
                cb_onAdLoaded = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, IntPtr>)n_onAdLoaded);
            }

            return cb_onAdLoaded;
        }

        private static void n_onAdLoaded(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
        {
            RewardedAdLoadCallback? @object = Java.Lang.Object.GetObject<RewardedAdLoadCallback>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            global::Android.Gms.Ads.Rewarded.RewardedAd object2 = Java.Lang.Object.GetObject<global::Android.Gms.Ads.Rewarded.RewardedAd>(native_p0, JniHandleOwnership.DoNotTransfer);
            @object!.OnRewardedAdLoaded(object2);
        }

        [Register("onAdLoaded", "(Lcom/google/android/gms/ads/rewarded/RewardedAd;)V", "GetOnAdLoadedHandler")]
        public virtual void OnRewardedAdLoaded(global::Android.Gms.Ads.Rewarded.RewardedAd rewardedAd)
        {
        }
    }
}
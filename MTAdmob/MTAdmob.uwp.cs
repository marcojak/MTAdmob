using MarcTron.Plugin.Extra;
using MarcTron.Plugin.Interfaces;
using System;

namespace MarcTron.Plugin
{
    /// <summary>
    /// Interface for MTAdmob
    /// This has not been implemented yet but it could be in a next version or you can implement and send it with a pull request :)
    /// </summary>
    public partial class MTAdmobImplementation : IMTAdmob
    {
        public void ShowRewardedInterstitial()
        {
            throw new NotImplementedException();
        }

        public string GetAdContentRatingString()
        {
            throw new NotImplementedException();
        }

        public bool IsInterstitialLoaded()
        {
            return false;
        }

        public bool IsRewardedLoaded()
        {
            return false;
        }

        public void LoadInterstitial(string adUnit)
        {
        }

        public void LoadRewarded(string adUnit, MTRewardedAdOptions options = null)
        {
        }

        public void ShowInterstitial(string adUnit)
        {
        }

        public void ShowInterstitial()
        {
        }

        public void ShowRewardedVideo(string adUnit)
        {

        }

        public void ShowRewarded()
        {
        }

        public bool IsRewardedInterstitialLoaded()
        {
            return false;
        }

        public void LoadRewardedInterstitial(string adUnit, MTRewardedAdOptions options = null)
        {
        }


        public void SetAppMuted(bool muted)
        {
        }

        public void SetAppVolume(float volume)
        {
        }
    }
}

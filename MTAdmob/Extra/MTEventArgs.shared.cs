using System;

namespace MarcTron.Plugin.Extra
{
    // ReSharper disable once InconsistentNaming
    public class MTEventArgs : EventArgs
    {
        public int? ErrorCode;
        public string ErrorMessage;
        public string ErrorDomain;
        public int RewardAmount;
        public string RewardType;
    }
}

using System;

namespace MarcTron.Plugin.Extra
{
    public class MTEventArgs : EventArgs
    {
        public int? ErrorCode;
        public string ErrorMessage;
        public string ErrorDomain;
        public int RewardAmount;
        public string RewardType;
    }
}

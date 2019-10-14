using System;

namespace MarcTron.Plugin.CustomEventArgs
{
    // ReSharper disable once InconsistentNaming
    public class MTEventArgs : EventArgs
    {
        public int? ErrorCode;
        public int RewardAmount;
        public string RewardType;
    }
}

using System;
using System.ComponentModel;

namespace RealTimeChat.EnumUtil
{
    [Flags]
    public enum EnumMessageType
    {
        [Description("chat")]
        Chat = 0,
        [Description("message")]
        Message = 1,
    }
}

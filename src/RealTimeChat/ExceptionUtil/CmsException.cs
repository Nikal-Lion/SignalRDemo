using RealTimeChat.EnumUtil;
using System;

namespace RealTimeChat.ExceptionUtil
{
    public class CmsException : Exception
    {

        public CmsException(EnumStatusCode enumStatusCode, string msg) : base(msg)
        {

        }
    }
}

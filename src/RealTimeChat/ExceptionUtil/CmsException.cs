using System;
using System.Collections.Generic;
using System.Text;
using RealTimeChat.EnumUtil;

namespace RealTimeChat.ExceptionUtil
{
    public class CmsException : Exception
    {

        public CmsException(EnumStatusCode enumStatusCode, string msg) : base(msg)
        {

        }
    }
}

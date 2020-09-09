using DB.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.Interfaces
{
    public interface IUidClient
    {
        Task<User> GetLoginUser();
        Task<bool> SendMarkdown(CorpSendTextDTO dto);
    }
}
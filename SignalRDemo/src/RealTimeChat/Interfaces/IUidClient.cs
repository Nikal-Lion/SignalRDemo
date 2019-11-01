using RealTimeChat.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.Interfaces
{
    public interface IUidClient
    {
        public Task<User> GetLoginUser();
        public Task SendMarkdown(CorpSendTextDTO dto);
    }
}
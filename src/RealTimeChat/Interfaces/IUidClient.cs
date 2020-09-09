using DB.Core.Entities;
using System.Threading.Tasks;

namespace RealTimeChat.Interfaces
{
    public interface IUidClient
    {
        Task<User> GetLoginUser();
        Task<bool> SendMarkdown(CorpSendTextDTO dto);
    }
}
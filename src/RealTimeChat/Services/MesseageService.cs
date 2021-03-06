using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common;
using DB.Core.Dapper.Util;
using DB.Core.EF.Util.DataRepo;
using DB.Core.Entities;
using Microsoft.AspNetCore.SignalR;
using RealTimeChat.EnumUtil;
using RealTimeChat.ExceptionUtil;
using RealTimeChat.Hubs;
using RealTimeChat.Interfaces;

namespace RealTimeChat.Services
{

    public class MessageService : BaseService<Message, ObjectId>, IMessageService
    {
        private readonly IUidClient _uidClient;
        private readonly IHubContext<MessageHub> _messageHub;

        public MessageService(IMessageRepository repository, IUidClient uidClient, IHubContext<MessageHub> messageHub) : base(repository)
        {
            _uidClient = uidClient;
            _messageHub = messageHub;
        }

        /// <summary>
        /// 添加并推送站内信
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task AddAsync(MessageDTO dto)
        {
            var now = DateTime.Now;
            if (dto is null)
                return;

            var log = new Message
            {
                Id = ObjectId.GenerateNewId(now),
                CreateTime = now,
                Name = dto.Name,
                Detail = dto.Detail,
                ToUser = dto.ToUser,
                Type = dto.Type
            };

            var push = new PushMessageDTO
            {
                Id = log.Id,
                Name = log.Name,
                Detail = log.Detail,
                Type = log.Type,
                ToUser = log.ToUser,
                CreateTime = now
            };

            await DB.Core.EF.Util.DataRepo.Repository.Insert(log).ConfigureAwait(false);
            //推送站内信
            await _messageHub.Clients.Groups(dto.ToUser).SendAsync("newmsg", push).ConfigureAwait(false);
            //推送未读条数
            await SendUnreadCountAsync(dto.ToUser).ConfigureAwait(false);

            if (dto.PushCorpWeixin)
            {
                const string content = @"<font color='blue'>{0}</font>
<font color='comment'>{1}</font>
系统：**CMS**
站内信ID：<font color='info'>{2}</font>
详情：<font color='comment'>{3}</font>";

                //把站内信推送到企业微信
                await _uidClient.SendMarkdown(new CorpSendTextDTO
                {
                    touser = dto.ToUser,
                    content = string.Format(content, dto.Name, now, log.Id, dto.Detail)
                }).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 获取本人的站内信列表
        /// </summary>
        /// <param name="name">标题</param>
        /// <param name="detail">详情</param>
        /// <param name="unread">只显示未读</param>
        /// <param name="type">类型</param>
        /// <param name="createStart">创建起始时间</param>
        /// <param name="createEnd">创建结束时间</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页个数</param>
        /// <returns></returns>
        public async Task<PagedData<PushMessageDTO>> GetMyMessageAsync(
            string name,
            string detail,
            bool unread = false,
            EnumMessageType? type = null,
            DateTime? createStart = null,
            DateTime? createEnd = null,
            int pageIndex = 1,
            int pageSize = 10)
        {
            var user = await _uidClient.GetLoginUser().ConfigureAwait(false);
            Expression<Func<Message, bool>> exp = o => o.ToUser == user.Account;

            if (unread)
            {
                exp = exp.And(o => o.ReadTime == null);
            }
            if (!string.IsNullOrEmpty(name))
            {
                exp = exp.And(o => o.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(detail))
            {
                exp = exp.And(o => o.Detail.Contains(detail));
            }
            if (type != null)
            {
                exp = exp.And(o => o.Type == type.Value.ToString());
            }
            if (createStart != null)
            {
                exp.And(o => o.CreateTime >= createStart.Value);
            }
            if (createEnd != null)
            {
                exp.And(o => o.CreateTime < createEnd.Value);
            }

            return await QueryUtil.FindPageObjectList(
                exp,
                o => o.Id,
                true,
                pageIndex,
                pageSize,
                o => new PushMessageDTO
                {
                    Id = o.Id,
                    CreateTime = o.CreateTime,
                    Detail = o.Detail,
                    Name = o.Name,
                    ToUser = o.ToUser,
                    Type = o.Type,
                    ReadTime = o.ReadTime
                }
            );
        }

        /// <summary>
        /// 设置已读
        /// </summary>
        /// <param name="id">站内信ID</param>
        /// <returns></returns>
        public async Task ReadAsync(ObjectId id)
        {
            var msg = await QueryUtil.First<Message>(id).ConfigureAwait(false);

            if (msg == null)
            {
                throw new CmsException(EnumStatusCode.ArgumentOutOfRange, "不存在此站内信");
            }

            if (msg.ReadTime != null)
            {
                //已读的不再更新读取时间
                return;
            }

            msg.ReadTime = DateTime.Now;
            await Repository.Update(msg, "ReadTime").ConfigureAwait(false);
            await SendUnreadCountAsync(msg.ToUser).ConfigureAwait(false);
        }

        /// <summary>
        /// 设置本人全部已读
        /// </summary>
        /// <returns></returns>
        public async Task ReadAllAsync()
        {
            var user = await _uidClient.GetLoginUser().ConfigureAwait(false);

            await Repository.UpdateMany(
                o => o.ToUser == user.Account && o.ReadTime == null,
                o => new Message
                {
                    ReadTime = DateTime.Now
                }).ConfigureAwait(false);

            await SendUnreadCountAsync(user.Account).ConfigureAwait(false);
        }

        /// <summary>
        /// 获取本人未读条数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetUnreadCountAsync()
        {
            var user = await _uidClient.GetLoginUser().ConfigureAwait(false);
            return await QueryUtil.Count(user.Account, null).ConfigureAwait(false);
        }

        /// <summary>
        /// 推送未读数到前端
        /// </summary>
        /// <returns></returns>
        public async Task SendUnreadCountAsync(string account)
        {
            int count = await QueryUtil.Count(account, null).ConfigureAwait(false);

            await _messageHub.Clients.Groups(account).SendAsync("unread", count).ConfigureAwait(false);
        }
    }
}
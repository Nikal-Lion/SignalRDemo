using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using RealTimeChat.Interfaces;
using System.Threading.Tasks;

namespace RealTimeChat.SocketHelper
{
	public class MessageHub : Hub
    {
        private readonly IUidClient _uidClient;

        public MessageHub(IUidClient uidClient)
        {
            _uidClient = uidClient;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _uidClient.GetLoginUser();
            //将同一个人的连接ID绑定到同一个分组，推送时就推送给这个分组
            await Groups.AddToGroupAsync(Context.ConnectionId, user.Account);
        }
	}
}
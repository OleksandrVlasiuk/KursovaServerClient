using ChatAndComentaries;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleChatWPF.Hubs
{
    public static class Manage
    {
        public static List<ChatUser> Users = new List<ChatUser>();
    }

    public class ChatHub : Hub
    {
        public void Connect(string userId)
        {
            Manage.Users.Add(new ChatUser()
            {
                Id = userId,
                ConnectionId = this.Context.ConnectionId
            });
        }

        public void SendAll(string text)
        {
            Clients.Clients(Manage.Users.Select(t => t.ConnectionId).ToList()).SendAsync("sendAll", text);
        }

        public void SendToUser(string text, string userId)
        {
            Clients.Client(Manage.Users.FirstOrDefault(t => t.Id == userId).ConnectionId).SendAsync("sendAll", text);
        }

        public void Disconnect(string userId)
        {
            Manage.Users.Remove(Manage.Users.FirstOrDefault(t => t.Id == userId));
        }
    }
}

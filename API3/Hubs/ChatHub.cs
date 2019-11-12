using API3.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API3.Hubs
{
    public static class Manage
    {
    public static List<ForChatUser>Users { get; set; }
    }
    public class ChatHub:Hub
    {
        public void Connect(string userId)
        {
            Manage.Users.Add(new ForChatUser()
            {
                Id = userId,
                ConnectionId = this.Context.ConnectionId
            }
            );
        }

        public void SendAll(string text)
        {
            Clients.Clients(Manage.Users.Select(t => t.ConnectionId).ToList()).SendAsync("SendAll",text);
        }

        public void SendToUser(string text, string userId)
        {
            Clients.Client(Manage.Users.FirstOrDefault(t=>t.Id==userId).ConnectionId).SendAsync("sendToUser",text);
        }


        public void Disonnect(string userId)
        {
            Manage.Users.Remove(Manage.Users.FirstOrDefault(t=>t.Id == userId));
        }

    }
}

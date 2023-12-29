using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
namespace AttendanceManagementSystem.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public static Dictionary<string, DateTime> activeUsers = new Dictionary<string, DateTime>();
        public void Send(string name, string message, string avatar)
        {
            DateTime date = DateTime.UtcNow;
            if (!activeUsers.ContainsKey(name))
            {
                activeUsers.Add(name, date);
            }
            else
            {
                activeUsers[name] = date;
            }
            Clients.All.addNewMessageToPage(name, message, avatar);
        }
        private void CheckActiveConnections()
        {
            foreach (KeyValuePair<string, DateTime> entry in activeUsers)
            {
                if(entry.Value.AddMinutes(3) <= DateTime.UtcNow)
                {
                    activeUsers.Remove(entry.Key);
                }
            }
        }
        public List<string> GetAllActiveConnections()
        {
            CheckActiveConnections();
            List<string> userNames = new List<string>();
            foreach (KeyValuePair<string, DateTime> entry in activeUsers)
            {
                userNames.Add(entry.Key); 
            }
            return userNames;
        }
        public Task JoinRoom(string roomName)
        {
            return Groups.Add(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }
        public async Task JoinRooms(string roomName)
        {
            await Groups.Add(Context.ConnectionId, roomName);
            Clients.Group(roomName).addChatMessage(Context.User.Identity.Name + " joined.");
        }

        //public override Task OnConnected()
        //{
        //    using (var db = new UserContext())
        //    {
        //        var user = db.Users
        //            .Include(u => u.Rooms)
        //            .SingleOrDefault(u => u.UserName == Context.User.Identity.Name);

        //        if (user == null)
        //        {
        //            user = new User()
        //            {
        //                UserName = Context.User.Identity.Name
        //            };
        //            db.Users.Add(user);
        //            db.SaveChanges();
        //        }
        //        else
        //        {
        //            // Add to each assigned group.
        //            foreach (var item in user.Rooms)
        //            {
        //                Groups.Add(Context.ConnectionId, item.RoomName);
        //            }
        //        }
        //    }
        //    return base.OnConnected();
        //}

        //public void AddToRoom(string roomName)
        //{
        //    using (var db = new UserContext())
        //    {
        //        // Retrieve room.
        //        var room = db.Rooms.Find(roomName);

        //        if (room != null)
        //        {
        //            var user = new User() { UserName = Context.User.Identity.Name };
        //            db.Users.Attach(user);

        //            room.Users.Add(user);
        //            db.SaveChanges();
        //            Groups.Add(Context.ConnectionId, roomName);
        //        }
        //    }
        //}

        //public void RemoveFromRoom(string roomName)
        //{
        //    using (var db = new UserContext())
        //    {
        //        // Retrieve room.
        //        var room = db.Rooms.Find(roomName);
        //        if (room != null)
        //        {
        //            var user = new User() { UserName = Context.User.Identity.Name };
        //            db.Users.Attach(user);

        //            room.Users.Remove(user);
        //            db.SaveChanges();

        //            Groups.Remove(Context.ConnectionId, roomName);
        //        }
        //    }
        //}
    }
    //Clients.Group(groupName, connectionId1, connectionId2).addChatMessage(name, message);
}

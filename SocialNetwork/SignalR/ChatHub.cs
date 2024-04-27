using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Models.DatabaseModels;

namespace SocialNetwork.SignalR
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string messageText,string idAccount,string idChat)
        {
            string chatName = Chat.Read(Convert.ToInt32(idChat)).Name;
            string account = Account.Read(Convert.ToInt32(idAccount)).Name;
            DateTime currentTime = DateTime.Now;
            int id;
            Message message = new Message() {IdAccount=Convert.ToInt32(idAccount),IdChat=Convert.ToInt32(idChat),Text=messageText,Time=currentTime};
            Message.Create(message,out id);
            await Clients.Group(chatName).SendAsync("ReceiveMessage",messageText,account, Convert.ToString(currentTime),Convert.ToInt32(id));
        }

        public Task AddToGroup(string idChat)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, Chat.Read(Convert.ToInt32(idChat)).Name);
        }

        public Task RemoveFromGroup(string idChat)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, Chat.Read(Convert.ToInt32(idChat)).Name);
        }
        public async Task DeleteMessage(int idMessage)
        {
            Message.Delete(idMessage);
        }
        public async Task UpdateMessage(string TextMessage,int idMessage)
        {
            Message message = Message.Read(idMessage);
            message.Text = TextMessage;
            Message.Update(message);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Models.DatabaseModels;

namespace SocialNetwork.Controllers
{
    public class ChatController : Controller
    {
        public async Task<IActionResult> Chat(int id, int idChat)
        {
            return View(SocialNetwork.Models.DatabaseModels.Chat.Read(idChat));
        }

        public async Task<IActionResult> EnterChat(int id, int idAccount)
        {
            var searchChatId = AccountInChat.GetAll()
                .Where(aic => aic.IdAccount == idAccount)
                .Join(
                    AccountInChat.GetAll().Where(aic => aic.IdAccount == id),
                    aic1 => aic1.IdChat,
                    aic2 => aic2.IdChat,
                    (aic1, aic2) => new { ChatId = aic1.IdChat, AccountInChat1 = aic1, AccountInChat2 = aic2 }
                );

            if (searchChatId.IsNullOrEmpty())
            {
                int idChat;
                Chat newChat = new Chat() { Name = "default name" };
                SocialNetwork.Models.DatabaseModels.Chat.Create(newChat, out idChat);
                AccountInChat firstAccountInChat = new AccountInChat() { IdAccount = id, IdChat = idChat };
                AccountInChat.Create(firstAccountInChat);
                AccountInChat secondAccountInChat = new AccountInChat() { IdAccount = idAccount, IdChat = idChat };
                AccountInChat.Create(secondAccountInChat);
                return RedirectToAction("Chat", new { id = id, idChat = newChat.Id });
            }
            else
            {
                return RedirectToAction("Chat", new { id = id, idChat = searchChatId.First().ChatId });
            }
        }

        public async Task<IActionResult> Chats(int id) => View(SocialNetwork.Models.DatabaseModels.Account.Read(id).Chats.Where(chat=>chat.IsDeleted==false).ToList());

        public async Task<IActionResult> Delete(int id,int idChat)
        {
            if (SocialNetwork.Models.DatabaseModels.Chat.Delete(idChat))
            {
                return RedirectToAction("Chats", new { id = id});
            }
            else
            {
                return View("Error", "Couldn't delete the chat");
            }
        }

        public async Task<IActionResult> Edit(int id, int idChat)
        { 
            return View("EditChat", SocialNetwork.Models.DatabaseModels.Chat.Read(Convert.ToInt32(idChat))); 
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id,int idChat,string updateName)
        {
            Chat chat = SocialNetwork.Models.DatabaseModels.Chat.Read(idChat);
            chat.Name = updateName;
            if (SocialNetwork.Models.DatabaseModels.Chat.Update(chat))
            {
                return RedirectToAction("Chat",new {id=id,idChat=idChat});
            }
            else
            {
                return View("Error", "Couldn't update the chat");
            }
        }

    }

}
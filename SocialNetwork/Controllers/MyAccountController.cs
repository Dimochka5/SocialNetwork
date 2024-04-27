using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SocialNetwork.Models.DatabaseModels;

namespace SocialNetwork.Controllers
{
    public class MyAccountController : Controller
    {
        public async Task<ActionResult> MyAccount(int id){
            if (id != -1)
            {
                Account account = Account.Read(id);
                if (account != null)
                {
                    return View(account);
                }
                else
                {
                    return View("Error", "User does not exist");
                }
            }
            else
            {
                return View("Error", "User does not exist");
            }
        }
     
        public async Task<ActionResult> Edit(int id)
        {
            if (id != -1)
            {
                Account account = Account.Read(id);
                if (account != null)
                {
                    return View(account);
                }
                else
                {
                    return View("Error", "A");
                }
            }
            else
            {
                return View("Error", "User does not exist");
            }

        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id,string? NewName,string? NewDescription, IFormFile? NewImage)
        {
            Account account = Account.Read(id);
            if (account != null)
            {
                if (NewName != null)
                {
                    account.Name = NewName;
                }
                if (NewDescription != null)
                {
                    account.Description = NewDescription;
                }
                if (NewImage != null)
                {
                    AccountImage newImage = new AccountImage() { IdAccount=id,IsDeleted=false};
                    using (var memoryStream = new MemoryStream())
                    {
                        NewImage.CopyToAsync(memoryStream);
                        newImage.Image = memoryStream.ToArray();
                    }
                    if (!AccountImage.Create(newImage))
                    {
                        return View("Error", "Image couldn't load");
                    }
                }

                if (Account.Update(account))
                {
                    return View("MyAccount", account);
                }
                else
                {
                    return View("Error","Couldn't update");
                }
            }
            else
            {
                return View("Error", "User does not exist");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            if (Account.Delete(id))
            {
                return RedirectToAction("Login", "RegLog");
            }
            else
            {
                return View("Error", "Couldn't delete");
            }
        }

        public async Task<ActionResult> Accounts(int id)=>View(Account.GetAll().ToList());

        public async Task<ActionResult> AnotherAccount(int id,int idAccount)
        {
            if (id != idAccount) {
                return View("Account", Account.Read(idAccount));
            }
            else
            {
                return RedirectToAction("MyAccount",new { id = id});
            }
        }
    }
}

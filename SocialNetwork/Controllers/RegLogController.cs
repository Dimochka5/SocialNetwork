using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Models.DatabaseModels;

namespace SocialNetwork.Controllers
{
    public class RegLogController:Controller
    {

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(string name,string email,string password,string passwordCheck)
        {
            if (name == null || name.Length == 0 || email == null || email.Length == 0 || password == null || password.Length == 0 || passwordCheck == null || passwordCheck.Length == 0)
            {
                    return View("Error", "Data isn't correct");
            }
            else
            {
                if (string.Equals(passwordCheck, password))
                {
                    int id;
                    Account newAccount = new Account() { Name=name, Email=email, Password=password};
                    if (Account.Create(newAccount,out id))
                    {
                        return RedirectToAction("MyAccount", "MyAccount", new { id = id });
                    }
                    else
                    {
                        return View("Error", "User couldn't create");
                    }
                    
                }
                else
                {
                    return View("Error", "Passwords isn't similar");
                }
            }
        }

        [HttpGet]
        public async Task<ActionResult> Login() {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Entering(string Email,string Password) {
            if (Email!=null&&Password!=null)
            {
                int id;
                if (Account.UserExist(Email,Password,out id)) {
                    return RedirectToAction("MyAccount", "MyAccount", new { id = id });
                }
                else
                {
                    return View("Error", "User does not exist");
                }
            }
            else
            {
                return View("Error", "Fields are empty");
            }
        }
    }
}

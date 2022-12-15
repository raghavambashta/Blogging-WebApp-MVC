using AspNetCoreHero.ToastNotification.Abstractions;
using Blogger.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Blogger.Controllers
{
    public class LoginController : Controller
    {
        private readonly INotyfService _notyf;
        public LoginController(INotyfService notyf)
        {
            _notyf = notyf;
        }

        //BloggerMvcContext db = new BloggerMvcContext();
        BloggerContext db = new BloggerContext();

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult UserPage()
        {
            _notyf.Success("Logged In Successfully");
            return RedirectToAction("List", "Post");
        }

        //function to encode password received from login form to compare with already encoded password of registered form
        public static string Encode(string value)
        {
            StringBuilder builder = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

        public IActionResult LoginUser(Login login)
        {
            if (ModelState.IsValid)
            {
                //encoding password received from login form and comparing that with
                //already encoded password in Rauser table
                string password = Encode(login.Password);
                login.Password = password;
                var accountfound = db.Users.Where(u => u.Email == login.Email
                        && u.Password == login.Password).SingleOrDefault();
                //User user;
                //Debug.WriteLine(login.Email);
                //Debug.WriteLine(login.Password);
                //Debug.WriteLine(accountfound);
                if (accountfound != null)
                {

                    TempData["Message"] = " Succesfully Logged In";
                    return RedirectToAction("UserPage");
                }
                else
                {
                    TempData["Message"] = "Incorrect Id or Password";
                    return RedirectToAction("UserPage");
                }
            }
            return RedirectToAction("UserPage");

        }
    }
}

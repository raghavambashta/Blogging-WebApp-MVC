using AspNetCoreHero.ToastNotification.Abstractions;
using Blogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Blogger.Controllers
{
    public class RegisterController : Controller
    {
        BloggerContext db = new BloggerContext();

        private readonly INotyfService _notyf;
        public RegisterController(INotyfService notyf)
        {
            _notyf = notyf;
        }

        public IActionResult Register()
        {
            return View();
        }


        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            _notyf.Success("Success");
            return View(await db.Users.ToListAsync());
        }

        public static String Encode(String value)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,UserName,Email,Password")] User user)
        {
            if (user.Id == 0)
            {
                //encoding password
                string password = user.Password;
                string encodedData = Encode(password);
                user.Password = encodedData;
                db.Add(user);
            }
            else
                db.Update(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public IActionResult AddorEdit(int id = 0)
        //{
        //    if (id == 0)
        //        return View(new User());
        //    else
        //        return View(db.Users.Find(id));
        //}


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}

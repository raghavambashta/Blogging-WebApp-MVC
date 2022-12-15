using AspNetCoreHero.ToastNotification.Abstractions;
using Blogger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Controllers
{
    public class ContactController : Controller
    {
        private readonly INotyfService _notyf;
        public ContactController(INotyfService notyf)
        {
            _notyf = notyf;
        }


        //BloggerMvcContext db = new BloggerMvcContext();
        BloggerContext db = new BloggerContext();

        public IActionResult contact()
        {
            return View();
        }


        public async Task<IActionResult> list()
        {
            //_notyf.Success("Success");
            return View(await db.Contacts.ToListAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Id,Name,Email,Phone, Gender, Subject, Message")] Contact contact)
        {
            db.Add(contact);
            await db.SaveChangesAsync();
            _notyf.Success("Submitted!");
            return RedirectToAction("list");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await db.Contacts.FindAsync(id);
            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(list));
        }

    }
}

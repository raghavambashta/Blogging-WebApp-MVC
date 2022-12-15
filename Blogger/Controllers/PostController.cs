using Blogger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Blogger.Controllers
{
    public class PostController : Controller
    {
        BloggerContext db = new BloggerContext();

        // GET: PostController
        public IActionResult New()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            return View(await db.Posts.ToListAsync());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            var post = db.Posts.Where(s => s.Id == id).FirstOrDefault();
            return View(post);
        }

        public ActionResult Edit(int id)
        {
            var post = db.Posts.Where(s => s.Id == id).FirstOrDefault();

            string str = post.PostedOn;
            char[] spearator = { ' ' };
            String[] strList = str.Split(spearator);
            String[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug"
                        ,"Sep", "Oct", "Nov", "Dec"};

            int monthNumber = 0;

            string s1 = strList[1];
            s1 = s1.Remove(s1.Length - 1, 1);
            System.Diagnostics.Debug.WriteLine(s1);
            for (int i = 0; i < months.Length; i++)
            {
                string s2 = months[i];
                //System.Diagnostics.Debug.WriteLine(s2);
                if (String.Equals(s1, s2))
                { 
                    monthNumber = i + 1;
                }
            }
            string monthNum = monthNumber.ToString();

            if (monthNumber < 10)
            {
                monthNum = "0" + monthNum;
            }
            
            string date = strList[2] + "-" + monthNum + "-" + strList[0];

            post.PostedOn = date;

            return View(post);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("Id,Title,Description,Author,PostedOn")] Post post)
        {
            if (post.Id == 0)
            {
                string str = post.PostedOn;
                char[] spearator = { '-' };
                String[] strList = str.Split(spearator);
                String[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug"
                        ,"Sep", "Oct", "Nov", "Dec"};
                int n = int.Parse(strList[1]);
                strList[1] = months[n - 1];

                string date = strList[2] + " " + strList[1] + ", " + strList[0];
                post.PostedOn = date;
                //System.Diagnostics.Debug.WriteLine("Hello");

                db.Add(post);
            }
            else
            {
                string str = post.PostedOn;
                char[] spearator = { '-' };
                String[] strList = str.Split(spearator);
                String[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug"
                        ,"Sep", "Oct", "Nov", "Dec"};
                int n = int.Parse(strList[1]);
                strList[1] = months[n - 1];

                string date = strList[2] + " " + strList[1] + ", " + strList[0];
                post.PostedOn = date;
                db.Update(post);
            }
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }
    }
}

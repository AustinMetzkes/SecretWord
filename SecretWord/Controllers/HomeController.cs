using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretWord.Data;
using SecretWord.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace SecretWord.Controllers


{
    [Authorize]
    public class HomeController : Controller
    {
    private readonly ApplicationDbContext db;
    private readonly UserManager<ApplicationUser> user;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> username)
        {
            db = context;
            user = username;
        }

        public IActionResult Index()
        {

            var word = db.SecretWord;

            return View();
        }
        public IActionResult AddWord(string SecretWord)
        {
            SecretWordModel n = new SecretWordModel();
            n.TimeStamp = DateTime.Now;
           // newWord.Username = User.Identity.Name;
            n.Word = SecretWord;
            db.SecretWord.Add(n);
            db.SaveChanges();

            return Redirect(@"/Home/Words");
        }
        public IActionResult Words()
        {
            var allWords = db.SecretWord.OrderByDescending(am => am.TimeStamp);
            return View(allWords);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

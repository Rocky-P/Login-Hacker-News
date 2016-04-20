using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginUser;
using System.Web.Security;
using _3._13Routing.Models;
using AttributeRouting.Web.Mvc;

namespace _3._13Routing.Controllers
{
    public class HomeController : Controller
    {
        string ConnectionString = @"Data Source=.\sqlexpress;Initial Catalog=Hacker;Integrated Security=True";

        //public List<Link> linkToShow;
        //public Link l;

        public ActionResult Index()
        {
            //int x = 1;
            ViewModel vm = new ViewModel();
            UserManager um = new UserManager(ConnectionString);
            LinkManager lm = new LinkManager(ConnectionString);
            //ViewBag.Counter = x;
            vm.Alllink = lm.GetAllLinks();
            return View(vm);
        }

        public ActionResult Signin()
        {
            return View();
        }
        [Authorize]
        public ActionResult Secret()
        {
            ViewModel vm = new ViewModel();
            UserManager um = new UserManager(ConnectionString);
            LinkManager lm = new LinkManager(ConnectionString);


            string username = User.Identity.Name;
            vm.UserId = um.GetUserbyUsername(username);
            vm.LoginUsers = um.GetAllUser();
            vm.User = um.GetUserbyUsernameReturnUser(username);
            vm.Date = DateTime.Now;
            vm.Alllink = lm.GetAllLinks();
            vm.linksByUser = lm.GetLinkbyUsers(username);
            //linkToShow.Add(l);
            //vm.linkToShow = linkToShow;


            return View(vm);
        }

        [HttpPost]
        public ActionResult Signin(string username, string password)
        {
            var mgr = new UserManager(ConnectionString);
            var user = mgr.GetUser(username, password);
            if (user == null)
            {
                TempData["error"] = "Incorrect User Name or Password";
                return View();
            }

            FormsAuthentication.SetAuthCookie(user.UserName, true);
            return RedirectToAction("Secret");
        }
        [HttpPost]
        public ActionResult Post(Link linktoPost)
        {
            LinkManager lm = new LinkManager(ConnectionString);

            Link l1 = lm.AddLink(linktoPost);
            //l = l1;

            //maybe return to users?
            //maybe change all redirect to users
            return RedirectToAction("secret");
        }
        [Route("Users/{userId}")]
        public ActionResult Users(int userId)
        {
            ViewModel vm = new ViewModel();
            LinkManager lm = new LinkManager(ConnectionString);
            UserManager um = new UserManager(ConnectionString);
            IEnumerable<Link> linkById = lm.GetLinkbyUserId(userId);
            vm.UserId = um.GetUserId(userId);
            vm.linksByUser = linkById;
            return View(vm);
        }

        [Route("upvote/{postId}")]
        public ActionResult upvote(int postId)
        {
            return View();
        }

        //public ActionResult ShowPeople()
        //{
        //    LinkManager lm = new LinkManager(ConnectionString);
        //    IEnumerable<Link> ShowLinks= lm.GetAllLinks();
        //    return Json(ShowLinks);

        //}


    }
}

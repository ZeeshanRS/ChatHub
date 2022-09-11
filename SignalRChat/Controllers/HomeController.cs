using SignalRChat.HubClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var cookieUserName = Request.Cookies["UserInfo"];
            if (cookieUserName == null) 
            {
                return RedirectToAction("NewUser");
            }
            ViewBag.UserName = HttpUtility.UrlDecode(cookieUserName.Value);
            if (cookieUserName.Value.Equals("Admin"))
            {
                return Redirect("/Home/Admin");
            }
            return View();
        }
        public ActionResult Admin()
        {
            var cookieUserName = Request.Cookies["UserInfo"];
            if (cookieUserName == null)
            {
                return RedirectToAction("NewUser");
            }
            ViewBag.UserName = HttpUtility.UrlDecode(cookieUserName.Value);
            return View();
        }

        public ActionResult NewUser()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult NewUser(string userName, string userEmail, string userNote, string userPassword, string hfMode)
        {
            if (hfMode == "Login")
            {
                var user = SignalRChat.Models.User.Authenticate(userEmail, userPassword);
                if (user != null)
                {
                    Response.Cookies.Add(new HttpCookie("UserInfo", HttpUtility.UrlEncode(user.Name)));
                    return Redirect(user.Admin?"/Home/Admin":"/");
                }
                else
                {
                    ViewBag.Msg = "Login Failed";
                    return View();
                }
            }
            SignalRChat.Models.User.SaveAnonymousUser(userName, userEmail, userPassword, userNote);
            Session["userName"] = userName;
            Response.Cookies.Add(new HttpCookie("UserInfo", HttpUtility.UrlEncode(userName)));
            return Redirect("/");
        }
        
        public ActionResult SignOut()
        {
            if (Request.Cookies["UserInfo"] != null)
            {
                //Fetch the Cookie using its Key.
                HttpCookie nameCookie = Request.Cookies["UserInfo"];

                //Set the Expiry date to past date.
                nameCookie.Expires = DateTime.Now.AddDays(-1);

                //Update the Cookie in Browser.
                Response.Cookies.Add(nameCookie);
            }
            return RedirectToAction("/");
        }
        
        public ActionResult CreateAdminAccount()
        {
            if (!dbHelper.Users.GetAll().Any())
            {
                var newUser = new SignalRChat.Models.User()
                {
                    Name = ConfigurationManager.AppSettings["Admin-DefaultName"],
                    Email = ConfigurationManager.AppSettings["Admin-DefaultEmail"],
                    Password = ConfigurationManager.AppSettings["Admin-DefaultPassword"],
                    PasswordKey = "",
                    Admin = true,
                    ConnectionId = "",
                    Note = "",
                    LastLogin = DateTime.Now
                };
                SignalRChat.Models.User.NewUser(newUser);
            }
            
            return RedirectToAction("Index");
        }
        //for future use if we want to send message through controller

        //public ActionResult SendSystemMsg()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public string SendSystemMsg(string msg)
        //{
        //    GroupChat.Instance.SendSystemMsg(msg);
        //    return "s";
        //}

    }
}
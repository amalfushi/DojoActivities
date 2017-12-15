using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DojoActivities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DojoActivities.Controllers
{
    
    public class UserController : Controller
    {
        private DojoActivitiesContext _context;
        
        public UserController(DojoActivitiesContext context)
        {
            _context = context;
        }

//****************************************************************************************************/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {

            // Lazy login.  remove this when completed
            // LoginUser dustin = new LoginUser();
            // dustin.LogEmail = "dps@gmail.com";
            // dustin.LogPassword = "password";

            // return Login(dustin);
            return View();
        }

//****************************************************************************************************/
        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser(RegisterUser u)
        {
            if(ModelState.IsValid){
                List<User> users = _context.users.Where(nu => nu.Email == u.Email).ToList();
                if(users.Count > 0){
                    ViewBag.InvalidEmail = true;
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();

                User nUser = new User();
                nUser.Password = Hasher.HashPassword(nUser, u.Password);
                nUser.FirstName = u.FirstName;
                nUser.LastName = u.LastName;
                nUser.Email = u.Email;
                // nUser.Wallet = 1000.00;

                _context.Add(nUser);
                _context.SaveChanges();
                
                HttpContext.Session.SetInt32("UserId", nUser.UserId);
                return Redirect("/dashboard");
            }
            return View("Index");
        }

//****************************************************************************************************/
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser lu)
        {
            if(ModelState.IsValid)
            {
                User user = _context.users.Where(u => u.Email == lu.LogEmail).SingleOrDefault();
                if(user != null)
                {   
                    PasswordHasher<User> hasher = new PasswordHasher<User>();
                    if(hasher.VerifyHashedPassword(user, user.Password, lu.LogPassword) != 0)
                    {
                        HttpContext.Session.SetInt32("UserId", user.UserId);
                        return Redirect("/dashboard");
                    }
                }
                ViewBag.InvalidLogin = true;
                return View ("Index");
            }
            return View("Index");
        }

//****************************************************************************************************/
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}
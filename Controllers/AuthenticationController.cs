using System;
using System.Web.Mvc;
using FileHandlingSystem.BL;
using FileHandlingSystem.DL;
using FileHandlingSystem.Resources;
using FIleHandlingSystem.VO;
using static FileHandlingSystem.DL.ClsAuthenticator;

namespace mvc_web.Controllers
{
    public class AuthenticationController : Controller
    {
        public static bool isValid;
        public ActionResult AuthenticationForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuthenticationForm(FormCollection form)
        {
            string username = form["Username"];
            string password = form["Password"];
            string userType = form["UserType"];


            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(userType))
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View();
            }


            AuthenticatorValueObject vo = new AuthenticatorValueObject
            {
                username = username,
                password = password,
                userType = (UserTypes)Enum.Parse(typeof(UserTypes), userType)
            };

            ClsDBAuthenticationDataLayer dataLayer = new FileHandlingSystem.DL.ClsDBAuthenticationDataLayer();

            ValueLayerObject vlo = HttpContext.Application["FHP.vo"] as ValueLayerObject;
            clsBusinessLogicBL bl = HttpContext.Application["FHP.bl"] as clsBusinessLogicBL;
            vlo.connectionString = "Data Source=DESKTOP-L8C9MNU;Database=FHP;Integrated Security=True;";
            vlo.dataStorageMethod = "database";



            bool isValidUser = dataLayer.Authenticate(vo, vlo);

            if (isValidUser)
            {
                isValid = true;
                bl.Initialize();
                HttpContext.Application["AuthorizedViewAccessed"] = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {

                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }
    }
}

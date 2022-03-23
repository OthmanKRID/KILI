using Kili.Models;
using Kili.Models.General;
using Kili.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Kili.Controllers
{
    public class UserAccountController : Controller
    {
        
       public IActionResult CreerUserAccount()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreerUserAccount(UserAccountViewModel viewModel)
        {
            //if (!ModelState.IsValid) { 
                

            UserAccount_Services userAccount_Services = new UserAccount_Services();
            {
                int id = userAccount_Services.CreerUserAccount(viewModel.UserAccount.UserName, viewModel.UserAccount.Password, viewModel.UserAccount.Mail, viewModel.UserAccount.Role);

                
                 var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, id.ToString()),
                        new Claim(ClaimTypes.Role, TypeRole.Utilisateur.ToString()),
                    };

                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });

                HttpContext.SignInAsync(userPrincipal);
                
                return Redirect("/home/index");
                }
            // } return View();
        }
        
        public IActionResult ModifierUserAccount(int id)
        {
            if (id != 0)
            {
                UserAccount_Services userAccount_Services = new UserAccount_Services();
                {
                    UserAccount userAccount = userAccount_Services.ObtenirUserAccounts().Where(r => r.Id == id).FirstOrDefault();

                    if (userAccount == null)
                    {
                        return View("Error");
                    }
                    return View(new UserAccountViewModel() {UserAccount = userAccount});
                }

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierUserAccount(UserAccountViewModel viewModel)
        {

            if (viewModel.UserAccount.Id != 0)
            {
                UserAccount_Services userAccount_Services = new UserAccount_Services();
                {
                    userAccount_Services.ModifierUserAccount(viewModel.UserAccount.Id, viewModel.UserAccount.UserName, viewModel.UserAccount.Password, viewModel.UserAccount.Mail, viewModel.UserAccount.Role);
                    return RedirectToAction("ModifierUserAccount", new { @id = viewModel.UserAccount.Id });
                }
            }
            else
            {
                return View("Error");
            }
        }

        /*
        public IActionResult AfficherUserAccount(int id)
        {
            if (id != 0)
            {
                UserAccount_Services userAccount_Services = new UserAccount_Services();
                {
                    UserAccount userAccount = userAccount_Services.ObtenirUserAccounts().Where(r => r.Id == id).FirstOrDefault();
                    if (userAccount == null)
                    {
                        return View("Error");
                    }
                    return View(new UserAccountViewModel() { UserAccount = userAccount });
                }

            }
            return View("Error");
        }
        */

        public IActionResult SupprimerUserAccount(int id)
        {
            if (id != 0)
            {
                UserAccount_Services userAccount_Services = new UserAccount_Services();
                {
                    UserAccount userAccount = userAccount_Services.ObtenirUserAccounts().Where(r => r.Id == id).FirstOrDefault();
                    if (userAccount == null)
                    {
                        return View("Error");
                    }

                    userAccount_Services.SupprimerUserAccount(id);
                    HttpContext.SignOutAsync();
                    return Redirect("/home/index");
                }

            }
            return View("Error");
        }


        /*
        public IActionResult AfficherActions(int id)
        {
            if (id != 0)
            {
                UserAccount_Services userAccount_Services = new UserAccount_Services();
                {
                    UserAccount userAccount = userAccount_Services.ObtenirUserAccounts().Where(r => r.Id == id).FirstOrDefault();
                    if (userAccount == null)
                    {
                        return View("Error");
                    }
                    return View(new UserAccountViewModel() { UserAccount = userAccount });
                }

            }
            return View("Error");
        }
        */



    }
}

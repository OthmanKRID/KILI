using Kili.Models;
using Kili.Models.General;
using Kili.Models.Services;
using Kili.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kili.Controllers
{
    public class UserAccountController : Controller
    {
        private IWebHostEnvironment _webEnv;

        public UserAccountController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
        }

        public IActionResult CreerUtilisateur()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreerUtilisateur(UserAccountViewModel viewModel)
        {
            //if (!ModelState.IsValid) { 


            UserAccount_Services userAccount_Services = new UserAccount_Services();
            UserAccount verifUsername = userAccount_Services.ObtenirUserAccounts().Where(r => r.Mail == viewModel.UserAccount.Mail).FirstOrDefault();
            {

                if (verifUsername == null)
                {

                    //int id = userAccount_Services.CreerUserAccount(viewModel.UserAccount.Prenom, viewModel.UserAccount.Nom, viewModel.UserAccount.Password, viewModel.UserAccount.Mail, viewModel.UserAccount.Role);
                    int id = userAccount_Services.CreerUtilisateur(viewModel.UserAccount.Prenom, viewModel.UserAccount.Nom, viewModel.UserAccount.Password, viewModel.UserAccount.Mail);

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
                return View(new UserAccountViewModel() { Message = "Email déjà enregistré sur le site" });
            }

        }

        /*
                public IActionResult CreerUserAccount()
                {

                    return View();
                }

                [HttpPost]
                public IActionResult CreerUserAccount(UserAccountViewModel viewModel)
                {
                    //if (!ModelState.IsValid) { 


                    UserAccount_Services userAccount_Services = new UserAccount_Services();
                    UserAccount verifUsername = userAccount_Services.ObtenirUserAccounts().Where(r => r.Mail == viewModel.UserAccount.Mail).FirstOrDefault();
                    {

                        if (verifUsername == null){ 

                        int id = userAccount_Services.CreerUserAccount(viewModel.UserAccount.Prenom, viewModel.UserAccount.Nom, viewModel.UserAccount.Password, viewModel.UserAccount.Mail, viewModel.UserAccount.Role);


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
                        return View(new UserAccountViewModel() { Message = "Email déjà enregistré sur le site" });
                    }

                }
          */
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
                if (viewModel.UserAccount.Image != null)
                {
                    if (viewModel.UserAccount.Image.Length > 0)
                    {
                        string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                        uploads = Path.Combine(uploads, "UserAccount");
                        string filePath = Path.Combine(uploads, viewModel.UserAccount.Image.FileName);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            viewModel.UserAccount.Image.CopyTo(stream);

                        }
                    }
                    viewModel.UserAccount.ImagePath = "/images/UserAccount/" + viewModel.UserAccount.Image.FileName;
                }

                UserAccount_Services userAccount_Services = new UserAccount_Services();
                userAccount_Services.ModifierUserAccount(viewModel.UserAccount.Id, viewModel.UserAccount.Prenom, viewModel.UserAccount.Nom, viewModel.UserAccount.Mail, viewModel.UserAccount.Role, viewModel.UserAccount.AssociationId, viewModel.UserAccount.DonateurId, viewModel.UserAccount.ImagePath);
                return RedirectToAction("ModifierUserAccount", new { @id = viewModel.UserAccount.Id });                 
            }
            else
            {
                return View("Error");
            }
        }
        
        public IActionResult ModifierPassword(int id, string oldPassword, string newPassword, string confirmationPassword, string message)
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

                    return View(new UserAccountViewModel() { UserAccount = userAccount, Authentifie =true, OldPassword=oldPassword, NewPassword=newPassword, ConfirmationPassword = confirmationPassword, Message = message });
                }

            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult ModifierPassword(UserAccountViewModel viewModel)
        {

            if (viewModel.UserAccount.Id != 0)
            {
                //On vérifie que les 2 nouveaux mots de passe correspondent
                if (viewModel.NewPassword == viewModel.ConfirmationPassword)
                {

                    string ancienMotDePasse = UserAccount_Services.EncodeMD5(viewModel.OldPassword);

                    //On vérifie que l'ancien mot de passe est bon
                    if (ancienMotDePasse == viewModel.UserAccount.Password)
                    {

                        UserAccount_Services userAccount_Services = new UserAccount_Services();

                        userAccount_Services.ModifierMotDePasse(viewModel.UserAccount.Id, viewModel.NewPassword);
                     

                        return RedirectToAction("Deconnexion", "login");
                    }
                    else
                    {
                        return View(new UserAccountViewModel() { UserAccount = viewModel.UserAccount, OldPassword = viewModel.OldPassword, NewPassword = viewModel.NewPassword, Message = "L'ancien mot de passe saisi n'est pas bon" });
                    }
                }
                else
                {
                    return View(new UserAccountViewModel() { UserAccount = viewModel.UserAccount, OldPassword = viewModel.OldPassword, NewPassword = viewModel.NewPassword, Message = "Les mots de passe saisis ne correspond pas au mot de passe de confirmation" });

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





    }
}

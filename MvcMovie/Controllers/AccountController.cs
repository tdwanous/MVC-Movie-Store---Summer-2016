﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using MvcMovie.Models;
using MvcMovie.Models.Identity;
using MvcMovie.Models.ViewModels.Account;
using Microsoft.AspNet.Identity.Owin;
using MvcMovie.Services;

namespace MvcMovie.Controllers
{
    //[Authorize]
    //public class AccountController : Controller
    //{
    //    private void MigrateShoppingCart(string UserName)
    //    {
    //        //Associate shopping cart items with logged-in user
    //        var cart = ShoppingCart.GetCart(this.HttpContext);

    //        cart.MigrateCart(UserName);
    //        Session[ShoppingCart.CartSessionKey] = UserName;
    //    }
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly IAuthenticationManager _authManager;
    //    private readonly ApplicationSignInManager _signInManager;
    //    public AccountController(UserManager<ApplicationUser> userManager, IAuthenticationManager authManager)
    //    {
    //        _userManager = userManager;
    //        _authManager = authManager;
    //    }

    //    //
    //    // GET: /Account/Login
    //    [AllowAnonymous]
    //    public ActionResult Login(string returnUrl)
    //    {
    //        ViewBag.ReturnUrl = returnUrl;
    //        return View();
    //    }

    //    //
    //    // POST: /Account/Login
    //    [HttpPost]
    //    [AllowAnonymous]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return View(model);
    //        }

    //        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
    //        switch (result)
    //        {
    //            case SignInStatus.Success:
    //                return RedirectToLocal(returnUrl);
    //            case SignInStatus.Failure:
    //            default:
    //                ModelState.AddModelError("", "Failed to Login");
    //                return View(model);
    //        }
    //    }

    //    //
    //    // GET: /Account/Register
    //    [AllowAnonymous]
    //    public ActionResult Register()
    //    {
    //        return View();
    //    }

    //    //
    //    // POST: /Account/Register
    //    [HttpPost]
    //    [AllowAnonymous]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> Register(RegisterViewModel model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var user = new ApplicationUser()
    //            {
    //                UserName = model.UserName,
    //                Address = model.Address,
    //                City = model.City,
    //                PostalCode = model.PostalCode
    //            };
    //            var result = await _userManager.CreateAsync(user, model.Password);
    //            if (result.Succeeded)
    //            {
    //                await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
    //                return RedirectToAction("Index", "Home");
    //            }
    //            else
    //            {
    //                AddErrors(result);
    //            }
    //        }

    //        // If we got this far, something failed, redisplay form
    //        return View(model);
    //    }

    //    //
    //    // POST: /Account/Disassociate
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
    //    {
    //        ManageMessageId? message = null;
    //        IdentityResult result = await _userManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
    //        if (result.Succeeded)
    //        {
    //            message = ManageMessageId.RemoveLoginSuccess;
    //        }
    //        else
    //        {
    //            message = ManageMessageId.Error;
    //        }
    //        return RedirectToAction("Manage", new { Message = message });
    //    }

    //    //
    //    // GET: /Account/Manage
    //    public ActionResult Manage(ManageMessageId? message)
    //    {
    //        ViewBag.StatusMessage =
    //            message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
    //            : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
    //            : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
    //            : message == ManageMessageId.Error ? "An error has occurred."
    //            : "";
    //        ViewBag.HasLocalPassword = HasPassword();
    //        ViewBag.ReturnUrl = Url.Action("Manage");
    //        return View();
    //    }

    //    //
    //    // POST: /Account/Manage
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> Manage(ManageUserViewModel model)
    //    {
    //        bool hasPassword = HasPassword();
    //        ViewBag.HasLocalPassword = hasPassword;
    //        ViewBag.ReturnUrl = Url.Action("Manage");
    //        if (hasPassword)
    //        {
    //            if (ModelState.IsValid)
    //            {
    //                IdentityResult result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
    //                if (result.Succeeded)
    //                {
    //                    return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
    //                }
    //                else
    //                {
    //                    AddErrors(result);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            // User does not have a password so remove any validation errors caused by a missing OldPassword field
    //            ModelState state = ModelState["OldPassword"];
    //            if (state != null)
    //            {
    //                state.Errors.Clear();
    //            }

    //            if (ModelState.IsValid)
    //            {
    //                IdentityResult result = await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
    //                if (result.Succeeded)
    //                {
    //                    return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
    //                }
    //                else
    //                {
    //                    AddErrors(result);
    //                }
    //            }
    //        }

    //        // If we got this far, something failed, redisplay form
    //        return View(model);
    //    }

    //    //
    //    // POST: /Account/ExternalLogin
    //    [HttpPost]
    //    [AllowAnonymous]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult ExternalLogin(string provider, string returnUrl)
    //    {
    //        // Request a redirect to the external login provider
    //        return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
    //    }

    //    //
    //    // GET: /Account/ExternalLoginCallback
    //    [AllowAnonymous]
    //    public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
    //    {
    //        var loginInfo = await _authManager.GetExternalLoginInfoAsync();
    //        if (loginInfo == null)
    //        {
    //            return RedirectToAction("Login");
    //        }

    //        // Sign in the user with this external login provider if the user already has a login
    //        var user = await _userManager.FindAsync(loginInfo.Login);
    //        if (user != null)
    //        {
    //            await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
    //            return RedirectToLocal(returnUrl);
    //        }
    //        else
    //        {
    //            // If the user does not have an account, then prompt the user to create an account
    //            ViewBag.ReturnUrl = returnUrl;
    //            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
    //            return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
    //        }
    //    }

    //    //
    //    // POST: /Account/LinkLogin
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult LinkLogin(string provider)
    //    {
    //        // Request a redirect to the external login provider to link a login for the current user
    //        return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
    //    }

    //    //
    //    // GET: /Account/LinkLoginCallback
    //    public async Task<ActionResult> LinkLoginCallback()
    //    {
    //        var loginInfo = await _authManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
    //        if (loginInfo == null)
    //        {
    //            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
    //        }
    //        var result = await _userManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
    //        if (result.Succeeded)
    //        {
    //            return RedirectToAction("Manage");
    //        }
    //        return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
    //    }

    //    //
    //    // POST: /Account/ExternalLoginConfirmation
    //    [HttpPost]
    //    [AllowAnonymous]
    //    [ValidateAntiForgeryToken]
    //    public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
    //    {
    //        if (User.Identity.IsAuthenticated)
    //        {
    //            return RedirectToAction("Manage");
    //        }

    //        if (ModelState.IsValid)
    //        {
    //            // Get the information about the user from the external login provider
    //            var info = await _authManager.GetExternalLoginInfoAsync();
    //            if (info == null)
    //            {
    //                return View("ExternalLoginFailure");
    //            }
    //            var user = new ApplicationUser() { UserName = model.UserName };
    //            var result = await _userManager.CreateAsync(user);
    //            if (result.Succeeded)
    //            {
    //                result = await _userManager.AddLoginAsync(user.Id, info.Login);
    //                if (result.Succeeded)
    //                {
    //                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
    //                    return RedirectToLocal(returnUrl);
    //                }
    //            }
    //            AddErrors(result);
    //        }

    //        ViewBag.ReturnUrl = returnUrl;
    //        return View(model);
    //    }

    //    //
    //    // POST: /Account/LogOff
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult LogOff()
    //    {
    //        _authManager.SignOut();
    //        return RedirectToAction("Index", "Home");
    //    }

    //    //
    //    // GET: /Account/ExternalLoginFailure
    //    [AllowAnonymous]
    //    public ActionResult ExternalLoginFailure()
    //    {
    //        return View();
    //    }

    //    [ChildActionOnly]
    //    public ActionResult RemoveAccountList()
    //    {
    //        var linkedAccounts = _userManager.GetLogins(User.Identity.GetUserId());
    //        ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
    //        return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
    //    }

    //    #region Helpers
    //    // Used for XSRF protection when adding external logins
    //    private const string XsrfKey = "XsrfId";

    //    private void AddErrors(IdentityResult result)
    //    {
    //        foreach (var error in result.Errors)
    //        {
    //            ModelState.AddModelError("", error);
    //        }
    //    }

    //    private bool HasPassword()
    //    {
    //        var user = _userManager.FindById(User.Identity.GetUserId());
    //        if (user != null)
    //        {
    //            return user.PasswordHash != null;
    //        }
    //        return false;
    //    }

    //    private ActionResult RedirectToLocal(string returnUrl)
    //    {
    //        if (Url.IsLocalUrl(returnUrl))
    //        {
    //            return Redirect(returnUrl);
    //        }
    //        else
    //        {
    //            return RedirectToAction("Index", "Home");
    //        }
    //    }

    //    private class ChallengeResult : HttpUnauthorizedResult
    //    {
    //        public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
    //        {
    //        }

    //        public ChallengeResult(string provider, string redirectUri, string userId)
    //        {
    //            LoginProvider = provider;
    //            RedirectUri = redirectUri;
    //            UserId = userId;
    //        }

    //        public string LoginProvider { get; set; }
    //        public string RedirectUri { get; set; }
    //        public string UserId { get; set; }

    //        public override void ExecuteResult(ControllerContext context)
    //        {
    //            var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
    //            if (UserId != null)
    //            {
    //                properties.Dictionary[XsrfKey] = UserId;
    //            }
    //            context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
    //        }
    //    }
    //    #endregion
    //}

    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IMailService _mailService;
        public AccountController(UserManager<ApplicationUser> userManager, IAuthenticationManager authManager, IMailService mailService)
        {
            _userManager = userManager;
            _authManager = authManager;
            _mailService = mailService;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    _mailService.sendEmail(2, 0, user.UserName, user.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await _userManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await _authManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await _userManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await _authManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await _userManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _authManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = _userManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            _authManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
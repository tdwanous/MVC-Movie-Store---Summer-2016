using Microsoft.AspNet.Identity;
using MvcMovie.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MvcMovie.Models.Identity.ManageMessageId;

namespace MvcMovie.Controllers
{
    public class ManageController : Controller
    {
        private readonly ApplicationUserManager _userManager;

        public ManageController(ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }
        // GET: Manage
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.Status =
                message == ManageMessageId.UserSettingsChangeSuccess ? "Your settings have been changed"
                : message == ManageMessageId.Error ? "An Error has occured"
                : "";

            var userId = User.Identity.GetUserId();
            var user = _userManager.FindById(userId);


        }


    }
}
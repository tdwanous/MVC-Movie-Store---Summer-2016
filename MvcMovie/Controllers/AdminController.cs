using MvcMovie.Models;
using MvcMovie.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcMovie.Models.ViewModels;
using MvcMovie.Models.Identity;
using System.Net;
using System.Data.Entity;

namespace MvcMovie.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IOrderDetailService _orderDetailService;

        public AdminController(ApplicationDbContext dbContext, IOrderDetailService orderDetailService)
        {
            _dbContext = dbContext;
            _orderDetailService = orderDetailService;

        }
        // GET: Admin
        public ActionResult Index()
        {

            var adminModel = new Admin();
            var roleId = _dbContext.Roles.FirstOrDefault(role => role.Name == "Admin")?.Id;

            adminModel.userInfo.AddRange(_dbContext.Users.Select(user => new Models.ViewModels.Admin.UserInfo
            {
                userName = user.UserName,
                Email = user.Email,
                isAdmin = user.Roles.Any(role => role.RoleId == roleId),
                userId = user.Id
            }));
            adminModel.movies.AddRange(_dbContext.Movies);

            adminModel.orderInfo.AddRange(_dbContext.Orders);

            if(User.IsInRole("Admin"))
            {
                return View(adminModel);
            }

            return RedirectToAction("Index", "Home");
        }
        public ActionResult OrderDelete(int? orderId)
        {
            if (User.IsInRole("Admin"))
            {
                if (orderId == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Order order = _orderDetailService.GetOrderByIds(orderId);
                if (order == null)
                {
                    return HttpNotFound();
                }
                return View(order);
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("OrderDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int orderId)
        {
            _orderDetailService.DeleteOrder(orderId);
            return RedirectToAction("Index", "Admin");
        }
    }
}
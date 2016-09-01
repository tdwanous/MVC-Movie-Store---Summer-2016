using MvcMovie.Models;
using MvcMovie.Models.Identity;
using MvcMovie.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace MvcMovie.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        private IShoppingCartService _shoppingCartService;
        private IContextService _contextService;
        private IMailService _mailService;
        // GET: 
        public CheckoutController(IShoppingCartService shoppingCartService, IContextService contextService, IMailService mailService)
        {
            _shoppingCartService = shoppingCartService;
            _contextService = contextService;
            _mailService = mailService;
        }
        public ActionResult AddressAndPayment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            _shoppingCartService.ShoppingCartId = _contextService.GetCartId(this.HttpContext);
            var order = new Order();
            TryUpdateModel(order);
            string userId = User.Identity.GetUserId();
            var user = storeDB.Users.Find(userId);
            try
            {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;
                    order.Total = _shoppingCartService.GetTotal();
                    order.Email = user.Email;

                    //Save Order
                    storeDB.Orders.Add(order);
                    storeDB.SaveChanges();
                    //Process the order
                    var cart = ShoppingCart.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }

        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = storeDB.Orders.Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                _mailService.sendEmail(1,id, "", "");
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }


    }
}
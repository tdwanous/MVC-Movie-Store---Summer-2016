using MvcMovie.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcMovie.Models.ViewModels;
using MvcMovie.Services;

namespace MvcMovie.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IShoppingCartService _shoppingCartService;
        private IMovieService _movieService;
        private IContextService _contextService;

        ApplicationDbContext storeDB = new ApplicationDbContext();
        //
        // GET: /ShoppingCart/

        public ShoppingCartController(IMovieService movieService, IShoppingCartService shoppingCartService, IContextService contextService)
        {
            _movieService = movieService;
            _shoppingCartService = shoppingCartService;
            _contextService = contextService;

        }
        public ActionResult Index()
        {
            _shoppingCartService.ShoppingCartId = _contextService.GetCartId(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = _shoppingCartService.GetCartItems(),
                CartTotal = _shoppingCartService.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the movie from the database
            var addedMovie = _movieService.GetMovieById(id);

            // Add it to the shopping cart
            _shoppingCartService.ShoppingCartId = _contextService.GetCartId(this.HttpContext);

            _shoppingCartService.AddToCart(addedMovie);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            _shoppingCartService.ShoppingCartId = _contextService.GetCartId(this.HttpContext);

            // Get the name of the album to display confirmation
            string movieName = storeDB.Carts
                .Single(item => item.RecordId == id).Movie.Title;

            // Remove from cart
            int itemCount = _shoppingCartService.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(movieName) +
                    " has been removed from your shopping cart.",
                CartTotal = _shoppingCartService.GetTotal(),
                CartCount = _shoppingCartService.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            _shoppingCartService.ShoppingCartId = _contextService.GetCartId(this.HttpContext);

            ViewData["CartCount"] = _shoppingCartService.GetCount();
            return PartialView("CartSummary");
        }
    }

}
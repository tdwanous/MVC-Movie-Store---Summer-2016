using MvcMovie.Models;
using MvcMovie.Models.Identity;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Services
{
    public interface IShoppingCartService
    {
        string ShoppingCartId { get; set; }

        void AddToCart(Movie movie);

        int RemoveFromCart(int id);

        void EmptyCart();

        List<Cart> GetCartItems();

        int GetCount();

        decimal GetTotal();
       
        int CreateOrder(Order order);

        void MigrateCart(string userName);


    }
}
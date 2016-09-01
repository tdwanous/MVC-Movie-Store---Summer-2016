﻿using MvcMovie.Models;
using MvcMovie.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        // Helper method to simplify shopping cart calls
        public void AddToCart(Movie movie)
        {
            // Get the matching cart and album instances
            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.MovieId == movie.ID);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    MovieId = movie.ID,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = storeDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
                // Save changes
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total



            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId && cartItems.Movie.isDiscounted != true
                              select (int?)cartItems.Count *
                              cartItems.Movie.Price).Sum();

            decimal? discountTotal = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId && cartItems.Movie.isDiscounted == true
                              select (int?)cartItems.Count *
                              cartItems.Movie.DiscountPrice).Sum();

            if (discountTotal == null)
            {
                return total ?? decimal.Zero;
            }
            else if (total == null)
            {
                return discountTotal ?? decimal.Zero;
            }
            
            return total + discountTotal ?? decimal.Zero;
        }
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    MovieId = item.MovieId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Movie.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                if(item.Movie.isDiscounted == true)
                {
                    orderTotal += (item.Count * Convert.ToDecimal(item.Movie.DiscountPrice));
                }
               else if (item.Movie.isDiscounted != true)
                {
                    orderTotal += (item.Count * item.Movie.Price);
                }

                storeDB.OrderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
        }
    }

}
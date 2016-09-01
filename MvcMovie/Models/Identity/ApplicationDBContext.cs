using Microsoft.AspNet.Identity.EntityFramework;
using MvcMovie.Models.ViewModels.Admin;
using MvcMovie.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Models.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        //public System.Data.Entity.DbSet<MvcMovie.Models.Identity.ApplicationUser> ApplicationUsers { get; set; }
    }
}
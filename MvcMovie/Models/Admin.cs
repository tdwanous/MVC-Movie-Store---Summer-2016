using MvcMovie.Models.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class Admin
    {

        public Admin()
        {
            movies = new List<Movie>();
            userInfo = new List<UserInfo>();
            orderInfo = new List<Order>();
            orderDetailInfo = new List<OrderDetail>();
        }

        public int orderId { get; set; }

        public List<Movie> movies { get; set; }

        public List<UserInfo> userInfo { get; set; }

        public List<Order> orderInfo { get; set; }

        public List<OrderDetail> orderDetailInfo { get; set; }
    }
}
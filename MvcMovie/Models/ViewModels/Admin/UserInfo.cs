using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcMovie.Models.ViewModels.Admin
{
    public class UserInfo
    {
        public string userName { get; set; }

        public string Email { get; set; }

        public Boolean isAdmin { get; set; }

        [ScaffoldColumn(false)]
        public string userId { get; set; }
    }
}
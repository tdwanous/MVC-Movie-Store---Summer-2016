using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcMovie.Models.ViewModels.Manage
{
    public class ManageViewModel
    {
        public IList<UserLoginInfo> Logins { get; set; }
    }
}
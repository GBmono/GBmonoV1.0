using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gbmono.Api.Admin.Models
{
    public class UserBindingModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Gbmono.EF;

namespace Gbmono.Api.Models
{
    /// <summary>
    /// user register binding model
    /// </summary>
    public class UserBindingModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
    }


    public class UserSaveModel
    {
        public short SaveItemType { get; set; }

        public int KeyId { get; set; }
    }

}
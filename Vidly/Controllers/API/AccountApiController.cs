using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Controllers;
using Vidly.ViewModels.Account;

namespace Vidly.Controllers.API
{
    public class AccountApiController : ApiController
    {
        Vidly.Controllers.AccountController accountController = new Controllers.AccountController();


        [HttpPost]
        public async void Register(RegisterViewModel model)
        {
            await accountController.Register(model);
            //return new ApplicationUser();
        }
    }
}

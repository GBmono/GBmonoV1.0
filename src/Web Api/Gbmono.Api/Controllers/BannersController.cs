using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Gbmono.EF.Infrastructure;


namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Banners")]
    public class BannersController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;


        #region ctor
        public BannersController()
        {
            _repositoryManager = new RepositoryManager();


        }
        #endregion


    }
}

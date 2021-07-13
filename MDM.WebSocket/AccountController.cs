using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;

namespace MDM.WebSocket
{
    public class AccountController : ControllerBase
    {
        [Route("test")]
        public string Index()
        {
            return "hello api";
        }
    }
}

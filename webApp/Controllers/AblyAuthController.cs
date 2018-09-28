using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IO.Ably;
using IO.Ably.Realtime;

namespace webApp.Controllers
{
    [Route("[controller]")]
    public class AblyAuthController : Controller
    {
        private AblyRest _client;
        
        public AblyAuthController()
        {
            var key = Environment.GetEnvironmentVariable("ABLY_API_KEY");
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("ABLY_API_KEY environment variable needs to be set before running this app!");
            }
            _client = new AblyRest(key);
        }
        
        [HttpGet("tokenrequest")]
        public async Task<object> TokenRequest()
        {
            var tokenRequest = await _client.Auth.CreateTokenRequestAsync();
            return tokenRequest;
        }
        
        [HttpGet("tokendetails")]
        public async Task<object> TokenDetails()
        {
            var tokenDetails = await _client.Auth.RequestTokenAsync();
            return tokenDetails;
        }
    }
}
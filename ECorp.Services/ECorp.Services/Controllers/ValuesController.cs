using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ECorp.Services.Controllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {
            //The string parameter is the scope you want to acquire for the resource you're accessing on the user's behalf.  In the example below we're acquiring a token that can access the nsappdev03 D365 instance
            //If your API application isn't configured with permissions for this type of scope the call with fail (which you would expect).
            //The secret we use to acquire the token is in the web.config in this project which is *terrible*.  It should be stashed safely in Azure Key Vault, and this web service should be using a Managed Identity to get at the Key Vault
            //This is OK for a demo but is not acceptable for a production deployment
            var token = await Utils.AadHelpers.GetAccessTokenForScopeOnBehalfOfLoggedInUser("https://nsappdev03.api.crm.dynamics.com/user_impersonation");
            return new string[] { User.Identity.Name,token, User.Identity.AuthenticationType.ToString() };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

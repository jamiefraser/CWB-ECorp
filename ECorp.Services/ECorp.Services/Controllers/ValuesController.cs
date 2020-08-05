using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ECorp.Services.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values
        public async Task<IEnumerable<string>> Get()
        {
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

using ECorp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Refit;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ECorp.Services.Controllers
{
    [System.Web.Http.Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ContactsController : ApiController
    {
        // GET: /api/contacts
        public async Task<IEnumerable<Contact>> Get()
        {
            var service = RestService.For<IDynamicsService>("https://nsappdev03.crm.dynamics.com");
            var token = await Utils.AadHelpers.GetAccessTokenForScopeOnBehalfOfLoggedInUser("https://nsappdev03.api.crm.dynamics.com/user_impersonation");
            var result = await service.GetContacts($"Bearer {token}");
            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(result.value));
            return result.value;
        }
    }
}
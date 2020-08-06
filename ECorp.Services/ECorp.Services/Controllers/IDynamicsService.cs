using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECorp.Services.Models;
using Refit;
namespace ECorp.Services.Controllers
{
    public interface IDynamicsService
    {
        [Get("/api/data/v9.1/accounts")]
        Task<AccountsResponse> GetAccounts([Header("Authorization")] string authHeader);
        [Get("/api/data/v9.1/contacts")]
        Task<ContactsResponse> GetContacts([Header("Authorization")] string authHeader);
    }
}

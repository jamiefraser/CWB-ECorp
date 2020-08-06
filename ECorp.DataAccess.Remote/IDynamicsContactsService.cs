using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ECorp.DataAccess.Contact;
using Refit;
namespace ECorp.DataAccess.Remote
{
    public interface IDynamicsContactsService
    {
        [Get("/contacts")]
        Task<IEnumerable<ContactEntity>> Get();
    }
}

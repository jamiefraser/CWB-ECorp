using ECorp.DataAccess;
using ECorp.DataAccess.Contact;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECorp.UI.Services
{
    public class ContactsBackgroundRefreshService<ContactEntity> : IBackgroundDataRefreshService<ContactEntity>
    {
        readonly IRemoteContactDal dal;
        private readonly IContactDal localDal;

        public ContactsBackgroundRefreshService(IRemoteContactDal _dal, IContactDal _localDal)
        {
            dal = _dal;
            localDal = _localDal;
        }
        public async Task Refresh()
        {
            await dal.Get().ContinueWith(async (contacts) =>
            {
                await localDal.SaveCollection(contacts.Result);
            });
        }
    }
}

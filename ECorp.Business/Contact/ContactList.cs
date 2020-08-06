using Csla;
using ECorp.DataAccess.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECorp.Business.Contact
{
    [Serializable]
    public class ContactList : ReadOnlyBindingListBase<ContactList, ContactInfo>
    {
        [Fetch]
        private async Task Fetch([Inject] ECorp.DataAccess.Contact.IContactDal dal, [Inject] IRemoteContactDal remoteDal)
        {
            IEnumerable<ContactEntity> contacts;
            var httpTransientErrorPolicy = Polly.Extensions.Http.HttpPolicyExtensions.HandleTransientHttpError();
            using (LoadListMode)
            {
                contacts = await dal.Get();
                var data = contacts.Select(ci => DataPortal.FetchChild<ContactInfo>(ci));
                AddRange(data);
                System.Diagnostics.Debug.WriteLine($"ContactList has {this.Count()} contacts");
            }
        }
        
    }
}

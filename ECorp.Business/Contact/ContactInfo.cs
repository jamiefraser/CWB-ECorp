using System;
using System.Collections.Generic;
using System.Text;
using Csla;
using Newtonsoft.Json;

namespace ECorp.Business.Contact
{
    [Serializable]
    public class ContactInfo : ReadOnlyBase<ContactInfo>
    {
        public static readonly PropertyInfo<string> Address1Property = RegisterProperty<string>(nameof(Address1));
        [JsonProperty("address1_composite")]
        public string Address1
        {
            get { 
                return GetProperty(Address1Property); 
            }
            private set { LoadProperty(Address1Property, value); }
        }
        public static readonly PropertyInfo<string> EtagProperty = RegisterProperty<string>(nameof(Etag));
        [JsonProperty("@odata.etag")]
        public string Etag
        {
            get { return GetProperty(EtagProperty); }
            private set { LoadProperty(EtagProperty, value); }
        }

        public static readonly PropertyInfo<string> IdProperty = RegisterProperty<string>(nameof(Id));
        [JsonProperty("contactid")]
        public string Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> FullNameProperty = RegisterProperty<string>(nameof(FullName));
        public string FullName
        {
            get 
            { 
                return GetProperty(FullNameProperty); 
            }
            set 
            { 
                LoadProperty(FullNameProperty, value); 
            }
        }
        [FetchChild]
        private void Fetch(ECorp.DataAccess.Contact.ContactEntity data)
        {
            Id = data.Id;
            Etag = data.ETag;
            FullName = data.FullName;
            Address1 = data.Address1;
        }

    }
}

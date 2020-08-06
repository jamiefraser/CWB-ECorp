using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECorp.DataAccess.Contact
{
    public class ContactEntity
    {
        [JsonProperty("@odata.etag")]
        public string ETag { get; set; }
        [JsonProperty("contactid")]
        [Key]
        public string Id { get; set; }
        [JsonProperty("fullname")]
        public string FullName { get; set; }
        [JsonProperty("address1_composite")]
        public string Address1 { get; set; }
    }
}

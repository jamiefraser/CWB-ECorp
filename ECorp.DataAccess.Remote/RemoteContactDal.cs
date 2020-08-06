using ECorp.DataAccess.Contact;
using Refit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ECorp.DataAccess.Remote
{
    public class RemoteContactDal : IRemoteContactDal
    {
        readonly IHttpClientFactory httpClientFactory;
        readonly HttpClient client;
        public RemoteContactDal(IHttpClientFactory _httpClientFactory)
        {
            httpClientFactory = _httpClientFactory;
            client = httpClientFactory.CreateClient("ServerAPI");
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        public async Task<IEnumerable<ContactEntity>> Get()
        {
            var service = RestService.For<IDynamicsContactsService>(client);
            var contacts = await service.Get();
            return contacts;
        }

        public Task SaveCollection(IEnumerable<ContactEntity> contacts)
        {
            throw new NotImplementedException();
        }
    }
}

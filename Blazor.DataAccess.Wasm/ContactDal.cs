using Csla;
using ECorp.DataAccess;
using ECorp.DataAccess.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TG.Blazor.IndexedDB;

namespace ECorp.DataAccess.Wasm
{
    public class ContactDal : IContactDal, INotifyPropertyChanged
    {
        private readonly IndexedDBManager dbManager;
        public ContactDal(IndexedDBManager _dbManager)
        {
            dbManager = _dbManager;
            dbManager.ActionCompleted += (sender, args) =>
            {
                if (args.Outcome.Equals(IndexDBActionOutCome.Failed))
                {
                    System.Diagnostics.Debug.WriteLine($"Encountered an issue writing a record \r\n {args.Message} :(");
                }
            };
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        public async Task<IEnumerable<ContactEntity>> Get()
        {
            var results = await dbManager.GetRecords<ECorp.DataAccess.Contact.ContactEntity>("Contacts");
            return results;
        }

        public async Task SaveCollection(IEnumerable<ContactEntity> contacts)
        {
            int newCount = 0;
            int updateCount=0;
            int skipCount=0;
            foreach(ContactEntity c in contacts)
            {
                var getContact = await dbManager.GetRecordById<string, ContactEntity>("Contacts", c.Id);
                var writeContact = new StoreRecord<ContactEntity>
                {
                    Storename = "Contacts",
                    Data = c
                };
                if (getContact == null || string.IsNullOrEmpty(getContact.Id))
                {
                    await dbManager.AddRecord(writeContact);
                    newCount++;
                }
                else
                {
                    if (getContact.ETag != c.ETag)
                    {

                        await dbManager.UpdateRecord<ContactEntity>(writeContact);
                        updateCount++;
                    }
                    else
                    {
                        skipCount++;
                    }
                }
            }
            NotifyPropertyChanged("Contacts");
        }
    }
}

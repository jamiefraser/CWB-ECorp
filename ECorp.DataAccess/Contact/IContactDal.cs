using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ECorp.DataAccess.Contact
{
    public interface IContactDal : INotifyPropertyChanged
    {
        Task<IEnumerable<ContactEntity>> Get();
        Task SaveCollection(IEnumerable<ContactEntity> contacts);
    }
}

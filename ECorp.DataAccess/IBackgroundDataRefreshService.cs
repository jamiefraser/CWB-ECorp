using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECorp.DataAccess
{
    public interface IBackgroundDataRefreshService<T>
    {
        Task Refresh();
    }
}

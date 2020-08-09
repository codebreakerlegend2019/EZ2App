using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyTwoJuetengApp.Interfaces
{
    public interface IGetAsync<T>
    {
        Task<T> Get();
    }
}

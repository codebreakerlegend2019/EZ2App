using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyTwoJuetengApp.Interfaces
{
    public interface IPutAsync<TParam, TResult>
    {
        Task<TResult> PutAsync(int id, TParam model);
    }
}
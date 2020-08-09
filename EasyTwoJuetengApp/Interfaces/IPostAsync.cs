using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyTwoJuetengApp.Interfaces
{
    public interface IPostAsync<TParam,TResult>
    {
        Task<TResult> PostAsync(TParam model);
    }
}

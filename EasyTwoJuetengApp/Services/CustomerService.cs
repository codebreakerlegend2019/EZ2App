using EasyTwoJuetengApp.Helpers.JdsHelpers;
using EasyTwoJuetengApp.Interfaces;
using EasyTwoJuetengApp.Models.CustomerRelated;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyTwoJuetengApp.Services
{
    public class CustomerService : IPostAsync<CustomerSaveDto, JdsResponse>,
        IGetAsync<JdsMultiReponse<CustomerReadDto>>,
        IPutAsync<CustomerSaveDto, JdsResponse>
    {
        public async Task<JdsMultiReponse<CustomerReadDto>> Get()
        {
            return await JdsClient.GetManyAsync<CustomerReadDto>("api/Customer", App.CurrentlyLoginnedUser().Token);
        }

        public async Task<JdsResponse> PostAsync(CustomerSaveDto model)
        {
            return await JdsClient.PostAsync("api/Customer", model,
                                            App.CurrentlyLoginnedUser().Token);
        }

        public async Task<JdsResponse> PutAsync(int id, CustomerSaveDto model)
        {
            return await JdsClient.PutAsync($"api/customer/{id}", model, App.CurrentlyLoginnedUser().Token);
        }
    }
}
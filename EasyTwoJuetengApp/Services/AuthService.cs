using EasyTwoJuetengApp.Helpers.JdsHelpers;
using EasyTwoJuetengApp.Interfaces;
using EasyTwoJuetengApp.Models.AuthRelated;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EasyTwoJuetengApp.Services
{
    public class AuthService: IPostAsync<LoginCredential,LoginResult>
    {
        public async Task<LoginResult> PostAsync(LoginCredential model)
        {
            var userLoginRequest = await JdsClient
                .PostWithSingleReturnAsync<LoginCredential,LoginResult>
                ("api/auth/login/token",model);
            if (userLoginRequest.StatusCode == HttpStatusCode.OK)
                return userLoginRequest.Data;
            return null;
        }
    }
}

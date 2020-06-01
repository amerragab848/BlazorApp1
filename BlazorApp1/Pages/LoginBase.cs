using BlazorApp1.Services;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class LoginBase:ComponentBase
    {
       
   [Inject]
      public  IAuthService AuthService { get; set; }
        [Inject]
       public NavigationManager NavigationManager { get; set; }
         protected LoginModel loginModel = new LoginModel();
        protected bool ShowErrors;
        protected string Error = "";

        protected async Task HandleLogin()
        {
            ShowErrors = false;

            loginModel.RememberMe = !loginModel.RememberMe;

            var result = await AuthService.Login(loginModel);

            if (result.Successful)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Error;
                ShowErrors = true;
            }
        }
    }
}

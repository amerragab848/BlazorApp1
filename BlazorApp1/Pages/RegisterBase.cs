using BlazorApp1.Services;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class RegisterBase:ComponentBase
    {
       [Inject]
        IAuthService AuthService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected RegisterModel RegisterModel = new RegisterModel();
        protected bool ShowErrors { get; set; }
        protected IEnumerable<string> Errors;

        protected async Task HandleRegistration()
        {
            ShowErrors = false;

            var result = await AuthService.Register(RegisterModel);

            if (result.Successful)
            {
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                Errors = result.Errors;
                ShowErrors = true;
            }
        }
    }
}

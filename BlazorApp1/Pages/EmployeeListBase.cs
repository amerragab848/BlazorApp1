using BlazorApp1.Services;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class EmployeeListBase: ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
        public bool ShowFooter { get; set; } = true;
        private string infoFromJs { get; set; }
        private ElementReference divElement { get; set; }
        protected int SelectedEmployeesCount { get; set; } = 0;
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployeesCount++;
            }
            else
            {
                SelectedEmployeesCount--;
            }
        }
        protected async Task EmployeeDeleted()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }
        protected override async Task OnInitializedAsync()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }
        public override async Task SetParametersAsync(ParameterView parameters)
        {

             await base.SetParametersAsync(parameters);
        }
        //protected override async Task OnParametersSetAsync()
        //{
        //  //  await ...
        //}

        //after first render for component
       
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                //  await ...
            }
        }
        //refresh component
        protected override bool ShouldRender()
        {
            var renderUI = true;

            return renderUI;
        }



        //Cancelable background work

        private Resource resource = new Resource();
        private CancellationTokenSource cts = new CancellationTokenSource();

        protected async Task LongRunningWork()
        {
            await Task.Delay(5000, cts.Token);

            cts.Token.ThrowIfCancellationRequested();
            resource.BackgroundResourceMethod();
        }

        public void Dispose()
        {
            cts.Cancel();
            cts.Dispose();
            resource.Dispose();
        }
    }

     class Resource : IDisposable
    {
        private bool disposed;

        public void BackgroundResourceMethod()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(Resource));
            }

        }

        public void Dispose()
        {
            disposed = true;
        }
    }
}

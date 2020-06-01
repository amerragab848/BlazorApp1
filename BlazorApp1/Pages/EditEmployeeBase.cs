using AutoMapper;
using BlazorApp1.Models;
using BlazorApp1.Services;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Pages
{
    public class EditEmployeeBase:ComponentBase
    {
        public Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
        [Inject]
        public ToastService toastService { get; set; }

        public string DepartmentId { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            Departments = (await DepartmentService.GetDepartments()).ToList();
            DepartmentId = Employee.DepartmentId.ToString();
            Mapper.Map(Employee, EditEmployeeModel);

        }
        protected async Task HandleValidSubmit()
        {
            Mapper.Map(EditEmployeeModel, Employee);

            Employee result = null;

            if (Employee.EmployeeId != 0)
            {
                result = await EmployeeService.UpdateEmployee(Employee);
                if (result != null)
                {
                    toastService.ShowToast("Data Saved Successfully", ToastLevel.Success);
                     

                }
            }
            else
            {
                result = await EmployeeService.AddEmployee(Employee);
                if (result != null)
                {
                    toastService.ShowToast("Data Saved Successfully", ToastLevel.Success);


                }
            }
           if (result != null)
           {
                NavigationManager.NavigateTo("/");
           }
        }
        protected async Task Delete_Click()
        {
          var res=  await EmployeeService.DeleteEmployee(Employee.EmployeeId);
            if (res==200)
            {
                toastService.ShowToast("Data Deleted Successfully", ToastLevel.Success);

            }
              NavigationManager.NavigateTo("/");
        }

    }
}

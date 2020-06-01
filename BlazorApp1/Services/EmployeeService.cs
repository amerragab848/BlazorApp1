using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorApp1.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
             
           var res= await httpClient.GetJsonAsync<Employee[]>("api/Employees");
            return res;
        }
        public async Task<Employee> GetEmployee(int id)
        {
            return await httpClient.GetJsonAsync<Employee>($"api/employees/{id}");
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await httpClient.PostJsonAsync<Employee>("api/employees", employee);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            return await httpClient.PutJsonAsync<Employee>("api/employees", employee);
        }

        public async Task<int> DeleteEmployee(int employeeId)
        {
            var res=  await httpClient.DeleteAsync($"api/employees/{employeeId}");
                return (int) res.StatusCode ;
        }

        public Task<Employee> GetEmployeeByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            throw new NotImplementedException();
        }
    }
}

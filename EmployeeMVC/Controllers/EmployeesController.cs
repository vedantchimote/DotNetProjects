using System.Reflection.Metadata.Ecma335;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsMVC.Data;
using StudentsMVC.Models.Domain;

namespace EmployeeMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext employeeDbContext;

        public EmployeesController(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await employeeDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department
            };

            await employeeDbContext.Employees.AddAsync(employee);
            await employeeDbContext.SaveChangesAsync();
            ;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await employeeDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    DateOfBirth = employee.DateOfBirth,
                    Department = employee.Department
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel updateEmployeeRequest)
        {
            var employee = await employeeDbContext.Employees.FindAsync(updateEmployeeRequest.Id);

            if (employee != null)
            {
                employee.Name = updateEmployeeRequest.Name;
                employee.Email = updateEmployeeRequest.Email;
                employee.Salary = updateEmployeeRequest.Salary;
                employee.DateOfBirth = updateEmployeeRequest.DateOfBirth;
                employee.Department = updateEmployeeRequest.Department;

                await employeeDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel updateEmployeeViewModel)
        {
            var employee = await employeeDbContext.Employees.FindAsync(updateEmployeeViewModel.Id);

            if (employee != null)
            {
                employeeDbContext.Employees.Remove(employee);
                await employeeDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
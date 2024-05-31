using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;
using Library.Models.Domain;
using LibraryManagementSystem.Models;

namespace Library.Controllers
{
    public class UsersController : Controller
    {
        private readonly EmployeeDbContext libraryDbContext;

        public UsersController(EmployeeDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await libraryDbContext.Users.ToListAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUsersViewModel addUsersRequest)
        {

            var users = new Users
            {
                Id = Guid.NewGuid(),
                Username = addUsersRequest.Username,
                Email = addUsersRequest.Email,
                Mobile_no = addUsersRequest.Mobile_no,
                Password = addUsersRequest.Password,
                Role = addUsersRequest.Role
            };

            await libraryDbContext.Users.AddAsync(users);
            await libraryDbContext.SaveChangesAsync();
            ;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var users = await libraryDbContext.Users.FirstOrDefaultAsync(e => e.Id == id);

            if (users != null)
            {
                var viewModel = new UpdateUsersViewModel()
                {
                    Id = users.Id,
                    Username = users.Username,
                    Email = users.Email,
                    Mobile_no = users.Mobile_no,
                    Password = users.Password,
                    Role = users.Role

                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateUsersViewModel updateEmployeeRequest)
        {
            var users = await libraryDbContext.Users.FindAsync(updateEmployeeRequest.Id);

            if (users != null)
            {
                users.Username = updateEmployeeRequest.Username;
                users.Email = updateEmployeeRequest.Email;
                users.Mobile_no = updateEmployeeRequest.Mobile_no;
                users.Password = updateEmployeeRequest.Password;
                users.Role = updateEmployeeRequest.Role;

                await libraryDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateUsersViewModel updateEmployeeViewModel)
        {
            var employee = await libraryDbContext.Users.FindAsync(updateEmployeeViewModel.Id);

            if (employee != null)
            {
                libraryDbContext.Users.Remove(employee);
                await libraryDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
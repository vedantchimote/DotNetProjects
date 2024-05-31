using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;
using Library.Models.Domain;
using LibraryManagementSystem.Models;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly EmployeeDbContext libraryDbContext;

        public BookController(EmployeeDbContext libraryDbContext)
        {
            this.libraryDbContext = libraryDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await libraryDbContext.Book.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookViewModel addEmployeeRequest)
        {
            var book = new Book
            {
                BName = addEmployeeRequest.BName,
                Author = addEmployeeRequest.Author,
                Category = addEmployeeRequest.Category,
                Price = addEmployeeRequest.Price
            };

            await libraryDbContext.Book.AddAsync(book);
            await libraryDbContext.SaveChangesAsync();
            ;
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await libraryDbContext.Book.FirstOrDefaultAsync(e => e.Id == id);

            if (employee != null)
            {

                var viewModel = new UpdateBookViewModel()
                {
                    Id = employee.Id,
                    BName = employee.BName,
                    Author = employee.Author,
                    Category = employee.Category,
                    Price = employee.Price
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateBookViewModel updateBookRequest)
        {
            var employee = await libraryDbContext.Book.FindAsync(updateBookRequest.Id);

            if (employee != null)
            {
                employee.BName = updateBookRequest.BName;
                employee.Author = updateBookRequest.Author;
                employee.Category = updateBookRequest.Category;
                employee.Price = updateBookRequest.Price;

                await libraryDbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateBookViewModel updateBookViewModel)
        {
            var employee = await libraryDbContext.Book.FindAsync(updateBookViewModel.Id);

            if (employee != null)
            {
                libraryDbContext.Book.Remove(employee);
                await libraryDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}

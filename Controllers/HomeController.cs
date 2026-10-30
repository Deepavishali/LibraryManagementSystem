
using LibraryManagement.Models;
using LibraryManagement.Models.Data;
using LibraryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookrepo;

        public HomeController(IBookRepository  bookrepo)
        {
            _bookrepo = bookrepo;
        }

     
        [HttpGet]
        public async Task<IActionResult> Read()
        {

            var books = await _bookrepo.GetAllBooksAsync();
            return View(books);
        }

// ADD Book

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // To Add the book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            await _bookrepo.AddBookAsync(book);
            TempData["SuccessMessage"] = $"Successfully added '{book.BookName}'!";
            return RedirectToAction("Read");
        }

        // Edit Action

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookrepo.GetBookByIdAsync(id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Book book)
        {
            if(id != book.BookId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            await _bookrepo.UpdateBookAsync(book);
            TempData["SuccessMessage"] = $"Successfully updated  '{book.BookName}'!";
            return RedirectToAction("Read");
        }

        //Delete Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookrepo.GetBookByIdAsync(id);
            if(book != null)
            {
                await _bookrepo.DeleteBookAsync(id);
                TempData["SucessMessage"] = "Successfully deleted the book!";
            }
            return RedirectToAction("Read");
        }


        //Borrow Action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(int id)
        {
            bool success = await _bookrepo.BorrowBookAsync(id);
            if(success)
            {
                TempData["SuccessMessage"] = $"Successfully borrowed";

 }
            else
            {
                TempData["ErrorMessage"] = "Book is out of stock!";
            }
            return RedirectToAction("Read");
        }

        //Return Action

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            bool success = await _bookrepo.ReturnBookAsync(id);
            if(success)
            {
               
                TempData["SuccessMessage"] = $"Successfuly returned!";
            }
            else
            {
                TempData["ErrorMessage"] = "Cannot return more than the total stock quantity!";
            }
            return RedirectToAction("Read");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unified.Application.DTOs.Book;
using Unified.Application.Interfaces;
using Unified.Domain.Entities;
using Unified.Infrastructure.Data;

namespace Unified.Core.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IBookService _bookService;
        private readonly ApplicationDbContext _context;

        public BookController(UserManager<Employee> userManager, IBookService bookService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _bookService = bookService;
            _context = context;
        }

        [Authorize(Roles = "Admin,Assistant")]
        [HttpGet("get-all-books")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                if (books == null || !books.Any())
                {
                    return NotFound(new { title = "No Books Found", message = "There are no books in the system." });
                }
                return Ok(books);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }

        [Authorize(Roles = "Admin,Assistant")]
        [HttpGet("get-book-by-id/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                if (book == null)
                {
                    return NotFound(new { title = "Book Not Found", message = "No active book found." });
                }
                return Ok(book);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto book)
        {            
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);

                if (book == null)
                {
                    return BadRequest(new { title = "Invalid Input", message = "Book data is required." });
                }

                if (await CheckBookExistsAsync(book.Title, book.Edition))
                {
                    return Conflict(new { title = "Book Exists", message = "A book with the same title and edition already exists." });
                }

                await _bookService.AddBookAsync(book);

                var systemAuditLog = new AuditTrail
                {
                    Action = "Created Book",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Book '{book.Title}' created successfully.",
                    TableAffected = "Book",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };

                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();

                return Ok(new { title = "Success", message = "The book has been added successfully." });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-book")]
        public async Task<IActionResult> UpdateBook([FromBody] BookDto book)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (book == null || book.BookId <= 0)
                {
                    return BadRequest(new { title = "Invalid Input", message = "Valid book data is required." });
                }
                if (await CheckBookExistsAsync(book.Title, book.Edition, book.BookId))
                {
                    return Conflict(new { title = "Book Exists", message = "A book with the same title and edition already exists." });
                }
                await _bookService.UpdateBookAsync(book);
                var systemAuditLog = new AuditTrail
                {
                    Action = "Updated Book",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Book '{book.Title}' updated successfully.",
                    TableAffected = "Book",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };
                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();
                return Ok(new { title = "Success", message = "The book has been updated successfully." });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("delete-book")]
        public async Task<IActionResult> DeleteBook([FromBody] BookDto book)
        {
            try
            {
                var loggedInUser = await _userManager.GetUserAsync(User);
                if (book == null || book.BookId <= 0)
                {
                    return BadRequest(new { title = "Invalid Input", message = "Valid book data is required." });
                }
                await _bookService.DeleteBookAsync(book);
                var systemAuditLog = new AuditTrail
                {
                    Action = "Deleted Book",
                    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Description = $"Book '{book.Title}' deleted successfully.",
                    TableAffected = "Book",
                    Date = DateTime.UtcNow,
                    EmployeeId = loggedInUser.Id,
                };
                _context.AuditTrails.Add(systemAuditLog);
                await _context.SaveChangesAsync();
                return Ok(new { title = "Success", message = "The book has been deleted successfully." });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    title = "Server Error",
                    message = "An unexpected error occurred. Please contact support."
                });
            }
        }


        #region
        private async Task<bool> CheckBookExistsAsync(string name, string edition, int? bookIdToExclude = null)
        {
            return await _context.Books
                .AnyAsync(x =>
                    x.Title.ToLower() == name.ToLower() &&
                    x.Edition.ToLower() == edition.ToLower() &&
                    (!bookIdToExclude.HasValue || x.BookId != bookIdToExclude.Value));
        }
        #endregion

    }
}

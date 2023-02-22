using ContactManager.Data.Dtos;
using ContactManager.Services.Interfaces;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ContactManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<ActionResult> All()
        {
            ViewData["Users"] = await _userService.GetAllAsync();
            return View();
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(IFormFile file)
        {
            if (file.ContentType != "text/csv")
            {
                ModelState.AddModelError("", "Only csv files.");
                return View();
            }
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<AddUserDto>();
                    foreach(var record in records)
                    {
                        await _userService.AddUserAsync(record);
                    }

                }
            }
            return RedirectToAction(nameof(All));
        }
        public async Task<ActionResult> RemoveUser(Guid id)
        {
            var deletedUser = await _userService.DeleteUserAsync(id);
            if (deletedUser == null)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            var userDto = new UpdateUserDto()
            {
                Name = user.Name,
                Salary = user.Salary,
                DateOfBirth = user.DateOfBirth,
                IsMarried = user.Married,
                Phone = user.Phone
            };
            ViewData["Id"] = id;
            return View(userDto);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, UpdateUserDto userDto)
        {
            if(!ModelState.IsValid)
            {
                ViewData["Id"] = id;
                return View(userDto);
            }
            var user = await _userService.EditUserAsync(id, userDto);
            if (user == null) return BadRequest();

            return RedirectToAction(nameof(All));
        }
    }
}

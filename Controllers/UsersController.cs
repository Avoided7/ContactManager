using ContactManager.Data.Dtos;
using ContactManager.Services.Interfaces;
using ContactManager.Новая_папка;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace ContactManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICSVFileReader<AddUserDto> _fileReader;

        public UsersController(IUserService userService,
                               ICSVFileReader<AddUserDto> fileReader)
        {
            _userService = userService;
            _fileReader = fileReader;
        }
        public async Task<ActionResult> All()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
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
            var records = _fileReader.ReadFromFileAll(file);
            await _userService.AddRangeAsync(records);
            return RedirectToAction(nameof(All));
        }
        public async Task<ActionResult> RemoveUser(Guid id)
        {
            var deletedUser = await _userService.DeleteAsync(id);
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

            var userDto = user.ConvertToUpdateDto();
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
            var user = await _userService.EditAsync(id, userDto);
            if (user == null) return BadRequest();

            return RedirectToAction(nameof(All));
        }
    }
}

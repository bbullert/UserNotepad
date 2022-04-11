using AgileObjects.AgileMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;
using UserNotepad.Services;
using UserNotepad.ViewModels;

namespace UserNotepad.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IPhoneNumberService phoneNumberService;

        public UserController(IUserService userService, IPhoneNumberService phoneNumberService)
        {
            this.userService = userService;
            this.phoneNumberService = phoneNumberService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(int id = 1, string filter = null)
        {
            await userService.MockDataAsync();

            int pageSize = 20;
            var users = await userService.GetUsers(id, filter, pageSize);

            var model = new UserExplorerViewModel
            {
                Filter = filter,
                Users = users
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(UserExplorerViewModel model)
        {
            return RedirectToAction("Index", new { id = 1, filter = model.Filter });
        }

        [HttpGet]
        public async Task<IActionResult> DetailsAsync(Guid id)
        {
            var user = await userService.GetUserById(id);

            var model = Mapper.Map(user).ToANew<UserViewModel>();
            model.PhoneNumber = phoneNumberService.GetPhoneNumber(user.PhoneNumber);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserViewModel();
            model.PhoneNumber = phoneNumberService.GetPhoneNumber();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map(model).ToANew<User>();
                user.PhoneNumber = model.PhoneNumber.InternationalNumber;

                await userService.CreateUser(user);

                return RedirectToAction("Index");
            }

            model.PhoneNumber.CountryCodeOptions = phoneNumberService.GetCountryCodes().ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            var user = await userService.GetUserById(id);

            var model = Mapper.Map(user).ToANew<UserViewModel>();
            model.PhoneNumber = phoneNumberService.GetPhoneNumber(user.PhoneNumber);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = Mapper.Map(model).ToANew<User>();
                user.PhoneNumber = model.PhoneNumber.InternationalNumber;

                await userService.UpdateUser(user.Id, user);

                return RedirectToAction("Index");
            }

            model.PhoneNumber.CountryCodeOptions = phoneNumberService.GetCountryCodes().ToList();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await userService.RemoveUser(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Export()
        {
            var dataTypes = new List<SelectListItem> {
                new SelectListItem { Text = "JSON", Value = "json" },
                new SelectListItem { Text = "Microsoft Excel (.xlsx)", Value = "xlsx" }
            };

            var dateTime = DateTime.Now;
            var model = new UserExportViewModel
            {
                FileName = dateTime.ToString(),
                Date = dateTime,
                FileTypeOptions = dataTypes
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExportAsync(UserExportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var report = await userService.GenerateReport(model);

                return File(report.Content, report.ContentType, report.FullFileName);
            }

            return RedirectToAction("Export");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Tuan7.Models;

namespace Tuan7.Controllers
{
    public class AccountController : Controller
    {
        private static List<Account> accounts = new List<Account>
        {
            new Account
            {
                Id = 1,
                FullName = "Nguyễn Văn An",
                Email = "an.nguyen@gmail.com",
                Phone = "0986421127",
                Address = "Hà Nội",
                Avatar = "avatar1.png",
                Birthday = new DateTime(2000, 1, 15),
                Gender = "Nam",
                Password = "Password123",
                Facebook = "https://facebook.com/an.nguyen"
            }
        };

        // GET: Account
        public ActionResult Index()
        {
            return View(accounts);
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            Account model = new Account();
            return View(model);
        }

        // POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account model)
        {
            if (ModelState.IsValid)
            {
                model.Id = accounts.Count > 0 ? accounts.Max(a => a.Id) + 1 : 1;
                accounts.Add(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPhone(string phone)
        {
            Regex _isPhone = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            if (string.IsNullOrEmpty(phone) || !_isPhone.IsMatch(phone))
            {
                return Json($"Số điện thoại {phone} Không đúng định dạng, VD: 0986421127 hoặc 098.421.1127");
            }
            return Json(true);
        }
    }
}

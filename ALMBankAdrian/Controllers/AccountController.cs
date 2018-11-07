using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALMBankAdrian.Models;
using Microsoft.AspNetCore.Mvc;

namespace ALMBankAdrian.Controllers
{
    public class AccountController : Controller
    {
        private BankRepository _repo;
        public AccountController(BankRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Index(string accountNumber, string amount, string action)
        {

            bool format = decimal.TryParse(amount, out decimal amount2);

            if (format == false)
            {
                ViewBag.Result = "Amount format invalid";
                return View();
            }

            string result = "";
            if (action == "Withdraw")
            {
                result = _repo.Withdraw(int.Parse(accountNumber), amount2);
            }
            else if (action == "Deposit")
            {
                result = _repo.Deposit(int.Parse(accountNumber), amount2);
            }

            ViewBag.Result = result;

            return View();
        }

    }
}
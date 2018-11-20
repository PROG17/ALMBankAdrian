using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ALMBankAdrian.Models;
using Microsoft.AspNetCore.Mvc;

namespace ALMBankAdrian.Controllers
{
    public class TransferController : Controller
    {
        private BankRepository _repo;
        public TransferController(BankRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string tooAccount, string fromAccount, string amount)
        {
            if (tooAccount != null && fromAccount !=null && amount != null)
            {
                ViewBag.Result = "all info måste ges..";
                return View();

            }

            bool format = decimal.TryParse(amount, out decimal amount2);

            if (format == false)
            {
                ViewBag.Result = "Amount format invalid";
                return View();
            }


            var result = _repo.Transfer(int.Parse(fromAccount), int.Parse(tooAccount), amount2);
            ViewBag.Result = result;

            return View();

        }
    }
}
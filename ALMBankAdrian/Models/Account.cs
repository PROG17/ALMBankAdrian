using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALMBankAdrian.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public Customer Customer { get; set; }
    }
}

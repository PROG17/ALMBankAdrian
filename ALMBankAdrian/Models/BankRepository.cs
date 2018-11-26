using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALMBankAdrian.Models
{
    public class BankRepository
    {
        public BankRepository()
        {
            Customers = new List<Customer>();
            Accounts = new List<Account>();

            var customer1 = new Customer { Name = "Adrian", CustomerID = 1 };
            var customer2 = new Customer { Name = "Elias", CustomerID = 2 };
            var customer3 = new Customer { Name = "Testman", CustomerID = 3 };

            Customers.Add(customer1);
            Customers.Add(customer2);
            Customers.Add(customer3);

            Accounts.Add(new Account { AccountNumber = 1, Balance = 1000m, Customer = customer1 });
            Accounts.Add(new Account { AccountNumber = 2, Balance = 2000m, Customer = customer1 });
            Accounts.Add(new Account { AccountNumber = 3, Balance = 3000m, Customer = customer2 });
            Accounts.Add(new Account { AccountNumber = 4, Balance = 4000m, Customer = customer2 });
            Accounts.Add(new Account { AccountNumber = 5, Balance = 5000m, Customer = customer3 });
        }
        public List<Customer> Customers { get; set; }

        public List<Account> Accounts { get; set; }

        public string Withdraw(int accountNumber, decimal amount)
        {
            var targetAccount = Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (targetAccount == null)
            {
                return "Couldn't find account number";
            }

            if (targetAccount.Balance >= amount)
            {
                targetAccount.Balance = targetAccount.Balance - amount;
                return "Success";
            }
            else
            {
                return "Insufficient balance";
            }
         
        }

        public string Deposit(int accountNumber, decimal amount)
        {
            var targetAccount = Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (targetAccount == null)
            {
                return "Couldn't find account number";
            }

            targetAccount.Balance = targetAccount.Balance + amount;
            return "Success";
        }


        public string Transfer(int FromAccountNumber, int ToAccountNumber, decimal amount)
        {
            var from = Accounts.FirstOrDefault(a => a.AccountNumber == FromAccountNumber);
            var too = Accounts.FirstOrDefault(a => a.AccountNumber == ToAccountNumber);

            if (too != null && from != null)
            {
                if (from.Balance >= amount)
                {
                    Withdraw(from.AccountNumber, amount);
                    Deposit(too.AccountNumber, amount);

                    return "Överförde " + amount + "kr \nFrån: " + FromAccountNumber + " \nTill: " + ToAccountNumber;

                }
                else
                    return "Saknas pengar";

                

            }


            return "har du rätt kontoNr? ";

        }


    }
}

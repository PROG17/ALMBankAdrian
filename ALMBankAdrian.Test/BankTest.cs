using System;
using Xunit;
using ALMBankAdrian.Models;

namespace ALMBankAdrian.Test
{
    public class BankTest
    {
        [Fact]
        public void TestWithdrawal_NoAccount()
        {
            BankRepository repo = new BankRepository();
            string result = repo.Withdraw(0, 100m);

            Assert.Equal("Couldn't find account number", result);
        }

        [Fact]
        public void TestDeposit_NoAccount()
        {
            BankRepository repo = new BankRepository();
            string result = repo.Deposit(0, 100m);

            Assert.Equal("Couldn't find account number", result);
        }

        [Fact]
        public void TestWithdrawal_Deny()
        {
            BankRepository repo = new BankRepository();
            Account TestAccount = new Account
            {
                AccountNumber = 0,
                Balance = 1000m,
                Customer = new Customer { Name = "Testman", CustomerID = 0 }
            };
            decimal withdrawalAmount = 2000m;
            repo.Accounts.Add(TestAccount);

            string result = repo.Withdraw(TestAccount.AccountNumber, withdrawalAmount);

            Assert.Equal("Insufficient balance", result);
            Assert.Equal(1000m, TestAccount.Balance);
        }

        [Fact]
        public void TestWithdrawal_Success()
        {
            BankRepository repo = new BankRepository();
            Account TestAccount = new Account
            {
                AccountNumber = 0,
                Balance = 1000m,
                Customer = new Customer { Name = "Testman", CustomerID = 0 }
            };
            decimal withdrawalAmount = 1000m;
            repo.Accounts.Add(TestAccount);

            string result = repo.Withdraw(TestAccount.AccountNumber, withdrawalAmount);

            Assert.Equal("Success", result);
            Assert.Equal(0m, TestAccount.Balance);
        }

        [Fact]
        public void TestDeposit_Success()
        {
            BankRepository repo = new BankRepository();
            Account TestAccount = new Account
            {
                AccountNumber = 0,
                Balance = 1000m,
                Customer = new Customer { Name = "Testman", CustomerID = 0 }
            };
            decimal depositAmount = 1000m;
            repo.Accounts.Add(TestAccount);

            string result = repo.Deposit(TestAccount.AccountNumber, depositAmount);

            Assert.Equal("Success", result);
            Assert.Equal(2000m, TestAccount.Balance);
        }

    }
}

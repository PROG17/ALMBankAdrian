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




        [Fact]
        public void TransferTest_Denied()
        {
            BankRepository repo = new BankRepository();
            Customer cust1 = new Customer { Name = "cust1", CustomerID = 22 };
            Account acc1 = new Account { AccountNumber = 6, Customer = cust1, Balance = 1 };
            Customer cust2 = new Customer { Name = "cust2", CustomerID = 23 };
            Account acc2 = new Account { AccountNumber = 7, Customer = cust2, Balance = 0 };

            repo.Accounts.Add(acc1);
            repo.Accounts.Add(acc2);

            var result = repo.Transfer(6,7, 5);

            Assert.Equal("Saknas pengar", result);

            Assert.Equal(1, acc1.Balance);
            Assert.Equal(0, acc2.Balance);
        }

        [Fact]
        public void TransferTest()
        {
            BankRepository repo = new BankRepository();
            Customer cust1 = new Customer { Name = "cust1", CustomerID = 22 };
            Account acc1 = new Account { AccountNumber = 6, Customer = cust1, Balance = 1000 };
            Customer cust2 = new Customer { Name = "cust2", CustomerID = 23 };
            Account acc2 = new Account { AccountNumber = 7, Customer = cust2, Balance = 0 };

            repo.Accounts.Add(acc1);
            repo.Accounts.Add(acc2);

            var result = repo.Transfer(6,7,1000);

            Assert.Equal(0, acc1.Balance);
            Assert.Equal(1000, acc2.Balance);
        }

    }
}

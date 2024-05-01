using System.Collections.Generic;

namespace ATMSystem.Tests
{
    public static class TestData
    {
        public static Account ValidAccount = new Account
        {
            AccountNumber = 12345,
            Login = "testlogin",
            Pin = "testpin",
            Name = "Test User",
            Balance = 1000,
            Status = "Active",
            IsAdmin = false
        };

        public static List<Account> AccountList = new List<Account>
        {
            new Account
            {
                AccountNumber = 12345,
                Login = "testlogin",
                Pin = "testpin",
                Name = "Test User",
                Balance = 1000,
                Status = "Active",
                IsAdmin = false
            },
            new Account
            {
                AccountNumber = 67890,
                Login = "adminlogin",
                Pin = "adminpin",
                Name = "Admin User",
                Balance = 0,
                Status = "Active",
                IsAdmin = true
            }
        };
    }
}

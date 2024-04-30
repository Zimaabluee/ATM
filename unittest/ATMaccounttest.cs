// <copyright file="ATMaccounttest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem.Tests
{
    using System;
    using Xunit;

    public class AccountTests
    {
        [Fact]
        public void Account_Properties_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var account = new Account
            {
                AccountNumber = 123456,
                Login = "testuser",
                Pin = "abc123",
                Name = "John Doe",
                Balance = 1000,
                Status = "Active",
                Date = new DateTime(2023, 5, 1),
                IsAdmin = false,
            };

            // Act
            // Nothing to do here, as we're just testing the properties

            // Assert
            Assert.Equal(123456, account.AccountNumber);
            Assert.Equal("testuser", account.Login);
            Assert.Equal("abc123", account.Pin);
            Assert.Equal("John Doe", account.Name);
            Assert.Equal(1000, account.Balance);
            Assert.Equal("Active", account.Status);
            Assert.Equal(new DateTime(2023, 5, 1), account.Date);
            Assert.False(account.IsAdmin);
        }
    }
}

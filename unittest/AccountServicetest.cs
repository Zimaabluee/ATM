// <copyright file="AccountServicetest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem.Tests
{
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public class AccountServiceTests
    {
        [Fact]
        public void CreateAccount_ValidAccount_CallsCreateAccountInRepository()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            var service = new AccountService(mockRepository.Object);
            var account = new Account();

            // Act
            service.CreateAccount(account);

            // Assert
            mockRepository.Verify(r => r.CreateAccount(account), Times.Once);
        }

        [Fact]
        public void DeleteAccount_ValidAccountId_CallsDeleteAccountInRepository()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            var service = new AccountService(mockRepository.Object);
            int accountId = 1;

            // Act
            service.DeleteAccount(accountId);

            // Assert
            mockRepository.Verify(r => r.DeleteAccount(accountId), Times.Once);
        }

        [Fact]
        public void GetAccount_ValidAccountId_CallsGetAccountInRepository()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            var service = new AccountService(mockRepository.Object);
            int accountId = 1;

            // Act
            service.GetAccount(accountId);

            // Assert
            mockRepository.Verify(r => r.GetAccount(accountId), Times.Once);
        }

        [Fact]
        public void UpdateAccount_ValidAccount_CallsUpdateAccountInRepository()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            var service = new AccountService(mockRepository.Object);
            var account = new Account();

            // Act
            service.UpdateAccount(account);

            // Assert
            mockRepository.Verify(r => r.UpdateAccount(account), Times.Once);
        }

        [Fact]
        public void Login_ValidCredentials_CallsLoginInRepository()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            var service = new AccountService(mockRepository.Object);
            string login = "testuser";
            string pin = "abc123";

            // Act
            service.Login(login, pin);

            // Assert
            mockRepository.Verify(r => r.Login(login, pin), Times.Once);
        }

        [Fact]
        public void GetAllAccounts_CallsGetAllAccountsInRepository()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            var service = new AccountService(mockRepository.Object);

            // Act
            service.GetAllAccounts();

            // Assert
            mockRepository.Verify(r => r.GetAllAccounts(), Times.Once);
        }
    }
}

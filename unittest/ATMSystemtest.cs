using System;
using ATMSystem;
using Moq;
using Xunit;

namespace ATMSystem.Tests
{
    public class ATMSystemTests
    {
        [Fact]
        public void Run_Should_Display_Welcome_Message()
        {
            // Arrange
            var mockAccountService = new Mock<AccountService>(MockBehavior.Strict);
            var mockConsole = new Mock<IConsole>(MockBehavior.Strict);
            var atmSystem = new ATMSystem(mockAccountService.Object, mockConsole.Object);

            // Expectations
            mockConsole.Setup(c => c.WriteLine("Welcome to the ATM System!"));

            // Act
            atmSystem.Run();

            // Assert
            mockConsole.VerifyAll();
        }

        [Fact]
        public void WithdrawCash_Should_Update_Account_Balance_And_Display_Success_Message()
        {
            // Arrange
            var mockAccountService = new Mock<AccountService>(MockBehavior.Strict);
            var mockConsole = new Mock<IConsole>(MockBehavior.Strict);
            var atmSystem = new ATMSystem(mockAccountService.Object, mockConsole.Object);
            var account = new Account { AccountNumber = 12345, Balance = 1000 };

            // Expectations
            mockConsole.SetupSequence(c => c.ReadLine())
                .Returns("500"); // Simulate user input of withdrawal amount

            mockAccountService.Setup(a => a.UpdateAccount(account));
            mockConsole.Setup(c => c.WriteLine("Cash Successfully Withdrawn"));
            mockConsole.Setup(c => c.WriteLine($"Account #{account.AccountNumber}"));
            mockConsole.Setup(c => c.WriteLine($"Date: {DateTime.Now.ToString("MM/dd/yyyy")}"));
            mockConsole.Setup(c => c.WriteLine($"Withdrawn: $500.00"));
            mockConsole.Setup(c => c.WriteLine($"Balance: $500.00"));

            // Act
            atmSystem.WithdrawCash(account);

            // Assert
            mockAccountService.VerifyAll();
            mockConsole.VerifyAll();
        }

        // Add more test methods for other functionalities of ATMSystem class as needed
    }
}

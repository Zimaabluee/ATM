// <copyright file="ATMSystemtest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem.Tests
{
    using Moq;
    using Xunit;

    public class AtmsystemTests
    {
        [Fact]
        public void Login_ValidCredentials_ReturnsAccount()
        {
            // Arrange
            var mockRepository = new Mock<IAccountRepository>();
            mockRepository.Setup(r => r.Login("testuser", "abc123"))
                .Returns(new Account { /* 初始化账户属性 */ });

            var service = new AccountService(mockRepository.Object);

            // Act
            var account = service.Login("testuser", "abc123");

            // Assert
            Assert.NotNull(account);

            // 其他断言...
        }

        // 其他测试方法...
    }
}

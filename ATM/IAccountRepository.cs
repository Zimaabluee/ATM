// <copyright file="IAccountRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem
{
    using System.Collections.Generic;

    public interface IAccountRepository
    {
        void CreateAccount(Account account);

        void DeleteAccount(int accountId);

        Account GetAccount(int accountId);

        void UpdateAccount(Account account);

        Account Login(string login, string pin);

        List<Account> GetAllAccounts();
    }
}

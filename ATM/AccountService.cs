// <copyright file="AccountService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem
{
    using System.Collections.Generic;

    public class AccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public void CreateAccount(Account account)
        {
            this.accountRepository.CreateAccount(account);
        }

        public void DeleteAccount(int accountId)
        {
            this.accountRepository.DeleteAccount(accountId);
        }

        public Account GetAccount(int accountId)
        {
            return this.accountRepository.GetAccount(accountId);
        }

        public void UpdateAccount(Account account)
        {
            this.accountRepository.UpdateAccount(account);
        }

        public Account Login(string login, string pin)
        {
            return this.accountRepository.Login(login, pin);
        }

        public List<Account> GetAllAccounts()
        {
            return this.accountRepository.GetAllAccounts();
        }
    }
}

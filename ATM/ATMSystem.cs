// <copyright file="ATMSystem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem
{
    using System;

    public interface IConsole
    {
        void WriteLine(string message);

        string ReadLine();

        void Write(string message);
    }

    public class MyConsole : IConsole
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.Write(message);
        }
    }

    public class ATMSystem
    {
        private readonly AccountService accountService;
        private readonly IConsole console;

        public ATMSystem(AccountService accountService, IConsole console)
        {
            this.accountService = accountService;
            this.console = console;
        }

        public void Run()
        {
            this.console.WriteLine("Welcome to the ATM System!");

            while (true)
            {
                this.console.WriteLine("Login:");
                this.console.Write("Enter Login: ");
                string login = this.console.ReadLine();
                this.console.Write("Enter PIN: ");
                string pin = this.console.ReadLine();

                Account account = this.accountService.Login(login, pin);

                if (account == null)
                {
                    this.console.WriteLine("Invalid login or PIN. Please try again.");
                    continue;
                }

                this.console.WriteLine($"Welcome, {account.Name}!");

                if (account.IsAdmin)
                {
                    this.AdminMenu(account);
                }
                else
                {
                    this.UserMenu(account);
                }
            }
        }

        private void UserMenu(Account account)
        {
            while (true)
            {
                this.console.WriteLine("Select an option:");
                this.console.WriteLine("1. Withdraw Cash");
                this.console.WriteLine("2. Deposit Cash");
                this.console.WriteLine("3. Display Balance");
                this.console.WriteLine("4. Exit");

                int choice;
                if (!int.TryParse(this.console.ReadLine(), out choice))
                {
                    this.console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        this.WithdrawCash(account);
                        break;
                    case 2:
                        this.DepositCash(account);
                        break;
                    case 3:
                        this.DisplayBalance(account);
                        break;
                    case 4:
                        this.console.WriteLine("Exiting ATM System. Goodbye!");
                        return;
                    default:
                        this.console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void AdminMenu(Account account)
        {
            while (true)
            {
                this.console.WriteLine("Select an option:");
                this.console.WriteLine("1. Create New Account");
                this.console.WriteLine("2. Delete Existing Account");
                this.console.WriteLine("3. Update Account Information");
                this.console.WriteLine("4. Search for Account");
                this.console.WriteLine("5. Exit");

                int choice;
                if (!int.TryParse(this.console.ReadLine(), out choice))
                {
                    this.console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        this.CreateAccount();
                        break;
                    case 2:
                        this.DeleteAccount();
                        break;
                    case 3:
                        this.UpdateAccount();
                        break;
                    case 4:
                        this.SearchAccount();
                        break;
                    case 5:
                        this.console.WriteLine("Exiting ATM System. Goodbye!");
                        return;
                    default:
                        this.console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void WithdrawCash(Account account)
        {
            this.console.WriteLine("Withdraw Cash");
            this.console.Write("Enter the withdrawal amount: ");
            int withdrawalAmount;
            while (!int.TryParse(this.console.ReadLine(), out withdrawalAmount) || withdrawalAmount <= 0 || withdrawalAmount > account.Balance)
            {
                this.console.WriteLine("Invalid amount. Please enter a valid withdrawal amount.");
                this.console.Write("Enter the withdrawal amount: ");
            }

            account.Balance -= withdrawalAmount;
            account.Date = DateTime.Now;
            this.accountService.UpdateAccount(account);
            this.console.WriteLine("Cash Successfully Withdrawn");
            this.console.WriteLine($"Account #{account.AccountNumber}");
            this.console.WriteLine($"Date: {account.Date.ToString("MM/dd/yyyy")}");
            this.console.WriteLine($"Withdrawn: {withdrawalAmount:C}");
            this.console.WriteLine($"Balance: {account.Balance:C}");
        }

        private void DepositCash(Account account)
        {
            this.console.WriteLine("Deposit Cash");
            this.console.Write("Enter the deposit amount: ");
            int depositAmount;
            while (!int.TryParse(this.console.ReadLine(), out depositAmount) || depositAmount <= 0)
            {
                this.console.WriteLine("Invalid amount. Please enter a valid deposit amount.");
                this.console.Write("Enter the deposit amount: ");
            }

            account.Balance += depositAmount;
            account.Date = DateTime.Now;
            this.accountService.UpdateAccount(account);
            this.console.WriteLine("Cash Successfully Deposited");
            this.console.WriteLine($"Account #{account.AccountNumber}");
            this.console.WriteLine($"Date: {account.Date.ToString("MM/dd/yyyy")}");
            this.console.WriteLine($"Deposited: {depositAmount:C}");
            this.console.WriteLine($"Balance: {account.Balance:C}");
        }

        private void DisplayBalance(Account account)
        {
            this.console.WriteLine($"Account #{account.AccountNumber}");
            this.console.WriteLine($"Date: {DateTime.Now.ToString("MM/dd/yyyy")}");
            this.console.WriteLine($"Balance: {account.Balance:N}");
        }

        private void CreateAccount()
        {
            this.console.WriteLine("Create New Account");

            this.console.Write("Enter Login: ");
            string login = this.console.ReadLine();
            this.console.Write("Enter PIN: ");
            string pin = this.console.ReadLine();
            this.console.Write("Enter Name: ");
            string name = this.console.ReadLine();
            this.console.Write("Enter Initial Balance: ");
            int balance = Convert.ToInt32(this.console.ReadLine());
            this.console.Write("Enter Status: ");
            string status = this.console.ReadLine();

            Account newAccount = new Account
            {
                Login = login,
                Pin = pin,
                Name = name,
                Balance = balance,
                Status = status,
                IsAdmin = false,
            };

            this.accountService.CreateAccount(newAccount);

            Account createdAccount = this.accountService.Login(login, pin);

            if (createdAccount != null)
            {
                this.console.WriteLine($"Account Successfully Created – the account number assigned is: {createdAccount.AccountNumber}");
            }
            else
            {
                this.console.WriteLine("Failed to retrieve the newly created account number.");
            }
        }

        private void DeleteAccount()
        {
            this.console.WriteLine("Delete Existing Account");
            this.console.Write("Enter Account Number to Delete: ");
            int accountId = Convert.ToInt32(this.console.ReadLine());

            Account accountToDelete = this.accountService.GetAccount(accountId);

            if (accountToDelete == null)
            {
                this.console.WriteLine("Account not found.");
                return;
            }

            this.console.WriteLine($"You wish to delete the account held by {accountToDelete.Name}.");
            this.console.WriteLine("If this information is correct, please re-enter the account number:");

            int confirmAccountId = Convert.ToInt32(this.console.ReadLine());

            if (confirmAccountId != accountId)
            {
                this.console.WriteLine("Account numbers do not match. Deletion aborted.");
                return;
            }

            this.accountService.DeleteAccount(accountId);
            this.console.WriteLine("Account Deleted Successfully.");
        }

        private void UpdateAccount()
        {
            this.console.WriteLine("Update Account Information");
            this.console.Write("Enter Account Number to Update: ");
            int accountId = Convert.ToInt32(this.console.ReadLine());

            Account existingAccount = this.accountService.GetAccount(accountId);

            if (existingAccount == null)
            {
                this.console.WriteLine("Account not found.");
                return;
            }

            this.console.WriteLine($"Account # {existingAccount.AccountNumber}");
            this.console.WriteLine($"Holder: {existingAccount.Name}  (can be update)");
            this.console.WriteLine($"Balance: {existingAccount.Balance}");
            this.console.WriteLine($"Status: {existingAccount.Status}  (can be update)");
            this.console.WriteLine($"Login: {existingAccount.Login}  (can be update)");
            this.console.WriteLine($"Pin Code: {existingAccount.Pin}  (can be update)");
            Console.ResetColor();

            this.console.Write("Enter New Holder Name: ");
            string newName = this.console.ReadLine();
            this.console.Write("Enter New Status: ");
            string newStatus = this.console.ReadLine();
            this.console.Write("Enter New Login: ");
            string newLogin = this.console.ReadLine();
            this.console.Write("Enter New Pin Code: ");
            string newPin = this.console.ReadLine();

            existingAccount.Name = newName;
            existingAccount.Status = newStatus;
            existingAccount.Login = newLogin;
            existingAccount.Pin = newPin;

            this.accountService.UpdateAccount(existingAccount);
            this.console.WriteLine("Account Updated Successfully.");
        }

        private void SearchAccount()
        {
            this.console.WriteLine("Search for Account");
            this.console.Write("Enter Account Number to Search: ");
            int accountId = Convert.ToInt32(this.console.ReadLine());

            Account searchedAccount = this.accountService.GetAccount(accountId);

            if (searchedAccount == null)
            {
                this.console.WriteLine("Account not found.");
            }
            else
            {
                this.console.WriteLine("The account information is:");
                this.console.WriteLine($"Account # {searchedAccount.AccountNumber}");
                this.console.WriteLine($"Holder: {searchedAccount.Name}");
                this.console.WriteLine($"Balance: {searchedAccount.Balance}");
                this.console.WriteLine($"Status: {searchedAccount.Status}");
            }
        }
    }
}

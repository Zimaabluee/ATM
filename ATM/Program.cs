using System;
using System.Collections.Generic;
using System.Security.Principal;
using MySql.Data.MySqlClient;
using Ninject;

namespace ATMSystem
{
    // Entry point of the application
    class Program
    {
        static void Main(string[] args)
        {
            // Setup Ninject DI container
            var kernel = new StandardKernel(new ATMModule());

            // Resolve ATMSystem and start the application
            var atmSystem = kernel.Get<ATMSystem>();
            atmSystem.Run();
        }
    }

    // Data Model
    // Data Model representing an account
    public class Account
    {
        // Private fields representing account properties
        private int account_Number;
        private string login;
        private string pin;
        private string name;
        private int balance;
        private string status;
        private DateTime date;
        private bool isAdmin;

        // Methods to set and get account properties
        public void SetAccountNumber(int accountNumber) { account_Number = accountNumber; }
        public int GetAccountNumber() { return account_Number; }
        public void SetLogin(string newLogin) { login = newLogin; }
        public string GetLogin() { return login; }
        public void SetPin(string newPin) { pin = newPin; }
        public string GetPin() { return pin; }
        public void SetName(string newName) { name = newName; }
        public string GetName() { return name; }
        public void SetBalance(int newBalance) { balance = newBalance; }
        public int GetBalance() { return balance; }
        public void SetStatus(string newStatus) { status = newStatus; }
        public string GetStatus() { return status; }
        public void SetDate(DateTime newDate) { date = newDate; }
        public DateTime GetDate() { return date; }
        public void SetIsAdmin(bool newIsAdmin) { isAdmin = newIsAdmin; }
        public bool GetIsAdmin() { return isAdmin; }
    }

    // Data Access Layer (DAL)
    public interface IAccountRepository
    {
        void CreateAccount(Account account);
        void DeleteAccount(int accountId);
        Account GetAccount(int accountId);
        void UpdateAccount(Account account);
        Account Login(string login, string pin);
        List<Account> GetAllAccounts(); // Added method to get all accounts
    }

    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateAccount(Account account)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO accounts (account_Number, Login, Pin, Name, Balance, Status, IsAdmin) VALUES (@account_Number, @Login, @Pin, @Name, @Balance, @Status, @IsAdmin)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@account_Number", account.GetAccountNumber());
                command.Parameters.AddWithValue("@Login", account.GetLogin());
                command.Parameters.AddWithValue("@Pin", account.GetPin());
                command.Parameters.AddWithValue("@Name", account.GetName());
                command.Parameters.AddWithValue("@Balance", account.GetBalance());
                command.Parameters.AddWithValue("@Status", account.GetStatus());
                command.Parameters.AddWithValue("@IsAdmin", account.GetIsAdmin());
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAccount(int accountId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM accounts WHERE account_Number = @AccountId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountId", accountId);
                command.ExecuteNonQuery();
            }
        }

        public Account GetAccount(int accountId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM accounts WHERE account_Number = @AccountId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@AccountId", accountId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Account account = new Account();
                        account.SetAccountNumber(Convert.ToInt32(reader["account_Number"]));
                        account.SetLogin(reader["Login"].ToString());
                        account.SetPin(reader["Pin"].ToString());
                        account.SetName(reader["Name"].ToString());
                        account.SetBalance(Convert.ToInt32(reader["Balance"]));
                        account.SetStatus(reader["Status"].ToString());
                        account.SetIsAdmin(Convert.ToBoolean(reader["IsAdmin"]));
                        return account;
                    }
                }
            }
            return null;
        }

        public void UpdateAccount(Account account)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE accounts SET Login = @Login, Pin = @Pin, Name = @Name, Balance = @Balance, Status = @Status WHERE account_Number = @account_Number";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@account_Number", account.GetAccountNumber());
                command.Parameters.AddWithValue("@Login", account.GetLogin());
                command.Parameters.AddWithValue("@Pin", account.GetPin());
                command.Parameters.AddWithValue("@Name", account.GetName());
                command.Parameters.AddWithValue("@Balance", account.GetBalance());
                command.Parameters.AddWithValue("@Status", account.GetStatus());
                command.ExecuteNonQuery();
            }
        }

        public Account Login(string login, string pin)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM accounts WHERE Login = @Login AND Pin = @Pin";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Pin", pin);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Account account = new Account();
                        account.SetAccountNumber(Convert.ToInt32(reader["account_Number"]));
                        account.SetLogin(reader["Login"].ToString());
                        account.SetPin(reader["Pin"].ToString());
                        account.SetName(reader["Name"].ToString());
                        account.SetBalance(Convert.ToInt32(reader["Balance"]));
                        account.SetStatus(reader["Status"].ToString());
                        account.SetIsAdmin(Convert.ToBoolean(reader["IsAdmin"]));
                        return account;
                    }
                }
            }
            return null;
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM accounts";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Account account = new Account();
                        account.SetAccountNumber(Convert.ToInt32(reader["account_Number"]));
                        account.SetLogin(reader["Login"].ToString());
                        account.SetPin(reader["Pin"].ToString());
                        account.SetName(reader["Name"].ToString());
                        account.SetBalance(Convert.ToInt32(reader["Balance"]));
                        account.SetStatus(reader["Status"].ToString());
                        account.SetIsAdmin(Convert.ToBoolean(reader["IsAdmin"]));
                        accounts.Add(account);
                    }
                }
            }
            return accounts;
        }
    }

    // Business Logic Layer (BLL)
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void CreateAccount(Account account)
        {
            _accountRepository.CreateAccount(account);
        }

        public void DeleteAccount(int accountId)
        {
            _accountRepository.DeleteAccount(accountId);
        }

        public Account GetAccount(int accountId)
        {
            return _accountRepository.GetAccount(accountId);
        }

        public void UpdateAccount(Account account)
        {
            _accountRepository.UpdateAccount(account);
        }

        public Account Login(string login, string pin)
        {
            return _accountRepository.Login(login, pin);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }
    }

    // ATM System
    public class ATMSystem
    {
        private readonly AccountService _accountService;
        private List<Account> _accounts;

        public ATMSystem(AccountService accountService)
        {
            _accountService = accountService;
            _accounts = _accountService.GetAllAccounts();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the ATM System!");

            while (true)
            {
                Console.WriteLine("Login:");
                Console.Write("Enter Login: ");
                string login = Console.ReadLine();
                Console.Write("Enter PIN: ");
                string pin = Console.ReadLine();

                Account account = _accountService.Login(login, pin);

                if (account == null)
                {
                    Console.WriteLine("Invalid login or PIN. Please try again.");
                    continue;
                }

                Console.WriteLine($"Welcome, {account.GetName()}!");

                if (account.GetIsAdmin())
                {
                    AdminMenu(account);
                }
                else
                {
                    UserMenu(account);
                }
            }
        }

        private void UserMenu(Account account)
        {
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Withdraw Cash");
                Console.WriteLine("2. Deposit Cash");
                Console.WriteLine("3. Display Balance");
                Console.WriteLine("4. Exit");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        WithdrawCash(account);
                        break;
                    case 2:
                        DepositCash(account);
                        break;
                    case 3:
                        DisplayBalance(account);
                        break;
                    case 4:
                        Console.WriteLine("Exiting ATM System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void AdminMenu(Account account)
        {
            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Create New Account");
                Console.WriteLine("2. Delete Existing Account");
                Console.WriteLine("3. Update Account Information");
                Console.WriteLine("4. Search for Account");
                Console.WriteLine("5. Exit");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        DeleteAccount();
                        break;
                    case 3:
                        UpdateAccount();
                        break;
                    case 4:
                        SearchAccount();
                        break;
                    case 5:
                        Console.WriteLine("Exiting ATM System. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void WithdrawCash(Account account)
        {
            Console.WriteLine("Withdraw Cash");
            // Implement withdrawal logic here
            Console.Write("Enter the withdrawal amount: ");
            int withdrawalAmount;
            while (!int.TryParse(Console.ReadLine(), out withdrawalAmount) || withdrawalAmount <= 0 || withdrawalAmount > account.GetBalance())
            {
                Console.WriteLine("Invalid amount. Please enter a valid withdrawal amount.");
                Console.Write("Enter the withdrawal amount: ");
            }

            // Update balance after withdrawal
            account.SetBalance(account.GetBalance() - withdrawalAmount);
            account.SetDate(DateTime.Now);
            _accountService.UpdateAccount(account);
            Console.WriteLine("Cash Successfully Withdrawn");
            Console.WriteLine($"Account #{account.GetAccountNumber()}");
            Console.WriteLine($"Date: {account.GetDate().ToString("MM/dd/yyyy")}");
            Console.WriteLine($"Withdrawn: {withdrawalAmount:C}");
            Console.WriteLine($"Balance: {account.GetBalance():C}");
        }

        private void DepositCash(Account account)
        {
            Console.WriteLine("Deposit Cash");
            // Implement deposit logic here
            Console.Write("Enter the deposit amount: ");
            int depositAmount;
            while (!int.TryParse(Console.ReadLine(), out depositAmount) || depositAmount <= 0)
            {
                Console.WriteLine("Invalid amount. Please enter a valid deposit amount.");
                Console.Write("Enter the deposit amount: ");
            }

            // Update balance after deposit
            account.SetBalance(account.GetBalance() + depositAmount);
            account.SetDate(DateTime.Now); // Set deposit date to current system date
            _accountService.UpdateAccount(account);
            Console.WriteLine("Cash Successfully Deposited");
            Console.WriteLine($"Account #{account.GetAccountNumber()}");
            Console.WriteLine($"Date: {account.GetDate().ToString("MM/dd/yyyy")}"); // Format date as MM/dd/yyyy
            Console.WriteLine($"Deposited: {depositAmount:C}");
            Console.WriteLine($"Balance: {account.GetBalance():C}");
        }

        private void DisplayBalance(Account account)
        {
            Console.WriteLine($"Account #{account.GetAccountNumber()}");
            Console.WriteLine($"Date: {DateTime.Now.ToString("MM/dd/yyyy")}");
            Console.WriteLine($"Balance: {account.GetBalance():N}");
        }

        private void CreateAccount()
        {
            Console.WriteLine("Create New Account");

            // Collect user input for the new account
            Console.Write("Enter Login: ");
            string login = Console.ReadLine();
            Console.Write("Enter PIN: ");
            string pin = Console.ReadLine();
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Initial Balance: ");
            int balance = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Status: ");
            string status = Console.ReadLine();

            // Create new Account object with empty account number
            Account newAccount = new Account();
            newAccount.SetLogin(login);
            newAccount.SetPin(pin);
            newAccount.SetName(name);
            newAccount.SetBalance(balance);
            newAccount.SetStatus(status);
            newAccount.SetIsAdmin(false);

            // Call AccountService to create the new account
            _accountService.CreateAccount(newAccount);

            // Account number will be assigned by the database, so fetch the newly created account
            Account createdAccount = _accountService.Login(login, pin); // Assuming login and pin uniquely identify the account

            if (createdAccount != null)
            {
                Console.WriteLine($"Account Successfully Created – the account number assigned is: {createdAccount.GetAccountNumber()}");
            }
            else
            {
                Console.WriteLine("Failed to retrieve the newly created account number.");
            }
        }

        private void DeleteAccount()
        {
            Console.WriteLine("Delete Existing Account");
            Console.Write("Enter Account Number to Delete: ");
            int accountId = Convert.ToInt32(Console.ReadLine());

            // Call AccountService to get the account details
            Account accountToDelete = _accountService.GetAccount(accountId);

            if (accountToDelete == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            // Confirm account details with the admin
            Console.WriteLine($"You wish to delete the account held by {accountToDelete.GetName()}.");
            Console.WriteLine("If this information is correct, please re-enter the account number:");

            // Re-enter account number for confirmation
            int confirmAccountId = Convert.ToInt32(Console.ReadLine());

            if (confirmAccountId != accountId)
            {
                Console.WriteLine("Account numbers do not match. Deletion aborted.");
                return;
            }

            // Call AccountService to delete the account
            _accountService.DeleteAccount(accountId);
            Console.WriteLine("Account Deleted Successfully.");
        }

        private void UpdateAccount()
        {
            Console.WriteLine("Update Account Information");
            Console.Write("Enter Account Number to Update: ");
            int accountId = Convert.ToInt32(Console.ReadLine());

            // Call AccountService to get the account details
            Account existingAccount = _accountService.GetAccount(accountId);

            if (existingAccount == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            // Display account details with updateable fields in green
            Console.WriteLine($"Account # {existingAccount.GetAccountNumber()}");
            Console.WriteLine($"Holder: {existingAccount.GetName()}  (can be update)");
            Console.WriteLine($"Balance: {existingAccount.GetBalance()}");
            Console.WriteLine($"Status: {existingAccount.GetStatus()}  (can be update)");
            Console.WriteLine($"Login: {existingAccount.GetLogin()}  (can be update)");
            Console.WriteLine($"Pin Code: {existingAccount.GetPin()}  (can be update)");
            Console.ResetColor();

            // Collect updated information from user
            Console.Write("Enter New Holder Name: ");
            string newName = Console.ReadLine();
            Console.Write("Enter New Status: ");
            string newStatus = Console.ReadLine();
            Console.Write("Enter New Login: ");
            string newLogin = Console.ReadLine();
            Console.Write("Enter New Pin Code: ");
            string newPin = Console.ReadLine();

            // Update account information
            existingAccount.SetName(newName);
            existingAccount.SetStatus(newStatus);
            existingAccount.SetLogin(newLogin);
            existingAccount.SetPin(newPin);

            // Call AccountService to update the account
            _accountService.UpdateAccount(existingAccount);
            Console.WriteLine("Account Updated Successfully.");
        }

        private void SearchAccount()
        {
            Console.WriteLine("Search for Account");
            Console.Write("Enter Account Number to Search: ");
            int accountId = Convert.ToInt32(Console.ReadLine());

            // Call AccountService to get the account details
            Account searchedAccount = _accountService.GetAccount(accountId);

            if (searchedAccount == null)
            {
                Console.WriteLine("Account not found.");
            }
            else
            {
                Console.WriteLine("The account information is:");
                Console.WriteLine($"Account # {searchedAccount.GetAccountNumber()}");
                Console.WriteLine($"Holder: {searchedAccount.GetName()}");
                Console.WriteLine($"Balance: {searchedAccount.GetBalance()}");
                Console.WriteLine($"Status: {searchedAccount.GetStatus()}");
            }
        }
    }

    // Dependency Injection
    public class ATMModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountRepository>().To<AccountRepository>().WithConstructorArgument("connectionString", "server=localhost;database=ATMDatabase;uid=root;password=akram918;\r\n");
            Bind<AccountService>().ToSelf();
            Bind<ATMSystem>().ToSelf();
        }
    }
}

// <copyright file="AccountRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem
{
    using System;
    using System.Collections.Generic;
    using MySql.Data.MySqlClient;

    public class AccountRepository : IAccountRepository
    {
        private readonly string connectionString;

        public AccountRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateAccount(Account account)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Open();
                string query = "INSERT INTO accounts (account_Number, Login, Pin, Name, Balance, Status, IsAdmin) VALUES (@account_Number, @Login, @Pin, @Name, @Balance, @Status, @IsAdmin)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@account_Number", account.AccountNumber);
                command.Parameters.AddWithValue("@Login", account.Login);
                command.Parameters.AddWithValue("@Pin", account.Pin);
                command.Parameters.AddWithValue("@Name", account.Name);
                command.Parameters.AddWithValue("@Balance", account.Balance);
                command.Parameters.AddWithValue("@Status", account.Status);
                command.Parameters.AddWithValue("@IsAdmin", account.IsAdmin);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteAccount(int accountId)
        {
            using (var connection = new MySqlConnection(this.connectionString))
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
            using (var connection = new MySqlConnection(this.connectionString))
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
                        account.AccountNumber = Convert.ToInt32(reader["account_Number"]);
                        account.Login = reader["Login"].ToString();
                        account.Pin = reader["Pin"].ToString();
                        account.Name = reader["Name"].ToString();
                        account.Balance = Convert.ToInt32(reader["Balance"]);
                        account.Status = reader["Status"].ToString();
                        account.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                        return account;
                    }
                }
            }

            return null;
        }

        public void UpdateAccount(Account account)
        {
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Open();
                string query = "UPDATE accounts SET Login = @Login, Pin = @Pin, Name = @Name, Balance = @Balance, Status = @Status WHERE account_Number = @account_Number";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@account_Number", account.AccountNumber);
                command.Parameters.AddWithValue("@Login", account.Login);
                command.Parameters.AddWithValue("@Pin", account.Pin);
                command.Parameters.AddWithValue("@Name", account.Name);
                command.Parameters.AddWithValue("@Balance", account.Balance);
                command.Parameters.AddWithValue("@Status", account.Status);
                command.ExecuteNonQuery();
            }
        }

        public Account Login(string login, string pin)
        {
            using (var connection = new MySqlConnection(this.connectionString))
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
                        account.AccountNumber = Convert.ToInt32(reader["account_Number"]);
                        account.Login = reader["Login"].ToString();
                        account.Pin = reader["Pin"].ToString();
                        account.Name = reader["Name"].ToString();
                        account.Balance = Convert.ToInt32(reader["Balance"]);
                        account.Status = reader["Status"].ToString();
                        account.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                        return account;
                    }
                }
            }

            return null;
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            using (var connection = new MySqlConnection(this.connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM accounts";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Account account = new Account();
                        account.AccountNumber = Convert.ToInt32(reader["account_Number"]);
                        account.Login = reader["Login"].ToString();
                        account.Pin = reader["Pin"].ToString();
                        account.Name = reader["Name"].ToString();
                        account.Balance = Convert.ToInt32(reader["Balance"]);
                        account.Status = reader["Status"].ToString();
                        account.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
                        accounts.Add(account);
                    }
                }
            }

            return accounts;
        }
    }
}

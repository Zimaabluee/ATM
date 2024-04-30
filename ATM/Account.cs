// <copyright file="Account.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem
{
    using System;

    // 数据模型 - 表示一个账户
    public class Account
    {
        public int AccountNumber { get; set; }

        public string Login { get; set; }

        public string Pin { get; set; }

        public string Name { get; set; }

        public int Balance { get; set; }

        public string Status { get; set; }

        public DateTime Date { get; set; }

        public bool IsAdmin { get; set; }
    }
}

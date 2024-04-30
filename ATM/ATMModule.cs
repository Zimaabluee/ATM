// <copyright file="ATMModule.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem
{
    using Ninject;

    public class ATMModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            this.Bind<IAccountRepository>().To<AccountRepository>().WithConstructorArgument("connectionString", "server=localhost;database=ATMDatabase;uid=root;password=akram918;\r\n");
            this.Bind<AccountService>().ToSelf();
            this.Bind<ATMSystem>().ToSelf();
            this.Bind<IConsole>().To<MyConsole>();
        }
    }
}

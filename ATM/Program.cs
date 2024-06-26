// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ATMSystem
{
    using Ninject;

    public class Program
    {
        public static void Main(string[] args)
        {
            // 设置Ninject DI容器并获取 ATMSystem 实例
            var atmSystem = SetupDependencyInjection().Get
            <ATMSystem>();
            atmSystem.Run();
        }

       public static IKernel SetupDependencyInjection()
        {
            // 创建 Ninject DI 容器并加载模块
            return new StandardKernel(new ATMModule());
        }
    }
}

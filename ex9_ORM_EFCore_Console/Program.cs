using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ex9_ORM_EFCore_Console.Database;
using ex9_ORM_EFCore_Console.Models;
using Microsoft.EntityFrameworkCore;

namespace ex9_ORM_EFCore_Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InitConsole();
            await new Startup().Start();
            Console.Read();
        }

        private static void InitConsole()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }
    }
}

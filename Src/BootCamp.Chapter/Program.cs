using System;
using BootCamp.Chapter.Menu;

namespace BootCamp.Chapter
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new PasswordManager();
            menu.DisplayMenu();

            Console.Read();
        }
    }
}

using System;

namespace BootCamp.Chapter.Menu
{
    public class InteractiveMenu
    {
        public MenuOptions Build()
        {
            var options = (MenuOptions[]) Enum.GetValues(typeof(MenuOptions));
            var selectOptionNumber = 0;
            
            Console.CursorVisible = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to Password Manager.");
                Console.WriteLine("Select an option.");
                Console.WriteLine("==================================================");

                DrawSelection(options, selectOptionNumber);
                var keyPressed = Console.ReadKey(false).Key;
                if (keyPressed == ConsoleKey.Enter)
                {
                    return (MenuOptions) selectOptionNumber;
                }
                    
                selectOptionNumber = GetSelection(keyPressed, selectOptionNumber, options.Length);
            } while (true);
        }

        private int GetSelection(ConsoleKey key, int currentOption, int optionsLength)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow when optionsLength != 0:
                    currentOption--;
                    break;
                case ConsoleKey.UpArrow:
                    currentOption = optionsLength - 1;
                    break;
                case ConsoleKey.DownArrow when currentOption != optionsLength - 1:
                    currentOption++;
                    break;
                case ConsoleKey.DownArrow:
                    currentOption = 0;
                    break;
            }

            return currentOption;
        }

        private static void DrawSelection(MenuOptions[] options, int selectOptionNumber)
        {
            foreach (var option in options)
            {
                if (option == (MenuOptions) selectOptionNumber)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(option);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(option);
                }
            }
        }
    }
}
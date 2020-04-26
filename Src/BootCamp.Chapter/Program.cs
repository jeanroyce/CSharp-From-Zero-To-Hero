﻿using System;

namespace BootCamp.Chapter
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyMac();
            Console.WriteLine("=============================");
            AssemblyPc();
        }

        private static void AssemblyMac()
        {
            Console.WriteLine("[APPLE ROBOT SYSTEM]");
            var macFactory = new MacFactory();
            var newMac = macFactory.Assemble();
            Console.WriteLine("Assembly complete!");
        }
        
        private static void AssemblyPc()
        {
            Console.WriteLine("[MICROSOFT ROBOT SYSTEM]");
            var msFactory = new MsFactory();
            var newPc = msFactory.Assemble();
            Console.WriteLine("Assembly complete!");
        }
    }
}

using System;
using System.Text;

namespace StudentManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            MenuManager menu = new MenuManager();
            menu.Run();
        }
    }
}
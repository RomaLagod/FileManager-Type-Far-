using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //Розмір консолі
            Console.SetWindowSize(147, 40);

            //Назва програм
            Console.Title = "File Manager {FM alpha v.0.001}  (©)Roma Lahodniuk";

            //Запуск
            Manager manager = new Manager();
            manager.Run();            
        }
    }
}

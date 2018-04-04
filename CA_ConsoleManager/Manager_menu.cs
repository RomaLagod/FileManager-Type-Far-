using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    public class Manager_menu
    {
        //меню
        string[] menu =
        {
            " (F1) Help ",
            " (F2) Drive ",
            "           ",//(F3) View
            "           ",//(F4) Edit
            " (F5) Copy ",
            " (F6) RenMov ",
            " (F7) MakeFold ",
            " (F8) Delete ",
            " (F9) Sort ",
            " (F10) Quit ",
            " (F11) Mode "
        };

        //Показуємо меню нижнє
        public void Show()
        {
            for (int i = 0; i < menu.Length; i++)
            {
                Color.SetBackColor(ConsoleColor.DarkCyan);
                Console.Write(menu[i]);
                Color.SetDefaultBackColor();
                Console.Write(" ");
            }
            Color.SetDefault();
        }

        //Заголовки для таблиць (режим менеджера 1)
        public void Title_mode1()
        {
            Color.SetBackColor(ConsoleColor.DarkBlue);
            Color.SetTextColor(ConsoleColor.Yellow);

            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH - Panel_Design.CONSOLE_WIDTH / 2) / 4 - 2,2);
            Console.WriteLine("Name");
            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH - Panel_Design.CONSOLE_WIDTH / 2) / 2 
                                    + (Panel_Design.CONSOLE_WIDTH - Panel_Design.CONSOLE_WIDTH / 2) / 4 - 1, 2);
            Console.WriteLine("Name");
            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH / 2) 
                                    + (Panel_Design.CONSOLE_WIDTH - Panel_Design.CONSOLE_WIDTH / 2) / 4 - 1, 2);
            Console.WriteLine("Name");
            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH / 2)
                                    + (Panel_Design.CONSOLE_WIDTH - Panel_Design.CONSOLE_WIDTH / 2) / 2
                                    + (Panel_Design.CONSOLE_WIDTH - Panel_Design.CONSOLE_WIDTH / 2) / 4 - 1, 2);
            Console.WriteLine("Name");
            Color.SetDefault();
        }

        //Заголовки для таблиць (режим менеджера 2)
        public void Title_mode2()
        {
            Color.SetBackColor(ConsoleColor.DarkBlue);
            Color.SetTextColor(ConsoleColor.Yellow);

            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH - Panel_Design.CONSOLE_WIDTH / 2) / 4, 2);
            Console.WriteLine("Name");
            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH / 2) - 9  , 2);
            Console.WriteLine("Size");
            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH / 2) - 27, 2);
            Console.WriteLine("Date");
            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH / 2)
                                    + (Panel_Design.CONSOLE_WIDTH - Panel_Design.CONSOLE_WIDTH / 2) / 4, 2);
            Console.WriteLine("Name");
            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH) - 10, 2);
            Console.WriteLine("Size");
            Console.SetCursorPosition((Panel_Design.CONSOLE_WIDTH) - 28, 2);
            Console.WriteLine("Date");
            Color.SetDefault();
        }

        //Поточний каталог
        public void CurrentDir(string dir_left, string dir_right)
        {
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Color.SetTextColor(ConsoleColor.Green);

            Console.SetCursorPosition(0, 0);
            if (dir_left.Length > Panel_Design.CONSOLE_WIDTH / 2 - 2)
                dir_left = dir_left.Substring(0, Panel_Design.CONSOLE_WIDTH / 2 - 2 - 3) + "...";
            Console.WriteLine(dir_left);

            Console.SetCursorPosition(Panel_Design.CONSOLE_WIDTH / 2 + 2, 0);
            if (dir_right.Length > Panel_Design.CONSOLE_WIDTH / 2 - 2)
                dir_right = dir_right.Substring(0, Panel_Design.CONSOLE_WIDTH / 2 - 2 - 3) + "...";
            Console.WriteLine(dir_right);

            Color.SetDefault();
        }

    }
}

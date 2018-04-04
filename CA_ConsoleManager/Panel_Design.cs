using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    public class Panel_Design
    {
        //Константи консолі
        static public int CONSOLE_WIDTH = 145;// Console.WindowWidth; 
        static public int CONSOLE_HEIGHT = 38;// Console.WindowHeight - 2;

        //Меню менеджера
        Manager_menu manager_menu = new Manager_menu();

        //Завантажуємо панелі (відображаються тільки імена)
        public void Load_Panel_mode1()
        {
            Console.Clear();
            Color.SetBackColor(ConsoleColor.DarkCyan);

            for (int i = 0; i < CONSOLE_WIDTH; i++)
            {
                Console.Write(" ");
                if (i == CONSOLE_WIDTH / 2 || i == CONSOLE_WIDTH / 2 - 1)
                {
                    Color.SetBackColor(ConsoleColor.DarkBlue);
                }
                else Color.SetBackColor(ConsoleColor.DarkCyan);
            }
            Console.WriteLine();

            Color.SetBackColor(ConsoleColor.DarkBlue);
            Console.Write(Chars.symbol[0].ToString());
            for (int i = 0; i < CONSOLE_WIDTH - 2; i++)
            {
                if (i == (CONSOLE_WIDTH / 2) / 2 - 1) Console.Write(Chars.symbol[8].ToString());
                else if (i == (CONSOLE_WIDTH / 2) / 2 + (CONSOLE_WIDTH / 2)) Console.Write(Chars.symbol[8].ToString());
                else if (i == ((CONSOLE_WIDTH / 2) - 1)) Console.Write(Chars.symbol[2].ToString());
                else if (i == ((CONSOLE_WIDTH / 2) + 0)) Console.Write(Chars.symbol[0].ToString());
                else Console.Write(Chars.symbol[1].ToString());
            }
            Console.Write(Chars.symbol[2].ToString());

            Console.WriteLine();
            for (int i = 0; i < CONSOLE_HEIGHT - 2; i++)
            {
                for (int j = 0; j < CONSOLE_WIDTH ; j++)
                {
                    switch (j)
                    {
                        case int k when (CONSOLE_HEIGHT - 4 == i && j == 0):
                            Console.Write(Chars.symbol[9]);
                            break;
                        case int k when (CONSOLE_HEIGHT - 4 == i && (((CONSOLE_WIDTH + 2) / 2) - 1) == j):
                            Console.Write(Chars.symbol[11]);
                            break;
                        case int k when (CONSOLE_HEIGHT - 4 == i && (((CONSOLE_WIDTH + 2) / 2) + 0) == j):
                            Console.Write(Chars.symbol[9]);
                            break;
                        case int k when (CONSOLE_HEIGHT - 4 == i && (CONSOLE_WIDTH - 1) == j):
                            Console.Write(Chars.symbol[11]);
                            break;
                        case 0:
                            Console.Write(Chars.symbol[3]);
                            break;
                        case int k when ((((CONSOLE_WIDTH + 2) / 2) - 1) == j):
                            Console.Write(Chars.symbol[3]);
                            break;
                        case int k when ((((CONSOLE_WIDTH + 2) / 2) + 0) == j):
                            Console.Write(Chars.symbol[3]);
                            break;
                        case int k when ((CONSOLE_WIDTH - 1) == j):
                            Console.Write(Chars.symbol[3]);
                            break;
                        case int k when (CONSOLE_HEIGHT - 4 == i && ((CONSOLE_WIDTH / 2) / 2) == j):
                            Console.Write(Chars.symbol[12]);
                            break;
                        case int k when (CONSOLE_HEIGHT - 4 == i && ((CONSOLE_WIDTH / 2) / 2) + (CONSOLE_WIDTH / 2) + 1 == j):
                            Console.Write(Chars.symbol[12]);
                            break;
                        case int k when (((CONSOLE_WIDTH  / 2) / 2)  == j && CONSOLE_HEIGHT - 4 > i):
                            Console.Write(Chars.symbol[7]);
                            break;
                        case int k when (((CONSOLE_WIDTH / 2) / 2) + (CONSOLE_WIDTH / 2) + 1 == j && CONSOLE_HEIGHT - 4 > i):
                            Console.Write(Chars.symbol[7]);
                            break;
                        case int k when (CONSOLE_HEIGHT - 4 == i):
                            Console.Write(Chars.symbol[6]);
                            break;
                        default:
                            Console.Write(" ");
                            break;
                    }
                }
                Console.WriteLine();
            }

            Console.Write(Chars.symbol[4].ToString());
            for (int i = 0; i < CONSOLE_WIDTH - 2; i++)
            {
                if  (i == ((CONSOLE_WIDTH / 2) ) - 1) Console.Write(Chars.symbol[5].ToString());
                else if (i == ((CONSOLE_WIDTH / 2) + 0)) Console.Write(Chars.symbol[4].ToString());
                else Console.Write(Chars.symbol[1].ToString());
            }
            Console.Write(Chars.symbol[5].ToString());
            Color.SetDefault();

            Console.WriteLine();
            manager_menu.Show();
            manager_menu.Title_mode1();
            Console.SetCursorPosition(CONSOLE_WIDTH-1, CONSOLE_HEIGHT+1);
        }

        //Завантажуємо панелі (відображаються імена дата та розмір)
        public void Load_Panel_mode2()
        {
            Console.Clear();
            Color.SetBackColor(ConsoleColor.DarkCyan);

            for (int i = 0; i < CONSOLE_WIDTH; i++)
            {
                Console.Write(" ");
                if (i == CONSOLE_WIDTH / 2 || i == CONSOLE_WIDTH / 2 - 1)
                {
                    Color.SetBackColor(ConsoleColor.DarkBlue);
                }
                else Color.SetBackColor(ConsoleColor.DarkCyan);
            }
            Console.WriteLine();

            Color.SetBackColor(ConsoleColor.DarkBlue);

            Console.Write(Chars.symbol[0].ToString());
            for (int i = 0; i < CONSOLE_WIDTH - 2; i++)
            {
                if (i == ((CONSOLE_WIDTH / 2) - 16)) Console.Write(Chars.symbol[8].ToString());
                else if (i == ((CONSOLE_WIDTH / 2) - 37)) Console.Write(Chars.symbol[8].ToString());
                else if (i == (CONSOLE_WIDTH  - 17)) Console.Write(Chars.symbol[8].ToString());
                else if (i == (CONSOLE_WIDTH - 38)) Console.Write(Chars.symbol[8].ToString());
                else if (i == ((CONSOLE_WIDTH / 2) - 1)) Console.Write(Chars.symbol[2].ToString());
                else if (i == ((CONSOLE_WIDTH / 2) + 0)) Console.Write(Chars.symbol[0].ToString());
                else Console.Write(Chars.symbol[1].ToString());
            }
            Console.Write(Chars.symbol[2].ToString());

            Console.WriteLine();
            for (int i = 0; i < CONSOLE_HEIGHT - 2; i++)
            {
                for (int j = 0; j < CONSOLE_WIDTH; j++)
                {
                    switch (j)
                    {
                        case 0:
                            Console.Write(Chars.symbol[3]);
                            break;
                        case int k when ((((CONSOLE_WIDTH + 2) / 2) - 1) == j):
                            Console.Write(Chars.symbol[3]);
                            break;
                        case int k when ((((CONSOLE_WIDTH + 2) / 2) + 0) == j):
                            Console.Write(Chars.symbol[3]);
                            break;
                        case int k when ((CONSOLE_WIDTH - 1) == j):
                            Console.Write(Chars.symbol[3]);
                            break;
                        case int k when ((CONSOLE_WIDTH / 2) - 15 == j):
                            Console.Write(Chars.symbol[7]);
                            break;
                        case int k when ((CONSOLE_WIDTH / 2) - 36 == j):
                            Console.Write(Chars.symbol[7]);
                            break;
                        case int k when (CONSOLE_WIDTH - 16 == j):
                            Console.Write(Chars.symbol[7]);
                            break;
                        case int k when (CONSOLE_WIDTH - 37 == j):
                            Console.Write(Chars.symbol[7]);
                            break;
                        default:
                            Console.Write(" ");
                            break;
                    }
                }
                Console.WriteLine();
            }

            Console.Write(Chars.symbol[4].ToString());
            for (int i = 0; i < CONSOLE_WIDTH - 2; i++)
            {
                if (i == ((CONSOLE_WIDTH / 2) - 16)) Console.Write(Chars.symbol[10].ToString());
                else if (i == ((CONSOLE_WIDTH / 2) - 37)) Console.Write(Chars.symbol[10].ToString());
                else if (i == (CONSOLE_WIDTH - 17)) Console.Write(Chars.symbol[10].ToString());
                else if (i == (CONSOLE_WIDTH - 38)) Console.Write(Chars.symbol[10].ToString());
                else if (i == ((CONSOLE_WIDTH / 2) - 1)) Console.Write(Chars.symbol[5].ToString());
                else if (i == ((CONSOLE_WIDTH / 2) + 0)) Console.Write(Chars.symbol[4].ToString());
                else Console.Write(Chars.symbol[1].ToString());
            }
            Console.Write(Chars.symbol[5].ToString());
            Color.SetDefault();

            Console.WriteLine();
            manager_menu.Show();
            manager_menu.Title_mode2();
            Console.SetCursorPosition(CONSOLE_WIDTH-1, CONSOLE_HEIGHT+1);
        }

        //Виводить стрічку властивості для першого режиму (ліва закладка)
        public void Properties_mode1_leftpanel(string name, string prop )
        {
            Color.SetBackColor(ConsoleColor.DarkBlue);
            Console.SetCursorPosition(1, CONSOLE_HEIGHT - 1);

            Console.Write(name + prop);
            Color.SetDefault();
        }

        //Виводить стрічку властивості для першого режиму (права закладка)
        public void Properties_mode1_rightpanel(string name, string prop)
        {
            Color.SetBackColor(ConsoleColor.DarkBlue);
            Console.SetCursorPosition(CONSOLE_WIDTH / 2 + 2, CONSOLE_HEIGHT - 1);

            Console.Write(name + prop);
            Color.SetDefault();
        }
    }
}

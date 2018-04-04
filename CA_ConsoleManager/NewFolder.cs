using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    public class NewFolder : IDialog
    {
        //режим
        static int now_stativ = 1;

        //делегат та подія
        public delegate int WindowDinmationHandler();
        public event WindowDinmationHandler WindowSize_Height;
        public event WindowDinmationHandler WindowSize_Width;

        //Малює вікно для створення папки
        public void EmptyDialogShow(int start_x, int start_y)
        {
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Console.SetCursorPosition(start_x, start_y);
            Console.WriteLine("                                                               ");
            Console.SetCursorPosition(start_x, start_y + 1);
            Console.WriteLine(" " + Chars.symbol[0].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + "   Make Folder   " + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[2] + " ");
            Console.SetCursorPosition(start_x, start_y + 2);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                           " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 3);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                           " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 4);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                           " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 5);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                           " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 6);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                           " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 7);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                           " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 8);
            Console.WriteLine(" " + Chars.symbol[4].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[5] + " ");
            Console.SetCursorPosition(start_x, start_y + 9);
            Console.WriteLine("                                                               ");
            Color.SetDefault();
        }

        //Показує вікно для створення папки
        public int ShowMakeFolder_Window(out String FolderName)
        {
            ConsoleKeyInfo menu = new ConsoleKeyInfo();
            string[] copy_menu =
            {
                " { Yes } ",
                " [ No ] ",
                " [ Cancel ] "
            };

            //Початкові координати
            int start_x = 0;// (Panel_Design.CONSOLE_WIDTH / 2) - 31;
            int start_y = 0;// (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
            if (WindowSize_Width != null)
                start_x = WindowSize_Width();
            if (WindowSize_Height != null)
                start_y = WindowSize_Height();
            Console.SetCursorPosition(start_x, start_y);


            //Виводимо рамку для режиму
            EmptyDialogShow(start_x, start_y);
            //назва файлу
            Console.SetCursorPosition(start_x + 5, start_y + 3);
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Color.SetTextColor(ConsoleColor.Yellow);
            Console.Write("Folder name: ");
            Color.SetBackColor(ConsoleColor.Black);
            Color.SetDefaultTextColor();
            Console.Write("                                         ");
            //Показуємо курсор
            Console.CursorVisible = true;
            Console.SetCursorPosition(start_x + 5 + 13, start_y + 3);
            FolderName = Console.ReadLine();
            //Ховаємо курсор
            Console.CursorVisible = false;
            Color.SetDefault();

            start_x += 19;
            start_y += 6;

            int COUNTER = 0;

            while (menu.Key != ConsoleKey.Enter)//цикл для руху по меню.
            {
                switch (menu.Key)
                {
                    case (ConsoleKey.RightArrow):
                        {
                            COUNTER++;
                            if (COUNTER > 2) COUNTER = 0;
                            break;
                        }
                    case (ConsoleKey.LeftArrow):
                        {
                            COUNTER--;
                            if (COUNTER < 0) COUNTER = 2;
                            break;
                        }
                    case (ConsoleKey.Escape):
                        {
                            Color.SetDefault();
                            COUNTER = now_stativ;
                            return COUNTER;
                        }
                }

                Console.SetCursorPosition(start_x, start_y);

                for (int i = 0; i < 3; i++)//виділяє кольором вибір
                {
                    Color.SetBackColor(ConsoleColor.DarkCyan);

                    if (COUNTER == i)
                    {
                        Color.SetBackColor(ConsoleColor.Black);
                    }

                    Console.Write(copy_menu[i]);
                }
                Console.SetCursorPosition(start_x, start_y);

                menu = Console.ReadKey();
            }
            Color.SetDefault();
            now_stativ = COUNTER;
            return COUNTER;
        }

        //інтерфейс нереалізований
        int IDialog.ShowWindow(params object[] list)
        {
            throw new NotImplementedException();
        }
    }
}

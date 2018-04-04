using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    public class MessageBox
    {
        //Малює вікно повідомлення
        private void PanelQuitShow(int start_x, int start_y)
        {
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Console.SetCursorPosition(start_x, start_y);
            Console.WriteLine("                                                           ");
            Console.SetCursorPosition(start_x, start_y + 1);
            Console.WriteLine(" " + Chars.symbol[0].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + "   Message   " + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[2] + " ");
            Console.SetCursorPosition(start_x, start_y + 2);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 3);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 4);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 5);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 6);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 7);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 8);
            Console.WriteLine(" " + Chars.symbol[4].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[5] + " ");
            Console.SetCursorPosition(start_x, start_y + 9);
            Console.WriteLine("                                                           ");
            Color.SetDefault();
        }

        //Показує вікно повідомлення
        public void ShowMessage_Window(string message)
        {
            ConsoleKeyInfo menu = new ConsoleKeyInfo();
            string[] message_menu =
            {
                " { Ok } "
            };

            //Початкові координати
            int start_x = (Panel_Design.CONSOLE_WIDTH / 2) - 28;
            int start_y = (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
            Console.SetCursorPosition(start_x, start_y);

            //Виводимо рамку для режиму
            PanelQuitShow(start_x, start_y);

            //назва файлу
            Console.SetCursorPosition(start_x + 5, start_y + 3);
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Color.SetTextColor(ConsoleColor.DarkRed);
            if (message.Contains("\n"))
            {
                string msg1 = message.Substring(0, message.IndexOf("\n") - 1);
                string msg2 = message.Substring(message.IndexOf("\n") + 1);
                Console.SetCursorPosition(start_x + 2, start_y + 3);
                Console.WriteLine(msg1);
                Console.SetCursorPosition(start_x + 2, start_y + 4);
                Console.WriteLine(msg2);
            }
            else Console.WriteLine(message);
            Color.SetDefault();

            start_x += 26;
            start_y += 7;

            Console.SetCursorPosition(start_x, start_y);

            Color.SetBackColor(ConsoleColor.Black);
            Console.Write(message_menu[0]);
            Console.SetCursorPosition(start_x, start_y);

            menu = Console.ReadKey();

            Color.SetDefault();
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        //Вікно підтвердження
        //Малює вікно повідомлення
        private void PanelConfirmShow(int start_x, int start_y)
        {
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Console.SetCursorPosition(start_x, start_y);
            Console.WriteLine("                                                           ");
            Console.SetCursorPosition(start_x, start_y + 1);
            Console.WriteLine(" " + Chars.symbol[0].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + "   Confirm   " + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[2] + " ");
            Console.SetCursorPosition(start_x, start_y + 2);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 3);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 4);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 5);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 6);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 7);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 8);
            Console.WriteLine(" " + Chars.symbol[4].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[5] + " ");
            Console.SetCursorPosition(start_x, start_y + 9);
            Console.WriteLine("                                                           ");
            Color.SetDefault();
        }

        //Показує вікно підтвердження
        public int ShowConfirm_Window(string confirm_info)
        {
            ConsoleKeyInfo menu = new ConsoleKeyInfo();
            string[] confirm_menu =
            {
                " { Yes } ",
                " [ No ] "
            };

            //Початкові координати
            int start_x = (Panel_Design.CONSOLE_WIDTH / 2) - 28;
            int start_y = (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
            Console.SetCursorPosition(start_x, start_y);

            //Виводимо рамку для режиму
            PanelConfirmShow(start_x, start_y);

            //назва файлу
            Console.SetCursorPosition(start_x + 5, start_y + 3);
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Color.SetTextColor(ConsoleColor.DarkRed);
            if (confirm_info.Contains("\n"))
            {
                string msg1 = confirm_info.Substring(0, confirm_info.IndexOf("\n") - 1);
                string msg2 = confirm_info.Substring(confirm_info.IndexOf("\n") + 1);
                Console.SetCursorPosition(start_x + 2, start_y + 3);
                Console.WriteLine(msg1);
                Console.SetCursorPosition(start_x + 2, start_y + 4);
                Console.WriteLine(msg2);
            }
            else Console.WriteLine(confirm_info);
            Color.SetDefault();

            start_x += 20;
            start_y += 7;

            int COUNTER = 0;

            while (menu.Key != ConsoleKey.Enter)//цикл для руху по меню.
            {
                switch (menu.Key)
                {
                    case (ConsoleKey.RightArrow):
                        {
                            COUNTER++;
                            if (COUNTER > 1) COUNTER = 0;
                            break;
                        }
                    case (ConsoleKey.LeftArrow):
                        {
                            COUNTER--;
                            if (COUNTER < 0) COUNTER = 1;
                            break;
                        }
                    case (ConsoleKey.Escape):
                        {
                            Color.SetDefault();
                            return COUNTER;
                        }
                }

                Console.SetCursorPosition(start_x, start_y);

                for (int i = 0; i < 2; i++)//виділяє кольором вибір
                {
                    Color.SetBackColor(ConsoleColor.DarkCyan);

                    if (COUNTER == i)
                    {
                        Color.SetBackColor(ConsoleColor.Black);
                    }

                    Console.Write(confirm_menu[i]);
                }
                Console.SetCursorPosition(start_x, start_y);

                menu = Console.ReadKey();
            }
            Color.SetDefault();
            return COUNTER;
        }

    }
}

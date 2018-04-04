using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    public class Help
    {
        public void Show()
        {
            //Початкові координати
            int start_x = (Panel_Design.CONSOLE_WIDTH / 2) - 32;
            int start_y = 5;//(Panel_Design.CONSOLE_HEIGHT / 2) - 5;
            Console.SetCursorPosition(start_x, start_y);

            Color.SetBackColor(ConsoleColor.DarkCyan);
            Console.SetCursorPosition(start_x, start_y);
            Console.WriteLine("                                                                   ");
            Console.SetCursorPosition(start_x, start_y + 1);
            Console.WriteLine(" " + Chars.symbol[0].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ "   Help   " + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[2] + " ");
            Console.SetCursorPosition(start_x, start_y + 2);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " FILE MANAGER - is a program for managing files and directories" + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 3);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " in Windows operating systems. File Manager works in text mode " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 4);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " and provides a simple and intuitive interface for performing  " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 5);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " most of the necessary actions:                                " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 6);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [(F11) Mode] - First mode show only file names                " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 7);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                Second mode (file name, date, size ).          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 8);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [(F2) Drive] - change disk or device on the current tab.      " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 9);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [(F5) Copy] - copy selected file from current tab dir         " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 10);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               to another tab dir.                             " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 11);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [(F6) RenMov] - rename file or move file from current tab dir " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 12);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                 to another tab dir                            " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 13);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [(F7) MakeFold] - create new folder in current tab dir.       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 14);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [(F8) Delete] - delete selected file or folder in             " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 15);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                 current tab dir.                              " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 16);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [(F9) Sort] - sorting files and dir by name, size, date       " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 17);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [(F10) Quit] - Quit from program.                             " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 18);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [SPACE] - Show size of folder.                                " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 19);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [Alt + F1] - change disk or device on the left tab.           " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 20);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " [Alt + F2] - change disk or device on the right tab.          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 21);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                               " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 22);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + " {FM alpha v.0.001}  (©)Roma Lahodniuk                         " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 23);
            Console.WriteLine(" " + Chars.symbol[4].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[5] + " ");
            Console.SetCursorPosition(start_x, start_y + 24);
            Console.WriteLine("                                                                   ");
            Color.SetDefault();
            Console.ReadKey();
        }
    }
}

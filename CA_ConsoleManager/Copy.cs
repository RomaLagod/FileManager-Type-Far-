using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    public class Copy : IDialog
    {
        //режим
        static int now_stativ = 1;

        //Малює вікно для копіювання
        public void EmptyDialogShow(int start_x, int start_y)
        {
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Console.SetCursorPosition(start_x, start_y);
            Console.WriteLine("                                                        ");
            Console.SetCursorPosition(start_x, start_y + 1);
            Console.WriteLine(" " + Chars.symbol[0].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + "   Copy   " + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[2] + " ");
            Console.SetCursorPosition(start_x, start_y + 2);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                    " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 3);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                    " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 4);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                    " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 5);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                    " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 6);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                    " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 7);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                                                    " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 8);
            Console.WriteLine(" " + Chars.symbol[4].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[5] + " ");
            Console.SetCursorPosition(start_x, start_y + 9);
            Console.WriteLine("                                                        ");
            Color.SetDefault();
        }

        //Показує вікно для копіювання
        //public int ShowCopy_Window(string FileName,string From, string to)
        public int ShowWindow(params object [] list)
        {
            ConsoleKeyInfo menu = new ConsoleKeyInfo();
            string[] copy_menu =
            {
                " { Yes } ",
                " [ No ] ",
                " [ Cancel ] "
            };

            //Початкові координати
            int start_x = (Panel_Design.CONSOLE_WIDTH / 2) - 27;
            int start_y = (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
            Console.SetCursorPosition(start_x, start_y);


            //Виводимо рамку для режиму
            EmptyDialogShow(start_x, start_y);
            //назва файлу
            Console.SetCursorPosition(start_x + 5, start_y + 3);
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Color.SetTextColor(ConsoleColor.Yellow);
            string temp_Name = "{ " + /*FileName*/(string) list[0] + " }";
            temp_Name = temp_Name.PadLeft(24 + temp_Name.Length / 2);
            Console.WriteLine(temp_Name);
            Color.SetDefault();
            //From
            Console.SetCursorPosition(start_x + 5, start_y + 4);
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Color.SetTextColor(ConsoleColor.Gray);
            Console.WriteLine("From: "+ /*From*/(string) list[1]);
            Color.SetDefault();
            //To
            Console.SetCursorPosition(start_x + 5, start_y + 5);
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Color.SetTextColor(ConsoleColor.Gray);
            Console.WriteLine("To:   " + /*to*/(string) list[2]);
            Color.SetDefault();

            start_x += 15;
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

        // Copy from the current directory, include subdirectories.
        //https://msdn.microsoft.com/en-us/library/bb762914%28v=vs.100%29.aspx?f=255&MSPPError=-2147217396

        static public void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    // Create the subdirectory.
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    // Copy the subdirectories.
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}

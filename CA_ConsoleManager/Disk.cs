using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CA_ConsoleManager
{
    public class Disk : IDialog
    {
        static int device = 0;

        //делегат та подія
        public delegate int WindowDinmationHandler();
        public event WindowDinmationHandler WindowSize_Height;
        public event WindowDinmationHandler WindowSize_Width;


        //малює вікно для вибору дисків
        public void EmptyDialogShow(int start_x, int start_y)
        {
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Console.SetCursorPosition(start_x, start_y);
            Console.WriteLine("                                         ");
            Console.SetCursorPosition(start_x, start_y + 1);
            Console.WriteLine(" " + Chars.symbol[0].ToString() + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + "   Drive   " + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1]+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[2] + " ");
            Console.SetCursorPosition(start_x, start_y + 2);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 3);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 4);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 5);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 6);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 7);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 8);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 9);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 10);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 11);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "               " + Chars.symbol[7] + "          " + Chars.symbol[7] + "          " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 12);
            Console.WriteLine(" " + Chars.symbol[4].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[10] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[10] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[5] + " ");
            Console.SetCursorPosition(start_x, start_y + 13);
            Console.WriteLine("                                         ");
            Color.SetDefault();
        }

        //Показує вікно вибору дисків в залежності від вибраної панелі
        //public int ShowWindow(object obj)
        public int ShowWindow(params object[] list)
        {
            int tab = (int)list[0];
            ConsoleKeyInfo menu = new ConsoleKeyInfo();
            DriveInfo[] drives = DriveInfo.GetDrives();

            List<string> drive_name = new List<string>(); //назва пристрою
            List<string> drive_TotalSize = new List<string>(); //обєм пристрою
            List<string> drive_FreeSpace = new List<string>(); //вільне місце

            long TotalSize_GB = 0;
            long FreeSpace_GB = 0;


            foreach (DriveInfo drive in drives)
            {
                drive_name.Add(" "+drive.Name+"  ["+drive.DriveType+"] ");

                if (drive.IsReady)
                {
                    TotalSize_GB = drive.TotalSize / Size.GB;
                    FreeSpace_GB = drive.TotalFreeSpace / Size.GB;
                }
                else
                {
                    TotalSize_GB = -1;
                    FreeSpace_GB = -1;
                }

                if (TotalSize_GB > 0) drive_TotalSize.Add(" "+TotalSize_GB.ToString() + " G ");
                else drive_TotalSize.Add("");
                if (FreeSpace_GB > 0) drive_FreeSpace.Add(" "+FreeSpace_GB.ToString() + " G ");
                else drive_FreeSpace.Add("");
            }

            //Початкові координати
            int start_x = 0;
            int start_y = 0;

            if (tab == 0)
            {
                start_x = 5;
                if (WindowSize_Height != null)
                    start_y = WindowSize_Height();
                Console.SetCursorPosition(start_x, start_y);
            }

            if (tab == 1)
            {
                if (WindowSize_Width != null)
                    start_x = WindowSize_Width();
                if (WindowSize_Height != null)
                    start_y = WindowSize_Height();

                Console.SetCursorPosition(start_x, start_y);
            }

            //Виводимо рамку для вікна вибору дисків
            EmptyDialogShow(start_x, start_y);
            start_x += 3;
            start_y += 2;

            int COUNTER = 0;

            while (menu.Key != ConsoleKey.Enter)//цикл для руху по меню.
            {
                switch (menu.Key)
                {
                    case (ConsoleKey.DownArrow):
                        {
                            COUNTER++;
                            if (COUNTER > drive_name.Count-1) COUNTER = 0;
                            break;
                        }
                    case (ConsoleKey.UpArrow):
                        {
                            COUNTER--;
                            if (COUNTER < 0) COUNTER = drive_name.Count - 1;
                            break;
                        }
                    case (ConsoleKey.Escape):
                        {                            
                            Color.SetDefault();
                            return -1;
                            //break;
                        }
                }

                Console.SetCursorPosition(start_x, start_y);

                for (int i = 0; i < drive_name.Count; i++)//виділяє кольором вибір
                {
                    Color.SetBackColor(ConsoleColor.DarkCyan);

                    if (COUNTER == i)
                    {
                        Color.SetBackColor(ConsoleColor.Black);
                    }

                    Console.SetCursorPosition(start_x, start_y + i);
                    Console.WriteLine(drive_name[i]);
                    Console.SetCursorPosition(start_x + 17, start_y + i);
                    Console.WriteLine(drive_TotalSize[i]);
                    Console.SetCursorPosition(start_x + 28, start_y + i);
                    Console.WriteLine(drive_FreeSpace[i]);
                }
                Console.SetCursorPosition(start_x, start_y);

                menu = Console.ReadKey();
            }
            Color.SetDefault();
            device = COUNTER;
            return COUNTER;
        }
    }   
}

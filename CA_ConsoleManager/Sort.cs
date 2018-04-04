using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    public class Sort : IDialog
    {
        static int sorting = 0;

        //малює вікно для сортування
        public void EmptyDialogShow(int start_x, int start_y)
        {
            Color.SetBackColor(ConsoleColor.DarkCyan);
            Console.SetCursorPosition(start_x, start_y);
            Console.WriteLine("                         ");
            Console.SetCursorPosition(start_x, start_y + 1);
            Console.WriteLine(" " + Chars.symbol[0].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + "   Sort by   "+ Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[2] + " ");
            Console.SetCursorPosition(start_x, start_y + 2);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                     " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 3);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                     " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 4);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                     " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 5);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                     " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 6);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                     " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 7);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                     " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 8);
            Console.WriteLine(" " + Chars.symbol[3].ToString() + "                     " + Chars.symbol[3] + " ");
            Console.SetCursorPosition(start_x, start_y + 9);
            Console.WriteLine(" " + Chars.symbol[4].ToString() + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[1] + Chars.symbol[5] + " ");
            Console.SetCursorPosition(start_x, start_y + 10);
            Console.WriteLine("                         ");
            Color.SetDefault();
        }

        //Показує вікно сортування в залежності від вибраної панелі
        //public int ShowSort_Window(int tab)
        public int ShowWindow(params object [] list)
        {
            int tab = (int)list[0];
            ConsoleKeyInfo menu = new ConsoleKeyInfo();
            string[] sort_menu =
            {
                " Name - " + Chars.symbol[14] + "   ",
                " Name - " + Chars.symbol[13] + "   ",
                " Size - " + Chars.symbol[14] + "   ",
                " Size - " + Chars.symbol[13] + "   ",
                " Date - " + Chars.symbol[14] + "   ",
                " Date - " + Chars.symbol[13] + "   "
            };

            //Початкові координати
            int start_x = 0;
            int start_y = 0;

            if (tab == 0)
            {
                start_x = 5;
                start_y = Panel_Design.CONSOLE_HEIGHT / 4;
                Console.SetCursorPosition(start_x, start_y);
            }

            if (tab == 1)
            {
                start_x = (Panel_Design.CONSOLE_WIDTH / 2) + 5;
                start_y = Panel_Design.CONSOLE_HEIGHT / 4;
                Console.SetCursorPosition(start_x, start_y);
            }

            //Виводимо рамку для меню сортування
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
                            if (COUNTER > 5) COUNTER = 0;
                            break;
                        }
                    case (ConsoleKey.UpArrow):
                        {
                            COUNTER--;
                            if (COUNTER < 0) COUNTER = 5;
                            break;
                        }
                    case (ConsoleKey.Escape):
                        {
                            COUNTER = sorting;
                            Color.SetDefault();
                            return COUNTER;
                            //break;
                        }
                }

                Console.SetCursorPosition(start_x, start_y);

                for (int i = 0; i < 6; i++)//виділяє кольором вибір
                {
                    Color.SetBackColor(ConsoleColor.DarkCyan);

                    if (COUNTER == i)
                    {
                        Color.SetBackColor(ConsoleColor.Black);
                    }

                    Console.SetCursorPosition(start_x, start_y + i);
                    Console.WriteLine(sort_menu[i]);
                }
                Console.SetCursorPosition(start_x, start_y);

                menu = Console.ReadKey();
            }
            Color.SetDefault();
            sorting = COUNTER;
            return COUNTER;
        }

        //сортування в залежності від вибраного типу
        public void Run(ref string[] array, int SortType, string ParamName)
        {
            if (ParamName == "File")
            {
                switch (SortType)
                {
                    case 0: //По імені за зростанням
                        Array.Sort(array, new SortByNameAsc());
                        break;
                    case 1: //по імені за спаданням
                        Array.Sort(array, new SortByNameDesc());
                        break;
                    case 2: //по розміру за зростанням
                        Array.Sort(array, new SortBySizeAsc());
                        break;
                    case 3: //по розміру за спаданням
                        Array.Sort(array, new SortBySizeDesc());
                        break;
                    case 4: //по даті за зростанням
                        Array.Sort(array, new SortByFileDateAsc());
                        break;
                    case 5: //по даті за спаданям
                        Array.Sort(array, new SortByFileDateDesc());
                        break;
                }
            }

            if (ParamName == "Directory")
            {
                switch (SortType)
                {
                    case 0: //По імені за зростанням
                        Array.Sort(array, new SortByNameAsc());
                        break;
                    case 1: //по імені за спаданням
                        Array.Sort(array, new SortByNameDesc());
                        break;
                    case 2: //по розміру за зростанням
                        //(папки сортуємо по імені)
                        Array.Sort(array, new SortByNameAsc());
                        break;
                    case 3: //по розміру за спаданням
                        //(папки сортуємо по імені)
                        Array.Sort(array, new SortByNameAsc());
                        break;
                    case 4: //по даті за зростанням
                        Array.Sort(array, new SortByDirDateAsc());
                        break;
                    case 5: //по даті за спаданям
                        Array.Sort(array, new SortByDirDateDesc());
                        break;
                }
            }
        }
    }

    //сортування по імені за зростанням (для файлів та папок)
    class SortByNameAsc : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is String && y is String)
            {
                return string.Compare(Path.GetFileName(x as String), Path.GetFileName(y as String));
            }
            throw new NotImplementedException();
        }
    }

    //сортування по імені за спаданням (для файлів та папок)
    class SortByNameDesc : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is String && y is String)
            {
                return string.Compare(Path.GetFileName(y as String), Path.GetFileName(x as String));
            }
            throw new NotImplementedException();
        }
    }

    //сортування по розміру за зростанням (для файлів)
    class SortBySizeAsc : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is String && y is String)
            {
                if (new FileInfo(x as String).Length > new FileInfo(y as String).Length)
                    return 1;
                if (new FileInfo(x as String).Length < new FileInfo(y as String).Length)
                    return -1;
                else return 0;
            }
            throw new NotImplementedException();
        }
    }

    //сортування по розміру за спаданням (для файлів)
    class SortBySizeDesc : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is String && y is String)
            {
                if (new FileInfo(x as String).Length < new FileInfo(y as String).Length)
                    return 1;
                if (new FileInfo(x as String).Length > new FileInfo(y as String).Length)
                    return -1;
                else return 0;
            }
            throw new NotImplementedException();
        }
    }

    //сортування по даті створення за зростанням (для файлів)
    class SortByFileDateAsc : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is String && y is String)
            {
                if (File.GetCreationTime(x as String) > File.GetCreationTime(y as String))
                    return 1;
                if (File.GetCreationTime(x as String) < File.GetCreationTime(y as String))
                    return -1;
                else return 0;
            }
            throw new NotImplementedException();
        }
    }

    //сортування по даті створення за спаданням (для файлів)
    class SortByFileDateDesc : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is String && y is String)
            {
                if (File.GetCreationTime(x as String) < File.GetCreationTime(y as String))
                    return 1;
                if (File.GetCreationTime(x as String) > File.GetCreationTime(y as String))
                    return -1;
                else return 0;
            }
            throw new NotImplementedException();
        }
    }

    //сортування по даті створення за зростанням (для папок)
    class SortByDirDateAsc : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is String && y is String)
            {
                if (Directory.GetCreationTime(x as String) > Directory.GetCreationTime(y as String))
                    return 1;
                if (Directory.GetCreationTime(x as String) < Directory.GetCreationTime(y as String))
                    return -1;
                else return 0;
            }
            throw new NotImplementedException();
        }
    }

    //сортування по даті створення за спаданням (для папок)
    class SortByDirDateDesc : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is String && y is String)
            {
                if (Directory.GetCreationTime(x as String) < Directory.GetCreationTime(y as String))
                    return 1;
                if (Directory.GetCreationTime(x as String) > Directory.GetCreationTime(y as String))
                    return -1;
                else return 0;
            }
            throw new NotImplementedException();
        }
    }
}

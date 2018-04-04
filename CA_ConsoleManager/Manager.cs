using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CA_ConsoleManager
{
    public class Manager
    {
        //Режими менеджера
        enum eMode
        {
            First, //тільки назви файлів
            Second //назви файлів, дата, розмір
        }

        //Закладки
        enum eTab
        {
            Left,
            Right
        }

        //Активна закладка
        private eTab ActiveTab = eTab.Left;

        //Активний режим відображення
        private eMode ManagerMode = eMode.First;

        //відображаємо панелі
        private Panel_Design panel_Design = new Panel_Design();
        //для відображення поточного каталогу в закладках
        private Manager_menu manager_Menu = new Manager_menu();

        //Директорії
        string[] dir_left;
        string[] dir_right;

        //Файли
        string[] files_left;
        string[] files_right;

        //для показу (файли та папки)
        string[] dir_file_left_show;
        string[] dir_file_right_show;

        //Дата
        string[] date_left;
        string[] date_right;

        //Розмір
        string[] size_left;
        string[] size_right;

        //Диски
        DriveInfo[] drives;
        bool drive_left = true;
        bool drive_right = true;

        //Позиція вибору
        int _counter_left = 0;
        int _counter_right = 0;

        //Поточна директорія
        string _current_dir_left = "";
        string _current_dir_right = "";

        //Попередня директорія
        string _prev_dir_left = "";
        string _prev_dir_right = "";

        //сторінка для відображення
        int page_left = 0;
        int page_right = 0;

        //змінна для розміру папки
        string FolderSize_left = "Folder ";
        string FolderSize_right = "Folder ";
        int _position_folder_mode2_left = -1;
        int _position_folder_mode2_right = -1;

        //сортування
        enum eSorting
        {
            NameAsc,    //за імям файлу за зростанням
            NameDesc,   //за імям файлу за спаданням
            SizeAsc,    //за розміром за зростанням
            SizeDesc,   //за розміром за спаданням
            DateAsc,    //за датою за зростанням
            DateDesc    //за датою за спаданням
        }
        eSorting eSortType_left = eSorting.NameAsc; //початкове значення сортування
        eSorting eSortType_right = eSorting.NameAsc; //початкове значення сортування



        //Паревіряє режими та завантажує відповідне вікно
        private void ModeReload()
        {
            //Якщо менеджер файлів працює в першому режимі
            if (ManagerMode == eMode.First)
            {
                panel_Design.Load_Panel_mode1();
            }

            //Якщо менеджер працює в другому режимі
            if (ManagerMode == eMode.Second)
            {
                panel_Design.Load_Panel_mode2();
            }
        }

        //Запускає програму (точка входу)
        public void Run()
        {
            //Ховаємо курсор
            Console.CursorVisible = false;

            //натиснута клавіша
            ConsoleKeyInfo choise = new ConsoleKeyInfo();

            //цикл
            bool work = true;

            while (work)
            {
                //оновлення розмірів вікна
                int buf_console_height = Panel_Design.CONSOLE_HEIGHT;
                int buf_console_width = Panel_Design.CONSOLE_WIDTH;
                Panel_Design.CONSOLE_HEIGHT = Console.WindowHeight - 2;
                Panel_Design.CONSOLE_WIDTH = Console.WindowWidth;
                if (buf_console_height != Panel_Design.CONSOLE_HEIGHT || buf_console_width != Panel_Design.CONSOLE_WIDTH)
                {
                    page_left = 0;
                    _counter_left = 0;
                    page_right = 0;
                    _counter_right = 0;
                }
                ModeReload();
                manager_Menu.CurrentDir(_current_dir_left, _current_dir_right);

                //Початкові координати в менеджері для лівої закладки
                int start_x_left = 1;
                int start_y_left = 3;
                //Початкові координати в менеджері для лівої закладки
                int start_x_right = Panel_Design.CONSOLE_WIDTH/2 + 2;
                int start_y_right = 3;

                /*--------------------------------------------------------------------------------------------------------------------------------------------*/
                //Ширина поля виведення
                int col_width_name = 0;
                int col_width_date = 0;
                int col_width_size = 0;
                if (ManagerMode == eMode.First)
                {
                    col_width_name = Panel_Design.CONSOLE_WIDTH / 4 - 1;
                }
                if (ManagerMode == eMode.Second)
                {
                    col_width_name = Panel_Design.CONSOLE_WIDTH / 2 - 37 - 1;
                    col_width_date = 20;
                    col_width_size = 14;
                }
                /*--------------------------------------------------------------------------------------------------------------------------------------------*/
                //Висота поля
                int col_hight_mode1 = Panel_Design.CONSOLE_HEIGHT - 2 - 3;
                int col_hight_mode2 = Panel_Design.CONSOLE_HEIGHT - 2 - 1;

                /*--------------------------------------------------------------------------------------------------------------------------------------------*/
                //Робота з дисками
                drives = DriveInfo.GetDrives();

                if (drive_left)
                {
                    for (int i = 0; i < drives.Length; i++)
                    {
                        Color.SetBackColor(ConsoleColor.DarkBlue);
                        if (ActiveTab == eTab.Left) if (i == _counter_left) Color.SetBackColor(ConsoleColor.DarkYellow);
                        Console.SetCursorPosition(start_x_left, start_y_left + i);
                        string temp = drives[i].Name;
                        if (temp.Length < col_width_name)
                        {
                            temp = temp.PadRight(col_width_name, ' ');                            
                        }
                        Console.WriteLine(temp);
                        Color.SetDefault();
                    }
                }

                if (drive_right)
                {
                    for (int i = 0; i < drives.Length; i++)
                    {
                        Color.SetBackColor(ConsoleColor.DarkBlue);
                        if (ActiveTab == eTab.Right) if (i == _counter_right) Color.SetBackColor(ConsoleColor.DarkYellow);
                        Console.SetCursorPosition(start_x_right, start_y_right + i);
                        string temp = drives[i].Name;
                        if (temp.Length < col_width_name)
                        {
                            temp = temp.PadRight(col_width_name, ' ');
                        }
                        Console.WriteLine(temp);
                        Color.SetDefault();
                    }
                }
                /*--------------------------------------------------------------------------------------------------------------------------------------------*/
                //Папки та файли
                if (!drive_left)
                {
                    if (Directory.Exists(_current_dir_left))
                    {
                        dir_left = Directory.GetDirectories(_current_dir_left);
                        files_left = Directory.GetFiles(_current_dir_left);

                        //Сортування !!!-------------------------------------------------------------------------------------------------------------
                        Sort sort = new Sort();
                        sort.Run(ref dir_left, (int)eSortType_left, "Directory");
                        sort.Run(ref files_left, (int)eSortType_left, "File");
                        //Сортування !!!-------------------------------------------------------------------------------------------------------------

                        dir_file_left_show = new string[dir_left.Length + files_left.Length + 1];
                        date_left = new string[dir_left.Length + files_left.Length + 1];
                        size_left = new string[dir_left.Length + files_left.Length + 1];

                        dir_file_left_show[0] = "..";
                        dir_file_left_show[0] = dir_file_left_show[0].PadRight(col_width_name);
                        date_left[0] = "";
                        size_left[0] = "";

                        for (int i = 1; i < dir_file_left_show.Length; i++)
                        {
                            if (i < dir_left.Length + 1)
                            {
                                DirectoryInfo dir_info = new DirectoryInfo(dir_left[i - 1]);
                                dir_file_left_show[i] = dir_info.Name;

                                date_left[i] = "     " + dir_info.CreationTime.ToShortDateString();
                                date_left[i] = date_left[i].PadRight(col_width_date);
                                size_left[i] = "   >Folder<";
                                size_left[i] = size_left[i].PadRight(col_width_size);

                                if (dir_file_left_show[i].Length < col_width_name)
                                {
                                    dir_file_left_show[i] = dir_file_left_show[i].PadRight(col_width_name);
                                }
                                else
                                {
                                    dir_file_left_show[i] = dir_file_left_show[i].Substring(0, col_width_name - 3);
                                    dir_file_left_show[i] += "...";
                                }
                            }
                            else
                            {
                                DirectoryInfo file_info = new DirectoryInfo(files_left[i - dir_left.Length - 1]);
                                dir_file_left_show[i] = file_info.Name;

                                date_left[i] = "     " + file_info.CreationTime.ToShortDateString();
                                date_left[i] = date_left[i].PadRight(col_width_date);

                                //Мучим розмір файлу---------------------------------------------------------
                                FileInfo fileInf_size = new FileInfo(files_left[i - dir_left.Length - 1]);
                                double size_temp = 0.0;
                                //байти                               
                                if (fileInf_size.Length < Size.KB)
                                {
                                    size_left[i] = "  " + (fileInf_size.Length).ToString() + " B";
                                }
                                //кілобайти
                                else if (fileInf_size.Length >= Size.KB && fileInf_size.Length < Size.MB)
                                {
                                    size_temp = (double) fileInf_size.Length / Size.KB;
                                    size_left[i] = String.Format("  {0:N2} Kb", size_temp);
                                }
                                //мегабайти
                                else if (fileInf_size.Length >= Size.MB && fileInf_size.Length < Size.GB)
                                {
                                    size_temp = (double) fileInf_size.Length / Size.MB;
                                    size_left[i] = String.Format("  {0:N2} Mb", size_temp);
                                }
                                //гігабайти
                                else if (fileInf_size.Length >= Size.GB)
                                {
                                    size_temp = (double) fileInf_size.Length / Size.GB;
                                    size_left[i] = String.Format("  {0:N2} Gb", size_temp);
                                }
                                //допилюємо пробіли
                                size_left[i] = size_left[i].PadRight(col_width_size);

                                if (dir_file_left_show[i].Length < col_width_name)
                                {
                                    dir_file_left_show[i] = dir_file_left_show[i].PadRight(col_width_name);
                                }
                                else
                                {
                                    string ext = dir_file_left_show[i].Substring(dir_file_left_show[i].Length - 3, 3);
                                    dir_file_left_show[i] = dir_file_left_show[i].Substring(0, col_width_name - 6);
                                    dir_file_left_show[i] += "..." + ext;
                                }
                            }
                        }

                        if (ManagerMode == eMode.First)
                        {
                            for (int i = 0 + page_left * (col_hight_mode1 * 2 - 1) - page_left; i < dir_file_left_show.Length ; i++)
                            {
                                Color.SetBackColor(ConsoleColor.DarkBlue);
                                if (ActiveTab == eTab.Left) if (i - (page_left * (col_hight_mode1 * 2 - 1) - page_left) == _counter_left) Color.SetBackColor(ConsoleColor.DarkYellow);
                                if (i - (page_left * (col_hight_mode1 * 2 - 1) - page_left) == 0)
                                {
                                    Console.SetCursorPosition(start_x_left , start_y_left + i - (page_left * (col_hight_mode1 * 2 - 1) - page_left));
                                    string temp = "..";
                                    temp = temp.PadRight(col_width_name);
                                    Console.WriteLine(temp);
                                }
                                else if (i - (page_left * (col_hight_mode1 * 2 - 1) - page_left) == col_hight_mode1 * 2 - 1)
                                {
                                    Console.SetCursorPosition(start_x_left + col_width_name + 1, start_y_left + i - col_hight_mode1 - (page_left * (col_hight_mode1 * 2 - 1) - page_left));
                                    string temp = "...";
                                    temp = temp.PadRight(col_width_name);
                                    Console.WriteLine(temp);
                                    Color.SetDefault();
                                    break;
                                }
                                else if (i - (page_left * (col_hight_mode1 * 2 - 1) - page_left) < col_hight_mode1)
                                {
                                    Console.SetCursorPosition(start_x_left, start_y_left + i - (page_left * (col_hight_mode1 * 2 - 1)-page_left));
                                    Console.WriteLine(dir_file_left_show[i]);
                                }
                                
                                else 
                                {
                                    Console.SetCursorPosition(start_x_left + col_width_name + 1, start_y_left + i - col_hight_mode1 - (page_left * (col_hight_mode1 * 2 - 1)-page_left));
                                    Console.WriteLine(dir_file_left_show[i]);
                                }
                                    Color.SetDefault();
                            }
                        }

                        if (ManagerMode == eMode.Second)
                        {
                            for (int i = 0 + page_left * (col_hight_mode2 - 1) - page_left; i < dir_file_left_show.Length; i++)
                            {
                                Color.SetBackColor(ConsoleColor.DarkBlue);
                                if (ActiveTab == eTab.Left) if (i - (page_left * (col_hight_mode2 - 1) - page_left) == _counter_left) Color.SetBackColor(ConsoleColor.DarkYellow);
                                if (i - (page_left * (col_hight_mode2 - 1) - page_left) == 0)
                                {
                                    Console.SetCursorPosition(start_x_left, start_y_left + i - (page_left * (col_hight_mode2 - 1) - page_left));
                                    string temp = "..";
                                    temp = temp.PadRight(col_width_name);
                                    Console.WriteLine(temp);
                                }
                                else if (i - (page_left * (col_hight_mode2 - 1) - page_left) == col_hight_mode2 - 1)
                                {
                                    Console.SetCursorPosition(start_x_left, start_y_left + i - (page_left * (col_hight_mode2 - 1) - page_left));
                                    string temp = "...";
                                    temp = temp.PadRight(col_width_name);
                                    Console.WriteLine(temp);
                                    Color.SetDefault();
                                    break;
                                }
                                else if (i - (page_left * (col_hight_mode2 - 1) - page_left) < col_hight_mode2)
                                {
                                    Console.SetCursorPosition(start_x_left, start_y_left + i - (page_left * (col_hight_mode2 - 1) - page_left));
                                    Console.WriteLine(dir_file_left_show[i]);
                                    Console.SetCursorPosition(start_x_left + col_width_name + 2, start_y_left + i - (page_left * (col_hight_mode2 - 1) - page_left));
                                    Console.WriteLine(date_left[i]);
                                    Console.SetCursorPosition(start_x_left + col_width_name + col_width_date + 3, start_y_left + i - (page_left * (col_hight_mode2 - 1) - page_left));
                                    if (i == _position_folder_mode2_left)
                                    {
                                        Console.WriteLine(FolderSize_left);
                                        _position_folder_mode2_left = -1;
                                        FolderSize_left = "Folder ";
                                    }
                                    else Console.WriteLine(size_left[i]);
                                }
                                else
                                {
                                    Console.SetCursorPosition(start_x_left + col_width_name + 1, start_y_left + i - col_hight_mode2 - (page_left * (col_hight_mode2 - 1) - page_left));
                                    Console.WriteLine(dir_file_left_show[i]);
                                }
                                Color.SetDefault();
                            }
                        }
                    }
                    else
                    {
                        MessageBox massageBox = new MessageBox();
                        massageBox.ShowMessage_Window("Current dirictory unavailable!");
                        _current_dir_left = "";
                        _prev_dir_left = "";
                        page_left = 0;
                        _counter_left = 0;
                        drive_left = true;
                        continue;
                    }
                }

                if (!drive_right)
                {
                    if (Directory.Exists(_current_dir_right))
                    {
                        dir_right = Directory.GetDirectories(_current_dir_right);
                        files_right = Directory.GetFiles(_current_dir_right);

                        //Сортування !!!-------------------------------------------------------------------------------------------------------------
                        Sort sort = new Sort();
                        sort.Run(ref dir_right, (int)eSortType_right, "Directory");
                        sort.Run(ref files_right, (int)eSortType_right, "File");
                        //Сортування !!!-------------------------------------------------------------------------------------------------------------

                        dir_file_right_show = new string[dir_right.Length + files_right.Length + 1];
                        date_right = new string[dir_right.Length + files_right.Length + 1];
                        size_right = new string[dir_right.Length + files_right.Length + 1];

                        dir_file_right_show[0] = "..";
                        dir_file_right_show[0] = dir_file_right_show[0].PadRight(col_width_name);
                        date_right[0] = "";
                        size_right[0] = "";

                        for (int i = 1; i < dir_file_right_show.Length; i++)
                        {
                            if (i < dir_right.Length + 1)
                            {
                                DirectoryInfo dir_info = new DirectoryInfo(dir_right[i - 1]);
                                dir_file_right_show[i] = dir_info.Name;

                                date_right[i] = "     " + dir_info.CreationTime.ToShortDateString();
                                date_right[i] = date_right[i].PadRight(col_width_date);
                                size_right[i] = "   >Folder<";
                                size_right[i] = size_right[i].PadRight(col_width_size);

                                if (dir_file_right_show[i].Length < col_width_name)
                                {
                                    dir_file_right_show[i] = dir_file_right_show[i].PadRight(col_width_name);
                                }
                                else
                                {
                                    dir_file_right_show[i] = dir_file_right_show[i].Substring(0, col_width_name - 3);
                                    dir_file_right_show[i] += "...";
                                }
                            }
                            else
                            {
                                DirectoryInfo file_info = new DirectoryInfo(files_right[i - dir_right.Length - 1]);
                                dir_file_right_show[i] = file_info.Name;

                                date_right[i] = "     " + file_info.CreationTime.ToShortDateString();
                                date_right[i] = date_right[i].PadRight(col_width_date);

                                //мучимо файли
                                FileInfo fileInf_size = new FileInfo(files_right[i - dir_right.Length - 1]);
                                double size_temp = 0.0;
                                //байти                               
                                if (fileInf_size.Length < Size.KB)
                                {
                                    size_right[i] = "  " + (fileInf_size.Length).ToString() + " B";
                                }
                                //кілобайти
                                else if (fileInf_size.Length >= Size.KB && fileInf_size.Length < Size.MB)
                                {
                                    size_temp = (double)fileInf_size.Length / Size.KB;
                                    size_right[i] = String.Format("  {0:N2} Kb", size_temp);
                                }
                                //мегабайти
                                else if (fileInf_size.Length >= Size.MB && fileInf_size.Length < Size.GB)
                                {
                                    size_temp = (double)fileInf_size.Length / Size.MB;
                                    size_right[i] = String.Format("  {0:N2} Mb", size_temp);
                                }
                                //гігабайти
                                else if (fileInf_size.Length >= Size.GB)
                                {
                                    size_temp = (double)fileInf_size.Length / Size.GB;
                                    size_right[i] = String.Format("  {0:N2} Gb", size_temp);
                                }
                                //допилюємо пробіли
                                size_right[i] = size_right[i].PadRight(col_width_size);

                                if (dir_file_right_show[i].Length < col_width_name)
                                {
                                    dir_file_right_show[i] = dir_file_right_show[i].PadRight(col_width_name);
                                }
                                else
                                {
                                    string ext = dir_file_right_show[i].Substring(dir_file_right_show[i].Length - 3, 3);
                                    dir_file_right_show[i] = dir_file_right_show[i].Substring(0, col_width_name - 6);
                                    dir_file_right_show[i] += "..." + ext;
                                }
                            }
                        }

                        if (ManagerMode == eMode.First)
                        {
                            for (int i = 0 + page_right * (col_hight_mode1 * 2 - 1) - page_right; i < dir_file_right_show.Length; i++)
                            {
                                Color.SetBackColor(ConsoleColor.DarkBlue);
                                if (ActiveTab == eTab.Right) if (i - (page_right * (col_hight_mode1 * 2 - 1) - page_right) == _counter_right) Color.SetBackColor(ConsoleColor.DarkYellow);
                                if (i - (page_right * (col_hight_mode1 * 2 - 1) - page_right) == 0)
                                {
                                    Console.SetCursorPosition(start_x_right, start_y_right + i - (page_right * (col_hight_mode1 * 2 - 1) - page_right));
                                    string temp = "..";
                                    temp = temp.PadRight(col_width_name);
                                    Console.WriteLine(temp);
                                }
                                else if (i - (page_right * (col_hight_mode1 * 2 - 1) - page_right) == col_hight_mode1 * 2 - 1)
                                {
                                    Console.SetCursorPosition(start_x_right + col_width_name + 1, start_y_right + i - col_hight_mode1 - (page_right * (col_hight_mode1 * 2 - 1) - page_right));
                                    string temp = "...";
                                    temp = temp.PadRight(col_width_name);
                                    Console.WriteLine(temp);
                                    Color.SetDefault();
                                    break;
                                }
                                else if (i - (page_right * (col_hight_mode1 * 2 - 1) - page_right) < col_hight_mode1)
                                {
                                    Console.SetCursorPosition(start_x_right, start_y_right + i - (page_right * (col_hight_mode1 * 2 - 1) - page_right));
                                    Console.WriteLine(dir_file_right_show[i]);
                                }
                                else
                                {
                                    Console.SetCursorPosition(start_x_right + col_width_name + 1, start_y_right + i - col_hight_mode1 - (page_right * (col_hight_mode1 * 2 - 1) - page_right));
                                    Console.WriteLine(dir_file_right_show[i]);
                                }
                                Color.SetDefault();
                            }
                        }

                        if (ManagerMode == eMode.Second)
                        {
                            for (int i = 0 + page_right * (col_hight_mode2 - 1) - page_right; i < dir_file_right_show.Length; i++)
                            {
                                Color.SetBackColor(ConsoleColor.DarkBlue);
                                if (ActiveTab == eTab.Right) if (i - (page_right * (col_hight_mode2 - 1) - page_right) == _counter_right) Color.SetBackColor(ConsoleColor.DarkYellow);
                                if (i - (page_right * (col_hight_mode2 - 1) - page_right) == 0)
                                {
                                    Console.SetCursorPosition(start_x_right, start_y_right + i - (page_right * (col_hight_mode2 - 1) - page_right));
                                    string temp = "..";
                                    temp = temp.PadRight(col_width_name);
                                    Console.WriteLine(temp);
                                }
                                else if (i - (page_right * (col_hight_mode2 - 1) - page_right) == col_hight_mode2 - 1)
                                {
                                    Console.SetCursorPosition(start_x_right, start_y_right + i - (page_right * (col_hight_mode2 - 1) - page_right));
                                    string temp = "...";
                                    temp = temp.PadRight(col_width_name);
                                    Console.WriteLine(temp);
                                    Color.SetDefault();
                                    break;
                                }
                                else if (i - (page_right * (col_hight_mode2 - 1) - page_right) < col_hight_mode2)
                                {
                                    Console.SetCursorPosition(start_x_right, start_y_right + i - (page_right * (col_hight_mode2 - 1) - page_right));
                                    Console.WriteLine(dir_file_right_show[i]);
                                    Console.SetCursorPosition(start_x_right + col_width_name + 1, start_y_right + i - (page_right * (col_hight_mode2 - 1) - page_right));
                                    Console.WriteLine(date_right[i]);
                                    Console.SetCursorPosition(start_x_right + col_width_name + col_width_date + 2, start_y_right + i - (page_right * (col_hight_mode2 - 1) - page_right));
                                    if (i == _position_folder_mode2_right)
                                    {
                                        Console.WriteLine(FolderSize_right);
                                        _position_folder_mode2_right = -1;
                                        FolderSize_right = "Folder ";
                                    }
                                    else Console.WriteLine(size_right[i]);
                                }
                                else
                                {
                                    Console.SetCursorPosition(start_x_right + col_width_name + 1, start_y_right + i - col_hight_mode2 - (page_right * (col_hight_mode2 - 1) - page_right));
                                    Console.WriteLine(dir_file_right_show[i]);
                                }
                                Color.SetDefault();
                            }
                        }

                    }
                    else
                    {
                        MessageBox massageBox = new MessageBox();
                        massageBox.ShowMessage_Window("Current dirictory unavailable!");
                        _current_dir_right = "";
                        _prev_dir_right = "";
                        page_right = 0;
                        _counter_right = 0;
                        drive_right = true;
                        continue;
                    }
                }

                /*--------------------------------------------------------------------------------------------------------------------------------------------*/
                //певні властивості файлу (при першому режимі)  дивися рядок
                if (!drive_left)
                {
                    if (ManagerMode == eMode.First)
                    {
                        string file_dir_name = dir_file_left_show[(page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left)];
                        ///тут далі
                        if (_counter_left == 0)
                        {
                            string temp_left_1 = "..";
                            string temp_left_2 = "Up";
                            temp_left_1 = temp_left_1.PadRight(col_width_name);
                            temp_left_2 = temp_left_2.PadLeft(col_width_name);
                            panel_Design.Properties_mode1_leftpanel(temp_left_1, temp_left_2);
                        }
                        else if (_counter_left == col_hight_mode1 * 2 - 1)
                        {
                            string temp_left_1 = "...";
                            string temp_left_2 = "Down (Next Page)";
                            temp_left_1 = temp_left_1.PadRight(col_width_name);
                            temp_left_2 = temp_left_2.PadLeft(col_width_name);

                            panel_Design.Properties_mode1_leftpanel(temp_left_1, temp_left_2);
                        }
                        else if ((page_left * (col_hight_mode1 * 2 - 1) - page_left) + _counter_left <= dir_left.Length)
                        {
                            DirectoryInfo temp_dir_info_left = new DirectoryInfo(dir_left[(page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)]);

                            string temp_dir_name_left = temp_dir_info_left.Name;

                            if (temp_dir_name_left.Length < col_width_name)
                            {
                                temp_dir_name_left = temp_dir_name_left.PadRight(col_width_name);
                            }
                            else
                            {
                                string ext = temp_dir_name_left.Substring(temp_dir_name_left.Length - 3, 3);
                                temp_dir_name_left = temp_dir_name_left.Substring(0, col_width_name - 6);
                                temp_dir_name_left += "..." + ext;
                            }

                            string temp_dir_param_left = FolderSize_left + temp_dir_info_left.CreationTime;
                            FolderSize_left = "Folder ";
                            temp_dir_param_left = temp_dir_param_left.PadLeft(col_width_name);

                            panel_Design.Properties_mode1_leftpanel(temp_dir_name_left, temp_dir_param_left);
                        }
                        else
                        {
                            DirectoryInfo file_info_left = new DirectoryInfo(files_left[((page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)) - dir_left.Length]);
                            string temp_file_name_left = file_info_left.Name;
                            if (temp_file_name_left.Length < col_width_name)
                            {
                                temp_file_name_left = temp_file_name_left.PadRight(col_width_name);
                            }
                            else
                            {
                                string ext = temp_file_name_left.Substring(temp_file_name_left.Length - 3, 3);
                                temp_file_name_left = temp_file_name_left.Substring(0, col_width_name - 6);
                                temp_file_name_left += "..." + ext;
                            }


                            string temp_file_date = file_info_left.CreationTime.ToString();

                            //мучимо файли
                            FileInfo fileInf_size = new FileInfo(files_left[((page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)) - dir_left.Length]);
                            double size_temp_left_double = 0.0;
                            string temp_left_file_size = "";

                            //байти                               
                            if (fileInf_size.Length < Size.KB)
                            {
                                temp_left_file_size = String.Format("{0:N2} B", fileInf_size.Length);
                            }
                            //кілобайти
                            else if (fileInf_size.Length >= Size.KB && fileInf_size.Length < Size.MB)
                            {
                                size_temp_left_double = (double)fileInf_size.Length / Size.KB;
                                temp_left_file_size = String.Format("{0:N2} Kb", size_temp_left_double);
                            }
                            //мегабайти
                            else if (fileInf_size.Length >= Size.MB && fileInf_size.Length < Size.GB)
                            {
                                size_temp_left_double = (double)fileInf_size.Length / Size.MB;
                                temp_left_file_size = String.Format("  {0:N2} Mb", size_temp_left_double);
                            }
                            //гігабайти
                            else if (fileInf_size.Length >= Size.GB)
                            {
                                size_temp_left_double = (double)fileInf_size.Length / Size.GB;
                                temp_left_file_size = String.Format("  {0:N2} Gb", size_temp_left_double);
                            }

                            //допилюємо пробіли та дату
                            temp_left_file_size += " " + temp_file_date;
                            temp_left_file_size = temp_left_file_size.PadLeft(col_width_name);

                            panel_Design.Properties_mode1_leftpanel(temp_file_name_left, temp_left_file_size);
                        }
                    }
                }

                if (!drive_right)
                {
                    if (ManagerMode == eMode.First)
                    {
                        string file_dir_name = dir_file_right_show[(page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right)];
                        ///тут далі
                        if (_counter_right == 0)
                        {
                            string temp_right_1 = "..";
                            string temp_right_2 = "Up";
                            temp_right_1 = temp_right_1.PadRight(col_width_name);
                            temp_right_2 = temp_right_2.PadLeft(col_width_name - 1);
                            panel_Design.Properties_mode1_rightpanel(temp_right_1, temp_right_2);
                        }
                        else if (_counter_right == col_hight_mode1 * 2 - 1)
                        {
                            string temp_right_1 = "...";
                            string temp_right_2 = "Down (Next Page)";
                            temp_right_1 = temp_right_1.PadRight(col_width_name);
                            temp_right_2 = temp_right_2.PadLeft(col_width_name - 1);

                            panel_Design.Properties_mode1_rightpanel(temp_right_1, temp_right_2);
                        }
                        else if ((page_right * (col_hight_mode1 * 2 - 1) - page_right) + _counter_right <= dir_right.Length)
                        {
                            DirectoryInfo temp_dir_info_right = new DirectoryInfo(dir_right[(page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)]);

                            string temp_dir_name_right = temp_dir_info_right.Name;
                            if (temp_dir_name_right.Length < col_width_name)
                            {
                                temp_dir_name_right = temp_dir_name_right.PadRight(col_width_name);
                            }
                            else
                            {
                                string ext = temp_dir_name_right.Substring(temp_dir_name_right.Length - 3, 3);
                                temp_dir_name_right = temp_dir_name_right.Substring(0, col_width_name - 6);
                                temp_dir_name_right += "..." + ext;
                            }

                            string temp_dir_param_right = FolderSize_right + temp_dir_info_right.CreationTime;
                            FolderSize_right = "Folder ";
                            temp_dir_param_right = temp_dir_param_right.PadLeft(col_width_name - 1);

                            panel_Design.Properties_mode1_rightpanel(temp_dir_name_right, temp_dir_param_right);
                        }
                        else
                        {
                            DirectoryInfo file_info_right = new DirectoryInfo(files_right[((page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)) - dir_right.Length]);
                            string temp_file_name_right = file_info_right.Name;
                            if (temp_file_name_right.Length < col_width_name)
                            {
                                temp_file_name_right = temp_file_name_right.PadRight(col_width_name);
                            }
                            else
                            {
                                string ext = temp_file_name_right.Substring(temp_file_name_right.Length - 3, 3);
                                temp_file_name_right = temp_file_name_right.Substring(0, col_width_name - 6);
                                temp_file_name_right += "..." + ext;
                            }

                            string temp_file_date = file_info_right.CreationTime.ToString();

                            //мучимо файли
                            FileInfo fileInf_size = new FileInfo(files_right[((page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)) - dir_right.Length]);
                            double size_temp_right_double = 0.0;
                            string temp_right_file_size = "";

                            //байти                               
                            if (fileInf_size.Length < Size.KB)
                            {
                                temp_right_file_size = String.Format("{0:N2} B", fileInf_size.Length);
                            }
                            //кілобайти
                            else if (fileInf_size.Length >= Size.KB && fileInf_size.Length < Size.MB)
                            {
                                size_temp_right_double = (double)fileInf_size.Length / Size.KB;
                                temp_right_file_size = String.Format("{0:N2} Kb", size_temp_right_double);
                            }
                            //мегабайти
                            else if (fileInf_size.Length >= Size.MB && fileInf_size.Length < Size.GB)
                            {
                                size_temp_right_double = (double)fileInf_size.Length / Size.MB;
                                temp_right_file_size = String.Format("  {0:N2} Mb", size_temp_right_double);
                            }
                            //гігабайти
                            else if (fileInf_size.Length >= Size.GB)
                            {
                                size_temp_right_double = (double)fileInf_size.Length / Size.GB;
                                temp_right_file_size = String.Format("  {0:N2} Gb", size_temp_right_double);
                            }

                            //допилюємо пробіли та дату
                            temp_right_file_size += " " + temp_file_date;
                            temp_right_file_size = temp_right_file_size.PadLeft(col_width_name - 1);

                            panel_Design.Properties_mode1_rightpanel(temp_file_name_right, temp_right_file_size);
                        }
                    }
                }
                /*--------------------------------------------------------------------------------------------------------------------------------------------*/

                choise = Console.ReadKey();

                /*--------------------------------------------------------------------------------------------------------------------------------------------*/
                //для комбінації клавіш
                switch (choise.Modifiers)
                {
                    //для вибору дисків
                    case ConsoleModifiers.Alt:
                        if (choise.Key == ConsoleKey.F1)
                        {
                            Disk disk = new Disk();
                            disk.WindowSize_Height += () => Panel_Design.CONSOLE_HEIGHT / 4;
                            disk.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) + 5;

                            int choise_device = disk.ShowWindow((int)eTab.Left);
                            if (choise_device != -1)
                            {
                                _prev_dir_left = "";
                                _current_dir_left = drives[choise_device].Name + "\\";
                                _counter_left = 0;
                                page_left = 0;
                                drive_left = false;
                            }
                            continue;
                        }
                        if (choise.Key == ConsoleKey.F2)
                        {
                            Disk disk = new Disk();
                            disk.WindowSize_Height += () => Panel_Design.CONSOLE_HEIGHT / 4;
                            disk.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) + 5;

                            int choise_device = disk.ShowWindow((int)eTab.Right);
                            if (choise_device != -1)
                            {
                                _prev_dir_right = "";
                                _current_dir_right = drives[choise_device].Name + "\\";
                                _counter_right = 0;
                                page_right = 0;
                                drive_right = false;
                            }
                            continue;
                        }
                        break;
                }
                /*--------------------------------------------------------------------------------------------------------------------------------------------*/
                //для однієї клавіши
                switch (choise.Key)
                {
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.Tab:
                        ActiveTab = ActiveTab == eTab.Left ? ActiveTab = eTab.Right :  ActiveTab = eTab.Left;
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.Escape:
                        ModeReload();
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.Spacebar:
                        //інфо про диски
                        if (ActiveTab == eTab.Left && drive_left)
                        {
                            //?
                        }
                        else if (ActiveTab == eTab.Right && drive_right)
                        {
                            //?
                        }
                        //розмір директорії
                        else if (ActiveTab == eTab.Left && !drive_left)
                        {
                            if (_counter_left == 0)
                            {
                                //?
                            }
                            else if (ManagerMode == eMode.First && _counter_left == col_hight_mode1 * 2 - 1)
                            {
                                //?
                            }
                            else if (ManagerMode == eMode.Second && _counter_left == col_hight_mode2 - 1)
                            {
                                //?
                            }
                            else if (_counter_left == 0 && page_left != 0)
                            {
                                //?
                            }
                            else
                            {
                                if (ManagerMode == eMode.First)
                                {
                                    if ((page_left * (col_hight_mode1 * 2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                    {
                                        double temp_catalogSize = 0;
                                        FolderSize_left = Size.sizeOfFolder(dir_left[(page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)], ref temp_catalogSize);
                                    }
                                    else
                                    {
                                        //??
                                        //На файлі натиснуто пробіл
                                    }
                                }
                                if (ManagerMode == eMode.Second)
                                {
                                    if ((page_left * (col_hight_mode2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                    {
                                        double temp_catalogSize = 0;
                                        FolderSize_left = "  ";
                                        FolderSize_left += Size.sizeOfFolder(dir_left[(page_left * (col_hight_mode2 - 1) - page_left) + (_counter_left - 1)], ref temp_catalogSize);                                        
                                        _position_folder_mode2_left = (page_left * (col_hight_mode2 - 1) - page_left) + (_counter_left - 1) + 1;
                                    }
                                    else
                                    {
                                        //??
                                        //На файлі натиснуто пробіл
                                    }
                                }
                            }
                        }
                        else if (ActiveTab == eTab.Right && !drive_right)
                        {
                            if (_counter_right == 0)
                            {
                                //?
                            }
                            else if (ManagerMode == eMode.First && _counter_right == col_hight_mode1 * 2 - 1)
                            {
                                //?
                            }
                            else if (ManagerMode == eMode.Second && _counter_right == col_hight_mode2 - 1)
                            {
                                //?
                            }
                            else if (_counter_right == 0 && page_right != 0)
                            {
                                //?
                            }
                            else
                            {
                                if (ManagerMode == eMode.First)
                                {
                                    if ((page_right * (col_hight_mode1 * 2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                    {
                                        double temp_catalogSize = 0;
                                        FolderSize_right = Size.sizeOfFolder(dir_right[(page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)], ref temp_catalogSize);
                                    }
                                    else
                                    {
                                        //??
                                        //На файлі натиснуто пробіл
                                    }
                                }
                                if (ManagerMode == eMode.Second)
                                {
                                    if ((page_right * (col_hight_mode2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                    {
                                        double temp_catalogSize = 0;
                                        FolderSize_right = "  ";
                                        FolderSize_right += Size.sizeOfFolder(dir_right[(page_right * (col_hight_mode2 - 1) - page_right) + (_counter_right - 1)], ref temp_catalogSize);
                                        _position_folder_mode2_right = (page_right * (col_hight_mode2 - 1) - page_right) + (_counter_right - 1) + 1;
                                    }
                                    else
                                    {
                                        //??
                                        //На файлі натиснуто пробіл
                                    }
                                }
                            }
                        }
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.Enter:
                        //коли відображено диски
                        if (ActiveTab == eTab.Left && drive_left)
                        {
                            _prev_dir_left = _current_dir_left;
                            _current_dir_left = drives[_counter_left].Name + "\\";
                            drive_left = false;
                            _counter_left = 0;
                            page_left = 0;
                        }
                        else if (ActiveTab == eTab.Right && drive_right)
                        {
                            _prev_dir_right = _current_dir_right;
                            _current_dir_right = drives[_counter_right].Name + "\\";
                            drive_right = false;
                            _counter_right = 0;
                            page_right = 0;
                        }
                        //коли ми в диску чи в папці
                        else if (ActiveTab == eTab.Left && !drive_left)
                        {
                            if(_counter_left == 0 && page_left == 0)
                            {
                                _current_dir_left = _prev_dir_left;
                                if (_prev_dir_left == "") drive_left = true;

                                string buf_prev = "";
                                if (_prev_dir_left.Contains("\\"))
                                {
                                    buf_prev = _prev_dir_left.Remove(_prev_dir_left.LastIndexOf('\\'), _prev_dir_left.Length - _prev_dir_left.LastIndexOf('\\'));
                                }
                                if (buf_prev.Contains("\\") == true)
                                {
                                    _prev_dir_left = _prev_dir_left.Remove(_prev_dir_left.LastIndexOf('\\') , _prev_dir_left.Length - _prev_dir_left.LastIndexOf('\\') );
                                }
                                else _prev_dir_left = "";
                            }
                            else if (ManagerMode == eMode.First && _counter_left == col_hight_mode1 * 2 - 1 )
                            {
                                page_left++;
                                _counter_left = 0;
                            }
                            else if (ManagerMode == eMode.Second && _counter_left == col_hight_mode2 - 1 )
                            {
                                page_left++;
                                _counter_left = 0;
                            }
                            else if (_counter_left == 0 && page_left != 0)
                            {
                                page_left--;
                                _counter_left = 0;
                            }
                            else
                            {
                                if (ManagerMode == eMode.First)
                                {
                                    if ((page_left * (col_hight_mode1 * 2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                    {
                                        _prev_dir_left = _current_dir_left;
                                        _current_dir_left = dir_left[(page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)];                                       
                                        _counter_left = 0;
                                        page_left = 0;
                                    }
                                    else
                                    {
                                        //??
                                        //На файлі натиснуто ентер
                                    }
                                }
                                if (ManagerMode == eMode.Second)
                                {
                                    if ((page_left * (col_hight_mode2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                    {
                                        _prev_dir_left = _current_dir_left;
                                        _current_dir_left = dir_left[(page_left * (col_hight_mode2 - 1) - page_left) + (_counter_left - 1)] ;
                                        _counter_left = 0;
                                        page_left = 0;
                                    }
                                    else
                                    {
                                        //??
                                        //На файлі натиснуто ентер
                                    }
                                }
                            }
                        }
                        else if (ActiveTab == eTab.Right && !drive_right)
                        {
                            if (_counter_right == 0 && page_right == 0)
                            {
                                _current_dir_right = _prev_dir_right;
                                if (_prev_dir_right == "") drive_right = true;

                                string buf_prev = "";
                                if (_prev_dir_right.Contains("\\"))
                                {
                                    buf_prev = _prev_dir_right.Remove(_prev_dir_right.LastIndexOf('\\'), _prev_dir_right.Length - _prev_dir_right.LastIndexOf('\\'));
                                }
                                if (buf_prev.Contains("\\") == true)
                                {
                                    _prev_dir_right = _prev_dir_right.Remove(_prev_dir_right.LastIndexOf('\\'), _prev_dir_right.Length - _prev_dir_right.LastIndexOf('\\'));
                                }
                                else _prev_dir_right = "";
                            }
                            else if (ManagerMode == eMode.First && _counter_right == col_hight_mode1 * 2 - 1)
                            {
                                page_right++;
                                _counter_right = 0;
                            }
                            else if (ManagerMode == eMode.Second && _counter_right == col_hight_mode2 - 1)
                            {
                                page_right++;
                                _counter_right = 0;
                            }
                            else if (_counter_right == 0 && page_right != 0)
                            {
                                page_right--;
                                _counter_right = 0;
                            }
                            else
                            {
                                if (ManagerMode == eMode.First)
                                {
                                    if ((page_right * (col_hight_mode1 * 2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                    {
                                        _prev_dir_right = _current_dir_right;
                                        _current_dir_right = dir_right[(page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)];
                                        _counter_right = 0;
                                        page_right = 0;
                                    }
                                    else
                                    {
                                        //??
                                        //На файлі натиснуто ентер
                                    }
                                }
                                if (ManagerMode == eMode.Second)
                                {
                                    if ((page_right * (col_hight_mode2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                    {
                                        _prev_dir_right = _current_dir_right;
                                        _current_dir_right = dir_right[(page_right * (col_hight_mode2 - 1) - page_right) + (_counter_right - 1)];
                                        _counter_right = 0;
                                        page_right = 0;
                                    }
                                    else
                                    {
                                        //??
                                        //На файлі натиснуто ентер
                                    }
                                }
                            }
                        }
                            break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.DownArrow:
                        //коли відображено диски
                        if (ActiveTab == eTab.Left && drive_left)
                        {
                            _counter_left++;
                            if (_counter_left > drives.Length - 1) _counter_left = 0;
                        }
                        else if (ActiveTab == eTab.Right && drive_right)
                        {
                            _counter_right++;
                            if (_counter_right > drives.Length - 1) _counter_right = 0;
                        }

                        //коли ми в диску чи в папці
                        else if (ActiveTab == eTab.Left && !drive_left)
                        {
                            _counter_left++;
                            //Переходи
                            //Mode 1
                            if (ManagerMode == eMode.First
                                 && (page_left + 1) * (col_hight_mode1 * 2) - page_left > dir_file_left_show.Length 
                                 && _counter_left > dir_file_left_show.Length - 1 - (page_left * (col_hight_mode1 * 2 - 1) - page_left))
                                 _counter_left = 0;
                            else if (ManagerMode == eMode.First
                                 && (page_left + 1) * (col_hight_mode1 * 2) - page_left < dir_file_left_show.Length 
                                 && _counter_left > col_hight_mode1 * 2 - 1)
                                 _counter_left = 0;
                            //Mode2
                            if (ManagerMode == eMode.Second
                                 && (page_left + 1) *col_hight_mode2 - page_left > dir_file_left_show.Length
                                 && _counter_left > dir_file_left_show.Length - 1 - (page_left*(col_hight_mode2 - 1) - page_left))
                                _counter_left = 0;
                            else if (ManagerMode == eMode.Second
                                 && (page_left + 1) * col_hight_mode2 - page_left < dir_file_left_show.Length
                                 && _counter_left > col_hight_mode2 - 1)
                                _counter_left = 0;
                        }
                        else if (ActiveTab == eTab.Right && !drive_right)
                        {
                            _counter_right++;
                            //Переходи
                            //Mode 1
                            if (ManagerMode == eMode.First
                                 && (page_right + 1) * (col_hight_mode1 * 2) - page_right > dir_file_right_show.Length
                                 && _counter_right > dir_file_right_show.Length - 1 - (page_right * (col_hight_mode1 * 2 - 1) - page_right))
                                _counter_right = 0;
                            else if (ManagerMode == eMode.First
                                 && (page_right + 1) * (col_hight_mode1 * 2) - page_right < dir_file_right_show.Length
                                 && _counter_right > col_hight_mode1 * 2 - 1)
                                _counter_right = 0;
                            //Mode2
                            if (ManagerMode == eMode.Second
                                 && (page_right + 1) * col_hight_mode2 - page_right > dir_file_right_show.Length
                                 && _counter_right > dir_file_right_show.Length - 1 - (page_right * (col_hight_mode2 - 1) - page_right))
                                _counter_right = 0;
                            else if (ManagerMode == eMode.Second
                                 && (page_right + 1) * col_hight_mode2 - page_right < dir_file_right_show.Length
                                 && _counter_right > col_hight_mode2 - 1)
                                _counter_right = 0;
                        }
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.UpArrow:
                        //коли відображено диски
                        if (ActiveTab == eTab.Left && drive_left)
                        {
                                _counter_left--;
                                if (_counter_left < 0) _counter_left = drives.Length - 1;
                        }
                        else if (ActiveTab == eTab.Right && drive_right)
                        {
                                _counter_right--;
                                if (_counter_right < 0) _counter_right = drives.Length - 1;
                        }

                        //коли ми в диску чи в папці
                        else if (ActiveTab == eTab.Left && !drive_left)
                        {
                            _counter_left--;
                            //Mode 1
                            if (ManagerMode == eMode.First
                                 && (page_left + 1) * (col_hight_mode1 * 2) - page_left > dir_file_left_show.Length
                                 && _counter_left < 0)
                                 _counter_left = dir_file_left_show.Length - 1 - (page_left*(col_hight_mode1 * 2 - 1) - page_left);
                            else if (ManagerMode == eMode.First
                                 && (page_left + 1) * (col_hight_mode1 * 2) - page_left < dir_file_left_show.Length - 1
                                 && _counter_left < 0)
                                _counter_left = col_hight_mode1 * 2 - 1;
                            //Mode2
                            if (ManagerMode == eMode.Second
                                 && (page_left + 1) * col_hight_mode2 - page_left > dir_file_left_show.Length
                                 && _counter_left < 0)
                                _counter_left = dir_file_left_show.Length - 1 - (page_left * (col_hight_mode2 - 1) - page_left);
                            else if (ManagerMode == eMode.Second
                                 && (page_left + 1) * col_hight_mode2 - page_left < dir_file_left_show.Length - 1
                                 && _counter_left < 0)
                                _counter_left = col_hight_mode2 - 1;
                        }
                        else if (ActiveTab == eTab.Right && !drive_right)
                        {
                            _counter_right--;
                            //Mode 1
                            if (ManagerMode == eMode.First
                                 && (page_right + 1) * (col_hight_mode1 * 2) - page_right > dir_file_right_show.Length
                                 && _counter_right < 0)
                                _counter_right = dir_file_right_show.Length - 1 - (page_right * (col_hight_mode1 * 2 - 1) - page_right);
                            else if (ManagerMode == eMode.First
                                 && (page_right + 1) * (col_hight_mode1 * 2) - page_right < dir_file_right_show.Length - 1
                                 && _counter_right < 0)
                                _counter_right = col_hight_mode1 * 2 - 1;
                            //Mode2
                            if (ManagerMode == eMode.Second
                                 && (page_right + 1) * col_hight_mode2 - page_right > dir_file_right_show.Length
                                 && _counter_right < 0)
                                _counter_right = dir_file_right_show.Length - 1 - (page_right * (col_hight_mode2 - 1) - page_right);
                            else if (ManagerMode == eMode.Second
                                 && (page_right + 1) * col_hight_mode2 - page_right < dir_file_right_show.Length - 1
                                 && _counter_right < 0)
                                _counter_right = col_hight_mode2 - 1;
                        }
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.LeftArrow:
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.RightArrow:
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F1://Help
                        Help help = new Help();
                        help.Show();
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F2://Disk
                        Disk disk = new Disk();
                        disk.WindowSize_Height += () => Panel_Design.CONSOLE_HEIGHT / 4;
                        disk.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) + 5;

                        int choise_device = disk.ShowWindow((int)ActiveTab);                        
                        if (choise_device != -1)
                        {
                            if (ActiveTab == eTab.Left)
                            {
                                _prev_dir_left = "";
                                _current_dir_left = drives[choise_device].Name + "\\";
                                _counter_left = 0;
                                page_left = 0;
                                drive_left = false;
                            }
                            if (ActiveTab == eTab.Right)
                            {
                                _prev_dir_right = "";
                                _current_dir_right = drives[choise_device].Name + "\\";
                                _counter_right = 0;
                                page_right = 0;
                                drive_right = false;
                            }
                        }
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F3://View
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F4://Edit
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F5://Copy
                        if (drive_left || drive_right)
                        {
                            MessageBox massageBox = new MessageBox();
                            massageBox.ShowMessage_Window("This operation is not valid!\nPlease select Drive or Folder in another tab.");
                        }
                        else
                        {
                            if (ActiveTab == eTab.Left && !drive_left)
                            {
                                if (_counter_left == 0)
                                {
                                    //nothing
                                }
                                else if (ManagerMode == eMode.First && _counter_left == col_hight_mode1 * 2 - 1)
                                {
                                    //nothing
                                }
                                else if (ManagerMode == eMode.Second && _counter_left == col_hight_mode2 - 1)
                                {
                                    //nothing
                                }
                                else if (_counter_left == 0 && page_left != 0)
                                {
                                    //nothing
                                }
                                else
                                {
                                    if (ManagerMode == eMode.First)
                                    {
                                        if ((page_left * (col_hight_mode1 * 2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                        {
                                            //На папці натиснуто копіювати (F5)
                                            string Full_FolderNameToCopy = dir_left[(page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)];

                                            Copy copyFolder = new Copy();

                                            string Short_FolderNameToCopy = Path.GetFileName(Full_FolderNameToCopy);
                                            int action = copyFolder.ShowWindow(Short_FolderNameToCopy, _current_dir_left, _current_dir_right);


                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> right_folders = Directory.GetDirectories(_current_dir_right).ToList();

                                                        foreach (var item in right_folders)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FolderNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        right_folders.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this folder \n{ " + Short_FolderNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    Copy.DirectoryCopy(Full_FolderNameToCopy, _current_dir_right + @"\" + Short_FolderNameToCopy, true);
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            Copy.DirectoryCopy(Full_FolderNameToCopy, _current_dir_right + @"\" + Short_FolderNameToCopy, true);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            //На файлі натиснуто копіювати (F5)
                                            string Full_FileNameToCopy = files_left[((page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)) - dir_left.Length];

                                            Copy copyFile = new Copy();

                                            string Short_FileNameToCopy = Path.GetFileName(Full_FileNameToCopy);
                                            int action = copyFile.ShowWindow(Short_FileNameToCopy,_current_dir_left,_current_dir_right);

                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> right_files = Directory.GetFiles(_current_dir_right).ToList();

                                                        foreach (var item in right_files)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FileNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        right_files.Clear();

                                                        //копіювання із заміною
                                                        if(confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this file \n{ " + Short_FileNameToCopy + "  }");
                                                            switch(confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    File.Copy(Full_FileNameToCopy, _current_dir_right + @"\" + Short_FileNameToCopy, true);
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            File.Copy(Full_FileNameToCopy, _current_dir_right + @"\" + Short_FileNameToCopy);
                                                        }                                                        
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                    }
                                    if (ManagerMode == eMode.Second)
                                    {
                                        if ((page_left * (col_hight_mode2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                        {
                                            //На папці натиснуто копіювати (F5)
                                            string Full_FolderNameToCopy = dir_left[(page_left * (col_hight_mode2 - 1) - page_left) + (_counter_left - 1)];

                                            Copy copyFolder = new Copy();

                                            string Short_FolderNameToCopy = Path.GetFileName(Full_FolderNameToCopy);
                                            int action = copyFolder.ShowWindow(Short_FolderNameToCopy, _current_dir_left, _current_dir_right);


                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> right_folders = Directory.GetDirectories(_current_dir_right).ToList();

                                                        foreach (var item in right_folders)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FolderNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        right_folders.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this folder \n{ " + Short_FolderNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    Copy.DirectoryCopy(Full_FolderNameToCopy, _current_dir_right + @"\" + Short_FolderNameToCopy, true);
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            Copy.DirectoryCopy(Full_FolderNameToCopy, _current_dir_right + @"\" + Short_FolderNameToCopy, true);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            //На файлі натиснуто копіювати (F5)
                                            string Full_FileNameToCopy = files_left[((page_left * (col_hight_mode2 - 1) - page_left) + (_counter_left - 1)) - dir_left.Length];

                                            Copy copyFile = new Copy();

                                            string Short_FileNameToCopy = Path.GetFileName(Full_FileNameToCopy);
                                            int action = copyFile.ShowWindow(Short_FileNameToCopy, _current_dir_left, _current_dir_right);

                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> right_files = Directory.GetFiles(_current_dir_right).ToList();

                                                        foreach (var item in right_files)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FileNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        right_files.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this file \n{ " + Short_FileNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    File.Copy(Full_FileNameToCopy, _current_dir_right + @"\" + Short_FileNameToCopy, true);
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            File.Copy(Full_FileNameToCopy, _current_dir_right + @"\" + Short_FileNameToCopy);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (ActiveTab == eTab.Right && !drive_right)
                            {
                                if (_counter_right == 0)
                                {
                                    //nothing
                                }
                                else if (ManagerMode == eMode.First && _counter_right == col_hight_mode1 * 2 - 1)
                                {
                                    //nothing
                                }
                                else if (ManagerMode == eMode.Second && _counter_right == col_hight_mode2 - 1)
                                {
                                    //nothing
                                }
                                else if (_counter_right == 0 && page_right != 0)
                                {
                                    //nothing
                                }
                                else
                                {
                                    if (ManagerMode == eMode.First)
                                    {
                                        if ((page_right * (col_hight_mode1 * 2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                        {
                                            //На папці натиснуто копіювати (F5)
                                            string Full_FolderNameToCopy = dir_right[(page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)];

                                            Copy copyFolder = new Copy();

                                            string Short_FolderNameToCopy = Path.GetFileName(Full_FolderNameToCopy);
                                            int action = copyFolder.ShowWindow(Short_FolderNameToCopy, _current_dir_right, _current_dir_left);


                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> left_folders = Directory.GetDirectories(_current_dir_left).ToList();

                                                        foreach (var item in left_folders)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FolderNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        left_folders.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this folder \n{ " + Short_FolderNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    Copy.DirectoryCopy(Full_FolderNameToCopy, _current_dir_left + @"\" + Short_FolderNameToCopy, true);
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            Copy.DirectoryCopy(Full_FolderNameToCopy, _current_dir_left + @"\" + Short_FolderNameToCopy, true);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            //На файлі натиснуто копіювати (F5)
                                            string Full_FileNameToCopy = files_right[((page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)) - dir_right.Length];

                                            Copy copyFile = new Copy();

                                            string Short_FileNameToCopy = Path.GetFileName(Full_FileNameToCopy);
                                            int action = copyFile.ShowWindow(Short_FileNameToCopy, _current_dir_right, _current_dir_left);

                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> left_files = Directory.GetFiles(_current_dir_left).ToList();

                                                        foreach (var item in left_files)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FileNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        left_files.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this file \n{ " + Short_FileNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    File.Copy(Full_FileNameToCopy, _current_dir_left + @"\" + Short_FileNameToCopy, true);
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            File.Copy(Full_FileNameToCopy, _current_dir_left + @"\" + Short_FileNameToCopy);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                    }
                                    if (ManagerMode == eMode.Second)
                                    {
                                        if ((page_right * (col_hight_mode2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                        {
                                            //На папці натиснуто копіювати (F5)
                                            string Full_FolderNameToCopy = dir_right[(page_right * (col_hight_mode2 - 1) - page_right) + (_counter_right - 1)];

                                            Copy copyFolder = new Copy();

                                            string Short_FolderNameToCopy = Path.GetFileName(Full_FolderNameToCopy);
                                            int action = copyFolder.ShowWindow(Short_FolderNameToCopy, _current_dir_right, _current_dir_left);


                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> left_folders = Directory.GetDirectories(_current_dir_left).ToList();

                                                        foreach (var item in left_folders)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FolderNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        left_folders.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this folder \n{ " + Short_FolderNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    Copy.DirectoryCopy(Full_FolderNameToCopy, _current_dir_left + @"\" + Short_FolderNameToCopy, true);
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            Copy.DirectoryCopy(Full_FolderNameToCopy, _current_dir_left + @"\" + Short_FolderNameToCopy, true);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            //На файлі натиснуто копіювати (F5)
                                            string Full_FileNameToCopy = files_right[((page_right * (col_hight_mode2 - 1) - page_right) + (_counter_right - 1)) - dir_right.Length];

                                            Copy copyFile = new Copy();

                                            string Short_FileNameToCopy = Path.GetFileName(Full_FileNameToCopy);
                                            int action = copyFile.ShowWindow(Short_FileNameToCopy, _current_dir_right, _current_dir_left);

                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> left_files = Directory.GetFiles(_current_dir_left).ToList();

                                                        foreach (var item in left_files)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FileNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        left_files.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this file \n{ " + Short_FileNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    File.Copy(Full_FileNameToCopy, _current_dir_left + @"\" + Short_FileNameToCopy, true);
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            File.Copy(Full_FileNameToCopy, _current_dir_left + @"\" + Short_FileNameToCopy);
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox massageBox = new MessageBox();
                                massageBox.ShowMessage_Window("This operation is not valid!");
                            }
                        }                        
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F6://Move rename
                        if (drive_left || drive_right)
                        {
                            MessageBox massageBox = new MessageBox();
                            massageBox.ShowMessage_Window("This operation is not valid!\nPlease select Drive or Folder in another tab.");
                        }
                        else
                        {
                            if (ActiveTab == eTab.Left && !drive_left)
                            {
                                if (_counter_left == 0)
                                {
                                    //nothing
                                }
                                else if (ManagerMode == eMode.First && _counter_left == col_hight_mode1 * 2 - 1)
                                {
                                    //nothing
                                }
                                else if (ManagerMode == eMode.Second && _counter_left == col_hight_mode2 - 1)
                                {
                                    //nothing
                                }
                                else if (_counter_left == 0 && page_left != 0)
                                {
                                    //nothing
                                }
                                else
                                {
                                    if (ManagerMode == eMode.First)
                                    {
                                        if ((page_left * (col_hight_mode1 * 2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                        {
                                            //На папці натиснуто копіювати (F5)
                                            string Full_FolderNameToCopy = dir_left[(page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)];

                                            RenameMove renameMove = new RenameMove();
                                            renameMove.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
                                            renameMove.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 31;

                                            string Short_FolderNameToCopy = Path.GetFileName(Full_FolderNameToCopy);
                                            int action = renameMove.ShowRenMove_Window(ref Short_FolderNameToCopy, _current_dir_left, _current_dir_right);


                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> right_folders = Directory.GetDirectories(_current_dir_right).ToList();

                                                        foreach (var item in right_folders)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FolderNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        right_folders.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this folder \n{ " + Short_FolderNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    //DirectoryInfo target = new DirectoryInfo(Full_FolderNameToCopy);
                                                                    //target.MoveTo(_current_dir_right + @"\" + Short_FolderNameToCopy);
                                                                    Directory.Move(Full_FolderNameToCopy, _current_dir_right + @"\" + Short_FolderNameToCopy);
                                                                    _counter_left--;
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            Directory.Move(Full_FolderNameToCopy, _current_dir_right + @"\" + Short_FolderNameToCopy);
                                                            _counter_left--;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            //На файлі натиснуто копіювати (F5)
                                            string Full_FileNameToCopy = files_left[((page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)) - dir_left.Length];
                                                                                        
                                            RenameMove renameMove = new RenameMove();
                                            renameMove.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
                                            renameMove.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 31;

                                            string Short_FileNameToCopy = Path.GetFileName(Full_FileNameToCopy);
                                            int action = renameMove.ShowRenMove_Window(ref Short_FileNameToCopy, _current_dir_left, _current_dir_right);

                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> right_files = Directory.GetFiles(_current_dir_right).ToList();

                                                        foreach (var item in right_files)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FileNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        right_files.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this file \n{ " + Short_FileNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    File.Move(Full_FileNameToCopy, _current_dir_right + @"\" + Short_FileNameToCopy);
                                                                    _counter_left--;
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            File.Move(Full_FileNameToCopy, _current_dir_right + @"\" + Short_FileNameToCopy);
                                                            _counter_left--;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                    }
                                    if (ManagerMode == eMode.Second)
                                    {
                                        if ((page_left * (col_hight_mode2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                        {
                                            //На папці натиснуто копіювати (F5)
                                            string Full_FolderNameToCopy = dir_left[(page_left * (col_hight_mode2 - 1) - page_left) + (_counter_left - 1)];

                                            RenameMove renameMove = new RenameMove();
                                            renameMove.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
                                            renameMove.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 31;

                                            string Short_FolderNameToCopy = Path.GetFileName(Full_FolderNameToCopy);
                                            int action = renameMove.ShowRenMove_Window(ref Short_FolderNameToCopy, _current_dir_left, _current_dir_right);


                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> right_folders = Directory.GetDirectories(_current_dir_right).ToList();

                                                        foreach (var item in right_folders)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FolderNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        right_folders.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this folder \n{ " + Short_FolderNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    Directory.Move(Full_FolderNameToCopy, _current_dir_right + @"\" + Short_FolderNameToCopy);
                                                                    _counter_left--;
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            Directory.Move(Full_FolderNameToCopy, _current_dir_right + @"\" + Short_FolderNameToCopy); 
                                                            _counter_left--;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            //На файлі натиснуто копіювати (F5)
                                            string Full_FileNameToCopy = files_left[((page_left * (col_hight_mode2 - 1) - page_left) + (_counter_left - 1)) - dir_left.Length];

                                            RenameMove renameMove = new RenameMove();
                                            renameMove.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
                                            renameMove.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 31;

                                            string Short_FileNameToCopy = Path.GetFileName(Full_FileNameToCopy);
                                            int action = renameMove.ShowRenMove_Window(ref Short_FileNameToCopy, _current_dir_left, _current_dir_right);

                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> right_files = Directory.GetFiles(_current_dir_right).ToList();

                                                        foreach (var item in right_files)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FileNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        right_files.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this file \n{ " + Short_FileNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    File.Move(Full_FileNameToCopy, _current_dir_right + @"\" + Short_FileNameToCopy);
                                                                    _counter_left--;
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            File.Move(Full_FileNameToCopy, _current_dir_right + @"\" + Short_FileNameToCopy);
                                                            _counter_left--;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (ActiveTab == eTab.Right && !drive_right)
                            {
                                if (_counter_right == 0)
                                {
                                    //nothing
                                }
                                else if (ManagerMode == eMode.First && _counter_right == col_hight_mode1 * 2 - 1)
                                {
                                    //nothing
                                }
                                else if (ManagerMode == eMode.Second && _counter_right == col_hight_mode2 - 1)
                                {
                                    //nothing
                                }
                                else if (_counter_right == 0 && page_right != 0)
                                {
                                    //nothing
                                }
                                else
                                {
                                    if (ManagerMode == eMode.First)
                                    {
                                        if ((page_right * (col_hight_mode1 * 2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                        {
                                            //На папці натиснуто копіювати (F5)
                                            string Full_FolderNameToCopy = dir_right[(page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)];

                                            RenameMove renameMove = new RenameMove();
                                            renameMove.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
                                            renameMove.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 31;

                                            string Short_FolderNameToCopy = Path.GetFileName(Full_FolderNameToCopy);
                                            int action = renameMove.ShowRenMove_Window(ref Short_FolderNameToCopy, _current_dir_right, _current_dir_left);


                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> left_folders = Directory.GetDirectories(_current_dir_left).ToList();

                                                        foreach (var item in left_folders)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FolderNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        left_folders.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this folder \n{ " + Short_FolderNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    Directory.Move(Full_FolderNameToCopy, _current_dir_left + @"\" + Short_FolderNameToCopy);
                                                                    _counter_right--;
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            Directory.Move(Full_FolderNameToCopy, _current_dir_left + @"\" + Short_FolderNameToCopy);
                                                            _counter_right--;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            //На файлі натиснуто копіювати (F5)
                                            string Full_FileNameToCopy = files_right[((page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)) - dir_right.Length];

                                            RenameMove renameMove = new RenameMove();
                                            renameMove.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
                                            renameMove.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 31;

                                            string Short_FileNameToCopy = Path.GetFileName(Full_FileNameToCopy);
                                            int action = renameMove.ShowRenMove_Window(ref Short_FileNameToCopy, _current_dir_right, _current_dir_left);

                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> left_files = Directory.GetFiles(_current_dir_left).ToList();

                                                        foreach (var item in left_files)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FileNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        left_files.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this file \n{ " + Short_FileNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    File.Move(Full_FileNameToCopy, _current_dir_left + @"\" + Short_FileNameToCopy);
                                                                    _counter_right--;
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            File.Move(Full_FileNameToCopy, _current_dir_left + @"\" + Short_FileNameToCopy);
                                                            _counter_right--;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                    }
                                    if (ManagerMode == eMode.Second)
                                    {
                                        if ((page_right * (col_hight_mode2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                        {
                                            //На папці натиснуто копіювати (F5)
                                            string Full_FolderNameToCopy = dir_right[(page_right * (col_hight_mode2 - 1) - page_right) + (_counter_right - 1)];

                                            RenameMove renameMove = new RenameMove();
                                            renameMove.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
                                            renameMove.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 31;

                                            string Short_FolderNameToCopy = Path.GetFileName(Full_FolderNameToCopy);
                                            int action = renameMove.ShowRenMove_Window(ref Short_FolderNameToCopy, _current_dir_right, _current_dir_left);


                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> left_folders = Directory.GetDirectories(_current_dir_left).ToList();

                                                        foreach (var item in left_folders)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FolderNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        left_folders.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this folder \n{ " + Short_FolderNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    Directory.Move(Full_FolderNameToCopy, _current_dir_left + @"\" + Short_FolderNameToCopy);
                                                                    _counter_right--;
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            Directory.Move(Full_FolderNameToCopy, _current_dir_left + @"\" + Short_FolderNameToCopy);
                                                            _counter_right--;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            //На файлі натиснуто копіювати (F5)
                                            string Full_FileNameToCopy = files_right[((page_right * (col_hight_mode2 - 1) - page_right) + (_counter_right - 1)) - dir_right.Length];

                                            RenameMove renameMove = new RenameMove();
                                            renameMove.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
                                            renameMove.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 31;

                                            string Short_FileNameToCopy = Path.GetFileName(Full_FileNameToCopy);
                                            int action = renameMove.ShowRenMove_Window(ref Short_FileNameToCopy, _current_dir_right, _current_dir_left);

                                            switch (action)
                                            {
                                                case 0://Yes
                                                    try
                                                    {
                                                        bool confirm_flag = false;
                                                        List<string> left_files = Directory.GetFiles(_current_dir_left).ToList();

                                                        foreach (var item in left_files)
                                                        {
                                                            if (Path.GetFileName(item) == Short_FileNameToCopy)
                                                            {
                                                                confirm_flag = true;
                                                                break;
                                                            }
                                                        }
                                                        left_files.Clear();

                                                        //копіювання із заміною
                                                        if (confirm_flag)
                                                        {
                                                            MessageBox confirm = new MessageBox();
                                                            int confim_action = confirm.ShowConfirm_Window("You really want to replace this file \n{ " + Short_FileNameToCopy + "  }");
                                                            switch (confim_action)
                                                            {
                                                                case 0: //Yes
                                                                    File.Move(Full_FileNameToCopy, _current_dir_left + @"\" + Short_FileNameToCopy);
                                                                    _counter_right--;
                                                                    break;
                                                                case 1: //No
                                                                    /*No*/
                                                                    break;
                                                                default:
                                                                    /*Default*/
                                                                    break;
                                                            }
                                                        }
                                                        //просто копіювання
                                                        else
                                                        {
                                                            File.Move(Full_FileNameToCopy, _current_dir_left + @"\" + Short_FileNameToCopy);
                                                            _counter_right--;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        ModeReload();
                                                        MessageBox massageBox = new MessageBox();
                                                        massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                    }
                                                    break;
                                                case 1://No
                                                       /*No*/
                                                    break;
                                                case 2://Cancel
                                                       /*Cancel*/
                                                    break;
                                                default:
                                                    /*Default*/
                                                    break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox massageBox = new MessageBox();
                                massageBox.ShowMessage_Window("This operation is not valid!");
                            }
                        }
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F7://Make folder
                        if (ActiveTab == eTab.Left && !drive_left)
                        {
                            NewFolder newFolder = new NewFolder();
                            newFolder.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 31;
                            newFolder.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;

                            int action = newFolder.ShowMakeFolder_Window(out string FolderName);
                            switch (action)
                            {
                                case 0://Yes
                                    try
                                    {
                                        Directory.CreateDirectory(_current_dir_left+@"\"+FolderName);
                                    }
                                    catch(Exception ex)
                                    {
                                        ModeReload();
                                        MessageBox massageBox = new MessageBox();
                                        massageBox.ShowMessage_Window("Incorrect folder name: \n"+ex.Message);
                                    }
                                    break;
                                case 1://No
                                    /*No*/
                                    break;
                                case 2://Cancel
                                    /*Cancel*/
                                    break;
                                default:
                                    /*Default*/
                                    break;
                            }

                        }
                        else if (ActiveTab == eTab.Right && !drive_right)
                        {
                            NewFolder newFolder = new NewFolder();
                            int action = newFolder.ShowMakeFolder_Window(out string FolderName);
                            switch (action)
                            {
                                case 0://Yes
                                    try
                                    {
                                        Directory.CreateDirectory(_current_dir_right + @"\" + FolderName);
                                    }
                                    catch (Exception ex)
                                    {
                                        ModeReload();
                                        MessageBox massageBox = new MessageBox();
                                        massageBox.ShowMessage_Window("Incorrect folder name: \n" + ex.Message);
                                    }
                                    break;
                                case 1://No
                                    /*No*/
                                    break;
                                case 2://Cancel
                                    /*Cancel*/
                                    break;
                                default:
                                    /*Default*/
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox massageBox = new MessageBox();
                            massageBox.ShowMessage_Window("I can't create a directory outside the drive :) !");
                        }
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F8://Delete
                        if (ActiveTab == eTab.Left && !drive_left)
                        {
                            if (_counter_left == 0)
                            {
                                //nothing
                            }
                            else if (ManagerMode == eMode.First && _counter_left == col_hight_mode1 * 2 - 1)
                            {
                                //nothing
                            }
                            else if (ManagerMode == eMode.Second && _counter_left == col_hight_mode2 - 1)
                            {
                                //nothing
                            }
                            else if (_counter_left == 0 && page_left != 0)
                            {
                                //nothing
                            }
                            else
                            {
                                if (ManagerMode == eMode.First)
                                {
                                    if ((page_left * (col_hight_mode1 * 2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                    {
                                        //На папці натиснуто видалити (F8)
                                        string Full_FolderNameToDelete = dir_left[(page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)];

                                        DeleteFolder deleteFolder = new DeleteFolder();
                                        string Short_FolderNameToDelete = Path.GetFileName(Full_FolderNameToDelete);
                                        int action = deleteFolder.ShowWindow(Short_FolderNameToDelete);

                                        switch (action)
                                        {
                                            case 0://Yes
                                                try
                                                {
                                                    Directory.Delete(Full_FolderNameToDelete, true);
                                                    _counter_left--;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ModeReload();
                                                    MessageBox massageBox = new MessageBox();
                                                    massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                }
                                                break;
                                            case 1://No
                                                   /*No*/
                                                break;
                                            case 2://Cancel
                                                   /*Cancel*/
                                                break;
                                            default:
                                                /*Default*/
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        //На файлі натиснуто видалити (F8)
                                        string Full_FileNameToDelete = files_left[((page_left * (col_hight_mode1 * 2 - 1) - page_left) + (_counter_left - 1)) - dir_left.Length];

                                        DeleteFolder deleteFolder = new DeleteFolder();
                                        string Short_FileNameToDelete = Path.GetFileName(Full_FileNameToDelete);
                                        int action = deleteFolder.ShowWindow(Short_FileNameToDelete);

                                        switch (action)
                                        {
                                            case 0://Yes
                                                try
                                                {
                                                    File.Delete(Full_FileNameToDelete);
                                                    _counter_left--;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ModeReload();
                                                    MessageBox massageBox = new MessageBox();
                                                    massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                }
                                                break;
                                            case 1://No
                                                   /*No*/
                                                break;
                                            case 2://Cancel
                                                   /*Cancel*/
                                                break;
                                            default:
                                                /*Default*/
                                                break;
                                        }
                                    }
                                }
                                if (ManagerMode == eMode.Second)
                                {
                                    if ((page_left * (col_hight_mode2 - 1) - page_left) + _counter_left <= dir_left.Length)
                                    {
                                        //На папці натиснуто видалити (F8)
                                        string Full_FolderNameToDelete = dir_left[(page_left * (col_hight_mode2 - 1) - page_left) + (_counter_left - 1)];

                                        DeleteFolder deleteFolder = new DeleteFolder();
                                        string Short_FolderNameToDelete = Path.GetFileName(Full_FolderNameToDelete);
                                        int action = deleteFolder.ShowWindow(Short_FolderNameToDelete);

                                        switch (action)
                                        {
                                            case 0://Yes
                                                try
                                                {
                                                    Directory.Delete(Full_FolderNameToDelete, true);
                                                    _counter_left--;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ModeReload();
                                                    MessageBox massageBox = new MessageBox();
                                                    massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                }
                                                break;
                                            case 1://No
                                                   /*No*/
                                                break;
                                            case 2://Cancel
                                                   /*Cancel*/
                                                break;
                                            default:
                                                /*Default*/
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        //На файлі натиснуто видалити (F8)
                                        string Full_FileNameToDelete = files_left[((page_left * (col_hight_mode2 - 1) - page_left) + (_counter_left - 1)) - dir_left.Length];

                                        DeleteFolder deleteFolder = new DeleteFolder();
                                        string Short_FileNameToDelete = Path.GetFileName(Full_FileNameToDelete);
                                        int action = deleteFolder.ShowWindow(Short_FileNameToDelete);

                                        switch (action)
                                        {
                                            case 0://Yes
                                                try
                                                {
                                                    File.Delete(Full_FileNameToDelete);
                                                    _counter_left--;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ModeReload();
                                                    MessageBox massageBox = new MessageBox();
                                                    massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                }
                                                break;
                                            case 1://No
                                                   /*No*/
                                                break;
                                            case 2://Cancel
                                                   /*Cancel*/
                                                break;
                                            default:
                                                /*Default*/
                                                break;
                                        }
                                    }
                                }
                            }

                        }
                        else if (ActiveTab == eTab.Right && !drive_right)
                        {
                            if (_counter_right == 0)
                            {
                                //nothing
                            }
                            else if (ManagerMode == eMode.First && _counter_right == col_hight_mode1 * 2 - 1)
                            {
                                //nothing
                            }
                            else if (ManagerMode == eMode.Second && _counter_right == col_hight_mode2 - 1)
                            {
                                //nothing
                            }
                            else if (_counter_right == 0 && page_right != 0)
                            {
                                //nothing
                            }
                            else
                            {
                                if (ManagerMode == eMode.First)
                                {
                                    if ((page_right * (col_hight_mode1 * 2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                    {
                                        //На папці натиснуто видалити (F8)
                                        string Full_FolderNameToDelete = dir_right[(page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)];

                                        DeleteFolder deleteFolder = new DeleteFolder();
                                        string Short_FolderNameToDelete = Path.GetFileName(Full_FolderNameToDelete);
                                        int action = deleteFolder.ShowWindow(Short_FolderNameToDelete);

                                        switch (action)
                                        {
                                            case 0://Yes
                                                try
                                                {
                                                    Directory.Delete(Full_FolderNameToDelete, true);
                                                    _counter_right--;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ModeReload();
                                                    MessageBox massageBox = new MessageBox();
                                                    massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                }
                                                break;
                                            case 1://No
                                                   /*No*/
                                                break;
                                            case 2://Cancel
                                                   /*Cancel*/
                                                break;
                                            default:
                                                /*Default*/
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        //На файлі натиснуто видалити (F8)
                                        string Full_FileNameToDelete = files_right[((page_right * (col_hight_mode1 * 2 - 1) - page_right) + (_counter_right - 1)) - dir_right.Length];

                                        DeleteFolder deleteFolder = new DeleteFolder();
                                        string Short_FileNameToDelete = Path.GetFileName(Full_FileNameToDelete);
                                        int action = deleteFolder.ShowWindow(Short_FileNameToDelete);

                                        switch (action)
                                        {
                                            case 0://Yes
                                                try
                                                {
                                                    File.Delete(Full_FileNameToDelete);
                                                    _counter_right--;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ModeReload();
                                                    MessageBox massageBox = new MessageBox();
                                                    massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                }
                                                break;
                                            case 1://No
                                                   /*No*/
                                                break;
                                            case 2://Cancel
                                                   /*Cancel*/
                                                break;
                                            default:
                                                /*Default*/
                                                break;
                                        }
                                    }
                                }
                                if (ManagerMode == eMode.Second)
                                {
                                    if ((page_right * (col_hight_mode2 - 1) - page_right) + _counter_right <= dir_right.Length)
                                    {
                                        //На папці натиснуто видалити (F8)
                                        string Full_FolderNameToDelete = dir_right[(page_right * (col_hight_mode2 - 1) - page_right) + (_counter_right - 1)];

                                        DeleteFolder deleteFolder = new DeleteFolder();
                                        string Short_FolderNameToDelete = Path.GetFileName(Full_FolderNameToDelete);
                                        int action = deleteFolder.ShowWindow(Short_FolderNameToDelete);

                                        switch (action)
                                        {
                                            case 0://Yes
                                                try
                                                {
                                                    Directory.Delete(Full_FolderNameToDelete, true);
                                                    _counter_right--;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ModeReload();
                                                    MessageBox massageBox = new MessageBox();
                                                    massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                }
                                                break;
                                            case 1://No
                                                   /*No*/
                                                break;
                                            case 2://Cancel
                                                   /*Cancel*/
                                                break;
                                            default:
                                                /*Default*/
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        //На файлі натиснуто видалити (F8)
                                        string Full_FileNameToDelete = files_right[((page_right * (col_hight_mode2 - 1) - page_right) + (_counter_right - 1)) - dir_right.Length];

                                        DeleteFolder deleteFolder = new DeleteFolder();
                                        string Short_FileNameToDelete = Path.GetFileName(Full_FileNameToDelete);
                                        int action = deleteFolder.ShowWindow(Short_FileNameToDelete);

                                        switch (action)
                                        {
                                            case 0://Yes
                                                try
                                                {
                                                    File.Delete(Full_FileNameToDelete);
                                                    _counter_right--;
                                                }
                                                catch (Exception ex)
                                                {
                                                    ModeReload();
                                                    MessageBox massageBox = new MessageBox();
                                                    massageBox.ShowMessage_Window("Error: \n" + ex.Message);
                                                }
                                                break;
                                            case 1://No
                                                   /*No*/
                                                break;
                                            case 2://Cancel
                                                   /*Cancel*/
                                                break;
                                            default:
                                                /*Default*/
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox massageBox = new MessageBox();
                            massageBox.ShowMessage_Window("I can not delete the drive!");
                        }
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F9://Sort
                        if (ActiveTab == eTab.Left && !drive_left)
                        {
                            Sort sort = new Sort();
                            eSortType_left = (eSorting)sort.ShowWindow((int)ActiveTab);

                        }
                        else if (ActiveTab == eTab.Right && !drive_right)
                        {
                            Sort sort = new Sort();
                            eSortType_right = (eSorting)sort.ShowWindow((int)ActiveTab);
                        }
                        else
                        {
                            MessageBox massageBox = new MessageBox();
                            massageBox.ShowMessage_Window("This operation is not valid!");
                        }
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F10: //Quit
                        Quit quit = new Quit();
                        quit.WindowSize_Width += () => (Panel_Design.CONSOLE_WIDTH / 2) - 27;
                        quit.WindowSize_Height += () => (Panel_Design.CONSOLE_HEIGHT / 2) - 5;
                        if (quit.ShowWindow() == 0) work = false;
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    case ConsoleKey.F11: //Manager Mode
                        Mode mode = new Mode();
                        eMode temp = ManagerMode;
                        ManagerMode = (eMode)mode.ShowWindow();
                        if (temp != ManagerMode)
                        {
                            page_left = 0;
                            page_right = 0;
                            _counter_left = 0;
                            _counter_right = 0;
                        }
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                    default:
                        break;
                    /*---------------------------------------------------------------------------------------------------*/
                }
            }
        }
    }
}

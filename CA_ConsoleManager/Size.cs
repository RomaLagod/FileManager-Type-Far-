using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    public class Size
    {
        //Одиниці виміру інформації
        static public readonly int KB = (1024);
        static public readonly int MB = (1024 * 1024);
        static public readonly int GB = (1024 * 1024 * 1024);
        //Folder size
        static public string sizeOfFolder(string folder, ref double catalogSize)
        {
            try
            {
                //В переменную catalogSize будем записывать размеры всех файлов, с каждым
                //новым файлом перезаписывая данную переменную
                DirectoryInfo di = new DirectoryInfo(folder);
                DirectoryInfo[] diA = di.GetDirectories();
                FileInfo[] fi = di.GetFiles();
                //В цикле пробегаемся по всем файлам директории di и складываем их размеры
                foreach (FileInfo f in fi)
                {
                    //Записываем размер файла в байтах
                    catalogSize = catalogSize + f.Length;
                }
                //В цикле пробегаемся по всем вложенным директориям директории di 
                foreach (DirectoryInfo df in diA)
                {
                    //рекурсивно вызываем наш метод
                    sizeOfFolder(df.FullName, ref catalogSize);
                }
                //1ГБ = 1024 Байта * 1024 КБайта * 1024 МБайта
                //return Math.Round((double)(catalogSize / 1024 / 1024 / 1024), 1);

                //мучимо файли
                double size_temp_double = 0.0;
                string temp_file_size = "";

                //байти                               
                if (catalogSize < Size.KB)
                {
                    temp_file_size = String.Format("[{0:N2} B] ", catalogSize);
                }
                //кілобайти
                else if (catalogSize >= Size.KB && catalogSize < Size.MB)
                {
                    size_temp_double = (double)catalogSize / Size.KB;
                    temp_file_size = String.Format("[{0:N2} Kb] ", size_temp_double);
                }
                //мегабайти
                else if (catalogSize >= Size.MB && catalogSize < Size.GB)
                {
                    size_temp_double = (double)catalogSize / Size.MB;
                    temp_file_size = String.Format("[{0:N2} Mb] ", size_temp_double);
                }
                //гігабайти
                else if (catalogSize >= Size.GB)
                {
                    size_temp_double = (double)catalogSize / Size.GB;
                    temp_file_size = String.Format("[{0:N2} Gb] ", size_temp_double);
                }
                return temp_file_size;
            }
            //Начинаем перехватывать ошибки
            //DirectoryNotFoundException - директория не найдена
            catch (DirectoryNotFoundException ex)
            {
                MessageBox massageBox = new MessageBox();
                massageBox.ShowMessage_Window("Folder not found. Error: \n"+ ex.Message);
                //Console.WriteLine("Директория не найдена. Ошибка: " + ex.Message);
                //return 0;
                return "Folder ";
            }
            //UnauthorizedAccessException - отсутствует доступ к файлу или папке
            catch (UnauthorizedAccessException ex)
            {
                MessageBox massageBox = new MessageBox();
                massageBox.ShowMessage_Window("Access denied. Error: \n" + ex.Message);
                //Console.WriteLine("Отсутствует доступ. Ошибка: " + ex.Message);
                //return 0;
                return "Folder ";
            }
            //Во всех остальных случаях
            catch (Exception ex)
            {
                MessageBox massageBox = new MessageBox();
                massageBox.ShowMessage_Window("An error has occurred. Error: \n" + ex.Message);
                //Console.WriteLine("Произошла ошибка. Обратитесь к администратору. Ошибка: " + ex.Message);
                //return 0;
                return "Folder ";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    static public class Color
    {
        //Колір консолі
        static ConsoleColor BackDefaultColor = Console.BackgroundColor;
        static ConsoleColor FrontDefaultColor = Console.ForegroundColor;

        //(Встановити)Колір тексту
        static public void SetTextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        //(встановити)Колір фону
        static public void SetBackColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }

        //(за замовчуванням) колір тексту
        static public void SetDefaultTextColor()
        {
            Console.ForegroundColor = FrontDefaultColor;
        }

        //(за замовчуванням) колір фону
        static public void SetDefaultBackColor()
        {
            Console.BackgroundColor = BackDefaultColor;
        }

        //(за замовчуванням) колір фону та тексту 
        static public void SetDefault()
        {
            Console.BackgroundColor = BackDefaultColor;
            Console.ForegroundColor = FrontDefaultColor;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    static public class Chars
    {
        //для рамки
        static public char[] symbol =
        {
                Encoding.GetEncoding(437).GetChars(new byte[] { 201 })[0], //[[v        0
                Encoding.GetEncoding(437).GetChars(new byte[] { 205 })[0], //--         1
                Encoding.GetEncoding(437).GetChars(new byte[] { 187 })[0], //]]v        2
                Encoding.GetEncoding(437).GetChars(new byte[] { 186 })[0], //||         3 
                Encoding.GetEncoding(437).GetChars(new byte[] { 200 })[0], //[[n        4
                Encoding.GetEncoding(437).GetChars(new byte[] { 188 })[0], //]]n        5
                Encoding.GetEncoding(437).GetChars(new byte[] { 196 })[0], //-          6
                Encoding.GetEncoding(437).GetChars(new byte[] { 179 })[0], //|          7
                Encoding.GetEncoding(437).GetChars(new byte[] { 209 })[0], //--| vnuz   8
                Encoding.GetEncoding(437).GetChars(new byte[] { 199 })[0], //||-        9
                Encoding.GetEncoding(437).GetChars(new byte[] { 207 })[0], //--| vverh  10
                Encoding.GetEncoding(437).GetChars(new byte[] { 182 })[0], //-||        11
                Encoding.GetEncoding(437).GetChars(new byte[] { 193 })[0], //-|-        12 
                Encoding.GetEncoding(437).GetChars(new byte[] { 024 })[0], //^          13
                Encoding.GetEncoding(437).GetChars(new byte[] { 025 })[0], //|          14
                Encoding.GetEncoding(437).GetChars(new byte[] { 026 })[0], //>          15
                Encoding.GetEncoding(437).GetChars(new byte[] { 027 })[0], //<          16
        };
    }
}

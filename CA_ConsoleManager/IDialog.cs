using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ConsoleManager
{
    public interface IDialog
    {
        /*Форує вікно для відображення елементів контенту*/
        void EmptyDialogShow(int start_x, int start_y);

        //Зразки заготовок
        //int ShowWindow(object obj);
        //TypeOfData ShowWindow<TypeOfData>(params object [] list);

        /*Відображає вікно із всіма елементами контенту*/
        int ShowWindow(params object[] list);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProblemGenerator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа программы.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandlingMethod);

            Form1 mainForm = new Form1();
            Application.Run(mainForm);
        }

        // Обработчик события, вызываемого при возникновении исключений
        private static void ExceptionHandlingMethod(object sender, ThreadExceptionEventArgs t)
        {
            MessageBox.Show("Произошла ошибка при работе Windows-формы.\n" +
                "Приложение принудительно завершит работу.");
            Environment.Exit(0);
        }
    }
}

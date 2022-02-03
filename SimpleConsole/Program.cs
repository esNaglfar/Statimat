using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransDataLib;
using System.Threading;


namespace SimpleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int code = -1;
            while (code != 0)
            {
                Console.WriteLine("Введите команду:\n1 - Инициализация БД (Создание схемы и таблиц).\n2 - Вывод информации по партии из базы MySQL.\n3 - Трансфер данных MS Access -> MySQL.\n4 - Выход.");
                code = Convert.ToInt32(Console.ReadLine());
                switch (code)
                {
                    case 1:
                        Console.WriteLine("Проверяем подключение к БД");
                        if (MySQLPutter.TestConnection())
                        {
                            Console.WriteLine("Соединение валидно");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось установить соединение. Проверьте настройки");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Введите номер партии для поиска информации (число)");
                        int lot = Convert.ToInt32(Console.ReadLine());
                        DataBase db;
                        db = Charon.GetQueryByLOTN(lot, lot);
                        //ExcellSaver.ESaver.Save(db);
                        db.PrintConsole();
                        break;
                    case 3:
                        Console.WriteLine(Setuper.Instance.LastRowsCount);
                        Message(Charon.Transfer(Setuper.Instance.LastRowsCount));
                        break;
                    case 4:
                        Console.WriteLine("In development......");
                        //Environment.Exit(0);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }
                Console.ReadKey();
            }
            /*
            Thread CharonsThread = new Thread(new ThreadStart(StartTheThread));
            //CharonsThread.Start();
            DataBase db;
            db = Charon.GetQueryByLOTN(1,99);

            ExcellSaver.ESaver.Save(db);
            db.PrintConsole();
            */
        }

        private static void StartTheThread()
        {
            while (Thread.CurrentThread.ThreadState != ThreadState.Aborted)
            {
                try
                {
                    Message(Charon.Transfer(Setuper.Instance.LastRowsCount));
                    Thread.Sleep(Setuper.Instance.RefreshRate);
                }
                catch
                {

                }
            }
        }

        static void Message(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}

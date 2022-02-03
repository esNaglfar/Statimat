using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TransDataLib
{

    struct MySQLSettings
    {
        public string server;
        public string port;
        public string database;
        public string login;
        public string password;
        public int lastRows;
        public int milsecDelay;
    }

    public class Setuper
    {
        public static Setuper Instance { get { return GetInstance(); } }
        private static Setuper _instance;

        public  string MSAccessDataBaseLocation { get { return MSAccess; } }
        public int RefreshRate { get { return settings.milsecDelay; } }
        public string ExcelFilePath { get { return ExcelPath; } }
        public int LastRowsCount { get { return settings.lastRows; } }
        public  string MySQLDataBaseLocation { get { return $"Server={settings.server};Port={settings.port};Database={settings.database};Uid={settings.login};Pwd={settings.password};"; } }

        private string MSAccess;
        private string ExcelPath; 

        private MySQLSettings settings;

        private Setuper()
        {
            settings = new MySQLSettings();
            //string setupFilePath = $@"C:\\StatimatSettings\Settings.ini";
            string setupFilePath = $@"C:\Users\Naglfar\source\repos\StealDaResult\SimpleConsole\bin\Debug\Settings.ini";
            using (var reader = new StreamReader(setupFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line[0] != '+') LoadSettings(line);
                }
            }
        }

        private void LoadSettings(string line)
        {
            try
            {
                var splited = line.Split('=');
                switch (splited[0].ToLower().Trim())
                {
                    case "access":
                        {
                            MSAccess = splited[1].Trim();
                            break;
                        }
                    case "server":
                        {
                            settings.server = splited[1].Trim();
                            break;
                        }
                    case "port":
                        {
                            settings.port = splited[1].Trim();
                            break;
                        }
                    case "database":
                        {
                            settings.database = splited[1].Trim();
                            break;
                        }
                    case "login":
                        {
                            settings.login = splited[1].Trim();
                            break;
                        }
                    case "password":
                        {
                            settings.password = splited[1].Trim();
                            break;
                        }
                    case "draw":
                        {
                            settings.lastRows = Convert.ToInt32(splited[1].Trim());
                            break;
                        }
                    case "timer":
                        {
                            settings.milsecDelay = Convert.ToInt32(splited[1].Trim()) * 1000;
                            break;
                        }
                    case "excel":
                        {
                            ExcelPath = splited[1].Trim();
                            break;
                        }
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Ошибка чтения файла настроек. Отправьте файл Settings.ini на 3xSoulz@gmail.com");
                Environment.Exit(0);
            }
        }

        public void printSettings()
        {
            Console.WriteLine(this.LastRowsCount);
            Console.WriteLine(this.ExcelFilePath);
        }

        private static Setuper GetInstance()
        {
            if (_instance == null) _instance = new Setuper();
            return _instance;
        }
    }
}

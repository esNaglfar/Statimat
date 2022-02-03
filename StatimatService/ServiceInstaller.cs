using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;
using System.IO;

namespace StatimatService
{
    [RunInstaller(true)]
    public partial class StatimatInstaller : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public StatimatInstaller()
        {
            InitializeComponent();
            GenerateSettingsFile();

            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "StatimatService",
                StartType = ServiceStartMode.Automatic
            };

            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem,
            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }

        public void GenerateSettingsFile()
        {
            string fileName = @"\Settings.ini";
            string filePath = @"C:\StatimatSettings";

            var dir = Directory.CreateDirectory(filePath);

            using (var sw = new StreamWriter(filePath+fileName))
            {
                sw.WriteLine(@"+ Имя базы данных Access и путь к ней. (Прим. 'C:\DataBaseFolder\Base.mdb')");
                sw.WriteLine("+ Воизбежание ошибок, новая база не должна содержать таблиц с именами tbGroup");
                sw.WriteLine("+ tbPackages и tbPackageValues.");
                sw.WriteLine("+ настройки БД должны быть default.");
                sw.WriteLine(@"Excel = C:\StatimatSettings\Шаблон.xlsx");
                sw.WriteLine("+ '+' используется для комментирования строки");
                sw.WriteLine("+***** Количество сливаемых испытаний за раз *****");
                sw.WriteLine("Draw = 1");
                sw.WriteLine("+******* Периодичность вытягивания инфы (сек) ********");
                sw.WriteLine("Timer = 2400");
                sw.WriteLine("+************ Настройки Access *****************");
                sw.WriteLine(@"Access= ...\Data.mdb");
                sw.WriteLine("+************ Настройки MySQL БД****************");
                sw.WriteLine("Server = w1c.aramid.ru");
                sw.WriteLine("Port = 3306");
                sw.WriteLine("Database = BASE515");
                sw.WriteLine("Login =  USER515");
                sw.WriteLine("Password = user515user");
                sw.WriteLine("+");
                sw.WriteLine("+ w1c.aramid.ru BASE515 USER515 user515user");
            }

        }

    }
}

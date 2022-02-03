using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIROJOK
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> names = new List<string>
            {
                "Андрей",
                "Никита",
                "Алексей",
                "Михаил",
                "Евгений"
            };

            DateTime dt = new DateTime(2019, 1, 20);
            Random rnd = new Random(Convert.ToInt32((DateTime.Now - dt).TotalMilliseconds));
            while (true)
            {
                Console.WriteLine($"Пиздуй за пирожками : {names[rnd.Next(0,names.Count)]}");
                Console.ReadKey();
            }
        }
    }
}

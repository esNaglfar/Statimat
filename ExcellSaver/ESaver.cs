using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransDataLib;
using Microsoft.Office.Interop.Excel;

namespace ExcellSaver
{

    public delegate void RecordSetDelegate();

    public static class ESaver
    {
        public static event RecordSetDelegate RecordSet;
        public static List<ExcellList> ListArray;
        static int col = 1;
        static int row = 3;
        static int curMachine = -1;
        static int curLot = -1;


        public static void Save(DataBase db)
        {
            int total = db.Groups.Count;
            ListArray = new List<ExcellList>();
            var misValue = System.Reflection.Missing.Value;

            Application app;
            Workbook wbook;
            Worksheet wsheet;
            
            Dictionary<int, int> LastColumn = new Dictionary<int, int>();
            
            app = new Application();
            
            wbook = app.Workbooks.Open(Setuper.Instance.ExcelFilePath);
            wsheet = null;
            Console.WriteLine("GROUPS : "+db.Groups.Count);
            try
            {
                foreach (var group in db.Groups)
                {
                    total--;
                    if (group.pgH3 != curMachine)
                    {
                        curMachine = group.pgH3;
                        Console.WriteLine($"ПМ{group.pgH3}");
                        wsheet = wbook.Worksheets[$"ПМ{group.pgH3}"];
                        ResetCols();
                    }
                    if (group.pgH2 != curLot)
                    {
                        col++;
                        row = 3;
                        Console.WriteLine($"refNo:{group.refNo} Старая партия {curLot} Новая партия {group.pgH2}");
                        curLot = group.pgH2;
                        wsheet.Cells[1, col] = group.pgH2;
                    }

                    foreach (tbPackages pack in group.Packages)
                    {
                        foreach (tbPackageValues value in pack.Values)
                        {
                            try
                            {
                                if (value._Key == "TEN_M3")
                                {
                                    wsheet.Cells[row, col] = value.X;
                                    row++;
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.ToString());
                            }
                        }
                    }
                    Console.WriteLine($"Осталось {total}");
                   // try { RecordSet(); } catch { }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            app.Visible = true;
            //app.Quit();
        }

        private static void AddToList(int No)
        {

        }

        private static void ResetCols()
        {
            curLot = -1;
            col = 1;
            row = 3;
            Console.WriteLine("Coordinates reseted");
        }

        public static List<string> PatternRows = new List<string>
        {
            "Партия",
            "Копса",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36"
        };
    }
}

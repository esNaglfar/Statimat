using System;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransDataLib
{
    public delegate void LogDelegate(string msg);

    public class MSAccessGrubber
    {
        public event LogDelegate CommandRowsAffected;

        DataBase db;
        OleDbConnection connection;
        OleDbCommand command;
        OleDbDataAdapter adapter;
        DataTable dataTable;

        #region Singletone

                private static MSAccessGrubber _Instance;


                private MSAccessGrubber(string dbPath)
                {
                    db = new DataBase();
                    connection = new OleDbConnection();
                    connection.ConnectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {dbPath }";
                    command = new OleDbCommand();
                    command.Connection = connection;
                }

                public static MSAccessGrubber GetInstance(string ConnectionString)
                {
                    if (_Instance == null)
                    {
                        _Instance = new MSAccessGrubber(ConnectionString);
                    }
                    return _Instance;
                }
                
                public static MSAccessGrubber GetInstance()
                {
                    if (_Instance == null)
                    {
                        throw new Exception()
                        {
                            Source = "No Instance of MSAccesGrubber there. We need to instantiate this with ConnectionString"
                        };
                    }
                    return _Instance;
                }
        #endregion
        /// <summary>
        /// Выгружает последние N строк из таблицы Access БД. Удалить это бесполезное говно можно(нужно)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataBase GetAllRows(string tableName, int limit)
        {
            List<string> DataSet = new List<string>();
            try
            {
                connection.Open();

                //
                //select top 10 * from t1
                //join t2 on t2.k1 = t1.k1
                //join t3 on t3.k1 = t1.k1
                //order by<> desc
                //
                // command.CommandText = $"SELECT * FROM tbpackageValues WHERE (refNo = '640' AND packNo = 16)";
                // command.CommandText = $"SELECT TOP {limit} * FROM (SELECT * FROM {tableName} ORDER BY refNo DESC);";
                // command.CommandText = $"SELECT TOP {limit} * FROM (SELECT * FROM tbpackageValues INNER JOIN tbGroup ON tbpackageValues.refNo = tbGroup.refNo WHERE tbGroup.refNo = '652' ORDER BY tbpackageValues.refNo DESC)";

                string ctext = $"SELECT TOP {limit} * FROM (tbGroup RIGHT JOIN tbPackages ON tbPackages.refNo = tbGroup.refNo) RIGHT JOIN tbpackageValues ON tbpackageValues.refNo = tbGroup.refNo ORDER BY tbGroup.refNo DESC";

                command.CommandText = ctext;
                adapter = new OleDbDataAdapter(command.CommandText, connection.ConnectionString);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
                {
                    for (int colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
                    {
                        Console.Write($"{dataTable.Rows[rowIndex][colIndex].ToString()} ");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }

                CommandRowsAffected($"COMMAND : {command.CommandText}");

                try
                {
                    CommandRowsAffected($"Затронуто {dataTable.Rows.Count} строк");
                }
                catch
                { }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        public List<tbGroup> GetTBGROUP(int lim)
        {
            string ctext = $"SELECT TOP {lim} * FROM tbGroup ORDER BY grpIndex DESC";

            List<tbGroup> tblist = new List<tbGroup>();
            DataTable dt = ExecuteRequest(ctext);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tbGroup row = new tbGroup();
                row.Packages = new List<tbPackages>();
                Console.WriteLine($"Getting {i}-th Group");
                try { row.refNo = Convert.ToInt32(dt.Rows[i]["refNo"].ToString()); } catch { }
                try { row.grpIndex = Convert.ToInt32(dt.Rows[i]["grpIndex"].ToString()); } catch { }
                try { row.Date = Convert.ToDateTime(dt.Rows[i]["pgDate"].ToString()); } catch { }
                try { row.MethodName = dt.Rows[i]["pgPar"].ToString(); } catch { }
                try { row.MethodGroup = dt.Rows[i]["pgGroup"].ToString(); } catch { }
                try { row.PacksAmount = Convert.ToInt32(dt.Rows[i]["pgNpack"].ToString()); } catch { }
                try { row.IterationsScheduled = Convert.ToInt32(dt.Rows[i]["pgNTest"].ToString()); } catch { }
                try { row.pgH1 = dt.Rows[i]["pgH1"].ToString(); } catch { }
                try { row.pgH2 = Convert.ToInt32(dt.Rows[i]["pgH2"].ToString()); } catch { row.pgH2 = 0; }
                try { row.pgH3 = Convert.ToInt32(dt.Rows[i]["pgH3"].ToString()); } catch { row.pgH2 = 0; }
                try { row.pgH4 = dt.Rows[i]["pgH4"].ToString(); } catch { }
                try { row.pgH5 = dt.Rows[i]["pgH5"].ToString(); } catch { }
                try { row.pgH6 = dt.Rows[i]["pgH6"].ToString(); } catch { }
                try { row.pgH7 = dt.Rows[i]["pgH7"].ToString(); } catch { }
                try { row.pgH8 = dt.Rows[i]["pgH8"].ToString(); } catch { }
                try { row.pgH9 = dt.Rows[i]["pgH9"].ToString(); } catch { }
                try
                {
                    row.Packages = GetTBPACKAGES(row.grpIndex);
                    var packValues = GetTBVALUES(row.grpIndex);
                    foreach (var pack in row.Packages)
                    {
                        pack.Values = new List<tbPackageValues>();
                        foreach (var value in packValues)
                        {
                            
                            if (pack.PackNo == value.PackNo)
                            {
                                pack.Values.Add(value);
                            }
                        }
                    }
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                tblist.Add(row);
            }
            return tblist;
        }

        public List<tbPackages> GetTBPACKAGES(int groupID)
        {
            string ctext = $"SELECT * FROM tbPackages WHERE grpIndex = {groupID} ORDER BY packNo DESC";
            DataTable dt = ExecuteRequest(ctext);
            List<tbPackages> tblist = new List<tbPackages>();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tbPackages row = new tbPackages();

                try { row.refNo = Convert.ToInt32(dt.Rows[i]["refNo"].ToString()); } catch { }
                try { row.PackNo = Convert.ToInt32(dt.Rows[i]["packNo"].ToString()); } catch { }
                try { row.PackLD = Convert.ToDecimal(dt.Rows[i]["packLD"].ToString()); } catch { }
                try { row.grpIndex = Convert.ToInt32(dt.Rows[i]["grpIndex"].ToString()); } catch { }

                tblist.Add(row);
            }

            
            return tblist;
        }

        public List<tbPackageValues> GetTBVALUES(int groupID)
        {
            string ctext = $"SELECT * FROM tbpackageValues WHERE grpIndex = {groupID} ORDER BY refNo DESC";
            DataTable dt = ExecuteRequest(ctext);

            List<tbPackageValues> tblist = new List<tbPackageValues>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tbPackageValues row = new tbPackageValues();

                try { row.refNo = Convert.ToInt32(dt.Rows[i]["refNo"].ToString()); } catch { }
                try { row.PackNo = Convert.ToInt32(dt.Rows[i]["packNo"].ToString()); } catch { }
                try { row._Key = dt.Rows[i]["Key"].ToString(); } catch { }
                try { row.MethodNumber = Convert.ToInt32(dt.Rows[i]["methNo"].ToString()); } catch { }
                try { row.Method = dt.Rows[i]["method"].ToString(); } catch { }
                try { row.grpIndex = Convert.ToInt32(dt.Rows[i]["grpIndex"].ToString()); } catch { }
                try { row.X = Convert.ToDecimal(dt.Rows[i]["X"].ToString()); } catch { }
                try { row.Min = Convert.ToDecimal(dt.Rows[i]["Min"].ToString()); } catch { }
                try { row.Max = Convert.ToDecimal(dt.Rows[i]["Max"].ToString()); } catch { }
                try { row.S = Convert.ToDecimal(dt.Rows[i]["S"].ToString()); } catch { }
                try { row.V = Convert.ToDecimal(dt.Rows[i]["V"].ToString()); } catch { }
                try { row.Q = Convert.ToDecimal(dt.Rows[i]["Q"].ToString()); } catch { }

                //Console.WriteLine(row.ToString());
                tblist.Add(row);
            }


            return tblist;
        }

        public DataBase GetDataBase(int limit)
        {
            db.Groups = GetTBGROUP(limit);
            return db;
        }

        private DataTable ExecuteRequest(string cmdtext)
        {
            try
            {
                connection.Open();
                
                command.CommandText = cmdtext;
                adapter = new OleDbDataAdapter(command.CommandText, connection.ConnectionString);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                connection.Close();
                try { CommandRowsAffected($"COMMAND : {command.CommandText}"); } catch { }

                try
                {
                    CommandRowsAffected($"Затронуто {dataTable.Rows.Count} строк");
                }
                catch
                { }
            }
            catch (Exception e)
            {
                Console.WriteLine(cmdtext);
                Console.WriteLine(e.ToString()); }
            return dataTable;
        }

    }
}

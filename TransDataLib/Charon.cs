using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransDataLib
{
    public static class Charon
    {
        public static string Transfer(int limitS)
        {

            try
            {
                var start = DateTime.Now;
                MSAccessGrubber instance = MSAccessGrubber.GetInstance(Setuper.Instance.MSAccessDataBaseLocation);
                DataBase db = instance.GetDataBase(Setuper.Instance.LastRowsCount);
                string finalMessage = $"Выгрузка следующих измерений прошла успешно: {Environment.NewLine}";
                using (var session = MySQLPutter.OpenSession())
                {
                    foreach (var _db in db.Groups)
                    {
                        using (var transaction = session.BeginTransaction())
                        {
                            var repos = new Repository<tbGroup>(session);
                            try
                            {
                                repos.Save(_db);
                            }
                            catch (Exception e) { Console.WriteLine(e.ToString()); }
                            transaction.Commit();
                            finalMessage += $"Измерение {_db.refNo} {Environment.NewLine}";
                        }
                    }

                }

                var end = DateTime.Now;
                finalMessage += $"______________________{Environment.NewLine}";
                finalMessage += $"TOTAL TIME ELAPSED {(end - start).TotalSeconds}";
                return finalMessage;
            }
            catch (Exception e)
            {
                return "Произошла непредвиденна ошибка." + e.ToString();
            }
        }

        public static tbGroup GetRecord(int refNo)
        {
            tbGroup result;
            using (var session = MySQLPutter.OpenSession())
            {
                var repos = new Repository<tbGroup>(session);
                result = repos.GetByRefNo(refNo);

                return result;
            }
        }

        public static DataBase GetQueryByLOTN(int lo, int hi)
        {

            using (var session = MySQLPutter.OpenSession())
            {
                DataBase result = new DataBase();
                var rep = new Repository<tbGroup>(session);

                var list = rep.GetByLotN(lo, hi);
                foreach (var lst in list)
                {
                    result.Groups.Add(lst);
                }

                return result;
            }
        }
        public static IList<LowFiltered> GetRawSQL(int min, int minlot, int maxlot)
        {
            IList<LowFiltered> result;
            using (var session = MySQLPutter.OpenSession())
            {
                var rep = new Repository<tbGroup>(session);
                result = rep.GetRAWSQL(min, minlot,maxlot);
            }
            return result;
        }
        public static IList<LowFiltered> GetLowerThan(int min)
        {
            IList<LowFiltered> result;
            using (var session = MySQLPutter.OpenSession())
            {
                var rep = new Repository<tbGroup>(session);
                result = rep.GetLowMachines(min);
            }
            return result;
        }
    }
}

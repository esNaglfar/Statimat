using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace TransDataLib
{
    public class MySQLPutter
    {
        public static ISession OpenSession()
        {
            return Fluently.Configure()
                 .Database(MySQLConfiguration.Standard.ConnectionString
                 (Setuper.Instance.MySQLDataBaseLocation))
                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<tbGroup>()
                 .Conventions
                 .Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never()))
                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                 .BuildSessionFactory().OpenSession();
        }



        public static bool TestConnection()
        {
            using (var session = MySQLPutter.OpenSession())
            {
                return session.IsOpen;
            }
        }

        public static bool Compare(DataBase access, DataBase mySQL)
        {
            bool isEqual = true;
            if (access.Groups.Count == mySQL.Groups.Count)
            {
                for (int i = 0; i < access.Groups.Count; i++)
                {
                    if (!access.Groups[i].CompareWith(mySQL.Groups[i])) isEqual = false;
                }
            }
            else
            {
                isEqual = false;
            }
            return isEqual;
        }
    }
}

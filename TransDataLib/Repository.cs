using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;


namespace TransDataLib
{
    interface IRepository<T>

    {
        /// <summary>
        /// Запрос сущности из базы по ID 
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        /// 
        T GetByID(int _id);
        /// <summary>
        /// Запрос всех записей из таблицы
        /// </summary>
        /// <returns></returns>
        /// 
        List<T> GetAll();
        /// <summary>
        /// удаление сущности из таблицы
        /// </summary>
        /// <param name="item"></param>
        /// 
        void Delete(T item);
        /// <summary>
        /// Сохранить в таблицу
        /// </summary>
        /// <param name="item"></param>
        void Save(T item);

    }


    public class Repository<T> : IRepository<T>
        where T : class
    {
        private ISession session;
        
        public Repository(ISession _session)
        {
            session = _session;
        }

        public void Delete(T item)
        {
            session.Delete(item);
        }

        public List<T> GetAll()
        {
            return session
                .Query<T>()
                .ToList();
        }

        public IList<T> GetByLotN(int minLot, int maxLot)
        {
            ICriteria crit = session.CreateCriteria<T>()
                .Add(Restrictions.Between("pgH2", minLot, maxLot))
                .Add(Restrictions.Not(Restrictions.Eq("pgH2",0)))
                .Add(Restrictions.Not(Restrictions.Eq("pgH3", 0)))
                .AddOrder(Order.Asc("pgH3"))
                .AddOrder(Order.Asc("pgH2"))
                ;
            return crit.List<T>();
        }

        public IList<LowFiltered> GetRAWSQL(int minTolerance, int minLot, int maxLot)
        {
            #region SQLQuery
            string command1 = 
           $@"SELECT
                tbPackageValues.X,
                tbGroup.Date,
                tbGroup.pgH2,
                tbGroup.pgH3,
                tbPackages.PackNo
            FROM
                tbPackageValues
            LEFT JOIN
                tbGroup
            ON
                tbPackageValues.refNo = tbGroup.refNo
            LEFT JOIN
                tbPackages
            ON
                tbPackageValues.PackNo = tbPackages.PackNo
            WHERE
                tbPackageValues._Key = {"'TEN_M3'"} AND tbPackageValues.X < {minTolerance}
            GROUP BY tbPackages.PackNo
            HAVING tbGroup.pgH2 BETWEEN {minLot} AND {maxLot};";

            #endregion

                return session.CreateSQLQuery(command1).SetResultTransformer(Transformers.AliasToBean<LowFiltered>()).List<LowFiltered>();
        }

        public IList<LowFiltered> GetLowMachines(int minTolerance)
        {
            

            
            tbGroup grAlias = null;
            tbPackages packAlias = null;
            tbPackageValues valAlias = null;


            var query = session.QueryOver<tbGroup>()
                .JoinAlias(x => x.Packages, () => packAlias)
                .JoinAlias(x => packAlias.Values, () => valAlias)
                .SelectList(list => list
                    .Select(x => x.Date)
                    .Select(x => x.pgH2)
                    .Select(x => x.pgH3)
                    .Select(x => packAlias.PackNo)
                )
                .TransformUsing(Transformers.AliasToBean<LowFiltered>())
                .List<LowFiltered>();

            return query;
        }


        public T GetByRefNo(int refNo)
        {
            ICriteria crit = session.CreateCriteria<T>()
                .Add(Restrictions.Like("refNo", refNo));
            return crit.List<T>()[0];
        }

        public T GetByID(int _id)
        {
            return session.Get<T>(_id);
        }

        public void Save(T item)
        {
            try
            {
                session.Save(item);
            }
            catch (NHibernate.Exceptions.GenericADOException e)
            {
                System.Console.WriteLine($"ERROR : {e.Message}");
            }
        }
    }
}

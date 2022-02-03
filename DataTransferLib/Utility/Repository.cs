using System.Collections.Generic;
using NHibernate;

namespace DataTransferLib.Utility
{
    public interface IRepository<T>
    {
        T LoadEntity(int id);

        List<T> LoadAllEntities();





    }

    class Repository
    {

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper.Historic;
using Entity_POJOS;

namespace DataAccess.Crud.Historic
{
    public class HistoricCrudFactory : CrudFactory
    {
        HistoricMapper mapper;

        public HistoricCrudFactory() : base()
        {
            mapper = new HistoricMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (HistoricType)entity;
            var sqlOperation = mapper.GetCreateStatement(customer);
            dao.ExecuteProcedure(sqlOperation);
        }



        public override T Retrieve<T>(BaseEntity entity)
        {
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstCustomers = new List<T>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCustomers.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstCustomers;
        }

        public override void Update(BaseEntity entity)
        {
            var customer = (HistoricType)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (HistoricType)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }
        
        public List<HistoricType> HistoricOfWord(string originWord)
        {
            var lstCustomers = new List<HistoricType>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetHistoricOfWordStatement(originWord));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCustomers.Add((HistoricType)c);
                }
            }

            return lstCustomers;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper.Word;
using Entity_POJOS;

namespace DataAccess.Crud.Word
{
    public class WordCrudFactory : CrudFactory
    {
        WordMapper mapper;

        public WordCrudFactory() : base()
        {
            mapper = new WordMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (WordType)entity;
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
            var customer = (WordType)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (WordType)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

        public List<WordType> RetrievePopularWord()
        {
            var lstCustomers = new List<WordType>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrivePopularWordStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCustomers.Add((WordType)c);
                }
            }

            return lstCustomers;
        }

        public List<WordType> RetrievePopularWordOfDay(string day)
        {
            var lstCustomers = new List<WordType>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetrivePopularWordForDayOfWeekStatement(day));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCustomers.Add((WordType)c);
                }
            }

            return lstCustomers;
        }

        public List<WordType> RetrieveDictionaryByLanguage(string destLang)
        {
            var lstCustomers = new List<WordType>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetRetriveDictionaryByLanguageStatement(destLang));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCustomers.Add((WordType)c);
                }
            }

            return lstCustomers;
        }
    }
}

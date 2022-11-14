using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using DataAccess.Mapper.Language;
using Entity_POJOS;

namespace DataAccess.Crud.Language
{
    public class LanguageCrudFactory : CrudFactory
    {
        LanguageMapper mapper;

        public LanguageCrudFactory() : base()
        {
            mapper = new LanguageMapper();
            dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var customer = (LanguageType)entity;
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
            var customer = (LanguageType)entity;
            dao.ExecuteProcedure(mapper.GetUpdateStatement(customer));
        }

        public override void Delete(BaseEntity entity)
        {
            var customer = (LanguageType)entity;
            dao.ExecuteProcedure(mapper.GetDeleteStatement(customer));
        }

        public LanguageType RetrievePopularLanguage()
        {
            var objs = new LanguageType();
            var lstResult = dao.ExecuteQueryProcedure(mapper.GetMostPopularLanguageStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                objs = (LanguageType) mapper.BuildObject(dic);
                return objs;
            }

            return objs;
        }

        public List<LanguageType> RetrieveLanguagesOfWord(string originWord)
        {
            var lstCustomers = new List<LanguageType>();

            var lstResult = dao.ExecuteQueryProcedure(mapper.GetLanguagesOfWordStatement(originWord));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCustomers.Add((LanguageType)c);
                }
            }

            return lstCustomers;
        }
    }
}

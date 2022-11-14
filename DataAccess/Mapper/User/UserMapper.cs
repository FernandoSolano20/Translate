using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entity_POJOS;

namespace DataAccess.Mapper.User
{
    public class UserMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_NAME = "NAME";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_USER_PR" };

            var c = (UserType)entity;
            operation.AddNVarcharParam(DB_COL_NAME, c.Name);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_USER_PR" };

            var c = (UserType)entity;
            operation.AddNVarcharParam(DB_COL_NAME, c.Name);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_USER_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_USER_PR" };

            var c = (UserType)entity;
            operation.AddNVarcharParam(DB_COL_NAME, c.Name);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_USER_PR" };

            var c = (UserType)entity;
            operation.AddNVarcharParam(DB_COL_NAME, c.Name);
            return operation;
        }

        public SqlOperation GetUserWithMoreTranslationsStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_USER_WITH_MORE_TRANSLATIONS_PR" };
            return operation;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var customer = BuildObject(row);
                lstResults.Add(customer);
            }

            return lstResults;
        }

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var customer = new UserType
            {
                Name = GetStringValue(row, DB_COL_NAME),
            };

            return customer;
        }
    }
}

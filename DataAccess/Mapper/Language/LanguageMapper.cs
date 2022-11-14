using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud;
using DataAccess.Dao;
using Entity_POJOS;

namespace DataAccess.Mapper.Language
{
    public class LanguageMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_NAME = "NAME";
        private const string DB_COL_ORIGIN_WORD = "ORIGIN_WORD";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_LANGUAGE_PR" };

            var c = (LanguageType)entity;
            operation.AddNVarcharParam(DB_COL_NAME, c.Name);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_LANGUAGE_PR" };

            var c = (LanguageType)entity;
            operation.AddNVarcharParam(DB_COL_NAME, c.Name);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_LANGUAGE_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_LANGUAGE_PR" };

            var c = (LanguageType)entity;
            operation.AddNVarcharParam(DB_COL_NAME, c.Name);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_LANGUAGE_PR" };

            var c = (LanguageType)entity;
            operation.AddNVarcharParam(DB_COL_NAME, c.Name);
            return operation;
        }

        public SqlOperation GetMostPopularLanguageStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_POPULAR_LANGUAGE_PR" };
            return operation;
        }

        public SqlOperation GetLanguagesOfWordStatement(string originWord)
        {
            var operation = new SqlOperation { ProcedureName = "RET_LANGUAGE_OF_WORD_PR" };
            operation.AddNVarcharParam(DB_COL_ORIGIN_WORD, originWord);
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
            var customer = new LanguageType
            {
                Name = GetStringValue(row, DB_COL_NAME),
            };

            return customer;
        }
    }
}

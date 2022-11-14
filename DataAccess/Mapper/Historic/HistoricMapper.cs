using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entity_POJOS;

namespace DataAccess.Mapper.Historic
{
    public class HistoricMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_ID_WORD = "ID_WORD";
        private const string DB_COL_ORIGIN_WORD = "ORIGIN_WORD";
        private const string DB_COL_ORIGIN_LANG = "ORIGIN_LANG";
        private const string DB_COL_DEST_WORD = "DEST_WORD";
        private const string DB_COL_DEST_LANG = "DEST_LANG";
        private const string DB_COL_POPULARITY = "POPULARITY";
        private const string DB_COL_ID_USER = "ID_USER";
        private const string DB_COL_DATE_NOW = "DATE_NOW";
        private const string DB_COL_DAY = "DAY";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_HISTORIC_PR" };

            var c = (HistoricType)entity;
            operation.AddIntParam(DB_COL_ID_WORD, c.Word.Id);
            operation.AddNVarcharParam(DB_COL_ID_USER, c.User.Name);
            operation.AddDateParam(DB_COL_DATE_NOW, c.DateNow);
            operation.AddNVarcharParam(DB_COL_DAY, c.DateNow.DayOfWeek.ToString());

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HISTORIC_PR" };

            var c = (HistoricType)entity;
            operation.AddIntParam(DB_COL_ID_WORD, c.Word.Id);
            operation.AddNVarcharParam(DB_COL_ID_USER, c.User.Name);
            operation.AddDateParam(DB_COL_DATE_NOW, c.DateNow);
            operation.AddNVarcharParam(DB_COL_DAY, c.DateNow.DayOfWeek.ToString());

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_HISTORIC_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_HISTORIC_PR" };

            var c = (HistoricType)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam(DB_COL_ID_WORD, c.Word.Id);
            operation.AddNVarcharParam(DB_COL_ID_USER, c.User.Name);
            operation.AddDateParam(DB_COL_DATE_NOW, c.DateNow);
            operation.AddNVarcharParam(DB_COL_DAY, c.DateNow.DayOfWeek.ToString());

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_HISTORIC_PR" };

            var c = (HistoricType)entity;
            operation.AddIntParam(DB_COL_ID_WORD, c.Word.Id);
            operation.AddNVarcharParam(DB_COL_ID_USER, c.User.Name);
            operation.AddDateParam(DB_COL_DATE_NOW, c.DateNow);
            operation.AddNVarcharParam(DB_COL_DAY, c.DateNow.DayOfWeek.ToString());

            return operation;
        }

        public SqlOperation GetHistoricOfWordStatement(string originWord)
        {
            var operation = new SqlOperation { ProcedureName = "RET_HISTORIC_OF_WORD_PR" };
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
            var customer = new HistoricType
            {
                Id = GetIntValue(row,DB_COL_ID),
                Word = new WordType
                {
                    Id = GetIntValue(row,DB_COL_ID_WORD),
                    OriginWord = GetStringValue(row,DB_COL_ORIGIN_WORD),
                    OriginLanguage = new LanguageType { Name = GetStringValue(row,DB_COL_ORIGIN_LANG)},
                    DestinationWord = GetStringValue(row,DB_COL_DEST_WORD),
                    DestinationLanguage = new LanguageType { Name = GetStringValue(row,DB_COL_DEST_LANG)},
                    Popularity = GetIntValue(row,DB_COL_POPULARITY)
                },
                User = new UserType{ Name = GetStringValue(row,DB_COL_ID_USER)},
                DateNow = GetDateValue(row,DB_COL_DATE_NOW),
                Day = GetStringValue(row,DB_COL_DAY)
            };

            return customer;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entity_POJOS;

namespace DataAccess.Mapper.Word
{
    public class WordMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "ID";
        private const string DB_COL_ORIGIN_WORD = "ORIGIN_WORD";
        private const string DB_COL_ORIGIN_LANG = "ORIGIN_LANG";
        private const string DB_COL_DEST_WORD = "DEST_WORD";
        private const string DB_COL_DEST_LANG = "DEST_LANG";
        private const string DB_COL_POPULARITY = "POPULARITY";

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CRE_WORD_PR" };

            var c = (WordType)entity;
            operation.AddNVarcharParam(DB_COL_ORIGIN_WORD, c.OriginWord);
            operation.AddNVarcharParam(DB_COL_ORIGIN_LANG, c.OriginLanguage.Name);
            operation.AddNVarcharParam(DB_COL_DEST_WORD, c.DestinationWord);
            operation.AddNVarcharParam(DB_COL_DEST_LANG, c.DestinationLanguage.Name);
            operation.AddIntParam(DB_COL_POPULARITY, c.Popularity);

            return operation;
        }


        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RET_WORD_PR" };

            var c = (WordType)entity;
            operation.AddNVarcharParam(DB_COL_ORIGIN_WORD, c.OriginWord);
            operation.AddNVarcharParam(DB_COL_ORIGIN_LANG, c.OriginLanguage.Name);
            operation.AddNVarcharParam(DB_COL_DEST_LANG, c.DestinationLanguage.Name);

            return operation;
        }

        public SqlOperation GetRetriveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_ALL_WORD_PR" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UPD_WORD_PR" };

            var c = (WordType)entity;
            operation.AddIntParam(DB_COL_ID, c.Id);
            operation.AddIntParam( DB_COL_POPULARITY, c.Popularity);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DEL_WORD_PR" };

            var c = (WordType)entity;
            operation.AddNVarcharParam(DB_COL_ORIGIN_WORD, c.OriginWord);
            operation.AddNVarcharParam(DB_COL_ORIGIN_LANG, c.OriginLanguage.Name);
            operation.AddNVarcharParam(DB_COL_DEST_LANG, c.DestinationLanguage.Name);

            return operation;
        }

        public SqlOperation GetRetrivePopularWordStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RET_POPULAR_WORD_PR" };
            return operation;
        }

        public SqlOperation GetRetrivePopularWordForDayOfWeekStatement(string day)
        {
            var operation = new SqlOperation { ProcedureName = "RET_POPULAR_WORD_OF_WEEK_PR" };
            operation.AddNVarcharParam("DAY", day);
            return operation;
        }

        public SqlOperation GetRetriveDictionaryByLanguageStatement(string destLang)
        {
            var operation = new SqlOperation { ProcedureName = "RET_DICTIONARY_PR" };
            operation.AddNVarcharParam(DB_COL_DEST_LANG, destLang);
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
            var customer = new WordType
            {
                Id = GetIntValue(row, DB_COL_ID),
                OriginWord = GetStringValue(row, DB_COL_ORIGIN_WORD),
                OriginLanguage = new LanguageType{Name = GetStringValue(row, DB_COL_ORIGIN_LANG)},
                DestinationWord = GetStringValue(row, DB_COL_DEST_WORD),
                DestinationLanguage = new LanguageType { Name = GetStringValue(row, DB_COL_DEST_LANG) },
                Popularity = GetIntValue(row, DB_COL_POPULARITY)
            };

            return customer;
        }
    }
}

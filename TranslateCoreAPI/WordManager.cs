using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud.Word;
using Entity_POJOS;

namespace TranslateCoreAPI
{
    public class WordManager : BaseManager
    {
        private WordCrudFactory crudFactory;

        public WordManager()
        {
            crudFactory = new WordCrudFactory();
        }

        public void Create(WordType entity)
        {
            var e = crudFactory.Retrieve<WordType>(entity);
            if (e == null)
            {
                crudFactory.Create(entity);
            }
        }

        public List<WordType> RetrieveAll()
        {
            return crudFactory.RetrieveAll<WordType>();
        }

        public WordType RetrieveById(WordType entity)
        {
            return crudFactory.Retrieve<WordType>(entity);
        }

        public void Update(WordType entity)
        {
            crudFactory.Update(entity);
        }

        public void Delete(WordType entity)
        {
            crudFactory.Delete(entity);
        }

        public List<WordType> RetrievePopularWord()
        {
            return crudFactory.RetrievePopularWord();
        }

        public List<WordType> RetrievePopularWordOfDay(string day)
        {
            return crudFactory.RetrievePopularWordOfDay(day);
        }

        public List<WordType> RetrieveDictionaryByLanguage(string destLang)
        {
            return crudFactory.RetrieveDictionaryByLanguage(destLang);
        }
        
    }
}

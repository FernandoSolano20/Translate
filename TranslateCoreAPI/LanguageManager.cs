using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud.Language;
using Entity_POJOS;

namespace TranslateCoreAPI
{
    public class LanguageManager : BaseManager
    {
        private LanguageCrudFactory crudFactory;

        public LanguageManager()
        {
            crudFactory = new LanguageCrudFactory();
        }

        public void Create(LanguageType entity)
        {
            var e = crudFactory.Retrieve<LanguageType>(entity);
            if (e == null)
            {
                crudFactory.Create(entity);
            }
        }

        public List<LanguageType> RetrieveAll()
        {
            return crudFactory.RetrieveAll<LanguageType>();
        }

        public LanguageType RetrieveById(LanguageType entity)
        {
            return crudFactory.Retrieve<LanguageType>(entity);
        }

        public void Update(LanguageType entity)
        {
            crudFactory.Update(entity);
        }

        public void Delete(LanguageType entity)
        {
            crudFactory.Delete(entity);
        }

        public LanguageType RetrievePopularLanguage()
        {
            return crudFactory.RetrievePopularLanguage();
        }

        public List<LanguageType> RetrieveLanguagesOfWord(string originWord)
        {
            return crudFactory.RetrieveLanguagesOfWord(originWord);
        }
    }
}

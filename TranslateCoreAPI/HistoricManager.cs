using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud.Historic;
using Entity_POJOS;

namespace TranslateCoreAPI
{
    public class HistoricManager : BaseManager
    {
        private HistoricCrudFactory crudFactory;

        public HistoricManager()
        {
            crudFactory = new HistoricCrudFactory();
        }

        public void Create(HistoricType entity)
        {
            var e = crudFactory.Retrieve<HistoricType>(entity);
            if (e == null)
            {
                crudFactory.Create(entity);
            }
        }

        public List<HistoricType> RetrieveAll()
        {
            return crudFactory.RetrieveAll<HistoricType>();
        }

        public HistoricType RetrieveById(HistoricType entity)
        {
            return crudFactory.Retrieve<HistoricType>(entity);
        }

        public void Update(HistoricType entity)
        {
            crudFactory.Update(entity);
        }

        public void Delete(HistoricType entity)
        {
            crudFactory.Delete(entity);
        }

        public List<HistoricType> HistoricOfWord(string originWord)
        {
            return crudFactory.HistoricOfWord(originWord);
        }
    }
}

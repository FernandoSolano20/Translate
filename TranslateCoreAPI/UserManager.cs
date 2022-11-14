using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Crud.User;
using Entity_POJOS;

namespace TranslateCoreAPI
{
    public class UserManager : BaseManager
    {
        private UserCrudFactory crudFactory;

        public UserManager()
        {
            crudFactory = new UserCrudFactory();
        }

        public void Create(UserType entity)
        {
            var e = crudFactory.Retrieve<UserType>(entity);
            if (e == null)
            {
                crudFactory.Create(entity);
            }
        }

        public List<UserType> RetrieveAll()
        {
            return crudFactory.RetrieveAll<UserType>();
        }

        public UserType RetrieveById(UserType entity)
        {
            return crudFactory.Retrieve<UserType>(entity);
        }

        public void Update(UserType entity)
        {
            crudFactory.Update(entity);
        }

        public void Delete(UserType entity)
        {
            crudFactory.Delete(entity);
        }
        
        public UserType RetrieveUserWithMoreTranslations()
        {
            return crudFactory.RetrieveUserWithMoreTranslations();
        }
    }
}

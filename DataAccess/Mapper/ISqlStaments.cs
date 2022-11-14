using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dao;
using Entity_POJOS;

namespace DataAccess.Mapper
{
    public interface ISqlStaments
    {
        SqlOperation GetCreateStatement(BaseEntity entity);
        SqlOperation GetRetriveStatement(BaseEntity entity);
        SqlOperation GetRetriveAllStatement();
        SqlOperation GetUpdateStatement(BaseEntity entity);
        SqlOperation GetDeleteStatement(BaseEntity entity);
    }
}

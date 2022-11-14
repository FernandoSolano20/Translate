using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_POJOS
{
    public class HistoricType : BaseEntity
    {
        public int Id { get; set; }
        public WordType Word { get; set; }
        public UserType User { get; set; }
        public DateTime DateNow { get; set; }
        public string Day { get; set; }
    }
}

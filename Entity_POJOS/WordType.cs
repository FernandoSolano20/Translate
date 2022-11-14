using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_POJOS
{
    public class WordType : BaseEntity
    {
        public int Id { get; set; }
        public string OriginWord { get; set; }
        public LanguageType OriginLanguage { get; set; }
        public string DestinationWord { get; set; }
        public LanguageType DestinationLanguage { get; set; }
        public int Popularity { get; set; }
    }
}

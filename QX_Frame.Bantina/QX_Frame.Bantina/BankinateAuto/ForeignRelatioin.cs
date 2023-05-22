using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QX_Frame.Bantina.BankinateAuto
{
    internal class ForeignRelatioin
    {
        public int ForeignRelationId { get; set; }
        public int ParentTableId { get; set; }
        public int RelationTableId { get; set; }
        public int ForeignKeyId { get; set; }
        public int RelationKeyId { get; set; }
    }
}

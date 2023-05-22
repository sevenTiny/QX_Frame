using System.Collections.Generic;

namespace QX_Frame.Bantina.BankinateAuto
{
    /// <summary>
    /// TableInfo class
    /// </summary>
    internal class TableInfo
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
        public List<TableFeildInfo> TableFildsInfoList { get; set; }
        public bool HasForeignKey { get; set; } = false;
        //parent foreignKeyId
        public int ForeignKeyId { get; set; }
        //this table relation parent relationKeyId
        public int RelationKeyId { get; set; }
        //foreignTableList
        public List<TableInfo> ForeignTableInfoList { get; set; }
    }
}

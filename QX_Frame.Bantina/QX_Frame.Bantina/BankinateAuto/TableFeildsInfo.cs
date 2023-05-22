namespace QX_Frame.Bantina.BankinateAuto
{
    /// <summary>
    /// Internal Class TableCOnstruction
    /// </summary>
    internal class TableFeildInfo
    {
        public int TableId { get; set; }
        public int ColumnId { get; set; }
        public string FeildName { get; set; }
        public int Length { get; set; }
        public string Type { get; set; }
        public int Nullable { get; set; }
        public bool IsIdentity { get; set; }
    }
}

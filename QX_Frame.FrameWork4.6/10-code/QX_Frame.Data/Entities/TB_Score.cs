namespace QX_Frame.Data.Entities
{
    using QX_Frame.App.Base;
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class TB_Score : Entity<DB_QX_Frame_Test, TB_Score>
    {
        [Key]
        public Guid Uid { get; set; }

        public double Score1 { get; set; }

        public double Score2 { get; set; }

        public double Score3 { get; set; }
    }
}

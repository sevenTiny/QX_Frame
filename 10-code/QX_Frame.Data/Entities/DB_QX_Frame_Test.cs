namespace QX_Frame.Data.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DB_QX_Frame_Test : DbContext
    {
        public DB_QX_Frame_Test()
            : base(Configs.QX_Frame_Data_Config.ConnectionString_DB_QX_Frame_Test)
        {
        }

        public virtual DbSet<TB_ClassName> TB_ClassName { get; set; }
        public virtual DbSet<TB_People> TB_People { get; set; }
        public virtual DbSet<TB_Score> TB_Score { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TB_ClassName>()
                .HasMany(e => e.TB_People)
                .WithRequired(e => e.TB_ClassName)
                .WillCascadeOnDelete(false);
        }
    }
}

namespace QX_Frame.Data.Entities
{
    using QX_Frame.App.Base;
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class TB_People : Entity<DB_QX_Frame_Test, TB_People>
    {
        [Key]
        public Guid Uid { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public int Age { get; set; }

        public int ClassId { get; set; }

        public virtual TB_ClassName TB_ClassName { get; set; }
    }
}

namespace QX_Frame.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_People
    {
        [Key]
        [Column(Order = 0)]
        public Guid Uid { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string Name { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Age { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string ClassName { get; set; }
    }
}

namespace QX_Frame.Data.Entities
{
    using Newtonsoft.Json;
    using QX_Frame.App.Base;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TB_ClassName:Entity<DB_QX_Frame_Test, TB_ClassName>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_ClassName()
        {
            TB_People = new HashSet<TB_People>();
        }

        [Key]
        public int ClassId { get; set; }

        [Required]
        [StringLength(10)]
        public string ClassName { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_People> TB_People { get; set; }
    }
}

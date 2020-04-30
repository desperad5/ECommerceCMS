using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace  SorubankCMS.Data.Entity
{
    public class BaseEntity
    {
        protected BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual int Id { get; set; }

        [Column]
        public virtual bool IsDeleted { get; set; }
        [Column]
        public virtual bool IsActive { get; set; }

        [Column]
        public virtual DateTime CreatedDate { get; set; }
    }
}

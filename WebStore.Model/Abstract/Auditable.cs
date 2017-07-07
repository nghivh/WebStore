using System;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Model.Abstract
{
    public abstract class Auditable : IAuditable
    {
        public DateTime? CreatedDate { set; get; }

        [MaxLength(250)]
        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        [MaxLength(250)]
        public string UpdatedBy { set; get; }

        [MaxLength(250)]
        public string MetaKeyword { set; get; }

        [MaxLength(250)]
        public string MetaDescription { set; get; }

        public bool Status { set; get; }      
    }
}

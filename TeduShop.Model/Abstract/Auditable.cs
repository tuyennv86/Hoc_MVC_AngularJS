using System;
using System.ComponentModel.DataAnnotations;

namespace TeduShop.Model.Abstract
{
    public abstract class Auditable : IAuditable
    {
        [DisplayFormat(DataFormatString = "{MM/dd/YYYY HH:mm:ss}", ApplyFormatInEditMode = true)]        
        public DateTime? CreatedDate { set; get; }

        [MaxLength(256)]
        public string CreatedBy { set; get; }

        [DisplayFormat(DataFormatString = "{MM/dd/YYYY HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedDate { set; get; }

        [MaxLength(256)]
        public string UpdatedBy { set; get; }

        [MaxLength(256)]
        public string MetaKeyword { set; get; }

        [MaxLength(256)]
        public string MetaDescription { set; get; }

        public bool Status { set; get; }
    }
}
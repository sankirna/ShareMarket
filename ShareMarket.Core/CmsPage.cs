using ShareMarket.Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareMarket.Core
{
    [Table("CmsPage")]
    public class CmsPage : BaseEntity
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public int? ParentId { get; set; }

        public string UrlLink { get; set; }

        public string Seo { get; set; }

        public string RootMenu { get; set; }

        public LinkTargetType LinkTargetType { get; set; }

        [ForeignKey("ParentId")]
        public virtual CmsPage ParentCmsPage { get; set; }

        [NotMapped]
        public List<CmsPage> CmsPages { get; set; }
    }
}

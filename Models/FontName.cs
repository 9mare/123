using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("FontName")]
    public class FontName
    {
        [Key]
        public int FontNameId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<AbstractFileFormat> AbstractFileFormats { get; set; }

    }
}
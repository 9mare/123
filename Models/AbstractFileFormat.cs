using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ConferenceManagementSystem.Models
{
    [Table("AbstractFileFormat")]
    public class AbstractFileFormat
    {
        [Key]
        public int AbstractFileFormatId { get; set; }

        public int AlignmentId { get; set; }

        public int FontNameId { get; set; }

        public float FontSize { get; set; }

        public double Margin_Top { get; set; }

        public double Margin_Bottom { get; set; }

        public double Margin_Left { get; set; }

        public double Margin_Right { get; set; }

        public double LineSpacing { get; set; }

        public int ConferenceId { get; set; }

        public virtual Conference Conference { get; set; }

        public virtual FontName FontName { get; set; }

        public virtual Alignment Alignment { get; set; }

    }
}
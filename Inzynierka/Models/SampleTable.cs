namespace Inzynierka.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SampleTable")]
    public partial class SampleTable
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string StrangeVal { get; set; }
    }
}

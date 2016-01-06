using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inzynierka.Models
{
    [Table( "ActivityPlan" )]
    public class ActivityPlan
    {
        public int Id
        {
            get; set;
        }

        [MinLength(5)]
        [Column("Name")]
        public string ActivityName
        {
            get; 
            set;
        }

        public string UserId
        {
            get; 
            set;
        }
        
        public DateTime? Date 
        { 
            get; 
            set; 
        }
    }
}
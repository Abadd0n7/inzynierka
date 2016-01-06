using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inzynierka.Models
{
    public enum ActivityType
    {
        Publication=1,
        Deadline=2,
        Reaview=3,
        Start=4
    }

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

        public string Description
        {
            get; 
            set;
        }

        public ActivityType Type { get; set; }

        public long WorkId { get; set; }
    }
}
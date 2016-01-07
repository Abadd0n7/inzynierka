using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Inzynierka.Models
{
    [Table( "WorkPlan" )]
    public class WorkPlan
    {
        public int Id
        {
            get; set;
        }

        [MinLength(10)]
        [Column("Name")]
        public string Title
        {
            get; 
            set;
        }

        public string UserId
        {
            get; 
            set;
        }
        
        public string Body
        {
            get; 
            set;
        }

        public long WorkId { get; set; }
    }
}
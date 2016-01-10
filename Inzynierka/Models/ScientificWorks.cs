using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Inzynierka.Models
{
    [Table("ScientificWorks")]
    public class ScientificWorks
    {
        public int Id { get; set; }

        [MinLength(6)]
        [Column("Title")]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        //public string UserId { get; set; }
        public DateTime? Created { get; set; }
    }
}
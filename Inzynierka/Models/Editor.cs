using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Inzynierka.Models
{
    public class Editor
    {

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 10)]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public long Id { get; set; }
        public string UserId { get; set; }
    }
}
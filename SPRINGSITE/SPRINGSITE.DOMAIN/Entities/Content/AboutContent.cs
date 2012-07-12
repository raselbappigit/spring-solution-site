using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SPRINGSITE.DOMAIN
{
    public class AboutContent
    {
        public AboutContent()
        {
            DateCreated = DateTime.Now;
        }
        
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        public string Title { get; set; }

        public string ImageURL { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        
        public DateTime DateCreated { get; set; }
    }
}

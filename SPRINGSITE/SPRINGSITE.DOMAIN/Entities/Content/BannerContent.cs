using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SPRINGSITE.DOMAIN
{
    public class BannerContent
    {
        public BannerContent()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Note is required")]
        [MaxLength(150)]
        public string Note { get; set; }

        [Required(ErrorMessage = "Image url is required")]
        [MaxLength(100)]
        public string ImageURL { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

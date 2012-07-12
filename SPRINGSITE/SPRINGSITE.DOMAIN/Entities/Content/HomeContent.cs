using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SPRINGSITE.DOMAIN
{
    public class HomeContent
    {
        public HomeContent()
        {
            DateCreated = DateTime.Now;
        }
        
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        public string Title { get; set; }

        public string TitleImageURL { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Redirect Link is required")]
        [MaxLength(200)]
        public string RedirectLink { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

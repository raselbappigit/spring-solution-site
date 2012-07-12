using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SPRINGSITE.DOMAIN
{
    public class ContactContent
    {
        public ContactContent()
        {
            DateCreated = DateTime.Now;
        }
        
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [MaxLength(50)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [MaxLength(50)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Map content is required")]
        public string MapContent { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

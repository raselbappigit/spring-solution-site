using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SPRINGSITE.DOMAIN
{
    public class QuickMailInfo
    {
        public QuickMailInfo()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(150)]
        public string Email { get; set; }

        public bool Status { get; set; }

        public DateTime DateCreated { get; set; }
    }
}

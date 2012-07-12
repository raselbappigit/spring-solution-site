﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SPRINGSITE.DOMAIN
{
    public  class ClientInfo
    {
        public ClientInfo()
        {
            DateCreated = DateTime.Now;
        }
        
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image url is required")]
        [MaxLength(150)]
        public string ImageURL { get; set; }

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

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(200)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
